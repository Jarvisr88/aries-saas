namespace DevExpress.Data.Browsing.Design
{
    using DevExpress.Data.Browsing;
    using System;
    using System.Collections;
    using System.ComponentModel;

    public class DataSortedPropertiesNativeProvider : PropertiesProvider
    {
        private IDataContextService serv;

        public DataSortedPropertiesNativeProvider();
        public DataSortedPropertiesNativeProvider(DataContext dataContext, IDataContextService serv, TypeSpecificsService typeSpecificsService);
        public DataSortedPropertiesNativeProvider(IDataContextService serv, DataContextOptions dataContextOptions, TypeSpecificsService typeSpecificsService);
        protected override PropertyDescriptor[] FilterProperties(ICollection properties, object dataSource, string dataMember);
        protected override void SortProperties(IPropertyDescriptor[] properties);
    }
}

