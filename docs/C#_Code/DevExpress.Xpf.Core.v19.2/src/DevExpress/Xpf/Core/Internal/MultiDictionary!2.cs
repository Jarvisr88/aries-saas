namespace DevExpress.Xpf.Core.Internal
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Runtime.InteropServices;

    public class MultiDictionary<TKey, TValue> : IMultiDictionary<TKey, TValue>, ICollection<KeyValuePair<TKey, TValue>>, IEnumerable<KeyValuePair<TKey, TValue>>, IEnumerable, DevExpress.Xpf.Core.Internal.IReadOnlyDictionary<TKey, ICollection<TValue>>, DevExpress.Xpf.Core.Internal.IReadOnlyCollection<KeyValuePair<TKey, ICollection<TValue>>>, IEnumerable<KeyValuePair<TKey, ICollection<TValue>>>
    {
        private readonly Dictionary<TKey, ICollection<TValue>> dictionary;
        private int count;
        private ValueCollection<TKey, TValue> values;
        private int version;

        public MultiDictionary()
        {
            this.dictionary = new Dictionary<TKey, ICollection<TValue>>();
        }

        public MultiDictionary(IEnumerable<KeyValuePair<TKey, TValue>> enumerable) : this(enumerable, null)
        {
        }

        public MultiDictionary(IEqualityComparer<TKey> comparer)
        {
            this.dictionary = new Dictionary<TKey, ICollection<TValue>>(comparer);
        }

        public MultiDictionary(int capacity)
        {
            this.dictionary = new Dictionary<TKey, ICollection<TValue>>(capacity);
        }

        public MultiDictionary(IEnumerable<KeyValuePair<TKey, TValue>> enumerable, IEqualityComparer<TKey> comparer)
        {
            this.dictionary = new Dictionary<TKey, ICollection<TValue>>(comparer);
            foreach (KeyValuePair<TKey, TValue> pair in enumerable)
            {
                this.Add(pair.Key, pair.Value);
            }
        }

        public MultiDictionary(int capacity, IEqualityComparer<TKey> comparer)
        {
            this.dictionary = new Dictionary<TKey, ICollection<TValue>>(capacity, comparer);
        }

        public void Add(KeyValuePair<TKey, TValue> item)
        {
            this.Add(item.Key, item.Value);
        }

        public void Add(TKey key, TValue value)
        {
            ICollection<TValue> is2;
            if (this.dictionary.TryGetValue(key, out is2))
            {
                is2.Add(value);
            }
            else
            {
                ICollection<TValue> is3 = this.NewCollection(null);
                is3.Add(value);
                this.dictionary.Add(key, is3);
            }
            this.count++;
            this.version++;
        }

        public void AddRange(TKey key, IEnumerable<TValue> values)
        {
            ICollection<TValue> is2;
            if (!this.dictionary.TryGetValue(key, out is2))
            {
                ICollection<TValue> is3 = this.NewCollection(values);
                this.dictionary.Add(key, is3);
                this.count += is3.Count;
            }
            else
            {
                foreach (TValue local in values)
                {
                    is2.Add(local);
                    this.count++;
                }
            }
            this.version++;
        }

        public void Clear()
        {
            this.count = 0;
            this.version++;
            this.dictionary.Clear();
        }

        public bool Contains(KeyValuePair<TKey, TValue> item) => 
            this.Contains(item.Key, item.Value);

        public bool Contains(TKey key, TValue value)
        {
            ICollection<TValue> is2;
            return (this.dictionary.TryGetValue(key, out is2) && is2.Contains(value));
        }

        public bool ContainsKey(TKey key)
        {
            ICollection<TValue> is2;
            return (this.dictionary.TryGetValue(key, out is2) && (is2.Count > 0));
        }

        [Obsolete]
        public bool ContainsValue(TValue value) => 
            this.dictionary.Values.Any<ICollection<TValue>>(collection => collection.Contains(value));

        public bool ContainsValue(TKey key, TValue value)
        {
            ICollection<TValue> is2;
            return (this.dictionary.TryGetValue(key, out is2) && is2.Contains(value));
        }

        public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
        {
            foreach (KeyValuePair<TKey, TValue> pair in this)
            {
                array[arrayIndex++] = pair;
            }
        }

        bool DevExpress.Xpf.Core.Internal.IReadOnlyDictionary<TKey, ICollection<TValue>>.TryGetValue(TKey key, out ICollection<TValue> value)
        {
            value = new InnerCollectionView<TKey, TValue>((MultiDictionary<TKey, TValue>) this, key);
            return true;
        }

        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator() => 
            new Enumerator<TKey, TValue>((MultiDictionary<TKey, TValue>) this);

        protected virtual ICollection<TValue> NewCollection(IEnumerable<TValue> collection = null) => 
            (collection != null) ? new MultiDictionaryEntry<TKey, TValue>(collection) : new MultiDictionaryEntry<TKey, TValue>();

        public bool Remove(TKey key)
        {
            ICollection<TValue> is2;
            if (!this.dictionary.TryGetValue(key, out is2) || !this.dictionary.Remove(key))
            {
                return false;
            }
            this.count -= is2.Count;
            this.version++;
            return true;
        }

        public bool Remove(KeyValuePair<TKey, TValue> item) => 
            this.RemoveItem(item.Key, item.Value);

        public bool RemoveItem(TKey key, TValue value)
        {
            ICollection<TValue> is2;
            if (!this.dictionary.TryGetValue(key, out is2) || !is2.Remove(value))
            {
                return false;
            }
            if (is2.Count == 0)
            {
                this.dictionary.Remove(key);
            }
            this.count--;
            this.version++;
            return true;
        }

        IEnumerator<KeyValuePair<TKey, ICollection<TValue>>> IEnumerable<KeyValuePair<TKey, ICollection<TValue>>>.GetEnumerator() => 
            this.dictionary.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => 
            this.GetEnumerator();

        public IDictionary<TKey, ICollection<TValue>> ToDictionary() => 
            new Dictionary<TKey, ICollection<TValue>>(this.dictionary, this.dictionary.Comparer);

        public ICollection<TValue> Values
        {
            get
            {
                ValueCollection<TKey, TValue> values = this.values;
                if (this.values == null)
                {
                    ValueCollection<TKey, TValue> local1 = this.values;
                    values = this.values = new ValueCollection<TKey, TValue>((MultiDictionary<TKey, TValue>) this);
                }
                return values;
            }
        }

        public ICollection<TKey> Keys =>
            this.dictionary.Keys;

        public ICollection<TValue> this[TKey key] =>
            new InnerCollectionView<TKey, TValue>((MultiDictionary<TKey, TValue>) this, key);

        public int Count =>
            this.count;

        public bool IsReadOnly =>
            false;

        IEnumerable<ICollection<TValue>> DevExpress.Xpf.Core.Internal.IReadOnlyDictionary<TKey, ICollection<TValue>>.Values =>
            this.dictionary.Values;

        IEnumerable<TKey> DevExpress.Xpf.Core.Internal.IReadOnlyDictionary<TKey, ICollection<TValue>>.Keys =>
            this.Keys;

        int DevExpress.Xpf.Core.Internal.IReadOnlyCollection<KeyValuePair<TKey, ICollection<TValue>>>.Count =>
            this.dictionary.Count;

        public class Enumerator : IEnumerator<KeyValuePair<TKey, TValue>>, IDisposable, IEnumerator
        {
            private readonly MultiDictionary<TKey, TValue> dictionary;
            private readonly IEnumerator<KeyValuePair<TKey, ICollection<TValue>>> keyEnumerator;
            private readonly int version;
            private KeyValuePair<TKey, ICollection<TValue>> currentListPair;
            private KeyValuePair<TKey, TValue> currentValuePair;
            private IEnumerator<TValue> valuesEnumerator;
            private bool disposed;

            internal Enumerator(MultiDictionary<TKey, TValue> multiDictionary)
            {
                this.dictionary = multiDictionary;
                this.keyEnumerator = multiDictionary.dictionary.GetEnumerator();
                this.currentListPair = this.keyEnumerator.Current;
                this.valuesEnumerator = null;
                this.currentValuePair = new KeyValuePair<TKey, TValue>();
                this.version = multiDictionary.version;
            }

            public void Dispose()
            {
                this.Dispose(true);
                GC.SuppressFinalize(this);
            }

            private void Dispose(bool disposing)
            {
                if (!this.disposed)
                {
                    try
                    {
                        if (disposing)
                        {
                            if (this.keyEnumerator != null)
                            {
                                this.keyEnumerator.Dispose();
                            }
                            if (this.valuesEnumerator != null)
                            {
                                this.valuesEnumerator.Dispose();
                            }
                        }
                    }
                    finally
                    {
                        this.disposed = true;
                    }
                }
            }

            ~Enumerator()
            {
                this.Dispose(false);
            }

            public bool MoveNext()
            {
                if (this.version != this.dictionary.version)
                {
                    throw new InvalidOperationException("Enumeration is modified");
                }
                if ((this.valuesEnumerator != null) && this.valuesEnumerator.MoveNext())
                {
                    this.currentValuePair = new KeyValuePair<TKey, TValue>(this.currentListPair.Key, this.valuesEnumerator.Current);
                    return true;
                }
                if (this.MoveNextKey())
                {
                    return this.MoveNext();
                }
                this.currentValuePair = new KeyValuePair<TKey, TValue>();
                return false;
            }

            private bool MoveNextKey()
            {
                if (!this.keyEnumerator.MoveNext())
                {
                    return false;
                }
                this.currentListPair = this.keyEnumerator.Current;
                this.valuesEnumerator = this.currentListPair.Value.GetEnumerator();
                return true;
            }

            public void Reset()
            {
                if (this.version != this.dictionary.version)
                {
                    throw new InvalidOperationException("Enumeration is modified");
                }
                this.keyEnumerator.Reset();
                this.currentListPair = this.keyEnumerator.Current;
                this.valuesEnumerator = null;
                this.currentValuePair = new KeyValuePair<TKey, TValue>();
            }

            public KeyValuePair<TKey, TValue> Current =>
                this.currentValuePair;

            object IEnumerator.Current =>
                this.currentValuePair;
        }

        private class InnerCollectionView : ICollection<TValue>, IEnumerable<TValue>, IEnumerable
        {
            private readonly TKey key;
            private readonly MultiDictionary<TKey, TValue> multidictionary;

            public InnerCollectionView(MultiDictionary<TKey, TValue> multidictionary, TKey key)
            {
                this.multidictionary = multidictionary;
                this.key = key;
            }

            public void Add(TValue item)
            {
                this.multidictionary.Add(this.key, item);
            }

            public void Clear()
            {
                this.multidictionary.Remove(this.key);
            }

            public bool Contains(TValue item)
            {
                ICollection<TValue> is2;
                return (this.multidictionary.dictionary.TryGetValue(this.key, out is2) && is2.Contains(item));
            }

            public void CopyTo(TValue[] array, int arrayIndex)
            {
                ICollection<TValue> is2;
                if (this.multidictionary.dictionary.TryGetValue(this.key, out is2))
                {
                    is2.CopyTo(array, arrayIndex);
                }
            }

            public IEnumerator<TValue> GetEnumerator() => 
                new Enumerator<TKey, TValue>(this.multidictionary, this.key);

            public bool Remove(TValue item) => 
                this.multidictionary.RemoveItem(this.key, item);

            IEnumerator IEnumerable.GetEnumerator() => 
                this.GetEnumerator();

            public int Count
            {
                get
                {
                    ICollection<TValue> is2;
                    return (!this.multidictionary.dictionary.TryGetValue(this.key, out is2) ? 0 : is2.Count);
                }
            }

            public bool IsReadOnly
            {
                get
                {
                    ICollection<TValue> is2;
                    return (!this.multidictionary.dictionary.TryGetValue(this.key, out is2) ? this.multidictionary.NewCollection(null).IsReadOnly : this.multidictionary.dictionary[this.key].IsReadOnly);
                }
            }

            private class Enumerator : IEnumerator<TValue>, IDisposable, IEnumerator
            {
                private readonly IEnumerator<TValue> enumerator;
                private readonly MultiDictionary<TKey, TValue> multiDictionary;
                private readonly int version;
                private bool disposed;

                public Enumerator(MultiDictionary<TKey, TValue> multiDictionary, TKey key)
                {
                    ICollection<TValue> is2;
                    this.multiDictionary = multiDictionary;
                    this.version = multiDictionary.version;
                    if (multiDictionary.dictionary.TryGetValue(key, out is2))
                    {
                        this.enumerator = is2.GetEnumerator();
                    }
                }

                public void Dispose()
                {
                    this.Dispose(true);
                    GC.SuppressFinalize(this);
                }

                private void Dispose(bool disposing)
                {
                    if (!this.disposed)
                    {
                        try
                        {
                            if (disposing && (this.enumerator != null))
                            {
                                this.enumerator.Dispose();
                            }
                        }
                        finally
                        {
                            this.disposed = true;
                        }
                    }
                }

                ~Enumerator()
                {
                    this.Dispose(false);
                }

                public bool MoveNext()
                {
                    if (this.version != this.multiDictionary.version)
                    {
                        throw new InvalidOperationException("Enumeration is modified");
                    }
                    return ((this.enumerator != null) ? this.enumerator.MoveNext() : false);
                }

                public void Reset()
                {
                    if (this.version != this.multiDictionary.version)
                    {
                        throw new InvalidOperationException("Enumeration is modified");
                    }
                    if (this.enumerator != null)
                    {
                        this.enumerator.Reset();
                    }
                }

                public TValue Current
                {
                    get
                    {
                        if (this.enumerator != null)
                        {
                            return this.enumerator.Current;
                        }
                        return default(TValue);
                    }
                }

                object IEnumerator.Current =>
                    this.Current;
            }
        }

        public class MultiDictionaryEntry : ICollection<TValue>, IEnumerable<TValue>, IEnumerable
        {
            private List<TValue> innerList;
            private HashSet<TValue> innerSet;

            public MultiDictionaryEntry()
            {
                this.innerList = new List<TValue>();
                this.innerSet = new HashSet<TValue>();
            }

            public MultiDictionaryEntry(IEnumerable<TValue> collection)
            {
                this.innerList = new List<TValue>(collection);
                this.innerSet = new HashSet<TValue>(collection);
            }

            public void Add(TValue item)
            {
                this.innerSet.Add(item);
                this.innerList.Add(item);
            }

            public void Clear()
            {
                this.innerSet.Clear();
                this.innerList.Clear();
            }

            public bool Contains(TValue item) => 
                this.innerSet.Contains(item);

            public void CopyTo(TValue[] array, int arrayIndex)
            {
                this.innerList.CopyTo(array, arrayIndex);
            }

            public IEnumerator<TValue> GetEnumerator() => 
                (IEnumerator<TValue>) this.innerList.GetEnumerator();

            public bool Remove(TValue item)
            {
                this.innerSet.Remove(item);
                return this.innerList.Remove(item);
            }

            IEnumerator IEnumerable.GetEnumerator() => 
                this.innerList.GetEnumerator();

            public int Count =>
                this.innerList.Count;

            public bool IsReadOnly =>
                this.innerList.IsReadOnly;
        }

        private class ValueCollection : ICollection<TValue>, IEnumerable<TValue>, IEnumerable
        {
            private readonly MultiDictionary<TKey, TValue> multiDictionary;

            public ValueCollection(MultiDictionary<TKey, TValue> multiDictionary)
            {
                this.multiDictionary = multiDictionary;
            }

            public void Add(TValue item)
            {
                throw new NotSupportedException("ReadOnly_Modification");
            }

            public void Clear()
            {
                throw new NotSupportedException("ReadOnly_Modification");
            }

            [Obsolete]
            public bool Contains(TValue item)
            {
                TKey key = default(TKey);
                return this.multiDictionary.ContainsValue(key, item);
            }

            public void CopyTo(TValue[] array, int arrayIndex)
            {
                foreach (TValue local in this)
                {
                    array[arrayIndex++] = local;
                }
            }

            public IEnumerator<TValue> GetEnumerator() => 
                new ValueCollectionEnumerator<TKey, TValue>(this.multiDictionary);

            public bool Remove(TValue item)
            {
                throw new NotSupportedException("ReadOnly_Modification");
            }

            IEnumerator IEnumerable.GetEnumerator() => 
                this.GetEnumerator();

            public int Count =>
                this.multiDictionary.Count;

            public bool IsReadOnly =>
                true;

            private class ValueCollectionEnumerator : IEnumerator<TValue>, IDisposable, IEnumerator
            {
                private readonly IEnumerator<KeyValuePair<TKey, TValue>> enumerator;
                private bool valid;

                internal ValueCollectionEnumerator(MultiDictionary<TKey, TValue> multidictionary)
                {
                    this.enumerator = multidictionary.GetEnumerator();
                    this.valid = false;
                }

                public void Dispose()
                {
                    this.Dispose(true);
                    GC.SuppressFinalize(this);
                }

                private void Dispose(bool disposing)
                {
                    if (disposing && (this.enumerator != null))
                    {
                        this.enumerator.Dispose();
                    }
                }

                ~ValueCollectionEnumerator()
                {
                    this.Dispose(false);
                }

                public bool MoveNext()
                {
                    this.valid = this.enumerator.MoveNext();
                    return this.valid;
                }

                public void Reset()
                {
                    this.enumerator.Reset();
                    this.valid = false;
                }

                public TValue Current
                {
                    get
                    {
                        if (this.valid)
                        {
                            return this.enumerator.Current.Value;
                        }
                        return default(TValue);
                    }
                }

                object IEnumerator.Current =>
                    this.Current;
            }
        }
    }
}

