namespace DevExpress.Pdf.Native
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;

    public class PdfWritableArray : PdfWritableArray<object>, IEnumerable<object>, IEnumerable
    {
        public PdfWritableArray(IEnumerable<object> enumerable) : base(enumerable)
        {
        }

        public PdfWritableArray(IEnumerable value) : base(GetEnumerable(value))
        {
        }

        [IteratorStateMachine(typeof(<GetEnumerable>d__0))]
        private static IEnumerable<object> GetEnumerable(IEnumerable value)
        {
            <GetEnumerable>d__0 d__1 = new <GetEnumerable>d__0(-2);
            d__1.<>3__value = value;
            return d__1;
        }

        [IteratorStateMachine(typeof(<System-Collections-Generic-IEnumerable<System-Object>-GetEnumerator>d__4))]
        IEnumerator<object> IEnumerable<object>.GetEnumerator()
        {
            <System-Collections-Generic-IEnumerable<System-Object>-GetEnumerator>d__4 d__1 = new <System-Collections-Generic-IEnumerable<System-Object>-GetEnumerator>d__4(0);
            d__1.<>4__this = this;
            return d__1;
        }

        protected override void WriteItem(PdfDocumentStream documentStream, object value, int number)
        {
            documentStream.WriteObject(value, number);
        }

        [CompilerGenerated]
        private sealed class <GetEnumerable>d__0 : IEnumerable<object>, IEnumerable, IEnumerator<object>, IDisposable, IEnumerator
        {
            private int <>1__state;
            private object <>2__current;
            private int <>l__initialThreadId;
            private IEnumerable value;
            public IEnumerable <>3__value;
            private IEnumerator <>7__wrap1;

            [DebuggerHidden]
            public <GetEnumerable>d__0(int <>1__state)
            {
                this.<>1__state = <>1__state;
                this.<>l__initialThreadId = Environment.CurrentManagedThreadId;
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
                        this.<>7__wrap1 = this.value.GetEnumerator();
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
                        this.<>2__current = current;
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
            IEnumerator<object> IEnumerable<object>.GetEnumerator()
            {
                PdfWritableArray.<GetEnumerable>d__0 d__;
                if ((this.<>1__state != -2) || (this.<>l__initialThreadId != Environment.CurrentManagedThreadId))
                {
                    d__ = new PdfWritableArray.<GetEnumerable>d__0(0);
                }
                else
                {
                    this.<>1__state = 0;
                    d__ = this;
                }
                d__.value = this.<>3__value;
                return d__;
            }

            [DebuggerHidden]
            IEnumerator IEnumerable.GetEnumerator() => 
                this.System.Collections.Generic.IEnumerable<System.Object>.GetEnumerator();

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

            object IEnumerator<object>.Current =>
                this.<>2__current;

            object IEnumerator.Current =>
                this.<>2__current;
        }

        [CompilerGenerated]
        private sealed class <System-Collections-Generic-IEnumerable<System-Object>-GetEnumerator>d__4 : IEnumerator<object>, IDisposable, IEnumerator
        {
            private int <>1__state;
            private object <>2__current;
            public PdfWritableArray <>4__this;
            private IEnumerator <enumerator>5__1;

            [DebuggerHidden]
            public <System-Collections-Generic-IEnumerable<System-Object>-GetEnumerator>d__4(int <>1__state)
            {
                this.<>1__state = <>1__state;
            }

            private bool MoveNext()
            {
                int num = this.<>1__state;
                if (num == 0)
                {
                    this.<>1__state = -1;
                    this.<enumerator>5__1 = this.<>4__this.GetEnumerator();
                }
                else
                {
                    if (num != 1)
                    {
                        return false;
                    }
                    this.<>1__state = -1;
                }
                if (!this.<enumerator>5__1.MoveNext())
                {
                    return false;
                }
                this.<>2__current = this.<enumerator>5__1.Current;
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

            object IEnumerator<object>.Current =>
                this.<>2__current;

            object IEnumerator.Current =>
                this.<>2__current;
        }
    }
}

