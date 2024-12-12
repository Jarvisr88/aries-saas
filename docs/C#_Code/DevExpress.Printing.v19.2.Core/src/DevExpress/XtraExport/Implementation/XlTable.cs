namespace DevExpress.XtraExport.Implementation
{
    using DevExpress.Export.Xl;
    using DevExpress.Utils;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class XlTable : XlTableFormattingBase, IXlTable, IXlNamedObject, IComparable<XlTable>
    {
        private string name;
        private readonly IXlTableRepository repository;
        private readonly XlCellRange range;
        private readonly XlTableStyleInfo styleInfo;
        private readonly List<XlTableColumn> innerColumns;
        private readonly XlTableColumnCollection columns;

        public XlTable(XlCellRange range, IEnumerable<string> columnNames, bool hasHeaderRow) : this(null, range, columnNames, hasHeaderRow)
        {
        }

        public XlTable(IXlTableRepository repository, XlCellRange range, IEnumerable<XlTableColumnInfo> columns, bool hasHeaderRow)
        {
            this.innerColumns = new List<XlTableColumn>();
            Guard.ArgumentNotNull(range, "range");
            this.repository = repository;
            this.range = range.AsRelative();
            this.range.SheetName = string.Empty;
            this.styleInfo = new XlTableStyleInfo();
            this.HasHeaderRow = hasHeaderRow;
            this.HasAutoFilter = true;
            this.innerColumns = new List<XlTableColumn>();
            foreach (XlTableColumnInfo info in columns)
            {
                XlTableColumn item = new XlTableColumn(this, info.Name, this.innerColumns.Count) {
                    HeaderRowFormatting = info.HeaderRowFormatting,
                    DataFormatting = info.DataFormatting,
                    TotalRowFormatting = info.TotalRowFormatting
                };
                this.innerColumns.Add(item);
            }
            this.columns = new XlTableColumnCollection(this.innerColumns);
        }

        public XlTable(IXlTableRepository repository, XlCellRange range, IEnumerable<string> columnNames, bool hasHeaderRow)
        {
            this.innerColumns = new List<XlTableColumn>();
            Guard.ArgumentNotNull(range, "range");
            this.repository = repository;
            this.range = range.AsRelative();
            this.range.SheetName = string.Empty;
            this.styleInfo = new XlTableStyleInfo();
            this.HasHeaderRow = hasHeaderRow;
            this.HasAutoFilter = true;
            this.innerColumns = new List<XlTableColumn>();
            foreach (string str in columnNames)
            {
                this.innerColumns.Add(new XlTableColumn(this, str, this.innerColumns.Count));
            }
            this.columns = new XlTableColumnCollection(this.innerColumns);
        }

        public int CompareTo(XlTable other) => 
            other.Range.TopLeft.Column - this.Range.TopLeft.Column;

        internal bool Contains(int column, int row) => 
            this.range.Contains(column, row);

        internal void ExtendRange(int rowIndex)
        {
            if (!this.IsClosed)
            {
                this.range.BottomRight = new XlCellPosition(this.range.BottomRight.Column, Math.Max(rowIndex, this.range.BottomRight.Row));
            }
        }

        internal XlExpression GetCalculatedColumnExpression(IXlExport exporter, int columnIndex, int rowIndex)
        {
            if (this.IsClosed || (this.HasHeaderRow && (rowIndex == this.Range.TopLeft.Row)))
            {
                return null;
            }
            this.HasData = true;
            XlExpression expression = this.innerColumns[columnIndex - this.range.FirstColumn].GetExpression(exporter);
            if (expression != null)
            {
                int rowOffset = rowIndex - this.Range.TopLeft.Row;
                if (this.HasHeaderRow)
                {
                    rowOffset--;
                }
                expression = expression.Offset(0, rowOffset);
            }
            return expression;
        }

        internal void GetCellsToCreate(IList<int> result, XlCellRange other)
        {
            int num = Math.Max(this.range.FirstColumn, other.FirstColumn);
            int num2 = Math.Min(this.range.LastColumn, other.LastColumn);
            if (num <= num2)
            {
                if (base.DataFormatting != null)
                {
                    for (int i = num; i <= num2; i++)
                    {
                        result.Add(i);
                    }
                }
                else
                {
                    int firstColumn = this.range.FirstColumn;
                    for (int i = num; i <= num2; i++)
                    {
                        XlTableColumn column = this.innerColumns[i - firstColumn];
                        if ((column.DataFormatting != null) || column.HasColumnFormula)
                        {
                            result.Add(i);
                        }
                    }
                }
            }
        }

        internal XlCellRange GetColumnDataRange(int columnIndex)
        {
            int left = this.range.TopLeft.Column + columnIndex;
            int right = left;
            int row = this.Range.TopLeft.Row;
            if (this.HasHeaderRow)
            {
                row++;
            }
            int bottom = this.Range.BottomRight.Row;
            if (this.HasTotalRow)
            {
                bottom--;
            }
            return new XlTableColumnReference(this.Name, this.Columns[columnIndex].Name, XlCellRange.FromLTRB(left, row, right, bottom));
        }

        internal XlDifferentialFormatting GetColumnHeaderRowFormat(XlTableColumn column) => 
            XlDifferentialFormatting.Merge(XlDifferentialFormatting.Merge(null, base.HeaderRowFormatting), column.HeaderRowFormatting);

        internal XlDifferentialFormatting GetColumnTotalRowFormat(XlTableColumn column) => 
            XlDifferentialFormatting.Merge(XlDifferentialFormatting.Merge(null, base.TotalRowFormatting), column.TotalRowFormatting);

        internal XlDifferentialFormatting GetDifferentialFormat(int columnIndex, int rowIndex)
        {
            XlDifferentialFormatting target = null;
            XlTableColumn column = this.innerColumns[columnIndex - this.range.FirstColumn];
            if (this.HasHeaderRow && (rowIndex == this.range.FirstRow))
            {
                target = XlDifferentialFormatting.Merge(XlDifferentialFormatting.Merge(target, base.HeaderRowFormatting), column.HeaderRowFormatting);
            }
            else if (this.HasTotalRow && (rowIndex == this.range.LastRow))
            {
                target = XlDifferentialFormatting.Merge(XlDifferentialFormatting.Merge(target, base.TotalRowFormatting), column.TotalRowFormatting);
            }
            else
            {
                this.HasData = true;
                target = XlDifferentialFormatting.Merge(XlDifferentialFormatting.Merge(target, base.DataFormatting), column.DataFormatting);
            }
            return target;
        }

        internal XlCellRange GetRange(XlTableReference tableReference, XlCellPosition currentCell)
        {
            XlTablePart part = tableReference.Part;
            part ??= (this.IsClosed ? XlTablePart.Data : XlTablePart.ThisRow);
            IXlTable table = tableReference.Table;
            int index = table.Columns.IndexOf(tableReference.FirstColumn);
            int num2 = table.Columns.IndexOf(tableReference.LastColumn);
            int column = table.Range.TopLeft.Column;
            int num4 = table.Range.BottomRight.Column;
            if (index >= 0)
            {
                column += index;
            }
            if (num2 >= 0)
            {
                num4 = table.Range.TopLeft.Column + num2;
            }
            int row = -1;
            int num6 = -1;
            XlPositionType absolute = XlPositionType.Absolute;
            switch (part)
            {
                case XlTablePart.All:
                    row = table.Range.TopLeft.Row;
                    num6 = table.Range.BottomRight.Row;
                    break;

                case XlTablePart.Data:
                    row = table.Range.TopLeft.Row;
                    num6 = table.Range.BottomRight.Row;
                    if (table.HasHeaderRow)
                    {
                        row++;
                    }
                    if (table.HasTotalRow)
                    {
                        num6--;
                    }
                    break;

                case XlTablePart.Headers:
                    row = table.Range.TopLeft.Row;
                    num6 = table.Range.TopLeft.Row;
                    break;

                case XlTablePart.Totals:
                    row = table.Range.BottomRight.Row;
                    num6 = table.Range.BottomRight.Row;
                    break;

                case XlTablePart.ThisRow:
                    if (!this.InsideDataPart(currentCell))
                    {
                        return null;
                    }
                    row = currentCell.Row;
                    num6 = row;
                    absolute = XlPositionType.Relative;
                    break;

                default:
                    break;
            }
            XlCellPosition topLeft = new XlCellPosition(column, row, XlPositionType.Absolute, absolute);
            return new XlCellRange(topLeft, new XlCellPosition(num4, num6, XlPositionType.Absolute, absolute));
        }

        public XlTableReference GetReference(XlTablePart part) => 
            new XlTableReference(this, part);

        public XlTableReference GetReference(XlTablePart part, string columnName) => 
            new XlTableReference(this, columnName, part);

        public XlTableReference GetReference(XlTablePart part, string firstColumnName, string lastColumnName) => 
            new XlTableReference(this, firstColumnName, lastColumnName, part);

        public XlTableReference GetRowReference(string columnName) => 
            new XlTableReference(this, columnName, XlTablePart.ThisRow);

        public XlTableReference GetRowReference(string firstColumnName, string lastColumnName) => 
            new XlTableReference(this, firstColumnName, lastColumnName, XlTablePart.ThisRow);

        internal bool HasIntersection(XlCellRange other) => 
            this.range.HasCommonCells(other);

        private bool InsideDataPart(XlCellPosition currentCell)
        {
            if (!currentCell.IsValid)
            {
                return false;
            }
            int row = this.Range.TopLeft.Row;
            if (this.HasHeaderRow)
            {
                row++;
            }
            int num2 = this.Range.BottomRight.Row;
            if (this.HasTotalRow)
            {
                num2--;
            }
            return ((currentCell.Row >= row) && (currentCell.Row <= num2));
        }

        private bool IsValidTableName(string value) => 
            !string.IsNullOrEmpty(value) ? XlNameChecker.IsValidIdentifier(value) : false;

        internal bool IsValidTablePart(XlTablePart part) => 
            (this.HasHeaderRow || (part != XlTablePart.Headers)) ? (this.IsClosed ? (this.HasTotalRow || (part != XlTablePart.Totals)) : ((part == XlTablePart.Any) || ((part == XlTablePart.Headers) || (part == XlTablePart.ThisRow)))) : false;

        internal void ValidateNumberOfRows()
        {
            int num = 1;
            if (this.HasHeaderRow)
            {
                num++;
            }
            if (this.HasTotalRow)
            {
                num++;
            }
            if (this.range.RowCount < num)
            {
                throw new Exception($"Table range must contains at least {num} rows.");
            }
        }

        internal int Id { get; set; }

        public string Comment { get; set; }

        public string Name
        {
            get => 
                this.name;
            set
            {
                if (!this.IsValidTableName(value))
                {
                    throw new ArgumentException($"'{value}' is not valid table name.");
                }
                if (this.repository != null)
                {
                    this.repository.UnregisterTableName(this.name);
                }
                this.name = value;
                if (this.repository != null)
                {
                    this.repository.RegisterTableName(this.name);
                }
            }
        }

        public bool HasHeaderRow { get; set; }

        public bool HasTotalRow { get; set; }

        public bool HasAutoFilter { get; set; }

        public bool InsertRowShowing { get; set; }

        public bool InsertRowShift { get; set; }

        public bool Published { get; set; }

        public IXlCellRange Range =>
            this.range;

        protected internal XlCellRange InnerRange =>
            this.range;

        public IXlTableColumnCollection Columns =>
            this.columns;

        protected internal IList<XlTableColumn> InnerColumns =>
            this.innerColumns;

        public IXlTableStyleInfo Style =>
            this.styleInfo;

        public XlDifferentialFormatting TableBorderFormatting { get; set; }

        internal bool IsClosed { get; set; }

        internal bool HasData { get; private set; }
    }
}

