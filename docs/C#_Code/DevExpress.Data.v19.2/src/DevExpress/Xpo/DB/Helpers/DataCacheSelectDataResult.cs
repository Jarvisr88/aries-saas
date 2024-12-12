namespace DevExpress.Xpo.DB.Helpers
{
    using DevExpress.Xpo.DB;
    using System;

    [Serializable]
    public class DataCacheSelectDataResult : DataCacheResult
    {
        public DataCacheCookie SelectingCookie;
        public DevExpress.Xpo.DB.SelectedData SelectedData;
    }
}

