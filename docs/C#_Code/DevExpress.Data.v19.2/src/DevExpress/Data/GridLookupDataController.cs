namespace DevExpress.Data
{
    using System;
    using System.Collections;

    public class GridLookupDataController : GridDataController
    {
        public override int FindRowByValue(string columnName, object value, params OperationCompleted[] completed);
        protected override IList GetList(object dataSource);
    }
}

