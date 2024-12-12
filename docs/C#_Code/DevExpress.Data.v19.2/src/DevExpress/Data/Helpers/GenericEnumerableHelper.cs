namespace DevExpress.Data.Helpers
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    public static class GenericEnumerableHelper
    {
        public static IEnumerable ApplyCast<T>(this IEnumerable<T> src, Type castToType, Func<string[]> exceptionAuxInfoGetter = null);
        public static IEnumerable<T> ApplyCast<T>(this IEnumerable src, Type srcGenericArgument, Func<string[]> exceptionAuxInfoGetter = null);
        public static IEnumerable ApplyCast(this IEnumerable src, Type srcGenericArgument, Type castToType, Func<string[]> exceptionAuxInfoGetter = null);
        public static int ApplyCount(this IEnumerable src, Type srcGenericArgument);
        public static IEnumerable ApplySelect<TSource>(this IEnumerable<TSource> src, Delegate selector, Type selectorResultType);
        public static IEnumerable<TResult> ApplySelect<TResult>(this IEnumerable src, Type srcGenericArgument, Delegate selector);
        public static IEnumerable ApplySelect(this IEnumerable src, Type srcGenericArgument, Delegate selector, Type selectorResultType);
        public static Array ApplyToArray(this IEnumerable src, Type srcGenericArgument);
        public static IEnumerable ApplyWhere(this IEnumerable src, Delegate predicate, Type srcGenericArgument);
        public static IEnumerable ApplyWhereNotNull(this IEnumerable src, Type srcGenericArgument);

        public abstract class CountApplier : GenericInvoker<Func<IEnumerable, int>, GenericEnumerableHelper.CountApplier.Impl<object>>
        {
            protected CountApplier();

            public class Impl<T> : GenericEnumerableHelper.CountApplier
            {
                private static int ApplyCount(IEnumerable<T> src);
                protected override Func<IEnumerable, int> CreateInvoker();

                [Serializable, CompilerGenerated]
                private sealed class <>c
                {
                    public static readonly GenericEnumerableHelper.CountApplier.Impl<T>.<>c <>9;
                    public static Func<IEnumerable, int> <>9__1_0;

                    static <>c();
                    internal int <CreateInvoker>b__1_0(IEnumerable src);
                }
            }
        }

        public abstract class SelectApplier : GenericInvoker<Func<IEnumerable, Delegate, IEnumerable>, GenericEnumerableHelper.SelectApplier.Impl<object, object>>
        {
            protected SelectApplier();

            public class Impl<T, R> : GenericEnumerableHelper.SelectApplier
            {
                private static IEnumerable<R> ApplySelect(IEnumerable<T> src, Func<T, R> selector);
                protected override Func<IEnumerable, Delegate, IEnumerable> CreateInvoker();

                [Serializable, CompilerGenerated]
                private sealed class <>c
                {
                    public static readonly GenericEnumerableHelper.SelectApplier.Impl<T, R>.<>c <>9;
                    public static Func<IEnumerable, Delegate, IEnumerable> <>9__1_0;

                    static <>c();
                    internal IEnumerable <CreateInvoker>b__1_0(IEnumerable src, Delegate selector);
                }
            }
        }

        public abstract class ToArrayApplier : GenericInvoker<Func<IEnumerable, Array>, GenericEnumerableHelper.ToArrayApplier.Impl<object>>
        {
            protected ToArrayApplier();

            public class Impl<T> : GenericEnumerableHelper.ToArrayApplier
            {
                private static T[] ApplyToArray(IEnumerable<T> src);
                protected override Func<IEnumerable, Array> CreateInvoker();

                [Serializable, CompilerGenerated]
                private sealed class <>c
                {
                    public static readonly GenericEnumerableHelper.ToArrayApplier.Impl<T>.<>c <>9;
                    public static Func<IEnumerable, Array> <>9__1_0;

                    static <>c();
                    internal Array <CreateInvoker>b__1_0(IEnumerable src);
                }
            }
        }

        public abstract class WhereApplier : GenericInvoker<Func<IEnumerable, Delegate, IEnumerable>, GenericEnumerableHelper.WhereApplier.Impl<object>>
        {
            protected WhereApplier();

            public class Impl<T> : GenericEnumerableHelper.WhereApplier
            {
                private static IEnumerable<T> Apply(IEnumerable<T> src, Func<T, bool> where);
                protected override Func<IEnumerable, Delegate, IEnumerable> CreateInvoker();

                [Serializable, CompilerGenerated]
                private sealed class <>c
                {
                    public static readonly GenericEnumerableHelper.WhereApplier.Impl<T>.<>c <>9;
                    public static Func<IEnumerable, Delegate, IEnumerable> <>9__1_0;

                    static <>c();
                    internal IEnumerable <CreateInvoker>b__1_0(IEnumerable enumerable, Delegate predicate);
                }
            }
        }
    }
}

