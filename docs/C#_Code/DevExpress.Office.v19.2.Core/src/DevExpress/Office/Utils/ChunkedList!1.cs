namespace DevExpress.Office.Utils
{
    using DevExpress.Utils;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Reflection;
    using System.Runtime.CompilerServices;

    public class ChunkedList<T> : IList<T>, ICollection<T>, IEnumerable<T>, IEnumerable, ICollection
    {
        private const int minChunkSize = 0x40;
        private const int defaultChunkSize = 0x1000;
        private readonly List<Chunk<T>> chunks;
        private readonly int normalChunkSize;
        private readonly int doubleChunkSize;
        private readonly int halfChunkSize;
        private int totalItemCount;
        private int mruChunkIndex;

        public ChunkedList() : this(0x1000)
        {
        }

        public ChunkedList(int chunkSize)
        {
            this.chunks = new List<Chunk<T>>();
            if (chunkSize < 0x40)
            {
                chunkSize = 0x40;
            }
            this.normalChunkSize = chunkSize;
            this.doubleChunkSize = this.normalChunkSize * 2;
            this.halfChunkSize = this.normalChunkSize / 2;
            this.Initialize();
        }

        public void Add(T value)
        {
            Chunk<T> item = this.chunks[this.chunks.Count - 1];
            if (item.Count >= this.normalChunkSize)
            {
                Chunk<T> chunk1 = new Chunk<T>();
                chunk1.StartIndex = this.totalItemCount;
                item = chunk1;
                this.chunks.Add(item);
            }
            item.Add(value);
            this.totalItemCount++;
        }

        public void AddRange(IEnumerable<T> items)
        {
            foreach (T local in items)
            {
                this.Add(local);
            }
        }

        private ChunkMergeType<T> CalcMergeType(int count, Chunk<T> prevChunk, Chunk<T> nextChunk)
        {
            if (prevChunk == null)
            {
                if ((nextChunk != null) && ((count + nextChunk.Count) < this.doubleChunkSize))
                {
                    return ChunkMergeType<T>.Next;
                }
            }
            else if (nextChunk == null)
            {
                if ((count + prevChunk.Count) < this.doubleChunkSize)
                {
                    return ChunkMergeType<T>.Prev;
                }
            }
            else if ((count + prevChunk.Count) > (count + nextChunk.Count))
            {
                if ((count + nextChunk.Count) < this.doubleChunkSize)
                {
                    return ChunkMergeType<T>.Next;
                }
            }
            else if ((count + prevChunk.Count) < this.doubleChunkSize)
            {
                return ChunkMergeType<T>.Prev;
            }
            return ChunkMergeType<T>.None;
        }

        public void Clear()
        {
            this.chunks.Clear();
            this.Initialize();
        }

        public bool Contains(T item)
        {
            bool flag;
            using (List<Chunk<T>>.Enumerator enumerator = this.chunks.GetEnumerator())
            {
                while (true)
                {
                    if (enumerator.MoveNext())
                    {
                        Chunk<T> current = enumerator.Current;
                        if (!current.Contains(item))
                        {
                            continue;
                        }
                        flag = true;
                    }
                    else
                    {
                        return false;
                    }
                    break;
                }
            }
            return flag;
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            foreach (Chunk<T> chunk in this.chunks)
            {
                chunk.CopyTo(array, arrayIndex + chunk.StartIndex);
            }
        }

        private Chunk<T> GetChunk(int itemIndex)
        {
            int chunkIndex = this.GetChunkIndex(itemIndex);
            return ((chunkIndex < 0) ? null : this.chunks[chunkIndex]);
        }

        private int GetChunkIndex(int itemIndex)
        {
            if (((this.mruChunkIndex < 0) || (this.mruChunkIndex >= this.chunks.Count)) || !this.chunks[this.mruChunkIndex].ContainsItemIndex(itemIndex))
            {
                this.mruChunkIndex = Algorithms.BinarySearchReverseOrder<Chunk<T>>(this.chunks, new ChunkComparable<T>(itemIndex));
            }
            return this.mruChunkIndex;
        }

        [IteratorStateMachine(typeof(<GetEnumerator>d__38))]
        public IEnumerator<T> GetEnumerator()
        {
            <GetEnumerator>d__38<T> d__1 = new <GetEnumerator>d__38<T>(0);
            d__1.<>4__this = (ChunkedList<T>) this;
            return d__1;
        }

        public int IndexOf(T item)
        {
            int num2;
            using (List<Chunk<T>>.Enumerator enumerator = this.chunks.GetEnumerator())
            {
                while (true)
                {
                    if (enumerator.MoveNext())
                    {
                        Chunk<T> current = enumerator.Current;
                        int index = current.IndexOf(item);
                        if (index < 0)
                        {
                            continue;
                        }
                        num2 = index + current.StartIndex;
                    }
                    else
                    {
                        return -1;
                    }
                    break;
                }
            }
            return num2;
        }

        private void Initialize()
        {
            this.chunks.Add(new Chunk<T>());
            this.totalItemCount = 0;
            this.mruChunkIndex = 0;
        }

        public void Insert(int index, T item)
        {
            if ((index < 0) || (index > this.totalItemCount))
            {
                throw new IndexOutOfRangeException();
            }
            if (index == this.totalItemCount)
            {
                this.Add(item);
            }
            else
            {
                int chunkIndex = this.GetChunkIndex(index);
                Chunk<T> chunk = this.chunks[chunkIndex];
                chunk.Insert(index - chunk.StartIndex, item);
                for (int i = chunkIndex + 1; i < this.chunks.Count; i++)
                {
                    Chunk<T> local1 = this.chunks[i];
                    int startIndex = local1.StartIndex;
                    local1.StartIndex = startIndex + 1;
                }
                this.totalItemCount++;
                if (chunk.Count >= this.doubleChunkSize)
                {
                    this.SplitChunk(chunkIndex);
                }
            }
        }

        public bool Remove(T item)
        {
            int index = this.IndexOf(item);
            if (index < 0)
            {
                return false;
            }
            this.RemoveAt(index);
            return true;
        }

        public void RemoveAt(int index)
        {
            int chunkIndex = this.GetChunkIndex(index);
            if (chunkIndex >= 0)
            {
                Chunk<T> chunk = this.chunks[chunkIndex];
                chunk.RemoveAt(index - chunk.StartIndex);
                bool flag = false;
                if ((chunk.Count == 0) && (this.chunks.Count > 1))
                {
                    this.chunks.RemoveAt(chunkIndex);
                }
                else
                {
                    chunkIndex++;
                    flag = true;
                }
                for (int i = chunkIndex; i < this.chunks.Count; i++)
                {
                    Chunk<T> local1 = this.chunks[i];
                    int startIndex = local1.StartIndex;
                    local1.StartIndex = startIndex - 1;
                }
                this.totalItemCount--;
                if (flag)
                {
                    this.TryMergeChunk(chunkIndex - 1);
                }
            }
        }

        public void Sort()
        {
            throw new NotSupportedException();
        }

        private void SplitChunk(int chunkIndex)
        {
            Chunk<T> chunk = this.chunks[chunkIndex];
            Chunk<T> item = new Chunk<T>(this.normalChunkSize) {
                StartIndex = chunk.StartIndex + this.normalChunkSize
            };
            for (int i = this.normalChunkSize; i < chunk.Count; i++)
            {
                item.Add(chunk[i]);
            }
            chunk.RemoveRange(this.normalChunkSize, chunk.Count - this.normalChunkSize);
            chunk.TrimExcess();
            this.chunks.Insert(chunkIndex + 1, item);
        }

        void ICollection.CopyTo(Array array, int index)
        {
            foreach (Chunk<T> chunk in this.chunks)
            {
                Array.Copy(chunk.ToArray(), 0, array, index + chunk.StartIndex, chunk.Count);
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => 
            this.GetEnumerator();

        private void TryMergeChunk(int chunkIndex)
        {
            Chunk<T> collection = this.chunks[chunkIndex];
            int count = collection.Count;
            if (count <= this.halfChunkSize)
            {
                Chunk<T> prevChunk = (chunkIndex == 0) ? null : this.chunks[chunkIndex - 1];
                Chunk<T> nextChunk = (chunkIndex == (this.chunks.Count - 1)) ? null : this.chunks[chunkIndex + 1];
                ChunkMergeType<T> type = this.CalcMergeType(count, prevChunk, nextChunk);
                if (type == ChunkMergeType<T>.Next)
                {
                    collection.AddRange(nextChunk);
                    this.chunks.RemoveAt(chunkIndex + 1);
                }
                else if (type == ChunkMergeType<T>.Prev)
                {
                    prevChunk.AddRange(collection);
                    this.chunks.RemoveAt(chunkIndex);
                    if (this.mruChunkIndex == chunkIndex)
                    {
                        this.mruChunkIndex--;
                    }
                }
            }
        }

        public int Count =>
            this.totalItemCount;

        public bool IsReadOnly =>
            false;

        public T this[int index]
        {
            get
            {
                if ((index >= 0) && (index < this.totalItemCount))
                {
                    Chunk<T> chunk = this.GetChunk(index);
                    if (chunk != null)
                    {
                        return chunk[index - chunk.StartIndex];
                    }
                }
                throw new IndexOutOfRangeException();
            }
            set
            {
                if ((index >= 0) && (index < this.totalItemCount))
                {
                    Chunk<T> chunk = this.GetChunk(index);
                    if (chunk != null)
                    {
                        chunk[index - chunk.StartIndex] = value;
                        return;
                    }
                }
                throw new IndexOutOfRangeException();
            }
        }

        protected internal int ChunkCount =>
            this.chunks.Count;

        int ICollection.Count =>
            this.totalItemCount;

        bool ICollection.IsSynchronized =>
            ((ICollection) this.chunks).IsSynchronized;

        object ICollection.SyncRoot =>
            ((ICollection) this.chunks).SyncRoot;

        [CompilerGenerated]
        private sealed class <GetEnumerator>d__38 : IEnumerator<T>, IDisposable, IEnumerator
        {
            private int <>1__state;
            private T <>2__current;
            public ChunkedList<T> <>4__this;
            private List<ChunkedList<T>.Chunk>.Enumerator <>7__wrap1;
            private List<T>.Enumerator <>7__wrap2;

            [DebuggerHidden]
            public <GetEnumerator>d__38(int <>1__state)
            {
                this.<>1__state = <>1__state;
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
                        this.<>7__wrap2 = this.<>7__wrap1.Current.GetEnumerator();
                        this.<>1__state = -4;
                    }
                    else
                    {
                        this.<>m__Finally1();
                        this.<>7__wrap1 = new List<ChunkedList<T>.Chunk>.Enumerator();
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

        private class Chunk : List<T>
        {
            public Chunk()
            {
                this.StartIndex = 0;
            }

            public Chunk(int capacity) : base(capacity)
            {
                this.StartIndex = 0;
            }

            public bool ContainsItemIndex(int itemIndex) => 
                (itemIndex >= this.StartIndex) && (itemIndex < (this.StartIndex + base.Count));

            public int StartIndex { get; set; }
        }

        private class ChunkComparable : IComparable<ChunkedList<T>.Chunk>
        {
            private int index;

            public ChunkComparable(int index)
            {
                this.index = index;
            }

            public int CompareTo(ChunkedList<T>.Chunk other) => 
                (this.index >= other.StartIndex) ? ((this.index < (other.StartIndex + other.Count)) ? 0 : -1) : 1;
        }

        private enum ChunkMergeType
        {
            public const ChunkedList<T>.ChunkMergeType None = ChunkedList<T>.ChunkMergeType.None;,
            public const ChunkedList<T>.ChunkMergeType Prev = ChunkedList<T>.ChunkMergeType.Prev;,
            public const ChunkedList<T>.ChunkMergeType Next = ChunkedList<T>.ChunkMergeType.Next;
        }
    }
}

