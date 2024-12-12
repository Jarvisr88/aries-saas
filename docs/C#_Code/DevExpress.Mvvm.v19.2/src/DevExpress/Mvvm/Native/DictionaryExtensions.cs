namespace DevExpress.Mvvm.Native
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public static class DictionaryExtensions
    {
        public static TValue GetOrAdd<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, Func<TValue> createValueDelegate)
        {
            TValue local;
            if (!dictionary.TryGetValue(key, out local))
            {
                dictionary[key] = local = createValueDelegate();
            }
            return local;
        }

        public static TValue GetOrAdd<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, Func<TKey, TValue> createValueDelegate)
        {
            TValue local;
            if (!dictionary.TryGetValue(key, out local))
            {
                dictionary[key] = local = createValueDelegate(key);
            }
            return local;
        }

        public static TValue GetValueOrDefault<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key)
        {
            TValue local;
            dictionary.TryGetValue(key, out local);
            return local;
        }

        public static TValue GetValueOrDefault<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, TValue defaultValue)
        {
            TValue local;
            return (!dictionary.TryGetValue(key, out local) ? defaultValue : local);
        }
    }
}

