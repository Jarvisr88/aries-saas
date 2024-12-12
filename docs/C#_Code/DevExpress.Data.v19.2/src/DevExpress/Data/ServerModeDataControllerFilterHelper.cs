namespace DevExpress.Data
{
    using DevExpress.Data.Helpers;
    using System;
    using System.Runtime.CompilerServices;

    public class ServerModeDataControllerFilterHelper : DataControllerFilterHelper
    {
        public ServerModeDataControllerFilterHelper(ServerModeDataController controller);
        public override object[] GetUniqueColumnValues(DataColumnInfo columnInfo, ColumnValuesArguments args, OperationCompleted completed);

        public ServerModeDataController Controller { get; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly ServerModeDataControllerFilterHelper.<>c <>9;
            public static Func<object, bool> <>9__3_0;

            static <>c();
            internal bool <GetUniqueColumnValues>b__3_0(object o);
        }
    }
}

