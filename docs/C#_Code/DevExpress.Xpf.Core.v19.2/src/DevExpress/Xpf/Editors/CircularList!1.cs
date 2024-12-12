namespace DevExpress.Xpf.Editors
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.Diagnostics;
    using System.Reflection;
    using System.Runtime.CompilerServices;

    public class CircularList<T> : ICollection<T>, IEnumerable<T>, IEnumerable, INotifyCollectionChanged
    {
        private List<T> storage;
        private int startIndex;
        private NotifyCollectionChangedEventHandler collectionChanged;

        public event NotifyCollectionChangedEventHandler CollectionChanged
        {
            add
            {
                this.collectionChanged += value;
            }
            remove
            {
                this.collectionChanged -= value;
            }
        }

        public CircularList(int size)
        {
            this.storage = new List<T>();
            this.startIndex = -1;
            this.Size = size;
        }

        public T Add(T item)
        {
            this.GetNextIndex();
            if (this.startIndex < 0)
            {
                this.storage.Add(item);
            }
            else if (this.storage.Count < this.Size)
            {
                this.storage.Insert(this.startIndex, item);
            }
            else
            {
                this.storage[this.startIndex] = item;
            }
            this.OnCollectionChanged();
            return item;
        }

        internal void Assign(CircularList<T> b)
        {
            this.storage.Clear();
            foreach (T local in b.storage)
            {
                this.storage.Add(local);
            }
            this.startIndex = b.startIndex;
            this.Size = b.Size;
            this.OnCollectionChanged();
        }

        public void Clear()
        {
            this.storage.Clear();
            this.startIndex = 0;
            this.OnCollectionChanged();
        }

        public bool Contains(T item) => 
            this.storage.Contains(item);

        public void CopyTo(T[] array, int arrayIndex)
        {
            foreach (T local in this)
            {
                array[arrayIndex++] = local;
            }
        }

        [IteratorStateMachine(typeof(<GetEnumerator>d__23))]
        public IEnumerator<T> GetEnumerator()
        {
            <GetEnumerator>d__23<T> d__1 = new <GetEnumerator>d__23<T>(0);
            d__1.<>4__this = (CircularList<T>) this;
            return d__1;
        }

        private int GetNextIndex()
        {
            if ((this.startIndex < 0) && (this.storage.Count != this.Size))
            {
                return 0;
            }
            this.startIndex = (this.startIndex + 1) % this.Size;
            if (this.startIndex > this.storage.Count)
            {
                this.startIndex = this.storage.Count;
            }
            return this.startIndex;
        }

        private T ItemAt(int index)
        {
            if ((index < 0) || (index >= this.storage.Count))
            {
                throw new IndexOutOfRangeException();
            }
            int num = this.ResolveIndex((this.storage.Count - 1) - index);
            return this.storage[num];
        }

        protected virtual void OnCollectionChanged()
        {
            if (this.collectionChanged != null)
            {
                this.collectionChanged(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
            }
        }

        public bool Remove(T item)
        {
            int index = this.storage.IndexOf(item);
            if (index < 0)
            {
                return false;
            }
            this.storage.Remove(item);
            if (index <= this.startIndex)
            {
                this.startIndex--;
            }
            this.OnCollectionChanged();
            return true;
        }

        private int ResolveIndex(int index) => 
            (this.startIndex >= 0) ? (((this.startIndex + 1) + index) % this.storage.Count) : index;

        internal void SetSize(int size)
        {
            this.Size = size;
            this.startIndex = 0;
            if (this.Count > this.Size)
            {
                this.storage = new List<T>(this.storage.GetRange(0, this.Size));
            }
            this.OnCollectionChanged();
        }

        void ICollection<T>.Add(T item)
        {
            this.Add(item);
        }

        IEnumerator IEnumerable.GetEnumerator() => 
            this.GetEnumerator();

        public int Size { get; private set; }

        public int Count =>
            this.storage.Count;

        public T this[int index] =>
            this.ItemAt(index);

        public bool IsReadOnly =>
            false;

        [CompilerGenerated]
        private sealed class <GetEnumerator>d__23 : IEnumerator<T>, IDisposable, IEnumerator
        {
            private int <>1__state;
            private T <>2__current;
            public CircularList<T> <>4__this;
            private int <i>5__1;

            [DebuggerHidden]
            public <GetEnumerator>d__23(int <>1__state)
            {
                this.<>1__state = <>1__state;
            }

            private bool MoveNext()
            {
                int num = this.<>1__state;
                if (num == 0)
                {
                    this.<>1__state = -1;
                    this.<i>5__1 = this.<>4__this.storage.Count - 1;
                }
                else
                {
                    if (num != 1)
                    {
                        return false;
                    }
                    this.<>1__state = -1;
                    int num2 = this.<i>5__1;
                    this.<i>5__1 = num2 - 1;
                }
                if (this.<i>5__1 < 0)
                {
                    return false;
                }
                this.<>2__current = this.<>4__this.storage[this.<>4__this.ResolveIndex(this.<i>5__1)];
                this.<>1__state = 1;
                return true;
            }

            [DebuggerHidden]
            void IEnumerator.Reset()
            {
                throw new NotSupportedException();
            }

            [DebuggerHidden]
            void IDisposable.Dispose()
            {
            }

            T IEnumerator<T>.Current =>
                this.<>2__current;

            object IEnumerator.Current =>
                this.<>2__current;
        }
    }
}

