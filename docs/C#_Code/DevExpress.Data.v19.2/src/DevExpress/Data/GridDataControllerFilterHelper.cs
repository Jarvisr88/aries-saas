namespace DevExpress.Data
{
    using DevExpress.Data.Helpers;
    using System;

    public class GridDataControllerFilterHelper : DataControllerFilterHelper
    {
        public GridDataControllerFilterHelper(DataController controller);
        public override object[] GetUniqueColumnValues(DataColumnInfo columnInfo, ColumnValuesArguments args, OperationCompleted completed);

        public BaseGridController Controller { get; }
    }
}

