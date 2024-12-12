namespace DevExpress.Xpf.Docking
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    internal static class IDictionaryExtensions
    {
        public static TKey GetKeyByValue<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TValue value)
        {
            TKey key;
            if (dictionary == null)
            {
                throw new ArgumentNullException("dictionary");
            }
            using (IEnumerator<KeyValuePair<TKey, TValue>> enumerator = dictionary.GetEnumerator())
            {
                while (true)
                {
                    if (!enumerator.MoveNext())
                    {
                        throw new ArgumentException("value");
                    }
                    KeyValuePair<TKey, TValue> current = enumerator.Current;
                    if (value.Equals(current.Value))
                    {
                        key = current.Key;
                        break;
                    }
                }
            }
            return key;
        }
    }
}

