namespace DevExpress.Xpf.Editors.Helpers
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;

    public class LocalVisibleListWrapper<T> : LocalVisibleListWrapper, IList<T>, ICollection<T>, IEnumerable<T>, IEnumerable
    {
        public LocalVisibleListWrapper(CurrentDataView dataView) : base(dataView)
        {
        }

        void ICollection<T>.Add(T item)
        {
            throw new NotImplementedException();
        }

        bool ICollection<T>.Contains(T item) => 
            base.Contains(item);

        void ICollection<T>.CopyTo(T[] array, int arrayIndex)
        {
            base.CopyTo(array, arrayIndex);
        }

        bool ICollection<T>.Remove(T item)
        {
            throw new NotImplementedException();
        }

        [IteratorStateMachine(typeof(<System-Collections-Generic-IEnumerable<T>-GetEnumerator>d__1))]
        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            <System-Collections-Generic-IEnumerable<T>-GetEnumerator>d__1<T> d__1 = new <System-Collections-Generic-IEnumerable<T>-GetEnumerator>d__1<T>(0);
            d__1.<>4__this = (LocalVisibleListWrapper<T>) this;
            return d__1;
        }

        int IList<T>.IndexOf(T item) => 
            base.IndexOf(item);

        void IList<T>.Insert(int index, T item)
        {
            throw new NotImplementedException();
        }

        T IList<T>.this[int index]
        {
            get => 
                (T) base[index];
            set
            {
                throw new NotImplementedException();
            }
        }

        [CompilerGenerated]
        private sealed class <System-Collections-Generic-IEnumerable<T>-GetEnumerator>d__1 : IEnumerator<T>, IDisposable, IEnumerator
        {
            private int <>1__state;
            private T <>2__current;
            public LocalVisibleListWrapper<T> <>4__this;
            private IEnumerator <>7__wrap1;

            [DebuggerHidden]
            public <System-Collections-Generic-IEnumerable<T>-GetEnumerator>d__1(int <>1__state)
            {
                this.<>1__state = <>1__state;
            }

            private void <>m__Finally1()
            {
                this.<>1__state = -1;
                IDisposable disposable = this.<>7__wrap1 as IDisposable;
                if (disposable != null)
                {
                    disposable.Dispose();
                }
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
                        this.<>7__wrap1 = this.<>4__this.GetEnumerator();
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
                        this.<>7__wrap1 = null;
                        flag = false;
                    }
                    else
                    {
                        object current = this.<>7__wrap1.Current;
                        this.<>2__current = (T) current;
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

            T IEnumerator<T>.Current =>
                this.<>2__current;

            object IEnumerator.Current =>
                this.<>2__current;
        }
    }
}

