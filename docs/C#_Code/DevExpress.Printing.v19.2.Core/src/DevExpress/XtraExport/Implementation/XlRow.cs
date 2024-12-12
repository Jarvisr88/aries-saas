namespace DevExpress.XtraExport.Implementation
{
    using DevExpress.Export.Xl;
    using DevExpress.Utils;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class XlRow : IXlRow, IDisposable
    {
        private IXlExport exporter;

        public XlRow(IXlExport exporter)
        {
            this.exporter = exporter;
            this.HeightInPixels = -1;
        }

        public void ApplyFormatting(XlCellFormatting formatting)
        {
            if (formatting != null)
            {
                this.Formatting ??= new XlCellFormatting();
                this.Formatting.MergeWith(formatting);
            }
        }

        public IXlTable BeginTable(IEnumerable<string> columnNames, bool hasHeaderRow) => 
            this.BeginTable(columnNames, hasHeaderRow, null);

        public IXlTable BeginTable(IEnumerable<XlTableColumnInfo> columns, bool hasHeaderRow, XlCellFormatting headerRowFormatting)
        {
            Guard.ArgumentNotNull(columns, "columns");
            int num = this.ProcessColumns(columns);
            Guard.ArgumentPositive(num, "Columns have to contain at least one item.");
            return this.BeginTableCore(columns, num, hasHeaderRow, headerRowFormatting);
        }

        public IXlTable BeginTable(IEnumerable<string> columnNames, bool hasHeaderRow, XlCellFormatting headerRowFormatting)
        {
            Guard.ArgumentNotNull(columnNames, "columnNames");
            int num = this.ProcessColumnNames(columnNames);
            Guard.ArgumentPositive(num, "Column names have to contain at least one item.");
            List<XlTableColumnInfo> columns = new List<XlTableColumnInfo>();
            foreach (string str in columnNames)
            {
                columns.Add(new XlTableColumnInfo(str));
            }
            return this.BeginTableCore(columns, num, hasHeaderRow, headerRowFormatting);
        }

        private IXlTable BeginTableCore(IEnumerable<XlTableColumnInfo> columns, int count, bool hasHeaderRow, XlCellFormatting headerRowFormatting)
        {
            IXlTableContainer currentSheet = this.exporter.CurrentSheet as IXlTableContainer;
            if (currentSheet == null)
            {
                return null;
            }
            XlCellRange range = new XlCellRange(new XlCellPosition(this.exporter.CurrentColumnIndex, this.exporter.CurrentRowIndex), new XlCellPosition((this.exporter.CurrentColumnIndex + count) - 1, this.exporter.CurrentRowIndex));
            if (!currentSheet.IsValidRange(range) || currentSheet.HasIntersectionWithTable(range))
            {
                throw new InvalidOperationException("Cannot create a table in the specified position.");
            }
            IXlTableRepository currentDocument = this.exporter.CurrentDocument as IXlTableRepository;
            XlTable table = new XlTable(currentDocument, range, columns, hasHeaderRow) {
                HeaderRowFormatting = headerRowFormatting
            };
            if (currentDocument != null)
            {
                table.Name = currentDocument.GetUniqueTableName();
            }
            currentSheet.AddTable(table);
            if (hasHeaderRow)
            {
                List<string> values = new List<string>();
                foreach (XlTableColumnInfo info in columns)
                {
                    values.Add(info.Name);
                }
                this.BulkCells(values, null);
            }
            return table;
        }

        public void BlankCells(int count, XlCellFormatting formatting)
        {
            Guard.ArgumentPositive(count, "count");
            Guard.ArgumentNotNull(formatting, "formatting");
            for (int i = 0; i < count; i++)
            {
                using (IXlCell cell = this.CreateCell())
                {
                    cell.ApplyFormatting(formatting);
                }
            }
        }

        public void BulkCells(IEnumerable values, XlCellFormatting formatting)
        {
            Guard.ArgumentNotNull(values, "values");
            foreach (object obj2 in values)
            {
                using (IXlCell cell = this.CreateCell())
                {
                    cell.Value = XlVariantValue.FromObject(obj2);
                    cell.ApplyFormatting(formatting);
                }
            }
        }

        public IXlCell CreateCell() => 
            new XlCellProxy(this.exporter, this.exporter.BeginCell());

        public IXlCell CreateCell(int columnIndex)
        {
            int count = columnIndex - this.exporter.CurrentColumnIndex;
            if (count < 0)
            {
                throw new ArgumentException("Value must be greater than or equals to current column index.");
            }
            if (count > 0)
            {
                this.SkipCells(count);
            }
            return new XlCellProxy(this.exporter, this.exporter.BeginCell());
        }

        private void CreateTotalRow(XlTable table)
        {
            int count = table.Columns.Count;
            for (int i = 0; i < count; i++)
            {
                IXlTableColumn column = table.Columns[i];
                using (IXlCell cell = this.CreateCell())
                {
                    if (column.TotalRowFunction != XlTotalRowFunction.None)
                    {
                        cell.SetFormula(XlFunc.Subtotal(table.GetColumnDataRange(i), this.GetSummary(column.TotalRowFunction), true));
                    }
                    else if (!string.IsNullOrEmpty(column.TotalRowLabel))
                    {
                        cell.Value = column.TotalRowLabel;
                    }
                }
            }
        }

        public void Dispose()
        {
            this.exporter = null;
        }

        public void EndTable(IXlTable table, bool hasTotalRow)
        {
            Guard.ArgumentNotNull(table, "table");
            IXlTableContainer currentSheet = this.exporter.CurrentSheet as IXlTableContainer;
            if (currentSheet == null)
            {
                throw new InvalidOperationException("BeginTable/EndTable calls consistency.");
            }
            XlTable table2 = table as XlTable;
            if (!currentSheet.ContainsTable(table2) || table2.IsClosed)
            {
                throw new InvalidOperationException("BeginTable/EndTable calls consistency.");
            }
            if (string.IsNullOrEmpty(table2.Name))
            {
                throw new InvalidOperationException("Table name can't be empty.");
            }
            table2.HasTotalRow = hasTotalRow;
            table2.ValidateNumberOfRows();
            if (!hasTotalRow)
            {
                int count = (table2.Range.BottomRight.Column - this.exporter.CurrentColumnIndex) + 1;
                if (count > 0)
                {
                    this.exporter.SkipCells(count);
                }
                table2.IsClosed = true;
                currentSheet.SetPendingTable(table2);
            }
            else
            {
                table2.IsClosed = true;
                int count = table2.Range.TopLeft.Column - this.exporter.CurrentColumnIndex;
                if (count < 0)
                {
                    throw new InvalidOperationException("Cannot create a total row in the current position..");
                }
                if (count > 0)
                {
                    this.exporter.SkipCells(count);
                }
                this.CreateTotalRow(table2);
            }
            currentSheet.DeactivateTable(table2);
        }

        private XlSummary GetSummary(XlTotalRowFunction totalRowFunction)
        {
            switch (totalRowFunction)
            {
                case XlTotalRowFunction.Average:
                    return XlSummary.Average;

                case XlTotalRowFunction.Count:
                    return XlSummary.CountA;

                case XlTotalRowFunction.CountNums:
                    return XlSummary.Count;

                case XlTotalRowFunction.Max:
                    return XlSummary.Max;

                case XlTotalRowFunction.Min:
                    return XlSummary.Min;

                case XlTotalRowFunction.StdDev:
                    return XlSummary.StdevS;

                case XlTotalRowFunction.Var:
                    return XlSummary.VarS;
            }
            return XlSummary.Sum;
        }

        private int ProcessColumnNames(IEnumerable<string> columnNames)
        {
            HashSet<string> set = new HashSet<string>();
            foreach (string str in columnNames)
            {
                if (set.Contains(str))
                {
                    throw new ArgumentException("Column names must be unique!");
                }
                set.Add(str);
            }
            return set.Count;
        }

        private int ProcessColumns(IEnumerable<XlTableColumnInfo> columns)
        {
            HashSet<string> set = new HashSet<string>();
            foreach (XlTableColumnInfo info in columns)
            {
                if (set.Contains(info.Name))
                {
                    throw new ArgumentException("Column names must be unique!");
                }
                set.Add(info.Name);
            }
            return set.Count;
        }

        public void SkipCells(int count)
        {
            this.exporter.SkipCells(count);
        }

        public int RowIndex { get; set; }

        public XlCellFormatting Formatting { get; set; }

        public bool IsHidden { get; set; }

        public bool IsCollapsed { get; set; }

        public int HeightInPixels { get; set; }

        public float HeightInPoints
        {
            get => 
                (this.HeightInPixels >= 0) ? ((this.HeightInPixels != 0) ? XlGraphicUnitConverter.PixelsToPointsF((float) this.HeightInPixels, XlGraphicUnitConverter.GetDpi(this.exporter)) : 0f) : -1f;
            set
            {
                if (value < 0f)
                {
                    this.HeightInPixels = -1;
                }
                if (value == 0f)
                {
                    this.HeightInPixels = 0;
                }
                this.HeightInPixels = (int) Math.Round((double) XlGraphicUnitConverter.PointsToPixelsF(value, XlGraphicUnitConverter.GetDpi(this.exporter)));
            }
        }
    }
}

