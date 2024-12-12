namespace DevExpress.Data.Browsing.Design
{
    using System;

    public interface IPropertiesProvider
    {
        void GetDataSourceDisplayName(object dataSource, string dataMember, EventHandler<GetDataSourceDisplayNameEventArgs> callback);
        void GetItemProperties(object dataSource, string dataMember, EventHandler<GetPropertiesEventArgs> callback);
        void GetListItemProperties(object dataSource, string dataMember, EventHandler<GetPropertiesEventArgs> callback);
    }
}

