namespace DevExpress.XtraReports.Native.Data
{
    using DevExpress.Data.Browsing;
    using DevExpress.Data.Browsing.Design;
    using DevExpress.XtraReports.Design;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Windows.Forms;

    public class DataContextServiceBase : IDisposable, IDataContextService
    {
        internal const SortOrder DefaultSortOrder = SortOrder.Ascending;
        protected Dictionary<DataContextOptions, DataContext> dictionary;
        private SortOrder propertiesSortOrder;
        private DevExpress.XtraReports.Design.ShowComplexProperties showComplexProperties;

        public event EventHandler<DataContextFilterPropertiesEventArgs> PrefilterProperties;

        public DataContextServiceBase();
        public DataContext CreateDataContext(DataContextOptions options);
        public DataContext CreateDataContext(DataContextOptions options, bool useCache);
        protected virtual DataContext CreateDataContextInternal(DataContextOptions options);
        PropertyDescriptor[] IDataContextService.FilterProperties(PropertyDescriptor[] properties, object dataSource, string dataMember, DataContext dataContext);
        public virtual void Dispose();
        protected void DisposeDataContext();
        protected virtual PropertyDescriptor[] FilterProperties(PropertyDescriptor[] properties, object dataSource, string dataMember, DataContext dataContext);
        public virtual void SortProperties(IPropertyDescriptor[] properties);
        private static int ToFactor(IPropertyDescriptor property);

        public DevExpress.XtraReports.Design.ShowComplexProperties ShowComplexProperties { get; set; }

        public SortOrder PropertiesSortOrder { get; set; }

        protected virtual IEnumerable<ICalculatedField> CalculatedFields { get; }

        protected virtual bool SuppressListFilling { get; }
    }
}

