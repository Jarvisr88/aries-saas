namespace DMEWorks.Core.Extensions
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Data;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;

    public static class IDataReaderExtensions
    {
        [IteratorStateMachine(typeof(<ToEnumerable>d__0))]
        public static IEnumerable<IDataRecord> ToEnumerable(this IDataReader reader)
        {
            <ToEnumerable>d__0 d__1 = new <ToEnumerable>d__0(-2);
            d__1.<>3__reader = reader;
            return d__1;
        }

        [IteratorStateMachine(typeof(<ToEnumerable>d__1))]
        public static IEnumerable<TWrapper> ToEnumerable<TWrapper>(this IDataReader reader, Func<IDataRecord, TWrapper> constructor)
        {
            <ToEnumerable>d__1<TWrapper> d__1 = new <ToEnumerable>d__1<TWrapper>(-2);
            d__1.<>3__reader = reader;
            d__1.<>3__constructor = constructor;
            return d__1;
        }

        [CompilerGenerated]
        private sealed class <ToEnumerable>d__0 : IEnumerable<IDataRecord>, IEnumerable, IEnumerator<IDataRecord>, IDisposable, IEnumerator
        {
            private int <>1__state;
            private IDataRecord <>2__current;
            private int <>l__initialThreadId;
            private IDataReader reader;
            public IDataReader <>3__reader;

            [DebuggerHidden]
            public <ToEnumerable>d__0(int <>1__state)
            {
                this.<>1__state = <>1__state;
                this.<>l__initialThreadId = Environment.CurrentManagedThreadId;
            }

            private bool MoveNext()
            {
                int num = this.<>1__state;
                if (num != 0)
                {
                    if (num != 1)
                    {
                        return false;
                    }
                    this.<>1__state = -1;
                }
                else
                {
                    this.<>1__state = -1;
                    if (this.reader == null)
                    {
                        throw new ArgumentNullException("reader");
                    }
                }
                if (!this.reader.Read())
                {
                    return false;
                }
                this.<>2__current = this.reader;
                this.<>1__state = 1;
                return true;
            }

            [DebuggerHidden]
            IEnumerator<IDataRecord> IEnumerable<IDataRecord>.GetEnumerator()
            {
                IDataReaderExtensions.<ToEnumerable>d__0 d__;
                if ((this.<>1__state != -2) || (this.<>l__initialThreadId != Environment.CurrentManagedThreadId))
                {
                    d__ = new IDataReaderExtensions.<ToEnumerable>d__0(0);
                }
                else
                {
                    this.<>1__state = 0;
                    d__ = this;
                }
                d__.reader = this.<>3__reader;
                return d__;
            }

            [DebuggerHidden]
            IEnumerator IEnumerable.GetEnumerator() => 
                this.System.Collections.Generic.IEnumerable<System.Data.IDataRecord>.GetEnumerator();

            [DebuggerHidden]
            void IEnumerator.Reset()
            {
                throw new NotSupportedException();
            }

            [DebuggerHidden]
            void IDisposable.Dispose()
            {
            }

            IDataRecord IEnumerator<IDataRecord>.Current =>
                this.<>2__current;

            object IEnumerator.Current =>
                this.<>2__current;
        }

        [CompilerGenerated]
        private sealed class <ToEnumerable>d__1<TWrapper> : IEnumerable<TWrapper>, IEnumerable, IEnumerator<TWrapper>, IDisposable, IEnumerator
        {
            private int <>1__state;
            private TWrapper <>2__current;
            private int <>l__initialThreadId;
            private IDataReader reader;
            public IDataReader <>3__reader;
            private Func<IDataRecord, TWrapper> constructor;
            public Func<IDataRecord, TWrapper> <>3__constructor;
            private TWrapper <wrapper>5__2;

            [DebuggerHidden]
            public <ToEnumerable>d__1(int <>1__state)
            {
                this.<>1__state = <>1__state;
                this.<>l__initialThreadId = Environment.CurrentManagedThreadId;
            }

            private bool MoveNext()
            {
                int num = this.<>1__state;
                if (num != 0)
                {
                    if (num != 1)
                    {
                        return false;
                    }
                    this.<>1__state = -1;
                }
                else
                {
                    this.<>1__state = -1;
                    if (this.reader == null)
                    {
                        throw new ArgumentNullException("reader");
                    }
                    if (this.constructor == null)
                    {
                        throw new ArgumentNullException("constructor");
                    }
                    this.<wrapper>5__2 = this.constructor(this.reader);
                }
                if (!this.reader.Read())
                {
                    return false;
                }
                this.<>2__current = this.<wrapper>5__2;
                this.<>1__state = 1;
                return true;
            }

            [DebuggerHidden]
            IEnumerator<TWrapper> IEnumerable<TWrapper>.GetEnumerator()
            {
                IDataReaderExtensions.<ToEnumerable>d__1<TWrapper> d__;
                if ((this.<>1__state != -2) || (this.<>l__initialThreadId != Environment.CurrentManagedThreadId))
                {
                    d__ = new IDataReaderExtensions.<ToEnumerable>d__1<TWrapper>(0);
                }
                else
                {
                    this.<>1__state = 0;
                    d__ = (IDataReaderExtensions.<ToEnumerable>d__1<TWrapper>) this;
                }
                d__.reader = this.<>3__reader;
                d__.constructor = this.<>3__constructor;
                return d__;
            }

            [DebuggerHidden]
            IEnumerator IEnumerable.GetEnumerator() => 
                this.System.Collections.Generic.IEnumerable<TWrapper>.GetEnumerator();

            [DebuggerHidden]
            void IEnumerator.Reset()
            {
                throw new NotSupportedException();
            }

            [DebuggerHidden]
            void IDisposable.Dispose()
            {
            }

            TWrapper IEnumerator<TWrapper>.Current =>
                this.<>2__current;

            object IEnumerator.Current =>
                this.<>2__current;
        }
    }
}

