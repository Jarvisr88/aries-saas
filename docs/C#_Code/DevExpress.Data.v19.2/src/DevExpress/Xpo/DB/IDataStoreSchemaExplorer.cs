namespace DevExpress.Xpo.DB
{
    using System;

    public interface IDataStoreSchemaExplorer
    {
        DBTable[] GetStorageTables(params string[] tables);
        string[] GetStorageTablesList(bool includeViews);
    }
}

