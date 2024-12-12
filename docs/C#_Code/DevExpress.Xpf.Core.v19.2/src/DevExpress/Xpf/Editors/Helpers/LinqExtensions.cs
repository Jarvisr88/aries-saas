namespace DevExpress.Xpf.Editors.Helpers
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Runtime.CompilerServices;

    public static class LinqExtensions
    {
        [IteratorStateMachine(typeof(<Append>d__0))]
        public static IEnumerable<T> Append<T>(this IEnumerable<T> source, IEnumerable<T> second)
        {
            <Append>d__0<T> d__1 = new <Append>d__0<T>(-2);
            d__1.<>3__source = source;
            d__1.<>3__second = second;
            return d__1;
        }

        public static bool IsSubsetOf<T>(this IEnumerable<T> a, IEnumerable<T> b) => 
            !a.Except<T>(b).Any<T>();

        public static bool TrueForEach<T>(this IEnumerable<T> source, Func<T, bool> action) => 
            source.All<T>(action);

        [CompilerGenerated]
        private sealed class <Append>d__0<T> : IEnumerable<T>, IEnumerable, IEnumerator<T>, IDisposable, IEnumerator
        {
            private int <>1__state;
            private T <>2__current;
            private int <>l__initialThreadId;
            private IEnumerable<T> source;
            public IEnumerable<T> <>3__source;
            private IEnumerable<T> second;
            public IEnumerable<T> <>3__second;
            private IEnumerator<T> <>7__wrap1;

            [DebuggerHidden]
            public <Append>d__0(int <>1__state)
            {
                this.<>1__state = <>1__state;
                this.<>l__initialThreadId = Environment.CurrentManagedThreadId;
            }

            private void <>m__Finally1()
            {
                this.<>1__state = -1;
                if (this.<>7__wrap1 != null)
                {
                    this.<>7__wrap1.Dispose();
                }
            }

            private void <>m__Finally2()
            {
                this.<>1__state = -1;
                if (this.<>7__wrap1 != null)
                {
                    this.<>7__wrap1.Dispose();
                }
            }

            private bool MoveNext()
            {
                bool flag;
                try
                {
                    switch (this.<>1__state)
                    {
                        case 0:
                            this.<>1__state = -1;
                            if (this.source == null)
                            {
                                goto TR_0008;
                            }
                            else
                            {
                                this.<>7__wrap1 = this.source.GetEnumerator();
                                this.<>1__state = -3;
                            }
                            break;

                        case 1:
                            this.<>1__state = -3;
                            break;

                        case 2:
                            this.<>1__state = -4;
                            goto TR_0006;

                        default:
                            return false;
                    }
                    if (this.<>7__wrap1.MoveNext())
                    {
                        T current = this.<>7__wrap1.Current;
                        this.<>2__current = current;
                        this.<>1__state = 1;
                        flag = true;
                    }
                    else
                    {
                        this.<>m__Finally1();
                        this.<>7__wrap1 = null;
                        goto TR_0008;
                    }
                    return flag;
                TR_0003:
                    return false;
                TR_0006:
                    if (this.<>7__wrap1.MoveNext())
                    {
                        T current = this.<>7__wrap1.Current;
                        this.<>2__current = current;
                        this.<>1__state = 2;
                        return true;
                    }
                    else
                    {
                        this.<>m__Finally2();
                        this.<>7__wrap1 = null;
                    }
                    goto TR_0003;
                TR_0008:
                    if (this.second == null)
                    {
                        goto TR_0003;
                    }
                    else
                    {
                        this.<>7__wrap1 = this.second.GetEnumerator();
                        this.<>1__state = -4;
                    }
                    goto TR_0006;
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
                LinqExtensions.<Append>d__0<T> d__;
                if ((this.<>1__state != -2) || (this.<>l__initialThreadId != Environment.CurrentManagedThreadId))
                {
                    d__ = new LinqExtensions.<Append>d__0<T>(0);
                }
                else
                {
                    this.<>1__state = 0;
                    d__ = (LinqExtensions.<Append>d__0<T>) this;
                }
                d__.source = this.<>3__source;
                d__.second = this.<>3__second;
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
                switch (this.<>1__state)
                {
                    case -4:
                    case 2:
                        try
                        {
                        }
                        finally
                        {
                            this.<>m__Finally2();
                        }
                        break;

                    case -3:
                    case 1:
                        try
                        {
                        }
                        finally
                        {
                            this.<>m__Finally1();
                        }
                        break;

                    case -2:
                    case -1:
                    case 0:
                        break;

                    default:
                        return;
                }
            }

            T IEnumerator<T>.Current =>
                this.<>2__current;

            object IEnumerator.Current =>
                this.<>2__current;
        }
    }
}

