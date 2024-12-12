namespace DevExpress.Pdf.Native
{
    using System;
    using System.Collections.Generic;

    public class WeakReferenceCache<TKey, TValue> where TKey: class where TValue: class
    {
        private readonly IDictionary<TKey, WeakReference<TValue>> cache;
        private readonly Func<TKey, TValue> factoryFunc;

        public WeakReferenceCache(Func<TKey, TValue> factoryFunc)
        {
            this.cache = new Dictionary<TKey, WeakReference<TValue>>();
            this.factoryFunc = factoryFunc;
        }

        public TValue GetValue(TKey key)
        {
            WeakReference<TValue> reference;
            TValue local;
            if (!this.cache.TryGetValue(key, out reference) || !reference.TryGetTarget(out local))
            {
                local = this.factoryFunc(key);
                if (local != null)
                {
                    this.cache[key] = new WeakReference<TValue>(local);
                }
            }
            return local;
        }
    }
}

