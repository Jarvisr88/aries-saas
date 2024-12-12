namespace DevExpress.Utils
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Reflection;
    using System.Runtime.CompilerServices;

    public class ChunkedArray<T> : IEnumerable<T>, IEnumerable
    {
        private readonly int chunkSize;
        private readonly List<List<T>> chunks;
        private int totalCount;

        public ChunkedArray() : this(0x2000, 0)
        {
        }

        public ChunkedArray(int chunkSize) : this(chunkSize, 0)
        {
        }

        public ChunkedArray(int chunkSize, int capacity)
        {
            Guard.ArgumentPositive(chunkSize, "chunkSize");
            this.chunkSize = chunkSize;
            if (capacity <= 0)
            {
                this.chunks = new List<List<T>>();
            }
            else
            {
                int num = capacity / chunkSize;
                if ((capacity % chunkSize) > 0)
                {
                    num++;
                }
                this.chunks = new List<List<T>>(num);
            }
            this.AddChunk();
        }

        public void Add(T item)
        {
            if (this.LastChunk.Count == this.chunkSize)
            {
                this.AddChunk();
            }
            this.LastChunk.Add(item);
            this.totalCount++;
        }

        private void AddChunk()
        {
            this.chunks.Add(new List<T>(this.chunkSize));
        }

        public void AddRange(IEnumerable<T> items)
        {
            foreach (T local in items)
            {
                this.Add(local);
            }
        }

        public void Clear()
        {
            this.totalCount = 0;
            this.chunks.Clear();
            this.AddChunk();
        }

        [IteratorStateMachine(typeof(<GetEnumerator>d__16))]
        public IEnumerator<T> GetEnumerator()
        {
            <GetEnumerator>d__16<T> d__1 = new <GetEnumerator>d__16<T>(0);
            d__1.<>4__this = (ChunkedArray<T>) this;
            return d__1;
        }

        IEnumerator IEnumerable.GetEnumerator() => 
            this.GetEnumerator();

        private List<T> LastChunk =>
            this.chunks[this.chunks.Count - 1];

        public int Count =>
            this.totalCount;

        public T this[int index]
        {
            get => 
                this.chunks[index / this.chunkSize][index % this.chunkSize];
            set => 
                this.chunks[index / this.chunkSize][index % this.chunkSize] = value;
        }

        [CompilerGenerated]
        private sealed class <GetEnumerator>d__16 : IEnumerator<T>, IDisposable, IEnumerator
        {
            private int <>1__state;
            private T <>2__current;
            public ChunkedArray<T> <>4__this;
            private List<List<T>>.Enumerator <>7__wrap1;
            private List<T>.Enumerator <>7__wrap2;

            [DebuggerHidden]
            public <GetEnumerator>d__16(int <>1__state)
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
                        this.<>7__wrap1 = new List<List<T>>.Enumerator();
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
    }
}

