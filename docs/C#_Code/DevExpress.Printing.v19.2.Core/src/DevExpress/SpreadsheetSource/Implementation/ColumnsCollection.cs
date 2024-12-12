namespace DevExpress.SpreadsheetSource.Implementation
{
    using DevExpress.Utils;
    using System;
    using System.Collections.Generic;

    public class ColumnsCollection : List<ColumnInfo>
    {
        public ColumnInfo FindColumn(int columnIndex)
        {
            int num = Algorithms.BinarySearchReverseOrder<ColumnInfo>(this, new ColumnInfoComparable(columnIndex));
            return ((num < 0) ? null : base[num]);
        }

        public int GetFormatIndex(int columnIndex)
        {
            ColumnInfo info = this.FindColumn(columnIndex);
            return ((info != null) ? info.FormatIndex : -1);
        }

        public bool IsColumnHidden(int columnIndex)
        {
            ColumnInfo info = this.FindColumn(columnIndex);
            return ((info != null) ? info.IsHidden : false);
        }
    }
}

