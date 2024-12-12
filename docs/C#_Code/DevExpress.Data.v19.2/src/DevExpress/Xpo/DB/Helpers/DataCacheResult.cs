namespace DevExpress.Xpo.DB.Helpers
{
    using System;

    [Serializable]
    public class DataCacheResult
    {
        public TableAge[] UpdatedTableAges;
        public DataCacheConfiguration CacheConfig;
        public DataCacheCookie Cookie;
    }
}

