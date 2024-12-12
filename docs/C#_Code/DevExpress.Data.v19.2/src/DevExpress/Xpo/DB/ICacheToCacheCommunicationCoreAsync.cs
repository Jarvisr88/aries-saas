namespace DevExpress.Xpo.DB
{
    using DevExpress.Xpo.DB.Helpers;
    using System;
    using System.Threading.Tasks;

    public interface ICacheToCacheCommunicationCoreAsync : ICacheToCacheCommunicationCore
    {
        Task<DataCacheModificationResult> ModifyDataAsync(DataCacheCookie cookie, ModificationStatement[] dmlStatements);
        Task<DataCacheResult> NotifyDirtyTablesAsync(DataCacheCookie cookie, params string[] dirtyTablesNames);
        Task<DataCacheResult> ProcessCookieAsync(DataCacheCookie cookie);
        Task<DataCacheSelectDataResult> SelectDataAsync(DataCacheCookie cookie, SelectStatement[] selects);
        Task<DataCacheUpdateSchemaResult> UpdateSchemaAsync(DataCacheCookie cookie, DBTable[] tables, bool doNotCreateIfFirstTableNotExist);
    }
}

