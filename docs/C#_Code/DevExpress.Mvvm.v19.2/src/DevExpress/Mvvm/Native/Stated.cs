namespace DevExpress.Mvvm.Native
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;

    public static class Stated
    {
        public static IEnumerable<TOut> Select<TIn, TOut, TState>(this Stated<TState, IEnumerable<TIn>> statedEnumerable, Func<TIn, TState, Stated<TState, TOut>> selector) => 
            SelectCore<TIn, TOut, TState>(new Ref<TState>(statedEnumerable.State), statedEnumerable.Value, selector);

        public static Stated<TState, TResult> Select<TIn, TOut, TResult, TState>(this Stated<TState, IEnumerable<TIn>> statedEnumerable, Func<TIn, TState, Stated<TState, TOut>> selector, Func<IEnumerable<TOut>, TResult> consumer)
        {
            Ref<TState> state = new Ref<TState>(statedEnumerable.State);
            return consumer(SelectCore<TIn, TOut, TState>(state, statedEnumerable.Value, selector)).WithState<TResult, TState>(state.Value);
        }

        [IteratorStateMachine(typeof(<SelectCore>d__5))]
        private static IEnumerable<TOut> SelectCore<TIn, TOut, TState>(Ref<TState> state, IEnumerable<TIn> enumerable, Func<TIn, TState, Stated<TState, TOut>> selector)
        {
            <SelectCore>d__5<TIn, TOut, TState> d__1 = new <SelectCore>d__5<TIn, TOut, TState>(-2);
            d__1.<>3__state = state;
            d__1.<>3__enumerable = enumerable;
            d__1.<>3__selector = selector;
            return d__1;
        }

        public static IEnumerable<TOut> SelectMany<TIn, TOut, TState>(this Stated<TState, IEnumerable<TIn>> statedEnumerable, Func<TIn, TState, Stated<TState, IEnumerable<TOut>>> selector) => 
            SelectManyCore<TIn, TOut, TState>(new Ref<TState>(statedEnumerable.State), statedEnumerable.Value, selector);

        [IteratorStateMachine(typeof(<SelectManyCore>d__2))]
        private static IEnumerable<TOut> SelectManyCore<TIn, TOut, TState>(Ref<TState> state, IEnumerable<TIn> enumerable, Func<TIn, TState, Stated<TState, IEnumerable<TOut>>> selector)
        {
            <SelectManyCore>d__2<TIn, TOut, TState> d__1 = new <SelectManyCore>d__2<TIn, TOut, TState>(-2);
            d__1.<>3__state = state;
            d__1.<>3__enumerable = enumerable;
            d__1.<>3__selector = selector;
            return d__1;
        }

        public static IEnumerable<TOut> SelectUntil<TIn, TOut, TState>(this Stated<TState, IEnumerable<TIn>> statedEnumerable, Func<TIn, TState, Stated<TState, TOut>> selector, Func<TState, bool> stop) => 
            SelectUntilCore<TIn, TOut, TState>(new Ref<TState>(statedEnumerable.State), statedEnumerable.Value, selector, stop);

        public static Stated<TState, TResult> SelectUntil<TIn, TOut, TResult, TState>(this Stated<TState, IEnumerable<TIn>> statedEnumerable, Func<TIn, TState, Stated<TState, TOut>> selector, Func<TState, bool> stop, Func<IEnumerable<TOut>, TResult> consumer)
        {
            Ref<TState> state = new Ref<TState>(statedEnumerable.State);
            return consumer(SelectUntilCore<TIn, TOut, TState>(state, statedEnumerable.Value, selector, stop)).WithState<TResult, TState>(state.Value);
        }

        [IteratorStateMachine(typeof(<SelectUntilCore>d__8))]
        private static IEnumerable<TOut> SelectUntilCore<TIn, TOut, TState>(Ref<TState> state, IEnumerable<TIn> enumerable, Func<TIn, TState, Stated<TState, TOut>> selector, Func<TState, bool> stop)
        {
            <SelectUntilCore>d__8<TIn, TOut, TState> d__1 = new <SelectUntilCore>d__8<TIn, TOut, TState>(-2);
            d__1.<>3__state = state;
            d__1.<>3__enumerable = enumerable;
            d__1.<>3__selector = selector;
            d__1.<>3__stop = stop;
            return d__1;
        }

        public static Stated<TState, TResult> Where<TIn, TResult, TState>(this Stated<TState, IEnumerable<TIn>> statedEnumerable, Func<TIn, TState, Stated<TState, bool>> selector, Func<IEnumerable<TIn>, TResult> consumer)
        {
            Ref<TState> state = new Ref<TState>(statedEnumerable.State);
            return consumer(SelectManyCore<TIn, TIn, TState>(state, statedEnumerable.Value, delegate (TIn i, TState state_) {
                Stated<TState, bool> stated = selector(i, state_);
                return (stated.Value ? i.Yield<TIn>() : ((IEnumerable<TIn>) EmptyArray<TIn>.Instance)).WithState<IEnumerable<TIn>, TState>(stated.State);
            })).WithState<TResult, TState>(state.Value);
        }

        public static Stated<TState, T> WithState<T, TState>(this T value, TState state) => 
            new Stated<TState, T>(state, value);

        [CompilerGenerated]
        private sealed class <SelectCore>d__5<TIn, TOut, TState> : IEnumerable<TOut>, IEnumerable, IEnumerator<TOut>, IDisposable, IEnumerator
        {
            private int <>1__state;
            private TOut <>2__current;
            private int <>l__initialThreadId;
            private IEnumerable<TIn> enumerable;
            public IEnumerable<TIn> <>3__enumerable;
            private Func<TIn, TState, Stated<TState, TOut>> selector;
            public Func<TIn, TState, Stated<TState, TOut>> <>3__selector;
            private Stated.Ref<TState> state;
            public Stated.Ref<TState> <>3__state;
            private IEnumerator<TIn> <>7__wrap1;

            [DebuggerHidden]
            public <SelectCore>d__5(int <>1__state)
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
                        this.<>7__wrap1 = this.enumerable.GetEnumerator();
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
                        TIn current = this.<>7__wrap1.Current;
                        Stated<TState, TOut> stated = this.selector(current, this.state.Value);
                        this.state.Value = stated.State;
                        this.<>2__current = stated.Value;
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
            IEnumerator<TOut> IEnumerable<TOut>.GetEnumerator()
            {
                Stated.<SelectCore>d__5<TIn, TOut, TState> d__;
                if ((this.<>1__state != -2) || (this.<>l__initialThreadId != Environment.CurrentManagedThreadId))
                {
                    d__ = new Stated.<SelectCore>d__5<TIn, TOut, TState>(0);
                }
                else
                {
                    this.<>1__state = 0;
                    d__ = (Stated.<SelectCore>d__5<TIn, TOut, TState>) this;
                }
                d__.state = this.<>3__state;
                d__.enumerable = this.<>3__enumerable;
                d__.selector = this.<>3__selector;
                return d__;
            }

            [DebuggerHidden]
            IEnumerator IEnumerable.GetEnumerator() => 
                this.System.Collections.Generic.IEnumerable<TOut>.GetEnumerator();

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

            TOut IEnumerator<TOut>.Current =>
                this.<>2__current;

            object IEnumerator.Current =>
                this.<>2__current;
        }

        [CompilerGenerated]
        private sealed class <SelectManyCore>d__2<TIn, TOut, TState> : IEnumerable<TOut>, IEnumerable, IEnumerator<TOut>, IDisposable, IEnumerator
        {
            private int <>1__state;
            private TOut <>2__current;
            private int <>l__initialThreadId;
            private IEnumerable<TIn> enumerable;
            public IEnumerable<TIn> <>3__enumerable;
            private Func<TIn, TState, Stated<TState, IEnumerable<TOut>>> selector;
            public Func<TIn, TState, Stated<TState, IEnumerable<TOut>>> <>3__selector;
            private Stated.Ref<TState> state;
            public Stated.Ref<TState> <>3__state;
            private IEnumerator<TIn> <>7__wrap1;
            private IEnumerator<TOut> <>7__wrap2;

            [DebuggerHidden]
            public <SelectManyCore>d__2(int <>1__state)
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
                this.<>1__state = -3;
                if (this.<>7__wrap2 != null)
                {
                    this.<>7__wrap2.Dispose();
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
                        this.<>7__wrap1 = this.enumerable.GetEnumerator();
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
                        TIn current = this.<>7__wrap1.Current;
                        Stated<TState, IEnumerable<TOut>> stated = this.selector(current, this.state.Value);
                        this.state.Value = stated.State;
                        this.<>7__wrap2 = stated.Value.GetEnumerator();
                        this.<>1__state = -4;
                    }
                    else
                    {
                        this.<>m__Finally1();
                        this.<>7__wrap1 = null;
                        return false;
                    }
                TR_0009:
                    while (true)
                    {
                        if (this.<>7__wrap2.MoveNext())
                        {
                            TOut current = this.<>7__wrap2.Current;
                            this.<>2__current = current;
                            this.<>1__state = 1;
                            flag = true;
                        }
                        else
                        {
                            this.<>m__Finally2();
                            this.<>7__wrap2 = null;
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
            IEnumerator<TOut> IEnumerable<TOut>.GetEnumerator()
            {
                Stated.<SelectManyCore>d__2<TIn, TOut, TState> d__;
                if ((this.<>1__state != -2) || (this.<>l__initialThreadId != Environment.CurrentManagedThreadId))
                {
                    d__ = new Stated.<SelectManyCore>d__2<TIn, TOut, TState>(0);
                }
                else
                {
                    this.<>1__state = 0;
                    d__ = (Stated.<SelectManyCore>d__2<TIn, TOut, TState>) this;
                }
                d__.state = this.<>3__state;
                d__.enumerable = this.<>3__enumerable;
                d__.selector = this.<>3__selector;
                return d__;
            }

            [DebuggerHidden]
            IEnumerator IEnumerable.GetEnumerator() => 
                this.System.Collections.Generic.IEnumerable<TOut>.GetEnumerator();

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

            TOut IEnumerator<TOut>.Current =>
                this.<>2__current;

            object IEnumerator.Current =>
                this.<>2__current;
        }

        [CompilerGenerated]
        private sealed class <SelectUntilCore>d__8<TIn, TOut, TState> : IEnumerable<TOut>, IEnumerable, IEnumerator<TOut>, IDisposable, IEnumerator
        {
            private int <>1__state;
            private TOut <>2__current;
            private int <>l__initialThreadId;
            private IEnumerable<TIn> enumerable;
            public IEnumerable<TIn> <>3__enumerable;
            private Func<TState, bool> stop;
            public Func<TState, bool> <>3__stop;
            private Stated.Ref<TState> state;
            public Stated.Ref<TState> <>3__state;
            private Func<TIn, TState, Stated<TState, TOut>> selector;
            public Func<TIn, TState, Stated<TState, TOut>> <>3__selector;
            private IEnumerator<TIn> <>7__wrap1;

            [DebuggerHidden]
            public <SelectUntilCore>d__8(int <>1__state)
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
                try
                {
                    int num = this.<>1__state;
                    if (num == 0)
                    {
                        this.<>1__state = -1;
                        this.<>7__wrap1 = this.enumerable.GetEnumerator();
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
                    if (this.<>7__wrap1.MoveNext())
                    {
                        TIn current = this.<>7__wrap1.Current;
                        if (!this.stop(this.state.Value))
                        {
                            Stated<TState, TOut> stated = this.selector(current, this.state.Value);
                            this.state.Value = stated.State;
                            this.<>2__current = stated.Value;
                            this.<>1__state = 1;
                            return true;
                        }
                    }
                    this.<>m__Finally1();
                    this.<>7__wrap1 = null;
                    return false;
                }
                fault
                {
                    this.System.IDisposable.Dispose();
                }
            }

            [DebuggerHidden]
            IEnumerator<TOut> IEnumerable<TOut>.GetEnumerator()
            {
                Stated.<SelectUntilCore>d__8<TIn, TOut, TState> d__;
                if ((this.<>1__state != -2) || (this.<>l__initialThreadId != Environment.CurrentManagedThreadId))
                {
                    d__ = new Stated.<SelectUntilCore>d__8<TIn, TOut, TState>(0);
                }
                else
                {
                    this.<>1__state = 0;
                    d__ = (Stated.<SelectUntilCore>d__8<TIn, TOut, TState>) this;
                }
                d__.state = this.<>3__state;
                d__.enumerable = this.<>3__enumerable;
                d__.selector = this.<>3__selector;
                d__.stop = this.<>3__stop;
                return d__;
            }

            [DebuggerHidden]
            IEnumerator IEnumerable.GetEnumerator() => 
                this.System.Collections.Generic.IEnumerable<TOut>.GetEnumerator();

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

            TOut IEnumerator<TOut>.Current =>
                this.<>2__current;

            object IEnumerator.Current =>
                this.<>2__current;
        }

        private sealed class Ref<T>
        {
            public T Value;

            public Ref(T value)
            {
                this.Value = value;
            }
        }
    }
}

