namespace DevExpress.Data.Filtering
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public abstract class CustomMultiKeyDictionaryCollection<K, T> : ICollection<T>, IEnumerable<T>, IEnumerable
    {
        private readonly Dictionary<K, T> items;

        protected CustomMultiKeyDictionaryCollection();
        protected CustomMultiKeyDictionaryCollection(IEqualityComparer<K> customComparer);
        public void Add(T item);
        public void Add(IEnumerable<T> items);
        public void Clear();
        public bool Contains(T item);
        public void CopyTo(T[] array, int arrayIndex);
        public IEnumerator<T> GetEnumerator();
        public T GetItem(K key);
        protected abstract K[] GetKey(T item);
        public bool Remove(T item);
        IEnumerator IEnumerable.GetEnumerator();

        public int Count { get; }

        public bool IsReadOnly { get; }
    }
}

