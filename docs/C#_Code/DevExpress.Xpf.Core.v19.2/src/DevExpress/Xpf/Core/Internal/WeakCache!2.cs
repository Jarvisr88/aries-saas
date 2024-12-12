namespace DevExpress.Xpf.Core.Internal
{
    using DevExpress.XtraPrinting.Native;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.InteropServices;

    public class WeakCache<TKey, TValue> where TValue: class
    {
        private int increment;
        private readonly IDictionary<TKey, WeakReference<TValue>> internalCache;

        public WeakCache()
        {
            this.internalCache = new Dictionary<TKey, WeakReference<TValue>>();
        }

        private static void AddToCache(TKey key, TValue value, IDictionary<TKey, WeakReference<TValue>> cache)
        {
            IDictionary<TKey, WeakReference<TValue>> dictionary = cache;
            lock (dictionary)
            {
                cache[key] = new WeakReference<TValue>(value);
            }
        }

        public void Collect(bool force = false)
        {
            if (!force && ((this.increment % 100) == 0))
            {
                WeakCache<TKey, TValue>.CollectCache(this.internalCache);
            }
        }

        private static void CollectCache(IDictionary<TKey, WeakReference<TValue>> cache)
        {
            IDictionary<TKey, WeakReference<TValue>> dictionary = cache;
            lock (dictionary)
            {
                foreach (KeyValuePair<TKey, WeakReference<TValue>> pair in cache.ToList<KeyValuePair<TKey, WeakReference<TValue>>>())
                {
                    TValue local;
                    if (!pair.Value.TryGetTarget(out local))
                    {
                        cache.Remove(pair.Key);
                    }
                }
            }
        }

        public TValue Get(TKey key) => 
            WeakCache<TKey, TValue>.GetFromCache(key, this.internalCache);

        private static TValue GetFromCache(TKey uri, IDictionary<TKey, WeakReference<TValue>> cache)
        {
            IDictionary<TKey, WeakReference<TValue>> dictionary = cache;
            lock (dictionary)
            {
                return WeakCache<TKey, TValue>.GetValue(uri, cache);
            }
        }

        private static TValue GetValue(TKey uri, IDictionary<TKey, WeakReference<TValue>> cache)
        {
            TValue local;
            WeakReference<TValue> reference = cache.GetValueOrDefault<TKey, WeakReference<TValue>>(uri, null);
            if ((reference != null) && reference.TryGetTarget(out local))
            {
                return local;
            }
            return default(TValue);
        }

        private void Increment()
        {
            this.increment++;
            this.Collect(false);
        }

        public void Remove(TKey key)
        {
            WeakCache<TKey, TValue>.RemoveFromCache(key, this.internalCache);
            this.Increment();
        }

        private static void RemoveFromCache(TKey uri, IDictionary<TKey, WeakReference<TValue>> cache)
        {
            IDictionary<TKey, WeakReference<TValue>> dictionary = cache;
            lock (dictionary)
            {
                cache.Remove(uri);
            }
        }

        public void Set(TKey key, TValue obj)
        {
            WeakCache<TKey, TValue>.AddToCache(key, obj, this.internalCache);
            this.Increment();
        }
    }
}

