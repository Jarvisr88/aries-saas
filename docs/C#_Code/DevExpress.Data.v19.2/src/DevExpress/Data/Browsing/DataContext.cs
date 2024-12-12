namespace DevExpress.Data.Browsing
{
    using DevExpress.Data;
    using System;
    using System.ComponentModel;
    using System.Runtime.InteropServices;

    public class DataContext : DataContextBase
    {
        private static Type[] standardTypes;

        static DataContext();
        public DataContext();
        public DataContext(bool suppressListFilling);
        public string GetCustomDataSourceDisplayName(object dataSource);
        private IRelatedDataBrowser[] GetDataBrowserAccessors(IRelatedDataBrowser startBrowser);
        public string GetDataMemberDisplayName(object dataSource, string dataMember);
        public string GetDataMemberDisplayName(object dataSource, string dataMemberPrefix, string dataMember);
        public virtual string GetDataSourceDisplayName(object dataSource, string dataMember);
        private Type GetDataSourceType(object dataSource);
        private string GetDisplayDataMemberCore(IDisplayNameProvider dataDictionary, IRelatedDataBrowser dataBrowser);
        private static string GetDisplayDataSourceTypeName(Type dataSourceType);
        private string[] GetFieldAccessors(IRelatedDataBrowser startBrowser);
        public PropertyDescriptor[] GetItemProperties(object dataSource, string dataMember, bool forceList);
        public static bool IsComplexType(Type type);
        protected virtual bool IsIDisplayNameProviderSupported(PropertyDescriptor property);
        internal static bool IsImageType(Type type);
        public static bool IsStandardType(Type type);
        private static bool IsStandardTypeCore(Type type);
        protected override bool ShouldExpand(DataBrowser dataBrowser);
        public bool TryGetDataMemberDisplayName(object dataSource, string dataMember, out string result);
    }
}

