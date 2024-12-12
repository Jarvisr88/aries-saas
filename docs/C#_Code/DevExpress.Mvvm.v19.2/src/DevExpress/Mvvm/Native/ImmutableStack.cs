namespace DevExpress.Mvvm.Native
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Runtime.CompilerServices;

    public static class ImmutableStack
    {
        public static IImmutableStack<T> Empty<T>() => 
            EmptyStack<T>.Instance;

        public static IImmutableStack<T> Push<T>(this IImmutableStack<T> stack, T item) => 
            new SimpleStack<T>(item, stack);

        public static IImmutableStack<T> PushMultiple<T>(this IImmutableStack<T> source, IEnumerable<T> items)
        {
            Func<IImmutableStack<T>, T, IImmutableStack<T>> func = <>c__4<T>.<>9__4_0;
            if (<>c__4<T>.<>9__4_0 == null)
            {
                Func<IImmutableStack<T>, T, IImmutableStack<T>> local1 = <>c__4<T>.<>9__4_0;
                func = <>c__4<T>.<>9__4_0 = (stack, x) => stack.Push<T>(x);
            }
            return items.Aggregate<T, IImmutableStack<T>>(source, func);
        }

        public static IImmutableStack<T> Reverse<T>(this IImmutableStack<T> stack)
        {
            IImmutableStack<T> stack2 = Empty<T>();
            while (!stack.IsEmpty)
            {
                stack2 = stack2.Push<T>(stack.Peek());
                stack = stack.Pop();
            }
            return stack2;
        }

        public static IImmutableStack<T> ToImmutableStack<T>(this IEnumerable<T> source) => 
            Empty<T>().PushMultiple<T>(source.Reverse<T>());

        [Serializable, CompilerGenerated]
        private sealed class <>c__4<T>
        {
            public static readonly ImmutableStack.<>c__4<T> <>9;
            public static Func<IImmutableStack<T>, T, IImmutableStack<T>> <>9__4_0;

            static <>c__4()
            {
                ImmutableStack.<>c__4<T>.<>9 = new ImmutableStack.<>c__4<T>();
            }

            internal IImmutableStack<T> <PushMultiple>b__4_0(IImmutableStack<T> stack, T x) => 
                stack.Push<T>(x);
        }

        private class EmptyStack<T> : IImmutableStack<T>, IEnumerable<T>, IEnumerable
        {
            public static readonly IImmutableStack<T> Instance;

            static EmptyStack()
            {
                ImmutableStack.EmptyStack<T>.Instance = new ImmutableStack.EmptyStack<T>();
            }

            private EmptyStack()
            {
            }

            T IImmutableStack<T>.Peek()
            {
                throw new InvalidOperationException();
            }

            IImmutableStack<T> IImmutableStack<T>.Pop()
            {
                throw new InvalidOperationException();
            }

            [IteratorStateMachine(typeof(<System-Collections-Generic-IEnumerable<T>-GetEnumerator>d__4<>))]
            IEnumerator<T> IEnumerable<T>.GetEnumerator() => 
                new <System-Collections-Generic-IEnumerable<T>-GetEnumerator>d__4<T>(0);

            [IteratorStateMachine(typeof(<System-Collections-IEnumerable-GetEnumerator>d__5<>))]
            IEnumerator IEnumerable.GetEnumerator() => 
                new <System-Collections-IEnumerable-GetEnumerator>d__5<T>(0);

            bool IImmutableStack<T>.IsEmpty =>
                true;

            [CompilerGenerated]
            private sealed class <System-Collections-Generic-IEnumerable<T>-GetEnumerator>d__4 : IEnumerator<T>, IDisposable, IEnumerator
            {
                private int <>1__state;
                private T <>2__current;

                [DebuggerHidden]
                public <System-Collections-Generic-IEnumerable<T>-GetEnumerator>d__4(int <>1__state)
                {
                    this.<>1__state = <>1__state;
                }

                private bool MoveNext()
                {
                    if (this.<>1__state == 0)
                    {
                        this.<>1__state = -1;
                    }
                    return false;
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

            [CompilerGenerated]
            private sealed class <System-Collections-IEnumerable-GetEnumerator>d__5 : IEnumerator<object>, IDisposable, IEnumerator
            {
                private int <>1__state;
                private object <>2__current;

                [DebuggerHidden]
                public <System-Collections-IEnumerable-GetEnumerator>d__5(int <>1__state)
                {
                    this.<>1__state = <>1__state;
                }

                private bool MoveNext()
                {
                    if (this.<>1__state == 0)
                    {
                        this.<>1__state = -1;
                    }
                    return false;
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

        private class SimpleStack<T> : IImmutableStack<T>, IEnumerable<T>, IEnumerable
        {
            private readonly T head;
            private readonly IImmutableStack<T> tail;

            public SimpleStack(T head, IImmutableStack<T> tail)
            {
                this.head = head;
                this.tail = tail;
            }

            T IImmutableStack<T>.Peek() => 
                this.head;

            IImmutableStack<T> IImmutableStack<T>.Pop() => 
                this.tail;

            private IEnumerator<T> GetEnumeratorCore()
            {
                // Unresolved stack state at '00000063'
            }

            IEnumerator<T> IEnumerable<T>.GetEnumerator() => 
                this.GetEnumeratorCore();

            IEnumerator IEnumerable.GetEnumerator() => 
                this.GetEnumeratorCore();

            bool IImmutableStack<T>.IsEmpty =>
                false;

            [Serializable, CompilerGenerated]
            private sealed class <>c
            {
                public static readonly ImmutableStack.SimpleStack<T>.<>c <>9;
                public static Func<IImmutableStack<T>, IImmutableStack<T>> <>9__9_0;
                public static Func<IImmutableStack<T>, bool> <>9__9_1;
                public static Func<IImmutableStack<T>, T> <>9__9_2;

                static <>c()
                {
                    ImmutableStack.SimpleStack<T>.<>c.<>9 = new ImmutableStack.SimpleStack<T>.<>c();
                }

                internal IImmutableStack<T> <GetEnumeratorCore>b__9_0(IImmutableStack<T> x) => 
                    x.Pop();

                internal bool <GetEnumeratorCore>b__9_1(IImmutableStack<T> x) => 
                    x.IsEmpty;

                internal T <GetEnumeratorCore>b__9_2(IImmutableStack<T> x) => 
                    x.Peek();
            }
        }
    }
}

