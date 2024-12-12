namespace DevExpress.Data.Browsing.Design
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;

    [EditorBrowsable(EditorBrowsableState.Never)]
    public static class DataSourceMemberResolutionMap
    {
        private static readonly object syncObj;
        private static readonly IDictionary<Type, Func<object, string, object>> cache;

        static DataSourceMemberResolutionMap();
        [EditorBrowsable(EditorBrowsableState.Never)]
        public static object GetDataSource(object dataSource, string dataMember);
        [EditorBrowsable(EditorBrowsableState.Never)]
        public static void Register<T>(Func<T, string, object> resolve);
        private static object ResolveDataTableFromDataSet(object dataSource, string dataMember);
    }
}

