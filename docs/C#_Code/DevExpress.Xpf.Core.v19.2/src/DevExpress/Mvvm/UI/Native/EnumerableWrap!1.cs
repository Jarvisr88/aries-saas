namespace DevExpress.Mvvm.UI.Native
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;

    public class EnumerableWrap<TItem> : IEnumerable<TItem>, IEnumerable
    {
        private readonly Func<object, object> convertMethod;

        public EnumerableWrap(IEnumerable innerCollection, Func<object, object> convertMethod)
        {
            if (innerCollection == null)
            {
                throw new ArgumentNullException("innerCollection");
            }
            if (convertMethod == null)
            {
                throw new ArgumentNullException("convertMethod");
            }
            this.convertMethod = convertMethod;
            this.InnerEnumerable = innerCollection;
        }

        protected TItem Convert(object innerItem) => 
            (TItem) this.convertMethod(innerItem);

        public void CopyTo(Array array, int index)
        {
            this.CopyTo((TItem[]) array, index);
        }

        public void CopyTo(TItem[] array, int arrayIndex)
        {
            foreach (TItem local in this)
            {
                array[arrayIndex++] = local;
            }
        }

        [IteratorStateMachine(typeof(<GetEnumerator>d__2))]
        public IEnumerator<TItem> GetEnumerator()
        {
            <GetEnumerator>d__2<TItem> d__1 = new <GetEnumerator>d__2<TItem>(0);
            d__1.<>4__this = (EnumerableWrap<TItem>) this;
            return d__1;
        }

        IEnumerator IEnumerable.GetEnumerator() => 
            this.GetEnumerator();

        protected IEnumerable InnerEnumerable { get; private set; }

        [CompilerGenerated]
        private sealed class <GetEnumerator>d__2 : IEnumerator<TItem>, IDisposable, IEnumerator
        {
            private int <>1__state;
            private TItem <>2__current;
            public EnumerableWrap<TItem> <>4__this;
            private IEnumerator <>7__wrap1;

            [DebuggerHidden]
            public <GetEnumerator>d__2(int <>1__state)
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
                        this.<>7__wrap1 = this.<>4__this.InnerEnumerable.GetEnumerator();
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
                        this.<>2__current = this.<>4__this.Convert(current);
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

            TItem IEnumerator<TItem>.Current =>
                this.<>2__current;

            object IEnumerator.Current =>
                this.<>2__current;
        }
    }
}

