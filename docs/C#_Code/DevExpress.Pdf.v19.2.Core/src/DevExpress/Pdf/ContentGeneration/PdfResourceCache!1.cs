namespace DevExpress.Pdf.ContentGeneration
{
    using DevExpress.Pdf.Native;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;

    public abstract class PdfResourceCache<T> : PdfDisposableObject where T: IDisposable
    {
        private const int cacheCleanUpInterval = 0xbb8;
        private readonly Dictionary<PdfResourcesCacheKey, T> cache;
        private int lastCleanUpTime;

        protected PdfResourceCache()
        {
            this.cache = new Dictionary<PdfResourcesCacheKey, T>();
            this.lastCleanUpTime = Environment.TickCount;
        }

        protected T CacheObject(PdfResourcesCacheKey key, Func<T> create)
        {
            T local;
            if ((Environment.TickCount - this.lastCleanUpTime) > 0xbb8)
            {
                Func<KeyValuePair<PdfResourcesCacheKey, T>, bool> predicate = <>c<T>.<>9__4_0;
                if (<>c<T>.<>9__4_0 == null)
                {
                    Func<KeyValuePair<PdfResourcesCacheKey, T>, bool> local1 = <>c<T>.<>9__4_0;
                    predicate = <>c<T>.<>9__4_0 = v => !v.Key.IsAlive;
                }
                foreach (KeyValuePair<PdfResourcesCacheKey, T> pair in this.cache.Where<KeyValuePair<PdfResourcesCacheKey, T>>(predicate).ToList<KeyValuePair<PdfResourcesCacheKey, T>>())
                {
                    pair.Value.Dispose();
                    this.cache.Remove(pair.Key);
                }
                this.lastCleanUpTime = Environment.TickCount;
            }
            if (!this.cache.TryGetValue(key, out local))
            {
                local = create();
                this.cache.Add(key, local);
            }
            return local;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                foreach (KeyValuePair<PdfResourcesCacheKey, T> pair in this.cache)
                {
                    pair.Value.Dispose();
                }
            }
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly PdfResourceCache<T>.<>c <>9;
            public static Func<KeyValuePair<PdfResourcesCacheKey, T>, bool> <>9__4_0;

            static <>c()
            {
                PdfResourceCache<T>.<>c.<>9 = new PdfResourceCache<T>.<>c();
            }

            internal bool <CacheObject>b__4_0(KeyValuePair<PdfResourcesCacheKey, T> v) => 
                !v.Key.IsAlive;
        }
    }
}

