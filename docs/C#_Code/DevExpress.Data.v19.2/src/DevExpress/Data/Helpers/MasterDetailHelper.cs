namespace DevExpress.Data.Helpers
{
    using DevExpress.Data;
    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.Data;
    using System.Reflection;
    using System.Runtime.InteropServices;
    using System.Windows.Forms;

    public class MasterDetailHelper
    {
        private static FieldInfo columnField;
        private static Type dataColumnPropertyDescriptorType;

        private bool CheckRecursive(DataTable table, DataTable childTable, int level);
        public DataColumnInfo[] GetDataColumnInfo(object data);
        protected DataColumnInfo[] GetDataColumnInfo(ITypedList list, PropertyDescriptor[] accessors);
        public DataColumnInfo[] GetDataColumnInfo(BindingContext context, object dataSource, string dataMember);
        public DataColumnInfo[] GetDataColumnInfo(BindingContext context, object dataSource, string dataMember, bool skipException);
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public DataColumnInfo[] GetDataColumnInfo(BindingContext context, object dataSource, string dataMember, IList listSource);
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public DataColumnInfo[] GetDataColumnInfo(BindingContext context, object dataSource, string dataMember, bool skipException, IList listSource);
        public static IList GetDataSource(object dataSource, string dataMember);
        public static IList GetDataSource(BindingContext context, object dataSource, string dataMember);
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public static IList GetDataSource(BindingContext context, object dataSource, string dataMember, IList existingListSource);
        private static IList GetDataSourceCore(BindingContext context, object dataSource, string dataMember, IList existingListSource);
        protected DataColumnInfo[] GetDetailColumnsInfo(IList list);
        protected DataColumnInfo[] GetDetailColumnsInfo(ITypedList list, PropertyDescriptor[] accessors);
        public DetailNodeInfo[] GetDetailInfo(BindingContext context, object dataSource, string dataMember);
        public DetailNodeInfo[] GetDetailInfo(BindingContext context, object dataSource, string dataMember, bool allowExceptions);
        public static string GetDisplayName(PropertyDescriptor descriptor);
        public static string GetDisplayNameCore(PropertyDescriptor descriptor);
        public static string GetDisplayNameCore(PropertyDescriptor descriptor, bool useSplitCasing);
        private static IList GetTypedDataSource(Type type);
        public static bool HasDisplayAttribute(PropertyDescriptor descriptor);
        public static bool IsDataSourceReady(BindingContext context, object dataSource, string dataMember);
        protected virtual void PopulateListRelations(DetailNodeInfo parent, IList list, int level, ref int callCount);
        protected virtual void PopulateRelations(DetailNodeInfo parent, DataTable table, int level);
        protected virtual void PopulateRelations(DetailNodeInfo parent, IRelationList list, int level, ref int callCount);
        protected virtual void PopulateTypedListRelations(DetailNodeInfo parent, ITypedList list, int level, ref int callCount, PropertyDescriptor[] accessors);
        public static string SplitPascalCaseString(string value);
        public static bool TryGetDataTableDataColumnCaption(PropertyDescriptor descriptor, out string caption);

        private static Type DataColumnPropertyDescriptorType { get; }

        private sealed class EmptyListSourceDataController : ListSourceDataController
        {
            protected override void DoRefresh(bool useRowsKeeper);
        }
    }
}

