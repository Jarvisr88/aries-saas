namespace DevExpress.Data.Browsing.Design
{
    using DevExpress.Data.Browsing;
    using System;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;

    public interface IDataContextService
    {
        event EventHandler<DataContextFilterPropertiesEventArgs> PrefilterProperties;

        DataContext CreateDataContext(DataContextOptions options);
        DataContext CreateDataContext(DataContextOptions options, bool useDictionary);
        PropertyDescriptor[] FilterProperties(PropertyDescriptor[] properties, object dataSource, string dataMember, DataContext dataContext);
        void SortProperties(IPropertyDescriptor[] properties);
    }
}

