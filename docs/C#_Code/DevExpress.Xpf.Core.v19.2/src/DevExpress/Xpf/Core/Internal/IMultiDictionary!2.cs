namespace DevExpress.Xpf.Core.Internal
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Reflection;

    public interface IMultiDictionary<TKey, TValue> : ICollection<KeyValuePair<TKey, TValue>>, IEnumerable<KeyValuePair<TKey, TValue>>, IEnumerable
    {
        void Add(TKey key, TValue value);
        void AddRange(TKey key, IEnumerable<TValue> values);
        bool Contains(TKey key, TValue value);
        bool ContainsKey(TKey key);
        bool ContainsValue(TValue value);
        bool Remove(TKey key);
        bool RemoveItem(TKey key, TValue value);
        IDictionary<TKey, ICollection<TValue>> ToDictionary();

        ICollection<TValue> Values { get; }

        ICollection<TKey> Keys { get; }

        ICollection<TValue> this[TKey key] { get; }
    }
}

