namespace DevExpress.Printing.DataAwareExport.Export.Utils
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;

    internal static class Utils<TCol> where TCol: IColumn
    {
        [IteratorStateMachine(typeof(<GetCollection>d__0))]
        public static IEnumerable<TCol> GetCollection(IEnumerable<IColumn> getAllColumns)
        {
            <GetCollection>d__0<TCol> d__1 = new <GetCollection>d__0<TCol>(-2);
            d__1.<>3__getAllColumns = getAllColumns;
            return d__1;
        }

        [CompilerGenerated]
        private sealed class <GetCollection>d__0 : IEnumerable<TCol>, IEnumerable, IEnumerator<TCol>, IDisposable, IEnumerator
        {
            private int <>1__state;
            private TCol <>2__current;
            private int <>l__initialThreadId;
            private IEnumerable<IColumn> getAllColumns;
            public IEnumerable<IColumn> <>3__getAllColumns;
            private IEnumerator<IColumn> <>7__wrap1;

            [DebuggerHidden]
            public <GetCollection>d__0(int <>1__state)
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
                    int num = this.<>1__state;
                    if (num == 0)
                    {
                        this.<>1__state = -1;
                        this.<>7__wrap1 = this.getAllColumns.GetEnumerator();
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
                        TCol current = this.<>7__wrap1.Current;
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
            IEnumerator<TCol> IEnumerable<TCol>.GetEnumerator()
            {
                DevExpress.Printing.DataAwareExport.Export.Utils.Utils<TCol>.<GetCollection>d__0 d__;
                if ((this.<>1__state != -2) || (this.<>l__initialThreadId != Environment.CurrentManagedThreadId))
                {
                    d__ = new DevExpress.Printing.DataAwareExport.Export.Utils.Utils<TCol>.<GetCollection>d__0(0);
                }
                else
                {
                    this.<>1__state = 0;
                    d__ = (DevExpress.Printing.DataAwareExport.Export.Utils.Utils<TCol>.<GetCollection>d__0) this;
                }
                d__.getAllColumns = this.<>3__getAllColumns;
                return d__;
            }

            [DebuggerHidden]
            IEnumerator IEnumerable.GetEnumerator() => 
                this.System.Collections.Generic.IEnumerable<TCol>.GetEnumerator();

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

            TCol IEnumerator<TCol>.Current =>
                this.<>2__current;

            object IEnumerator.Current =>
                this.<>2__current;
        }
    }
}

