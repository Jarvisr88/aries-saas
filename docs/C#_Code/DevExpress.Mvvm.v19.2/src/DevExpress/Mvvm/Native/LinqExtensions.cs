namespace DevExpress.Mvvm.Native
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    public static class LinqExtensions
    {
        public static TAccumulate AggregateUntil<T, TAccumulate>(this IEnumerable<T> source, TAccumulate seed, Func<TAccumulate, T, TAccumulate> func, Func<TAccumulate, bool> stop)
        {
            foreach (T local in source)
            {
                if (stop(seed))
                {
                    break;
                }
                seed = func(seed, local);
            }
            return seed;
        }

        public static bool AllEqual<T>(this IEnumerable<T> source, Func<T, T, bool> comparer = null)
        {
            if (!source.Any<T>())
            {
                return true;
            }
            Func<T, T, bool> func1 = comparer;
            if (comparer == null)
            {
                Func<T, T, bool> local1 = comparer;
                func1 = <>c__28<T>.<>9__28_0;
                if (<>c__28<T>.<>9__28_0 == null)
                {
                    Func<T, T, bool> local2 = <>c__28<T>.<>9__28_0;
                    func1 = <>c__28<T>.<>9__28_0 = (x, y) => EqualityComparer<T>.Default.Equals(x, y);
                }
            }
            comparer = func1;
            T first = source.First<T>();
            return source.Skip<T>(1).All<T>(x => comparer(x, first));
        }

        public static Action CombineActions(params Action[] actions) => 
            delegate {
                Action<Action> action = <>c.<>9__29_1;
                if (<>c.<>9__29_1 == null)
                {
                    Action<Action> local1 = <>c.<>9__29_1;
                    action = <>c.<>9__29_1 = x => x();
                }
                actions.ForEach<Action>(action);
            };

        public static Action<T> CombineActions<T>(params Action<T>[] actions) => 
            delegate (T p) {
                actions.ForEach<Action<T>>(x => x(p));
            };

        public static Action<T1, T2> CombineActions<T1, T2>(params Action<T1, T2>[] actions) => 
            delegate (T1 p1, T2 p2) {
                actions.ForEach<Action<T1, T2>>(x => x(p1, p2));
            };

        public static string ConcatStringsWithDelimiter(this IEnumerable<string> source, string delimiter) => 
            string.Join(delimiter, source);

        public static IEnumerable<T> Flatten<T>(this IEnumerable<T> source, Func<T, IEnumerable<T>> getItems) => 
            source.Flatten<T>((x, _) => getItems(x));

        [IteratorStateMachine(typeof(<Flatten>d__18))]
        public static IEnumerable<T> Flatten<T>(this IEnumerable<T> source, Func<T, int, IEnumerable<T>> getItems)
        {
            <Flatten>d__18<T> d__1 = new <Flatten>d__18<T>(-2);
            d__1.<>3__source = source;
            d__1.<>3__getItems = getItems;
            return d__1;
        }

        public static void ForEach<T>(this IEnumerable<T> source, Action<T> action)
        {
            if (source != null)
            {
                foreach (T local in source)
                {
                    action(local);
                }
            }
        }

        public static void ForEach<T>(this IEnumerable<T> source, Action<T, int> action)
        {
            if (source != null)
            {
                int num = 0;
                foreach (T local in source)
                {
                    action(local, num++);
                }
            }
        }

        public static void ForEach<T>(this IEnumerable<T> source, Func<T, int, IEnumerable<T>> getItems, Action<T, int> action)
        {
            source.ForEachCore<T>(getItems, action, 0);
        }

        public static void ForEach<TFirst, TSecond>(this IEnumerable<TFirst> first, IEnumerable<TSecond> second, Action<TFirst, TSecond> action)
        {
            using (IEnumerator<TFirst> enumerator = first.GetEnumerator())
            {
                using (IEnumerator<TSecond> enumerator2 = second.GetEnumerator())
                {
                    while (enumerator.MoveNext() && enumerator2.MoveNext())
                    {
                        action(enumerator.Current, enumerator2.Current);
                    }
                }
            }
        }

        private static void ForEachCore<T>(this IEnumerable<T> source, Func<T, int, IEnumerable<T>> getItems, Action<T, int> action, int level)
        {
            source.ForEach<T>(x => action(x, level));
            if (source.Any<T>())
            {
                (from x in source select getItems(x, level + 1)).ForEachCore<T>(getItems, action, level + 1);
            }
        }

        public static int IndexOf<T>(this IEnumerable<T> source, Predicate<T> predicate)
        {
            int num2;
            int num = 0;
            using (IEnumerator<T> enumerator = source.GetEnumerator())
            {
                while (true)
                {
                    if (enumerator.MoveNext())
                    {
                        T current = enumerator.Current;
                        if (!predicate(current))
                        {
                            num++;
                            continue;
                        }
                        num2 = num;
                    }
                    else
                    {
                        return -1;
                    }
                    break;
                }
            }
            return num2;
        }

        [IteratorStateMachine(typeof(<InsertDelimiter>d__22))]
        public static IEnumerable<T> InsertDelimiter<T>(this IEnumerable<T> source, T delimiter)
        {
            IEnumerator<T> enumerator = source.GetEnumerator();
            if (enumerator.MoveNext())
            {
                yield return enumerator.Current;
            }
            if (!enumerator.MoveNext())
            {
                enumerator = null;
                yield break;
            }
            else
            {
                yield return delimiter;
                yield break;
            }
        }

        public static bool IsEmptyOrSingle<T>(this IEnumerable<T> source) => 
            !source.Any<T>() || !source.Skip<T>(1).Any<T>();

        public static bool IsSingle<T>(this IEnumerable<T> source) => 
            source.Any<T>() && !source.Skip<T>(1).Any<T>();

        public static T MaxBy<T, TKey>(this IEnumerable<T> source, Func<T, TKey> keySelector) where TKey: IComparable
        {
            Comparer<TKey> comparer = Comparer<TKey>.Default;
            return source.Aggregate<T>((x, y) => ((comparer.Compare(keySelector(x), keySelector(y)) >= 0) ? x : y));
        }

        public static Func<T> Memoize<T>(this Func<T> getValue)
        {
            Lazy<T> lazy = new Lazy<T>(getValue);
            return () => lazy.Value;
        }

        public static Func<TIn, TOut> Memoize<TIn, TOut>(this Func<TIn, TOut> getValue)
        {
            Dictionary<TIn, TOut> dict = new Dictionary<TIn, TOut>();
            return x => dict.GetOrAdd<TIn, TOut>(x, getValue);
        }

        public static T MinBy<T, TKey>(this IEnumerable<T> source, Func<T, TKey> keySelector) where TKey: IComparable
        {
            Comparer<TKey> comparer = Comparer<TKey>.Default;
            return source.Aggregate<T>((x, y) => ((comparer.Compare(keySelector(x), keySelector(y)) <= 0) ? x : y));
        }

        public static T MinByLast<T, TKey>(this IEnumerable<T> source, Func<T, TKey> keySelector) where TKey: IComparable
        {
            Comparer<TKey> comparer = Comparer<TKey>.Default;
            return source.Aggregate<T>((x, y) => ((comparer.Compare(keySelector(x), keySelector(y)) < 0) ? x : y));
        }

        public static IOrderedEnumerable<TSource> OrderBy<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, ListSortDirection sortDirection) => 
            (sortDirection == ListSortDirection.Ascending) ? source.OrderBy<TSource, TKey>(keySelector) : source.OrderByDescending<TSource, TKey>(keySelector);

        public static ObservableCollection<T> ToObservableCollection<T>(this IEnumerable<T> source) => 
            new ObservableCollection<T>(source);

        public static ReadOnlyCollection<T> ToReadOnlyCollection<T>(this IEnumerable<T> source) => 
            source.ToList<T>().AsReadOnly();

        public static ReadOnlyObservableCollection<T> ToReadOnlyObservableCollection<T>(this IEnumerable<T> source) => 
            new ReadOnlyObservableCollection<T>(source.ToObservableCollection<T>());

        [IteratorStateMachine(typeof(<Unfold>d__7))]
        public static IEnumerable<T> Unfold<T>(T seed, Func<T, T> next, Func<T, bool> stop)
        {
            <Unfold>d__7<T> d__1 = new <Unfold>d__7<T>(-2);
            d__1.<>3__seed = seed;
            d__1.<>3__next = next;
            d__1.<>3__stop = stop;
            return d__1;
        }

        public static T WithReturnValue<T>(this Func<Lazy<T>, T> func)
        {
            T t = default(T);
            bool tHasValue = false;
            t = func(new Lazy<T>(delegate {
                if (!tHasValue)
                {
                    throw new InvalidOperationException("Fix");
                }
                return t;
            }));
            tHasValue = true;
            return t;
        }

        [IteratorStateMachine(typeof(<Yield>d__8))]
        public static IEnumerable<T> Yield<T>(this T singleElement)
        {
            <Yield>d__8<T> d__1 = new <Yield>d__8<T>(-2);
            d__1.<>3__singleElement = singleElement;
            return d__1;
        }

        [IteratorStateMachine(typeof(<YieldIfNotEmpty>d__10))]
        public static IEnumerable<string> YieldIfNotEmpty(this string singleElement)
        {
            <YieldIfNotEmpty>d__10 d__1 = new <YieldIfNotEmpty>d__10(-2);
            d__1.<>3__singleElement = singleElement;
            return d__1;
        }

        public static string[] YieldIfNotEmptyToArray(this string singleElement)
        {
            if (string.IsNullOrEmpty(singleElement))
            {
                return EmptyArray<string>.Instance;
            }
            return new string[] { singleElement };
        }

        [IteratorStateMachine(typeof(<YieldIfNotNull>d__9))]
        public static IEnumerable<T> YieldIfNotNull<T>(this T singleElement)
        {
            <YieldIfNotNull>d__9<T> d__1 = new <YieldIfNotNull>d__9<T>(-2);
            d__1.<>3__singleElement = singleElement;
            return d__1;
        }

        public static T[] YieldIfNotNullToArray<T>(this T singleElement)
        {
            if (singleElement == null)
            {
                return EmptyArray<T>.Instance;
            }
            return new T[] { singleElement };
        }

        public static T[] YieldToArray<T>(this T singleElement) => 
            new T[] { singleElement };

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly LinqExtensions.<>c <>9 = new LinqExtensions.<>c();
            public static Action<Action> <>9__29_1;

            internal void <CombineActions>b__29_1(Action x)
            {
                x();
            }
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c__28<T>
        {
            public static readonly LinqExtensions.<>c__28<T> <>9;
            public static Func<T, T, bool> <>9__28_0;

            static <>c__28()
            {
                LinqExtensions.<>c__28<T>.<>9 = new LinqExtensions.<>c__28<T>();
            }

            internal bool <AllEqual>b__28_0(T x, T y) => 
                EqualityComparer<T>.Default.Equals(x, y);
        }

        [CompilerGenerated]
        private sealed class <Flatten>d__18<T> : IEnumerable<T>, IEnumerable, IEnumerator<T>, IDisposable, IEnumerator
        {
            private int <>1__state;
            private T <>2__current;
            private int <>l__initialThreadId;
            private IEnumerable<T> source;
            public IEnumerable<T> <>3__source;
            private Stack<LinqExtensions.EnumeratorAndLevel<T>> <stack>5__1;
            private LinqExtensions.EnumeratorAndLevel<T> <top>5__2;
            private Func<T, int, IEnumerable<T>> getItems;
            public Func<T, int, IEnumerable<T>> <>3__getItems;
            private T <current>5__3;

            [DebuggerHidden]
            public <Flatten>d__18(int <>1__state)
            {
                this.<>1__state = <>1__state;
                this.<>l__initialThreadId = Environment.CurrentManagedThreadId;
            }

            private bool MoveNext()
            {
                int num = this.<>1__state;
                if (num == 0)
                {
                    this.<>1__state = -1;
                    this.<stack>5__1 = new Stack<LinqExtensions.EnumeratorAndLevel<T>>();
                    IEnumerator<T> en = this.source.GetEnumerator();
                    if (en.MoveNext())
                    {
                        this.<stack>5__1.Push(new LinqExtensions.EnumeratorAndLevel<T>(en, 0));
                    }
                }
                else
                {
                    IEnumerator<T> enumerator;
                    if (num != 1)
                    {
                        return false;
                    }
                    this.<>1__state = -1;
                    if (!this.<top>5__2.En.MoveNext())
                    {
                        this.<stack>5__1.Pop();
                    }
                    IEnumerable<T> local1 = this.getItems(this.<current>5__3, this.<top>5__2.Level);
                    if (local1 != null)
                    {
                        enumerator = local1.GetEnumerator();
                    }
                    else
                    {
                        IEnumerable<T> local2 = local1;
                        enumerator = null;
                    }
                    IEnumerator<T> en = enumerator;
                    if ((en != null) && en.MoveNext())
                    {
                        this.<stack>5__1.Push(new LinqExtensions.EnumeratorAndLevel<T>(en, this.<top>5__2.Level + 1));
                    }
                    this.<top>5__2 = new LinqExtensions.EnumeratorAndLevel<T>();
                    this.<current>5__3 = default(T);
                }
                if (this.<stack>5__1.Count == 0)
                {
                    return false;
                }
                this.<top>5__2 = this.<stack>5__1.Peek();
                this.<current>5__3 = this.<top>5__2.En.Current;
                this.<>2__current = this.<current>5__3;
                this.<>1__state = 1;
                return true;
            }

            [DebuggerHidden]
            IEnumerator<T> IEnumerable<T>.GetEnumerator()
            {
                LinqExtensions.<Flatten>d__18<T> d__;
                if ((this.<>1__state != -2) || (this.<>l__initialThreadId != Environment.CurrentManagedThreadId))
                {
                    d__ = new LinqExtensions.<Flatten>d__18<T>(0);
                }
                else
                {
                    this.<>1__state = 0;
                    d__ = (LinqExtensions.<Flatten>d__18<T>) this;
                }
                d__.source = this.<>3__source;
                d__.getItems = this.<>3__getItems;
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
            }

            T IEnumerator<T>.Current =>
                this.<>2__current;

            object IEnumerator.Current =>
                this.<>2__current;
        }

        [CompilerGenerated]
        private sealed class <InsertDelimiter>d__22<T> : IEnumerable<T>, IEnumerable, IEnumerator<T>, IDisposable, IEnumerator
        {
            private int <>1__state;
            private T <>2__current;
            private int <>l__initialThreadId;
            private IEnumerable<T> source;
            public IEnumerable<T> <>3__source;
            private T delimiter;
            public T <>3__delimiter;
            private IEnumerator<T> <en>5__1;

            [DebuggerHidden]
            public <InsertDelimiter>d__22(int <>1__state)
            {
                this.<>1__state = <>1__state;
                this.<>l__initialThreadId = Environment.CurrentManagedThreadId;
            }

            private void <>m__Finally1()
            {
                this.<>1__state = -1;
                if (this.<en>5__1 != null)
                {
                    this.<en>5__1.Dispose();
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
                            this.<en>5__1 = this.source.GetEnumerator();
                            this.<>1__state = -3;
                            if (!this.<en>5__1.MoveNext())
                            {
                                break;
                            }
                            this.<>2__current = this.<en>5__1.Current;
                            this.<>1__state = 1;
                            return true;

                        case 1:
                            this.<>1__state = -3;
                            break;

                        case 2:
                            this.<>1__state = -3;
                            this.<>2__current = this.<en>5__1.Current;
                            this.<>1__state = 3;
                            return true;

                        case 3:
                            this.<>1__state = -3;
                            break;

                        default:
                            return false;
                    }
                    if (!this.<en>5__1.MoveNext())
                    {
                        this.<>m__Finally1();
                        this.<en>5__1 = null;
                        flag = false;
                    }
                    else
                    {
                        this.<>2__current = this.delimiter;
                        this.<>1__state = 2;
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
            IEnumerator<T> IEnumerable<T>.GetEnumerator()
            {
                LinqExtensions.<InsertDelimiter>d__22<T> d__;
                if ((this.<>1__state != -2) || (this.<>l__initialThreadId != Environment.CurrentManagedThreadId))
                {
                    d__ = new LinqExtensions.<InsertDelimiter>d__22<T>(0);
                }
                else
                {
                    this.<>1__state = 0;
                    d__ = (LinqExtensions.<InsertDelimiter>d__22<T>) this;
                }
                d__.source = this.<>3__source;
                d__.delimiter = this.<>3__delimiter;
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
                    case -3:
                    case 1:
                    case 2:
                    case 3:
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

        [CompilerGenerated]
        private sealed class <Unfold>d__7<T> : IEnumerable<T>, IEnumerable, IEnumerator<T>, IDisposable, IEnumerator
        {
            private int <>1__state;
            private T <>2__current;
            private int <>l__initialThreadId;
            private T seed;
            public T <>3__seed;
            private Func<T, T> next;
            public Func<T, T> <>3__next;
            private T <current>5__1;
            private Func<T, bool> stop;
            public Func<T, bool> <>3__stop;

            [DebuggerHidden]
            public <Unfold>d__7(int <>1__state)
            {
                this.<>1__state = <>1__state;
                this.<>l__initialThreadId = Environment.CurrentManagedThreadId;
            }

            private bool MoveNext()
            {
                int num = this.<>1__state;
                if (num == 0)
                {
                    this.<>1__state = -1;
                    this.<current>5__1 = this.seed;
                }
                else
                {
                    if (num != 1)
                    {
                        return false;
                    }
                    this.<>1__state = -1;
                    this.<current>5__1 = this.next(this.<current>5__1);
                }
                if (this.stop(this.<current>5__1))
                {
                    this.<current>5__1 = default(T);
                    return false;
                }
                this.<>2__current = this.<current>5__1;
                this.<>1__state = 1;
                return true;
            }

            [DebuggerHidden]
            IEnumerator<T> IEnumerable<T>.GetEnumerator()
            {
                LinqExtensions.<Unfold>d__7<T> d__;
                if ((this.<>1__state != -2) || (this.<>l__initialThreadId != Environment.CurrentManagedThreadId))
                {
                    d__ = new LinqExtensions.<Unfold>d__7<T>(0);
                }
                else
                {
                    this.<>1__state = 0;
                    d__ = (LinqExtensions.<Unfold>d__7<T>) this;
                }
                d__.seed = this.<>3__seed;
                d__.next = this.<>3__next;
                d__.stop = this.<>3__stop;
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
            }

            T IEnumerator<T>.Current =>
                this.<>2__current;

            object IEnumerator.Current =>
                this.<>2__current;
        }

        [CompilerGenerated]
        private sealed class <Yield>d__8<T> : IEnumerable<T>, IEnumerable, IEnumerator<T>, IDisposable, IEnumerator
        {
            private int <>1__state;
            private T <>2__current;
            private int <>l__initialThreadId;
            private T singleElement;
            public T <>3__singleElement;

            [DebuggerHidden]
            public <Yield>d__8(int <>1__state)
            {
                this.<>1__state = <>1__state;
                this.<>l__initialThreadId = Environment.CurrentManagedThreadId;
            }

            private bool MoveNext()
            {
                int num = this.<>1__state;
                if (num == 0)
                {
                    this.<>1__state = -1;
                    this.<>2__current = this.singleElement;
                    this.<>1__state = 1;
                    return true;
                }
                if (num == 1)
                {
                    this.<>1__state = -1;
                }
                return false;
            }

            [DebuggerHidden]
            IEnumerator<T> IEnumerable<T>.GetEnumerator()
            {
                LinqExtensions.<Yield>d__8<T> d__;
                if ((this.<>1__state != -2) || (this.<>l__initialThreadId != Environment.CurrentManagedThreadId))
                {
                    d__ = new LinqExtensions.<Yield>d__8<T>(0);
                }
                else
                {
                    this.<>1__state = 0;
                    d__ = (LinqExtensions.<Yield>d__8<T>) this;
                }
                d__.singleElement = this.<>3__singleElement;
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
            }

            T IEnumerator<T>.Current =>
                this.<>2__current;

            object IEnumerator.Current =>
                this.<>2__current;
        }

        [CompilerGenerated]
        private sealed class <YieldIfNotEmpty>d__10 : IEnumerable<string>, IEnumerable, IEnumerator<string>, IDisposable, IEnumerator
        {
            private int <>1__state;
            private string <>2__current;
            private int <>l__initialThreadId;
            private string singleElement;
            public string <>3__singleElement;

            [DebuggerHidden]
            public <YieldIfNotEmpty>d__10(int <>1__state)
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
                    if (!string.IsNullOrEmpty(this.singleElement))
                    {
                        this.<>2__current = this.singleElement;
                        this.<>1__state = 1;
                        return true;
                    }
                }
                return false;
            }

            [DebuggerHidden]
            IEnumerator<string> IEnumerable<string>.GetEnumerator()
            {
                LinqExtensions.<YieldIfNotEmpty>d__10 d__;
                if ((this.<>1__state != -2) || (this.<>l__initialThreadId != Environment.CurrentManagedThreadId))
                {
                    d__ = new LinqExtensions.<YieldIfNotEmpty>d__10(0);
                }
                else
                {
                    this.<>1__state = 0;
                    d__ = this;
                }
                d__.singleElement = this.<>3__singleElement;
                return d__;
            }

            [DebuggerHidden]
            IEnumerator IEnumerable.GetEnumerator() => 
                this.System.Collections.Generic.IEnumerable<System.String>.GetEnumerator();

            [DebuggerHidden]
            void IEnumerator.Reset()
            {
                throw new NotSupportedException();
            }

            [DebuggerHidden]
            void IDisposable.Dispose()
            {
            }

            string IEnumerator<string>.Current =>
                this.<>2__current;

            object IEnumerator.Current =>
                this.<>2__current;
        }

        [CompilerGenerated]
        private sealed class <YieldIfNotNull>d__9<T> : IEnumerable<T>, IEnumerable, IEnumerator<T>, IDisposable, IEnumerator
        {
            private int <>1__state;
            private T <>2__current;
            private int <>l__initialThreadId;
            private T singleElement;
            public T <>3__singleElement;

            [DebuggerHidden]
            public <YieldIfNotNull>d__9(int <>1__state)
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
                    if (this.singleElement != null)
                    {
                        this.<>2__current = this.singleElement;
                        this.<>1__state = 1;
                        return true;
                    }
                }
                return false;
            }

            [DebuggerHidden]
            IEnumerator<T> IEnumerable<T>.GetEnumerator()
            {
                LinqExtensions.<YieldIfNotNull>d__9<T> d__;
                if ((this.<>1__state != -2) || (this.<>l__initialThreadId != Environment.CurrentManagedThreadId))
                {
                    d__ = new LinqExtensions.<YieldIfNotNull>d__9<T>(0);
                }
                else
                {
                    this.<>1__state = 0;
                    d__ = (LinqExtensions.<YieldIfNotNull>d__9<T>) this;
                }
                d__.singleElement = this.<>3__singleElement;
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
            }

            T IEnumerator<T>.Current =>
                this.<>2__current;

            object IEnumerator.Current =>
                this.<>2__current;
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct EnumeratorAndLevel<T>
        {
            public readonly IEnumerator<T> En;
            public readonly int Level;
            public EnumeratorAndLevel(IEnumerator<T> en, int level)
            {
                this.En = en;
                this.Level = level;
            }
        }
    }
}

