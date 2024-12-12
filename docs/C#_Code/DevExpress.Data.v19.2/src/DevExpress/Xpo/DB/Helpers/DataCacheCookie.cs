namespace DevExpress.Xpo.DB.Helpers
{
    using System;

    [Serializable]
    public class DataCacheCookie
    {
        public System.Guid Guid;
        public long Age;
        public static readonly DataCacheCookie Empty = new DataCacheCookie(System.Guid.Empty, 0L);

        public DataCacheCookie()
        {
        }

        public DataCacheCookie(System.Guid guid, long age)
        {
            this.Guid = guid;
            this.Age = age;
        }
    }
}

