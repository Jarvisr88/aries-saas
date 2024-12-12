namespace DevExpress.Data.Helpers
{
    using System;
    using System.Collections.Concurrent;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    public static class GenericDelegateHelper
    {
        private static readonly ConcurrentDictionary<GenericHelpersParts.TypesDuple, Delegate> Conversions;

        static GenericDelegateHelper();
        public static Func<A, R> ApplyChain<A, R>(this Delegate nested, Delegate wrapper, Type intermediateType);
        public static Delegate ApplyChain<I, R>(this Delegate nested, Func<I, R> wrapper, Type nestedArgType);
        public static Delegate ApplyChain<A, I>(this Func<A, I> nested, Delegate wrapper, Type wrapperResultType);
        public static Delegate ApplyChain(this Delegate nested, Delegate wrapper, Type nestedArgType, Type intermediateType, Type wrapperResultType);
        public static Func<A, R> ConvertFunc<A, R>(this Delegate func, Type currentArgumentType, Type currentResultType, Func<string[]> exceptionAuxInfoGetter = null);
        public static Delegate ConvertFunc<A, R>(this Func<A, R> func, Type expectedArgumentType, Type expectedResultType, Func<string[]> exceptionAuxInfoGetter = null);
        public static Delegate ConvertFunc(this Delegate func, Type currentArgumentType, Type currentResultType, Type expectedArgumentType, Type expectedResultType, Func<string[]> exceptionAuxInfoGetter = null);
        public static Func<A, R> ConvertFuncArgument<A, R>(this Delegate func, Type currentArgumentType, Func<string[]> exceptionAuxInfoGetter = null);
        public static Delegate ConvertFuncArgument<A, R>(this Func<A, R> func, Type expectedArgumentType, Func<string[]> exceptionAuxInfoGetter = null);
        public static Delegate ConvertFuncArgument(this Delegate func, Type currentArgumentType, Type resultType, Type expectedArgumentType, Func<string[]> exceptionAuxInfoGetter = null);
        public static Func<A, R> ConvertFuncResult<A, R>(this Delegate func, Type currentResultType, Func<string[]> exceptionAuxInfoGetter = null);
        public static Delegate ConvertFuncResult<A, R>(this Func<A, R> func, Type expectedResultType, Func<string[]> exceptionAuxInfoGetter = null);
        public static Delegate ConvertFuncResult(this Delegate func, Type argumentType, Type currentResultType, Type expectedResultType, Func<string[]> exceptionAuxInfoGetter = null);
        private static Delegate CreateConversion(Type from, Type to);
        public static Func<From, To> GetCastFunc<From, To>(Func<string[]> exceptionAuxInfoGetter = null);
        public static Delegate GetCastFunc(Type from, Type to, Func<string[]> exceptionAuxInfoGetter = null);
        private static Delegate GetCastFuncCore(Type from, Type to);
        public static Delegate HedgeNullArg(Delegate dlg, Type mayBeNullArgumentType, Type resultType);
        public static Func<T, bool> MakeNullCheck<T>();
        public static Func<T, bool> MakeNullCheck<T>(bool isNotCheck);
        public static Delegate MakeNullCheck(Type argType);
        public static Delegate MakeNullCheck(Type argType, bool isNotCheck);

        public abstract class BoxingConversionCreator : GenericInvoker<Func<Delegate>, GenericDelegateHelper.BoxingConversionCreator.Impl<object>>
        {
            protected BoxingConversionCreator();

            public class Impl<T> : GenericDelegateHelper.BoxingConversionCreator
            {
                private static object Box(T arg);
                protected override Func<Delegate> CreateInvoker();

                [Serializable, CompilerGenerated]
                private sealed class <>c
                {
                    public static readonly GenericDelegateHelper.BoxingConversionCreator.Impl<T>.<>c <>9;
                    public static Func<Delegate> <>9__1_0;

                    static <>c();
                    internal Delegate <CreateInvoker>b__1_0();
                }
            }
        }

        public abstract class ChainApplier : GenericInvoker<Func<Delegate, Delegate, Delegate>, GenericDelegateHelper.ChainApplier.Impl<object, object, object>>
        {
            protected ChainApplier();

            public class Impl<A, I, R> : GenericDelegateHelper.ChainApplier
            {
                private static Func<A, R> ApplyChain(Func<A, I> nested, Func<I, R> wrapper);
                protected override Func<Delegate, Delegate, Delegate> CreateInvoker();

                [Serializable, CompilerGenerated]
                private sealed class <>c
                {
                    public static readonly GenericDelegateHelper.ChainApplier.Impl<A, I, R>.<>c <>9;
                    public static Func<Delegate, Delegate, Delegate> <>9__1_0;

                    static <>c();
                    internal Delegate <CreateInvoker>b__1_0(Delegate nested, Delegate wrapper);
                }
            }
        }

        public abstract class InvalidCastVerboseLogger : GenericInvoker<Func<Delegate, Func<string[]>, Delegate>, GenericDelegateHelper.InvalidCastVerboseLogger.Impl<object, object>>
        {
            public static bool Force;

            protected InvalidCastVerboseLogger();

            public class Impl<A, R> : GenericDelegateHelper.InvalidCastVerboseLogger
            {
                protected override Func<Delegate, Func<string[]>, Delegate> CreateInvoker();
                private static Func<A, R> CreateLoggedCast(Func<A, R> nakedCast, Func<string[]> exceptionAuxInfoGetter = null);
                private static R LoggedCast(Func<A, R> nakedCast, A arg, Func<string[]> exceptionAuxInfoGetter = null);

                [Serializable, CompilerGenerated]
                private sealed class <>c
                {
                    public static readonly GenericDelegateHelper.InvalidCastVerboseLogger.Impl<A, R>.<>c <>9;
                    public static Func<string, string> <>9__0_0;
                    public static Func<Delegate, Func<string[]>, Delegate> <>9__2_0;

                    static <>c();
                    internal Delegate <CreateInvoker>b__2_0(Delegate nakedCast, Func<string[]> exceptionAuxInfoGetter);
                    internal string <LoggedCast>b__0_0(string s);
                }
            }
        }

        public abstract class Maybe_NullArgHedger : GenericInvoker<Func<Delegate, Delegate>, GenericDelegateHelper.Maybe_NullArgHedger.Impl<object, object>>
        {
            protected Maybe_NullArgHedger();

            public class Impl<A, R> : GenericDelegateHelper.Maybe_NullArgHedger
            {
                protected override Func<Delegate, Delegate> CreateInvoker();
                private static Func<A, R> Hedge(Func<A, R> nakedFunc);

                [Serializable, CompilerGenerated]
                private sealed class <>c
                {
                    public static readonly GenericDelegateHelper.Maybe_NullArgHedger.Impl<A, R>.<>c <>9;
                    public static Func<Delegate, Delegate> <>9__1_0;

                    static <>c();
                    internal Delegate <CreateInvoker>b__1_0(Delegate nakedFunc);
                }
            }
        }

        public abstract class NullCheckCreatorClass : GenericInvoker<Func<bool, Delegate>, GenericDelegateHelper.NullCheckCreatorClass.Impl<object>>
        {
            protected NullCheckCreatorClass();

            public class Impl<T> : GenericDelegateHelper.NullCheckCreatorClass where T: class
            {
                protected override Func<bool, Delegate> CreateInvoker();
                private static bool IsNotNull(T arg);
                private static bool IsNull(T arg);

                [Serializable, CompilerGenerated]
                private sealed class <>c
                {
                    public static readonly GenericDelegateHelper.NullCheckCreatorClass.Impl<T>.<>c <>9;
                    public static Func<bool, Delegate> <>9__2_0;

                    static <>c();
                    internal Delegate <CreateInvoker>b__2_0(bool isNotNull);
                }
            }
        }

        public abstract class NullCheckCreatorNullableStruct : GenericInvoker<Func<bool, Delegate>, GenericDelegateHelper.NullCheckCreatorNullableStruct.Impl<bool>>
        {
            protected NullCheckCreatorNullableStruct();

            public class Impl<T> : GenericDelegateHelper.NullCheckCreatorNullableStruct where T: struct
            {
                protected override Func<bool, Delegate> CreateInvoker();
                private static bool IsNotNull(T? arg);
                private static bool IsNull(T? arg);

                [Serializable, CompilerGenerated]
                private sealed class <>c
                {
                    public static readonly GenericDelegateHelper.NullCheckCreatorNullableStruct.Impl<T>.<>c <>9;
                    public static Func<bool, Delegate> <>9__2_0;

                    static <>c();
                    internal Delegate <CreateInvoker>b__2_0(bool isNotNull);
                }
            }
        }

        public abstract class ToNullableConversionCreator : GenericInvoker<Func<Delegate>, GenericDelegateHelper.ToNullableConversionCreator.Impl<int>>
        {
            protected ToNullableConversionCreator();

            public class Impl<T> : GenericDelegateHelper.ToNullableConversionCreator where T: struct
            {
                protected override Func<Delegate> CreateInvoker();
                private static T? Nullise(T arg);

                [Serializable, CompilerGenerated]
                private sealed class <>c
                {
                    public static readonly GenericDelegateHelper.ToNullableConversionCreator.Impl<T>.<>c <>9;
                    public static Func<Delegate> <>9__1_0;

                    static <>c();
                    internal Delegate <CreateInvoker>b__1_0();
                }
            }
        }

        public abstract class UnboxingConversionCreator : GenericInvoker<Func<Delegate>, GenericDelegateHelper.UnboxingConversionCreator.Impl<object>>
        {
            protected UnboxingConversionCreator();

            public class Impl<T> : GenericDelegateHelper.UnboxingConversionCreator
            {
                protected override Func<Delegate> CreateInvoker();
                private static T Unbox(object arg);

                [Serializable, CompilerGenerated]
                private sealed class <>c
                {
                    public static readonly GenericDelegateHelper.UnboxingConversionCreator.Impl<T>.<>c <>9;
                    public static Func<Delegate> <>9__1_0;

                    static <>c();
                    internal Delegate <CreateInvoker>b__1_0();
                }
            }
        }
    }
}

