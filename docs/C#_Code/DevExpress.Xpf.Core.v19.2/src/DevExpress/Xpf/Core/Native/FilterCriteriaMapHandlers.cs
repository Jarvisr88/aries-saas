namespace DevExpress.Xpf.Core.Native
{
    using DevExpress.Data.Filtering;
    using DevExpress.Mvvm.Native;
    using System;
    using System.Runtime.CompilerServices;

    internal static class FilterCriteriaMapHandlers
    {
        private static BinaryOperatorType? Invert(this BinaryOperatorType type);
        public static Option<T> MapBetweenEx<T>(this BetweenOperator theOperator, Func<string, ValueData, ValueData, Option<T>> map);
        public static Option<T> MapBinaryEx<T>(this BinaryOperator theOperator, Func<string, ValueData, BinaryOperatorType, Option<T>> map);
        public static Option<T> MapFunctionEx<T>(this FunctionOperator theOperator, Func<string, ValueData[], FunctionOperatorType, Option<T>> map);
        public static Option<T> MapInEx<T>(this InOperator theOperator, Func<string, ValueData[], Option<T>> map);

        [Serializable, CompilerGenerated]
        private sealed class <>c__1<T>
        {
            public static readonly FilterCriteriaMapHandlers.<>c__1<T> <>9;
            public static Func<CriteriaOperator, ValueData> <>9__1_0;
            public static Func<ValueData, bool> <>9__1_1;

            static <>c__1();
            internal ValueData <MapInEx>b__1_0(CriteriaOperator x);
            internal bool <MapInEx>b__1_1(ValueData x);
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c__3<T>
        {
            public static readonly FilterCriteriaMapHandlers.<>c__3<T> <>9;
            public static Func<CriteriaOperator, ValueData> <>9__3_0;
            public static Func<ValueData, bool> <>9__3_1;

            static <>c__3();
            internal ValueData <MapFunctionEx>b__3_0(CriteriaOperator x);
            internal bool <MapFunctionEx>b__3_1(ValueData x);
        }
    }
}

