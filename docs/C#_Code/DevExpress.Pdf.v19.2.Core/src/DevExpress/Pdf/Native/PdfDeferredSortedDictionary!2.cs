namespace DevExpress.Pdf.Native
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    public class PdfDeferredSortedDictionary<K, T> : IDictionary<K, T>, ICollection<KeyValuePair<K, T>>, IEnumerable<KeyValuePair<K, T>>, IEnumerable, IEnumerable<KeyValuePair<K, PdfDeferredItem<T>>> where T: class
    {
        private static IComparer<string> stringComparer;
        private readonly SortedDictionary<K, PdfDeferredItem<T>> dictionary;

        static PdfDeferredSortedDictionary()
        {
            PdfDeferredSortedDictionary<K, T>.stringComparer = StringComparer.Ordinal;
        }

        public PdfDeferredSortedDictionary()
        {
            this.dictionary = new SortedDictionary<K, PdfDeferredItem<T>>(PdfDeferredSortedDictionary<K, T>.stringComparer as IComparer<K>);
        }

        public void Add(KeyValuePair<K, T> item)
        {
            this.AddToDictionary(item.Key, new PdfDeferredItem<T>(item.Value));
        }

        public void Add(K key, T value)
        {
            this.AddToDictionary(key, new PdfDeferredItem<T>(value));
        }

        public void AddDeferred(K key, object value, Func<object, T> create)
        {
            this.AddToDictionary(key, new PdfDeferredItem<T>(value, create));
        }

        public void AddRange(PdfDeferredSortedDictionary<K, T> value)
        {
            if (value != null)
            {
                foreach (KeyValuePair<K, PdfDeferredItem<T>> pair in value.dictionary)
                {
                    this.AddToDictionary(pair.Key, pair.Value);
                }
            }
        }

        private void AddToDictionary(K key, PdfDeferredItem<T> value)
        {
            if (!this.dictionary.ContainsKey(key))
            {
                this.dictionary.Add(key, value);
            }
        }

        public void Clear()
        {
            this.dictionary.Clear();
        }

        public bool Contains(KeyValuePair<K, T> item)
        {
            PdfDeferredItem<T> item2;
            return (this.dictionary.TryGetValue(item.Key, out item2) && item.Value.Equals(item2.Item));
        }

        public bool ContainsKey(K key) => 
            this.dictionary.ContainsKey(key);

        public void CopyTo(KeyValuePair<K, T>[] array, int arrayIndex)
        {
            int num = arrayIndex;
            foreach (KeyValuePair<K, T> pair in this)
            {
                array[num++] = pair;
            }
        }

        [IteratorStateMachine(typeof(<GetEnumerator>d__25))]
        public IEnumerator<KeyValuePair<K, T>> GetEnumerator()
        {
            <GetEnumerator>d__25<K, T> d__1 = new <GetEnumerator>d__25<K, T>(0);
            d__1.<>4__this = (PdfDeferredSortedDictionary<K, T>) this;
            return d__1;
        }

        public bool Remove(K key) => 
            this.dictionary.Remove(key);

        public bool Remove(KeyValuePair<K, T> item) => 
            this.Contains(item) && this.dictionary.Remove(item.Key);

        internal void ResolveAll()
        {
            IEnumerator<KeyValuePair<K, T>> enumerator = this.GetEnumerator();
            while (true)
            {
                try
                {
                    while (true)
                    {
                        if (enumerator.MoveNext())
                        {
                            break;
                        }
                        return;
                    }
                }
                finally
                {
                    if (enumerator != null)
                    {
                        enumerator.Dispose();
                    }
                }
            }
        }

        IEnumerator<KeyValuePair<K, PdfDeferredItem<T>>> IEnumerable<KeyValuePair<K, PdfDeferredItem<T>>>.GetEnumerator() => 
            this.dictionary.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => 
            this.GetEnumerator();

        public bool TryGetValue(K key, out T value)
        {
            PdfDeferredItem<T> item;
            if (this.dictionary.TryGetValue(key, out item))
            {
                value = item.Item;
                return true;
            }
            value = default(T);
            return false;
        }

        public ICollection<K> Keys =>
            this.dictionary.Keys;

        public ICollection<T> Values
        {
            get
            {
                ICollection<T> is2 = new List<T>();
                foreach (PdfDeferredItem<T> item in this.dictionary.Values)
                {
                    is2.Add(item.Item);
                }
                return is2;
            }
        }

        public T this[K key]
        {
            get => 
                this.dictionary[key].Item;
            set => 
                this.dictionary[key] = new PdfDeferredItem<T>(value);
        }

        public int Count =>
            this.dictionary.Count;

        public bool IsReadOnly =>
            false;

        [CompilerGenerated]
        private sealed class <GetEnumerator>d__25 : IEnumerator<KeyValuePair<K, T>>, IDisposable, IEnumerator
        {
            private int <>1__state;
            private KeyValuePair<K, T> <>2__current;
            public PdfDeferredSortedDictionary<K, T> <>4__this;
            private SortedDictionary<K, PdfDeferredItem<T>>.Enumerator <>7__wrap1;

            [DebuggerHidden]
            public <GetEnumerator>d__25(int <>1__state)
            {
                this.<>1__state = <>1__state;
            }

            private void <>m__Finally1()
            {
                this.<>1__state = -1;
                this.<>7__wrap1.Dispose();
            }

            private bool MoveNext()
            {
                bool flag;
                try
                {
                    int num = this.<>1__state;
                    if (num == 0)
                    {
                        this.<>1__state = -1;
                        this.<>7__wrap1 = this.<>4__this.dictionary.GetEnumerator();
                        this.<>1__state = -3;
                    }
                    else if (num == 1)
                    {
                        this.<>1__state = -3;
                    }
                    else
                    {
                        return false;
                    }
                    if (!this.<>7__wrap1.MoveNext())
                    {
                        this.<>m__Finally1();
                        this.<>7__wrap1 = new SortedDictionary<K, PdfDeferredItem<T>>.Enumerator();
                        flag = false;
                    }
                    else
                    {
                        KeyValuePair<K, PdfDeferredItem<T>> current = this.<>7__wrap1.Current;
                        this.<>2__current = new KeyValuePair<K, T>(current.Key, current.Value.Item);
                        this.<>1__state = 1;
                        flag = true;
                    }
                }
                fault
                {
                    this.System.IDisposable.Dispose();
                }
                return flag;
            }

            [DebuggerHidden]
            void IEnumerator.Reset()
            {
                throw new NotSupportedException();
            }

            [DebuggerHidden]
            void IDisposable.Dispose()
            {
                int num = this.<>1__state;
                if ((num == -3) || (num == 1))
                {
                    try
                    {
                    }
                    finally
                    {
                        this.<>m__Finally1();
                    }
                }
            }

            KeyValuePair<K, T> IEnumerator<KeyValuePair<K, T>>.Current =>
                this.<>2__current;

            object IEnumerator.Current =>
                this.<>2__current;
        }
    }
}

