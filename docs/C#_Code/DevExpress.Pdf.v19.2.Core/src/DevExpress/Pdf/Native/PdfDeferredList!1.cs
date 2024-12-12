namespace DevExpress.Pdf.Native
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Reflection;
    using System.Runtime.CompilerServices;

    public abstract class PdfDeferredList<T> : IList<T>, ICollection<T>, IEnumerable<T>, IEnumerable where T: PdfObject
    {
        private readonly List<T> values;
        private IEnumerator<object> source;
        private int sourceCount;

        protected PdfDeferredList()
        {
            this.values = new List<T>();
            this.source = new List<object>().GetEnumerator();
        }

        protected PdfDeferredList(IEnumerator<object> source, int sourceCount)
        {
            this.values = new List<T>();
            this.sourceCount = sourceCount;
            this.source = source;
        }

        public void Add(T item)
        {
            this.ResolveAll();
            this.values.Add(item);
        }

        public void AddRange(IEnumerable<T> items)
        {
            this.ResolveAll();
            this.values.AddRange(items);
        }

        public void Clear()
        {
            this.values.Clear();
            if (this.source != null)
            {
                this.source.Dispose();
                this.source = null;
            }
        }

        public bool Contains(T item)
        {
            this.ResolveAll();
            return this.values.Contains(item);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            this.ResolveAll();
            this.values.CopyTo(array, arrayIndex);
        }

        private void EnsureUptoIndex(int index)
        {
            IEnumerator<T> enumerator = this.GetEnumerator();
            while (true)
            {
                try
                {
                    while (true)
                    {
                        if ((this.values.Count <= index) && enumerator.MoveNext())
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

        [IteratorStateMachine(typeof(<GetEnumerator>d__21))]
        public IEnumerator<T> GetEnumerator()
        {
            int count = this.Count;
            int <i>5__1 = 0;
            while (true)
            {
                while (true)
                {
                    if ((<i>5__1 >= count) && (this.source != null))
                    {
                        this.source.Dispose();
                        this.source = null;
                    }
                    if (<i>5__1 < this.values.Count)
                    {
                        yield return this.values[<i>5__1];
                    }
                    else
                    {
                        if ((this.source == null) || !this.source.MoveNext())
                        {
                            break;
                        }
                        object current = this.source.Current;
                        T item = this.ParseObject(current);
                        this.sourceCount--;
                        this.values.Add(item);
                        yield return item;
                    }
                    break;
                }
                int num2 = <i>5__1;
                <i>5__1 = num2 + 1;
            }
        }

        public int IndexOf(T item)
        {
            this.ResolveAll();
            return this.values.IndexOf(item);
        }

        public void Insert(int index, T item)
        {
            if (index >= this.values.Count)
            {
                this.ResolveAll();
            }
            this.values.Insert(index, item);
        }

        protected abstract T ParseObject(object value);
        public bool Remove(T item)
        {
            this.ResolveAll();
            return this.values.Remove(item);
        }

        public void RemoveAt(int index)
        {
            this.EnsureUptoIndex(index);
            this.values.RemoveAt(index);
        }

        private void ResolveAll()
        {
            this.EnsureUptoIndex(0x7fffffff);
        }

        IEnumerator IEnumerable.GetEnumerator() => 
            this.GetEnumerator();

        public T this[int index]
        {
            get
            {
                this.EnsureUptoIndex(index);
                return this.values[index];
            }
            set
            {
                this.EnsureUptoIndex(index);
                this.values[index] = value;
            }
        }

        public int Count =>
            this.sourceCount + this.values.Count;

        public bool IsReadOnly =>
            false;

        [CompilerGenerated]
        private sealed class <GetEnumerator>d__21 : IEnumerator<T>, IDisposable, IEnumerator
        {
            private int <>1__state;
            private T <>2__current;
            public PdfDeferredList<T> <>4__this;
            private int <i>5__1;
            private int <cnt>5__2;

            [DebuggerHidden]
            public <GetEnumerator>d__21(int <>1__state)
            {
                this.<>1__state = <>1__state;
            }

            private bool MoveNext()
            {
                int num2;
                switch (this.<>1__state)
                {
                    case 0:
                        this.<>1__state = -1;
                        this.<cnt>5__2 = this.<>4__this.Count;
                        this.<i>5__1 = 0;
                        break;

                    case 1:
                        this.<>1__state = -1;
                        goto TR_0006;

                    case 2:
                        this.<>1__state = -1;
                        goto TR_0006;

                    default:
                        return false;
                }
                goto TR_000A;
            TR_0006:
                num2 = this.<i>5__1;
                this.<i>5__1 = num2 + 1;
            TR_000A:
                while (true)
                {
                    if (this.<i>5__1 >= this.<cnt>5__2)
                    {
                        if (this.<>4__this.source != null)
                        {
                            this.<>4__this.source.Dispose();
                            this.<>4__this.source = null;
                        }
                        return false;
                    }
                    if (this.<i>5__1 < this.<>4__this.values.Count)
                    {
                        this.<>2__current = this.<>4__this.values[this.<i>5__1];
                        this.<>1__state = 1;
                        return true;
                    }
                    if ((this.<>4__this.source == null) || !this.<>4__this.source.MoveNext())
                    {
                        break;
                    }
                    object current = this.<>4__this.source.Current;
                    T item = this.<>4__this.ParseObject(current);
                    this.<>4__this.sourceCount--;
                    this.<>4__this.values.Add(item);
                    this.<>2__current = item;
                    this.<>1__state = 2;
                    return true;
                }
                goto TR_0006;
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

