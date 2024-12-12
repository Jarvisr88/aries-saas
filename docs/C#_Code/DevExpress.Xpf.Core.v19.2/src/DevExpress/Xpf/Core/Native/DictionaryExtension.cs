namespace DevExpress.Xpf.Core.Native
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public static class DictionaryExtension
    {
        public static void AddRange<TKey, TValue>(this IDictionary<TKey, TValue> source, IDictionary<TKey, TValue> dictionary);
    }
}

