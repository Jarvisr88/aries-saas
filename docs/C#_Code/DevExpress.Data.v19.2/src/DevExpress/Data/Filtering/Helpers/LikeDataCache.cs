namespace DevExpress.Data.Filtering.Helpers
{
    using System;
    using System.Collections.Concurrent;

    public static class LikeDataCache
    {
        private static readonly ConcurrentDictionary<string, Lazy<Func<string, bool?>>> sensetiveStore;
        private static readonly ConcurrentDictionary<string, Lazy<Func<string, bool?>>> insensetiveStore;

        static LikeDataCache();
        public static Func<string, bool?> Get(string pattern);
        public static Func<string, bool?> Get(string pattern, bool caseSensitive);
    }
}

