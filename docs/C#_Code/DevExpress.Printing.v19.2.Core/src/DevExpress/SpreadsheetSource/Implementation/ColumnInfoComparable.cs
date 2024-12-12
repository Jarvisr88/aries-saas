namespace DevExpress.SpreadsheetSource.Implementation
{
    using System;

    internal class ColumnInfoComparable : IComparable<ColumnInfo>
    {
        private int index;

        public ColumnInfoComparable(int index)
        {
            this.index = index;
        }

        public int CompareTo(ColumnInfo other) => 
            (this.index >= other.FirstIndex) ? ((this.index <= other.LastIndex) ? 0 : -1) : 1;
    }
}

