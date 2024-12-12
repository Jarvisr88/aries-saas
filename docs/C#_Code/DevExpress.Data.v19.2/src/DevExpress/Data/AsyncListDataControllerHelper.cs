namespace DevExpress.Data
{
    using DevExpress.Data.Helpers;
    using System;

    public class AsyncListDataControllerHelper : ListDataControllerHelper
    {
        public AsyncListDataControllerHelper(AsyncServerModeDataController controller);
        protected override Delegate GetGetRowValueCore(DataColumnInfo columnInfo, Type expectedReturnType);
        public override object GetRow(int listSourceRow, OperationCompleted completed);
        public override object GetRowKey(int listSourceRow);
        public override object GetRowValue(int listSourceRow, DataColumnInfo columnInfo, OperationCompleted completed);
    }
}

