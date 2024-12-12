namespace DevExpress.Data
{
    using System;

    public class DataControllerSortCellEventArgs : DataControllerSortRowEventArgs
    {
        internal DataColumnInfo sortColumn;
        internal object value1;
        internal object value2;

        public DataControllerSortCellEventArgs();
        public DataControllerSortCellEventArgs(int listSourceRow1, int listSourceRow2, object value1, object value2, DataColumnInfo sortColumn);
        internal void Setup(int listSourceRow1, int listSourceRow2, object value1, object value2, DataColumnInfo sortColumn);

        public DataColumnInfo SortColumn { get; }

        public object Value1 { get; }

        public object Value2 { get; }
    }
}

