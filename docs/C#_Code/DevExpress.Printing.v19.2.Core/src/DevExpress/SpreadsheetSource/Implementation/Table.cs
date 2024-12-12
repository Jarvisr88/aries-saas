namespace DevExpress.SpreadsheetSource.Implementation
{
    using DevExpress.Export.Xl;
    using DevExpress.SpreadsheetSource;
    using DevExpress.Utils;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class Table : ITable
    {
        private readonly List<TableColumnInfo> columns = new List<TableColumnInfo>();

        public Table(string name, XlCellRange range, bool hasHeaderRow, bool hasTotalRow)
        {
            Guard.ArgumentIsNotNullOrEmpty(name, "name");
            Guard.ArgumentNotNull(range, "range");
            Guard.ArgumentIsNotNullOrEmpty(range.SheetName, "range.SheetName");
            this.Name = name;
            this.Range = range.AsAbsolute();
            this.HasHeaderRow = hasHeaderRow;
            this.HasTotalRow = hasTotalRow;
            int firstRow = range.FirstRow;
            int lastRow = range.LastRow;
            if (hasHeaderRow)
            {
                firstRow++;
            }
            if (hasTotalRow)
            {
                lastRow--;
            }
            XlCellRange range2 = XlCellRange.FromLTRB(range.FirstColumn, firstRow, range.LastColumn, lastRow);
            range2.SheetName = this.Range.SheetName;
            this.DataRange = range2.AsAbsolute();
            this.RefersTo = this.Range.ToString(true);
        }

        public bool HasDxfNumberFormats()
        {
            bool flag;
            using (List<TableColumnInfo>.Enumerator enumerator = this.columns.GetEnumerator())
            {
                while (true)
                {
                    if (enumerator.MoveNext())
                    {
                        TableColumnInfo current = enumerator.Current;
                        if ((current.NumberFormat == null) && (current.TotalRowNumberFormat == null))
                        {
                            continue;
                        }
                        flag = true;
                    }
                    else
                    {
                        return false;
                    }
                    break;
                }
            }
            return flag;
        }

        public string Name { get; private set; }

        public XlCellRange Range { get; private set; }

        public XlCellRange DataRange { get; private set; }

        public string RefersTo { get; private set; }

        public bool HasHeaderRow { get; private set; }

        public bool HasTotalRow { get; private set; }

        public List<TableColumnInfo> Columns =>
            this.columns;
    }
}

