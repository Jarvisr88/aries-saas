namespace DevExpress.Data.Helpers
{
    using DevExpress.Data;
    using DevExpress.Data.Filtering;
    using System;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    public class AsyncServerModeDataControllerFilterHelper : DataControllerFilterHelper
    {
        public AsyncServerModeDataControllerFilterHelper(AsyncServerModeDataController controller);
        public override object[] GetUniqueColumnValues(DataColumnInfo columnInfo, ColumnValuesArguments args, OperationCompleted completed);
        internal static void PrepareUniqueVales(DataController controller, DataColumnInfo columnInfo, ColumnValuesArguments args, OperationCompleted completed, out OperationCompleted patchedCompleted, out CriteriaOperator valueExpression, out CriteriaOperator filterExpression, out int maxCount);

        public AsyncServerModeDataController Controller { get; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly AsyncServerModeDataControllerFilterHelper.<>c <>9;
            public static Func<object, bool> <>9__4_1;

            static <>c();
            internal bool <PrepareUniqueVales>b__4_1(object o);
        }
    }
}

