namespace DevExpress.Xpf.Core.Internal
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Reflection;
    using System.Runtime.InteropServices;

    public interface IReadOnlyDictionary<TKey, TValue> : DevExpress.Xpf.Core.Internal.IReadOnlyCollection<KeyValuePair<TKey, TValue>>, IEnumerable<KeyValuePair<TKey, TValue>>, IEnumerable
    {
        bool ContainsKey(TKey key);
        bool TryGetValue(TKey key, out TValue value);

        TValue this[TKey key] { get; }

        IEnumerable<TKey> Keys { get; }

        IEnumerable<TValue> Values { get; }
    }
}

