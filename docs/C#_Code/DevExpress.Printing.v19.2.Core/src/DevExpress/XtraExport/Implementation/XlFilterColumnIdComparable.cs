namespace DevExpress.XtraExport.Implementation
{
    using DevExpress.Export.Xl;
    using System;

    internal class XlFilterColumnIdComparable : IComparable<XlFilterColumn>
    {
        private int columnId;

        public XlFilterColumnIdComparable(int columnId)
        {
            this.columnId = columnId;
        }

        public int CompareTo(XlFilterColumn other) => 
            (this.columnId >= other.ColumnId) ? ((this.columnId <= other.ColumnId) ? 0 : 1) : -1;
    }
}

