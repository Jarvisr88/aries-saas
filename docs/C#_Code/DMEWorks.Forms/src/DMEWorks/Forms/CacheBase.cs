namespace DMEWorks.Forms
{
    using System;
    using System.Collections;
    using System.Runtime.CompilerServices;
    using System.Threading;

    public class CacheBase
    {
        protected static Hashtable hash = CollectionsUtil.CreateCaseInsensitiveHashtable();

        public static event EventHandler BeginLoadData;

        public static event EventHandler EndLoadData;

        public static void Clear()
        {
            hash.Clear();
        }

        public static void ClearTable(string tableName)
        {
            hash.Remove(tableName);
        }

        protected static void OnBeginLoadData(EventArgs e)
        {
            if (BeginLoadData != null)
            {
                BeginLoadData(typeof(CacheBase), e);
            }
        }

        protected static void OnEndLoadData(EventArgs e)
        {
            if (EndLoadData != null)
            {
                EndLoadData(typeof(CacheBase), e);
            }
        }
    }
}

