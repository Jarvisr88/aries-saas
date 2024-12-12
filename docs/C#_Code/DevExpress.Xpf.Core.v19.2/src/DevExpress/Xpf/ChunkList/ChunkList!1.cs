namespace DevExpress.Xpf.ChunkList
{
    using DevExpress.Xpf.ChunkList.Native;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Threading;

    public class ChunkList<T> : IList<T>, ICollection<T>, IEnumerable<T>, IEnumerable, IBindingList, IList, ICollection, IListChanging
    {
        private readonly int chunkSize;
        private int count;
        private List<Chunk<T>> chunks;
        [CompilerGenerated]
        private ListChangingEventHandler ListChanging;
        [CompilerGenerated]
        private ListChangedEventHandler ListChanged;
        private Dictionary<T, Chunk<T>> chunksCache;
        internal const string SlowIndexOfException = "Attempting to perform a slow 'index of' operation. Either implement the IChunkListObject interface for your data objects or enable the UseChunksCache option.";
        private int lastItemIndex;
        private PropertyDescriptorCollection itemProperties;
        private object syncRoot;

        public event ListChangedEventHandler ListChanged
        {
            [CompilerGenerated] add
            {
                ListChangedEventHandler listChanged = this.ListChanged;
                while (true)
                {
                    ListChangedEventHandler comparand = listChanged;
                    ListChangedEventHandler handler3 = comparand + value;
                    listChanged = Interlocked.CompareExchange<ListChangedEventHandler>(ref this.ListChanged, handler3, comparand);
                    if (ReferenceEquals(listChanged, comparand))
                    {
                        return;
                    }
                }
            }
            [CompilerGenerated] remove
            {
                ListChangedEventHandler listChanged = this.ListChanged;
                while (true)
                {
                    ListChangedEventHandler comparand = listChanged;
                    ListChangedEventHandler handler3 = comparand - value;
                    listChanged = Interlocked.CompareExchange<ListChangedEventHandler>(ref this.ListChanged, handler3, comparand);
                    if (ReferenceEquals(listChanged, comparand))
                    {
                        return;
                    }
                }
            }
        }

        public event ListChangingEventHandler ListChanging
        {
            [CompilerGenerated] add
            {
                ListChangingEventHandler listChanging = this.ListChanging;
                while (true)
                {
                    ListChangingEventHandler comparand = listChanging;
                    ListChangingEventHandler handler3 = comparand + value;
                    listChanging = Interlocked.CompareExchange<ListChangingEventHandler>(ref this.ListChanging, handler3, comparand);
                    if (ReferenceEquals(listChanging, comparand))
                    {
                        return;
                    }
                }
            }
            [CompilerGenerated] remove
            {
                ListChangingEventHandler listChanging = this.ListChanging;
                while (true)
                {
                    ListChangingEventHandler comparand = listChanging;
                    ListChangingEventHandler handler3 = comparand - value;
                    listChanging = Interlocked.CompareExchange<ListChangingEventHandler>(ref this.ListChanging, handler3, comparand);
                    if (ReferenceEquals(listChanging, comparand))
                    {
                        return;
                    }
                }
            }
        }

        public ChunkList(int capacity, bool useChunksCache = false) : this((int) Math.Ceiling(Math.Sqrt((double) capacity)), true, useChunksCache)
        {
        }

        public ChunkList(int chunkSize, bool supportPropertyChanged, bool useChunksCache = false)
        {
            this.chunks = new List<Chunk<T>>();
            this.lastItemIndex = -1;
            this.syncRoot = new object();
            this.chunkSize = chunkSize;
            this.SupportPropertyChanged = supportPropertyChanged;
            this.UseChunksCache = useChunksCache;
        }

        public void Add(T item)
        {
            if ((this.chunks.Count == 0) || (this.chunks[this.chunks.Count - 1].Items.Count == this.chunkSize))
            {
                Chunk<T> chunk1 = new Chunk<T>((DevExpress.Xpf.ChunkList.ChunkList<T>) this, this.chunkSize);
                chunk1.PrevCount = this.count;
                this.chunks.Add(chunk1);
            }
            this.chunks[this.chunks.Count - 1].AddItem(item);
            this.count++;
            this.RaiseListChanged(new ListChangedEventArgs(ListChangedType.ItemAdded, this.count - 1));
        }

        public void Clear()
        {
            foreach (Chunk<T> chunk in this.chunks)
            {
                foreach (T local in chunk.Items)
                {
                    chunk.OnItemRemoved(local);
                }
                chunk.Items.Clear();
            }
            this.chunks.Clear();
            this.count = 0;
            this.RaiseListChanged(new ListChangedEventArgs(ListChangedType.Reset, -1));
        }

        private void CombineChunks(int chunkIndex)
        {
            Chunk<T> chunk = this.chunks[chunkIndex];
            Chunk<T> chunk2 = this.chunks[chunkIndex + 1];
            int num = 0;
            while (num < chunk2.Items.Count)
            {
                T item = chunk2.Items[0];
                chunk2.RemoveItem(item);
                chunk.AddItem(item);
            }
            this.chunks.RemoveAt(chunkIndex + 1);
        }

        public bool Contains(T item) => 
            this.IndexOf(item) != -1;

        public void CopyTo(T[] array, int arrayIndex)
        {
            ((ICollection) this).CopyTo(array, arrayIndex);
        }

        private int FindChunk(int index)
        {
            int num = 0;
            int num2 = this.chunks.Count - 1;
            while (num < num2)
            {
                int num3 = num + ((num2 - num) / 2);
                if (index < this.chunks[num3 + 1].PrevCount)
                {
                    num2 = num3;
                    continue;
                }
                num = num3 + 1;
            }
            return num2;
        }

        [IteratorStateMachine(typeof(<GetEnumerable>d__51))]
        private IEnumerable<T> GetEnumerable()
        {
            <GetEnumerable>d__51<T> d__1 = new <GetEnumerable>d__51<T>(-2);
            d__1.<>4__this = (DevExpress.Xpf.ChunkList.ChunkList<T>) this;
            return d__1;
        }

        public IEnumerator<T> GetEnumerator() => 
            this.GetEnumerable().GetEnumerator();

        private PropertyDescriptor GetPropertyDescriptor(object item, string propertyName)
        {
            this.itemProperties ??= TypeDescriptor.GetProperties(item);
            return this.itemProperties.Find(propertyName, true);
        }

        public int IndexOf(T item)
        {
            Chunk<T> chunkObject;
            IChunkListObject obj2 = item as IChunkListObject;
            if (obj2 != null)
            {
                chunkObject = (Chunk<T>) obj2.ChunkObject;
            }
            else
            {
                if (!this.UseChunksCache)
                {
                    throw new InvalidOperationException("Attempting to perform a slow 'index of' operation. Either implement the IChunkListObject interface for your data objects or enable the UseChunksCache option.");
                }
                chunkObject = this.ChunksCache[item];
            }
            return ((chunkObject != null) ? chunkObject.IndexOf(item) : -1);
        }

        private int IndexOf(Chunk<T> chunk, T item)
        {
            if (((this.lastItemIndex < 0) || (this.lastItemIndex >= this.count)) || !this[this.lastItemIndex].Equals(item))
            {
                this.lastItemIndex = chunk.IndexOf(item);
            }
            return this.lastItemIndex;
        }

        public void Insert(int index, T item)
        {
            if ((index == 0) && (this.chunks.Count == 0))
            {
                this.Add(item);
            }
            else
            {
                int startIndex = this.FindChunk(index);
                Chunk<T> chunk = this.chunks[startIndex];
                chunk.Items.Insert(index - chunk.PrevCount, item);
                chunk.OnItemAdded(item);
                this.count++;
                if (chunk.Items.Count > (this.chunkSize * 1.5))
                {
                    Chunk<T> chunk2 = new Chunk<T>((DevExpress.Xpf.ChunkList.ChunkList<T>) this, this.chunkSize);
                    this.chunks.Insert(this.chunks.IndexOf(chunk) + 1, chunk2);
                    int num2 = chunk.Items.Count / 2;
                    while (num2 < chunk.Items.Count)
                    {
                        T local = chunk.Items[num2];
                        chunk.RemoveItem(local);
                        chunk2.AddItem(local);
                    }
                }
                this.UpdatePrevCount(startIndex);
                this.RaiseListChanged(new ListChangedEventArgs(ListChangedType.ItemAdded, index));
            }
        }

        internal void OnItemPropertyChanged(Chunk<T> chunk, T item, string propertyName)
        {
            this.RaiseListChanged(new ListChangedEventArgs(ListChangedType.ItemChanged, this.IndexOf(chunk, item), this.GetPropertyDescriptor(item, propertyName)));
        }

        internal void OnItemPropertyChanging(Chunk<T> chunk, T item, string propertyName)
        {
            this.RaiseListChanging(new ListChangingEventArgs(this.IndexOf(chunk, item), this.GetPropertyDescriptor(item, propertyName)));
        }

        private void RaiseListChanged(ListChangedEventArgs args)
        {
            if (this.ListChanged != null)
            {
                this.ListChanged(this, args);
            }
        }

        private void RaiseListChanging(ListChangingEventArgs args)
        {
            if (this.ListChanging != null)
            {
                this.ListChanging(this, args);
            }
        }

        public bool Remove(T item)
        {
            this.RemoveAt(this.IndexOf(item));
            return true;
        }

        public void RemoveAt(int index)
        {
            int chunkIndex = this.FindChunk(index);
            Chunk<T> chunk = this.chunks[chunkIndex];
            int num2 = index - chunk.PrevCount;
            T item = chunk.Items[num2];
            chunk.OnItemRemoved(item);
            chunk.Items.RemoveAt(num2);
            this.count--;
            if (chunk.Items.Count < this.chunkSize)
            {
                if ((chunkIndex > 0) && ((this.chunks[chunkIndex - 1].Items.Count + chunk.Items.Count) <= this.chunkSize))
                {
                    this.CombineChunks(chunkIndex - 1);
                }
                if ((chunkIndex < (this.chunks.Count - 1)) && ((this.chunks[chunkIndex + 1].Items.Count + chunk.Items.Count) <= this.chunkSize))
                {
                    this.CombineChunks(chunkIndex);
                }
            }
            this.UpdatePrevCount((chunkIndex > 0) ? (chunkIndex - 1) : 0);
            this.RaiseListChanged(new ChunkListChangedEventArgs(ListChangedType.ItemDeleted, index, item));
        }

        void ICollection.CopyTo(Array array, int index)
        {
            for (int i = 0; i < this.count; i++)
            {
                array.SetValue(this[i], (int) (i + index));
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => 
            this.GetEnumerable().GetEnumerator();

        int IList.Add(object value)
        {
            this.Add((T) value);
            return (this.count - 1);
        }

        bool IList.Contains(object value) => 
            this.Contains((T) value);

        int IList.IndexOf(object value) => 
            this.IndexOf((T) value);

        void IList.Insert(int index, object value)
        {
            this.Insert(index, (T) value);
        }

        void IList.Remove(object value)
        {
            this.Remove((T) value);
        }

        void IBindingList.AddIndex(PropertyDescriptor property)
        {
            throw new NotSupportedException();
        }

        object IBindingList.AddNew()
        {
            throw new NotSupportedException();
        }

        void IBindingList.ApplySort(PropertyDescriptor property, ListSortDirection direction)
        {
            throw new NotSupportedException();
        }

        int IBindingList.Find(PropertyDescriptor property, object key)
        {
            throw new NotSupportedException();
        }

        void IBindingList.RemoveIndex(PropertyDescriptor property)
        {
            throw new NotSupportedException();
        }

        void IBindingList.RemoveSort()
        {
            throw new NotSupportedException();
        }

        private void UpdatePrevCount(int startIndex)
        {
            List<Chunk<T>> chunks = this.chunks;
            Chunk<T> chunk = chunks[startIndex];
            int num = chunk.PrevCount + chunk.Items.Count;
            for (int i = startIndex + 1; i < chunks.Count; i++)
            {
                Chunk<T> chunk2 = chunks[i];
                chunk2.PrevCount = num;
                num += chunk2.Items.Count;
            }
        }

        public bool SupportPropertyChanged { get; private set; }

        public bool UseChunksCache { get; private set; }

        internal Dictionary<T, Chunk<T>> ChunksCache
        {
            get
            {
                this.chunksCache ??= new Dictionary<T, Chunk<T>>();
                return this.chunksCache;
            }
        }

        public T this[int index]
        {
            get
            {
                Chunk<T> chunk = this.chunks[this.FindChunk(index)];
                return chunk.Items[index - chunk.PrevCount];
            }
            set
            {
                Chunk<T> chunk = this.chunks[this.FindChunk(index)];
                int num = index - chunk.PrevCount;
                T item = chunk.Items[num];
                chunk.OnItemRemoved(item);
                chunk.Items[num] = value;
                chunk.OnItemAdded(value);
                this.RaiseListChanged(new ChunkListChangedEventArgs(ListChangedType.ItemChanged, index, item));
            }
        }

        public int Count =>
            this.count;

        bool IList.IsReadOnly =>
            false;

        bool ICollection<T>.IsReadOnly =>
            false;

        object IList.this[int index]
        {
            get => 
                this[index];
            set => 
                this[index] = (T) value;
        }

        bool IBindingList.AllowEdit =>
            true;

        bool IBindingList.AllowNew =>
            false;

        bool IBindingList.AllowRemove =>
            true;

        bool IBindingList.IsSorted =>
            false;

        ListSortDirection IBindingList.SortDirection
        {
            get
            {
                throw new NotSupportedException();
            }
        }

        PropertyDescriptor IBindingList.SortProperty
        {
            get
            {
                throw new NotSupportedException();
            }
        }

        bool IBindingList.SupportsChangeNotification =>
            true;

        bool IBindingList.SupportsSearching =>
            false;

        bool IBindingList.SupportsSorting =>
            false;

        bool IList.IsFixedSize =>
            false;

        bool ICollection.IsSynchronized =>
            false;

        object ICollection.SyncRoot =>
            this.syncRoot;

        [CompilerGenerated]
        private sealed class <GetEnumerable>d__51 : IEnumerable<T>, IEnumerable, IEnumerator<T>, IDisposable, IEnumerator
        {
            private int <>1__state;
            private T <>2__current;
            private int <>l__initialThreadId;
            public DevExpress.Xpf.ChunkList.ChunkList<T> <>4__this;
            private List<Chunk<T>>.Enumerator <>7__wrap1;
            private List<T>.Enumerator <>7__wrap2;

            [DebuggerHidden]
            public <GetEnumerable>d__51(int <>1__state)
            {
                this.<>1__state = <>1__state;
                this.<>l__initialThreadId = Environment.CurrentManagedThreadId;
            }

            private void <>m__Finally1()
            {
                this.<>1__state = -1;
                this.<>7__wrap1.Dispose();
            }

            private void <>m__Finally2()
            {
                this.<>1__state = -3;
                this.<>7__wrap2.Dispose();
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
                        this.<>7__wrap1 = this.<>4__this.chunks.GetEnumerator();
                        this.<>1__state = -3;
                        goto TR_0005;
                    }
                    else if (num == 1)
                    {
                        this.<>1__state = -4;
                    }
                    else
                    {
                        return false;
                    }
                    goto TR_0009;
                TR_0005:
                    if (this.<>7__wrap1.MoveNext())
                    {
                        Chunk<T> current = this.<>7__wrap1.Current;
                        this.<>7__wrap2 = current.Items.GetEnumerator();
                        this.<>1__state = -4;
                    }
                    else
                    {
                        this.<>m__Finally1();
                        this.<>7__wrap1 = new List<Chunk<T>>.Enumerator();
                        return false;
                    }
                TR_0009:
                    while (true)
                    {
                        if (this.<>7__wrap2.MoveNext())
                        {
                            T current = this.<>7__wrap2.Current;
                            this.<>2__current = current;
                            this.<>1__state = 1;
                            flag = true;
                        }
                        else
                        {
                            this.<>m__Finally2();
                            this.<>7__wrap2 = new List<T>.Enumerator();
                            goto TR_0005;
                        }
                        break;
                    }
                }
                fault
                {
                    this.System.IDisposable.Dispose();
                }
                return flag;
            }

            [DebuggerHidden]
            IEnumerator<T> IEnumerable<T>.GetEnumerator()
            {
                DevExpress.Xpf.ChunkList.ChunkList<T>.<GetEnumerable>d__51 d__;
                if ((this.<>1__state == -2) && (this.<>l__initialThreadId == Environment.CurrentManagedThreadId))
                {
                    this.<>1__state = 0;
                    d__ = (DevExpress.Xpf.ChunkList.ChunkList<T>.<GetEnumerable>d__51) this;
                }
                else
                {
                    d__ = new DevExpress.Xpf.ChunkList.ChunkList<T>.<GetEnumerable>d__51(0) {
                        <>4__this = this.<>4__this
                    };
                }
                return d__;
            }

            [DebuggerHidden]
            IEnumerator IEnumerable.GetEnumerator() => 
                this.System.Collections.Generic.IEnumerable<T>.GetEnumerator();

            [DebuggerHidden]
            void IEnumerator.Reset()
            {
                throw new NotSupportedException();
            }

            [DebuggerHidden]
            void IDisposable.Dispose()
            {
                int num = this.<>1__state;
                if (((num == -4) || (num == -3)) || (num == 1))
                {
                    try
                    {
                        if ((num == -4) || (num == 1))
                        {
                            try
                            {
                            }
                            finally
                            {
                                this.<>m__Finally2();
                            }
                        }
                    }
                    finally
                    {
                        this.<>m__Finally1();
                    }
                }
            }

            T IEnumerator<T>.Current =>
                this.<>2__current;

            object IEnumerator.Current =>
                this.<>2__current;
        }
    }
}

