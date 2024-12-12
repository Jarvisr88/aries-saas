namespace DevExpress.Xpf.Core.FilteringUI
{
    using DevExpress.Mvvm.Native;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    internal class DictionaryWithNullableKey<TKey, TValue>
    {
        private readonly IDictionary<TKey, TValue> dictionary;
        private TValue nullKeyValue;

        public DictionaryWithNullableKey(IDictionary<TKey, TValue> dictionary, TValue nullKeyValue)
        {
            this.dictionary = dictionary;
            this.nullKeyValue = nullKeyValue;
        }

        public TValue GetValueOrDefault(TKey key) => 
            (key != null) ? this.dictionary.GetValueOrDefault<TKey, TValue>(key) : this.nullKeyValue;

        public void Remove(TKey key)
        {
            if (key == null)
            {
                this.nullKeyValue = default(TValue);
            }
            else
            {
                this.dictionary.Remove(key);
            }
        }

        public IEnumerable<TValue> Values =>
            this.nullKeyValue.YieldIfNotNull<TValue>().Concat<TValue>(this.dictionary.Values);
    }
}

