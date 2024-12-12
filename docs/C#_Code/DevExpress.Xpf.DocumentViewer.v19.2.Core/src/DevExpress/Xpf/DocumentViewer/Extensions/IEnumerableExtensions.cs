namespace DevExpress.Xpf.DocumentViewer.Extensions
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Runtime.CompilerServices;

    public static class IEnumerableExtensions
    {
        [IteratorStateMachine(typeof(<Partition>d__0))]
        public static IEnumerable<TSource[]> Partition<TSource>(this IEnumerable<TSource> sequence, int partitionSize)
        {
            TSource[] source = new TSource[partitionSize];
            int index = 0;
            IEnumerator<TSource> enumerator = sequence.GetEnumerator();
        Label_PostSwitchInIterator:;
            while (true)
            {
                if (enumerator.MoveNext())
                {
                    source[index] = enumerator.Current;
                    index++;
                    if (index != partitionSize)
                    {
                        continue;
                    }
                    yield return source;
                    source = new TSource[partitionSize];
                    index = 0;
                    goto Label_PostSwitchInIterator;
                }
                else
                {
                    enumerator = null;
                    if (index > 0)
                    {
                        Func<TSource, bool> predicate = <>c__0<TSource>.<>9__0_0;
                        if (<>c__0<TSource>.<>9__0_0 == null)
                        {
                            Func<TSource, bool> local1 = <>c__0<TSource>.<>9__0_0;
                            predicate = <>c__0<TSource>.<>9__0_0 = x => x != null;
                        }
                        yield return source.Where<TSource>(predicate).ToArray<TSource>();
                        break;
                    }
                }
                break;
            }
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c__0<TSource>
        {
            public static readonly IEnumerableExtensions.<>c__0<TSource> <>9;
            public static Func<TSource, bool> <>9__0_0;

            static <>c__0()
            {
                IEnumerableExtensions.<>c__0<TSource>.<>9 = new IEnumerableExtensions.<>c__0<TSource>();
            }

            internal bool <Partition>b__0_0(TSource x) => 
                x != null;
        }

        [CompilerGenerated]
        private sealed class <Partition>d__0<TSource> : IEnumerable<TSource[]>, IEnumerable, IEnumerator<TSource[]>, IDisposable, IEnumerator
        {
            private int <>1__state;
            private TSource[] <>2__current;
            private int <>l__initialThreadId;
            private int partitionSize;
            public int <>3__partitionSize;
            private IEnumerable<TSource> sequence;
            public IEnumerable<TSource> <>3__sequence;
            private IEnumerator<TSource> <>7__wrap1;

            [DebuggerHidden]
            public <Partition>d__0(int <>1__state)
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

            private bool MoveNext()
            {
                bool flag;
                try
                {
                    TSource[] localArray;
                    int num2;
                    switch (this.<>1__state)
                    {
                        case 0:
                            this.<>1__state = -1;
                            localArray = new TSource[this.partitionSize];
                            num2 = 0;
                            this.<>7__wrap1 = this.sequence.GetEnumerator();
                            this.<>1__state = -3;
                            break;

                        case 1:
                            this.<>1__state = -3;
                            localArray = new TSource[this.partitionSize];
                            num2 = 0;
                            break;

                        case 2:
                            this.<>1__state = -1;
                            goto TR_0007;

                        default:
                            return false;
                    }
                    while (true)
                    {
                        if (this.<>7__wrap1.MoveNext())
                        {
                            localArray[num2] = this.<>7__wrap1.Current;
                            num2++;
                            if (num2 != this.partitionSize)
                            {
                                continue;
                            }
                            this.<>2__current = localArray;
                            this.<>1__state = 1;
                            return true;
                        }
                        else
                        {
                            this.<>m__Finally1();
                            this.<>7__wrap1 = null;
                            if (num2 > 0)
                            {
                                Func<TSource, bool> predicate = IEnumerableExtensions.<>c__0<TSource>.<>9__0_0;
                                if (IEnumerableExtensions.<>c__0<TSource>.<>9__0_0 == null)
                                {
                                    Func<TSource, bool> local1 = IEnumerableExtensions.<>c__0<TSource>.<>9__0_0;
                                    predicate = IEnumerableExtensions.<>c__0<TSource>.<>9__0_0 = new Func<TSource, bool>(this.<Partition>b__0_0);
                                }
                                this.<>2__current = localArray.Where<TSource>(predicate).ToArray<TSource>();
                                this.<>1__state = 2;
                                return true;
                            }
                        }
                        break;
                    }
                TR_0007:
                    flag = false;
                }
                fault
                {
                    this.System.IDisposable.Dispose();
                }
                return flag;
            }

            [DebuggerHidden]
            IEnumerator<TSource[]> IEnumerable<TSource[]>.GetEnumerator()
            {
                IEnumerableExtensions.<Partition>d__0<TSource> d__;
                if ((this.<>1__state != -2) || (this.<>l__initialThreadId != Environment.CurrentManagedThreadId))
                {
                    d__ = new IEnumerableExtensions.<Partition>d__0<TSource>(0);
                }
                else
                {
                    this.<>1__state = 0;
                    d__ = (IEnumerableExtensions.<Partition>d__0<TSource>) this;
                }
                d__.sequence = this.<>3__sequence;
                d__.partitionSize = this.<>3__partitionSize;
                return d__;
            }

            [DebuggerHidden]
            IEnumerator IEnumerable.GetEnumerator() => 
                this.System.Collections.Generic.IEnumerable<TSource[]>.GetEnumerator();

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

            TSource[] IEnumerator<TSource[]>.Current =>
                this.<>2__current;

            object IEnumerator.Current =>
                this.<>2__current;
        }
    }
}

