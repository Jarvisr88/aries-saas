namespace DevExpress.Xpo.DB
{
    using DevExpress.Xpo.DB.Helpers;
    using System;

    public interface ICacheToCacheCommunicationCore
    {
        DataCacheModificationResult ModifyData(DataCacheCookie cookie, ModificationStatement[] dmlStatements);
        DataCacheResult NotifyDirtyTables(DataCacheCookie cookie, params string[] dirtyTablesNames);
        DataCacheResult ProcessCookie(DataCacheCookie cookie);
        DataCacheSelectDataResult SelectData(DataCacheCookie cookie, SelectStatement[] selects);
        DataCacheUpdateSchemaResult UpdateSchema(DataCacheCookie cookie, DBTable[] tables, bool doNotCreateIfFirstTableNotExist);
    }
}

