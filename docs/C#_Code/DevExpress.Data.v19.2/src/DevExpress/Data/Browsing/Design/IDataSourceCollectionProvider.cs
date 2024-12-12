namespace DevExpress.Data.Browsing.Design
{
    using System;

    public interface IDataSourceCollectionProvider
    {
        object[] GetDataSourceCollection(IServiceProvider serviceProvider);
    }
}

