namespace DevExpress.Data.Helpers
{
    using DevExpress.Data;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;

    public class BaseListDataControllerHelper : BaseDataControllerHelper
    {
        public BaseListDataControllerHelper(DataControllerBase controller);
        [IteratorStateMachine(typeof(BaseListDataControllerHelper.<GetColumnAuxInfo>d__5))]
        internal static IEnumerable<string> GetColumnAuxInfo(DataColumnInfo columnInfo);
        protected static Func<string[]> GetExceptionAuxInfoGetter(DataColumnInfo columnInfo, Type expectedReturnType);
        protected override Delegate GetGetRowValueCore(DataColumnInfo columnInfo, Type expectedReturnType);
        public override object GetRow(int listSourceRow, OperationCompleted completed);
        public override object GetRowValue(int listSourceRow, DataColumnInfo columnInfo, OperationCompleted completed);
        public override object GetRowValueDetail(int listSourceRow, DataColumnInfo detailColumn);
        protected static object KillDBNull(object nullableSomethig);
        public override void SetRowValue(int listSourceRow, int column, object val);

    }
}

