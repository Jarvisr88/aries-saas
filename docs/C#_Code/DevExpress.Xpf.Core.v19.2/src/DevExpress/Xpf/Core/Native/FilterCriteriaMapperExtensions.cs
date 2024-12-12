namespace DevExpress.Xpf.Core.Native
{
    using DevExpress.Data.Filtering;
    using DevExpress.Mvvm.Native;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    public static class FilterCriteriaMapperExtensions
    {
        private static Option<T[]> Collapse<T>(this IEnumerable<Option<T>> source);
        public static T Map<T>(this CriteriaOperator criteria, BinaryOperatorMapper<T> binary = null, UnaryOperatorMapper<T> unary = null, InOperatorMapper<T> @in = null, BetweenOperatorMapper<T> between = null, FunctionOperatorMapper<T> function = null, GroupOperatorMapper<T> and = null, GroupOperatorMapper<T> or = null, NotOperatorMapper<T> not = null, FallbackMapper<T> fallback = null, NullMapper<T> @null = null);
        public static T MapExtended<T>(this CriteriaOperator criteria, Func<string, ValueData, BinaryOperatorType, Option<T>> binary = null, UnaryOperatorMapper<T> unary = null, Func<string, ValueData[], Option<T>> @in = null, Func<string, ValueData, ValueData, Option<T>> between = null, Func<string, ValueData[], FunctionOperatorType, Option<T>> function = null, GroupOperatorMapper<T> and = null, GroupOperatorMapper<T> or = null, NotOperatorMapper<T> not = null, FallbackMapper<T> fallback = null, NullMapper<T> @null = null);

        [Serializable, CompilerGenerated]
        private sealed class <>c__0<T>
        {
            public static readonly FilterCriteriaMapperExtensions.<>c__0<T> <>9;
            public static Func<object, Option<object>> <>9__0_10;
            public static Func<Option<object>> <>9__0_11;
            public static Func<ValueData, Option<object>> <>9__0_9;
            public static Func<object, Option<object>> <>9__0_17;
            public static Func<Option<object>> <>9__0_18;
            public static Func<object, Option<object>> <>9__0_19;
            public static Func<Option<object>> <>9__0_20;
            public static Func<object, Option<object>> <>9__0_27;
            public static Func<Option<object>> <>9__0_28;
            public static Func<ValueData, Option<object>> <>9__0_26;

            static <>c__0();
            internal Option<object> <Map>b__0_10(object y);
            internal Option<object> <Map>b__0_11();
            internal Option<object> <Map>b__0_17(object y);
            internal Option<object> <Map>b__0_18();
            internal Option<object> <Map>b__0_19(object y);
            internal Option<object> <Map>b__0_20();
            internal Option<object> <Map>b__0_26(ValueData x);
            internal Option<object> <Map>b__0_27(object y);
            internal Option<object> <Map>b__0_28();
            internal Option<object> <Map>b__0_9(ValueData x);
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c__1<T>
        {
            public static readonly FilterCriteriaMapperExtensions.<>c__1<T> <>9;
            public static Func<Option<T>, bool> <>9__1_0;
            public static Func<Option<T>, T> <>9__1_1;

            static <>c__1();
            internal bool <Collapse>b__1_0(Option<T> x);
            internal T <Collapse>b__1_1(Option<T> x);
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c__2<T>
        {
            public static readonly FilterCriteriaMapperExtensions.<>c__2<T> <>9;
            public static Func<Func<string, ValueData, BinaryOperatorType, Option<T>>, Func<BinaryOperator, Option<T>>> <>9__2_0;
            public static Func<Func<string, ValueData[], Option<T>>, Func<InOperator, Option<T>>> <>9__2_2;
            public static Func<Func<string, ValueData, ValueData, Option<T>>, Func<BetweenOperator, Option<T>>> <>9__2_4;
            public static Func<Func<string, ValueData[], FunctionOperatorType, Option<T>>, Func<FunctionOperator, Option<T>>> <>9__2_6;

            static <>c__2();
            internal Func<BinaryOperator, Option<T>> <MapExtended>b__2_0(Func<string, ValueData, BinaryOperatorType, Option<T>> x);
            internal Func<InOperator, Option<T>> <MapExtended>b__2_2(Func<string, ValueData[], Option<T>> x);
            internal Func<BetweenOperator, Option<T>> <MapExtended>b__2_4(Func<string, ValueData, ValueData, Option<T>> x);
            internal Func<FunctionOperator, Option<T>> <MapExtended>b__2_6(Func<string, ValueData[], FunctionOperatorType, Option<T>> x);
        }
    }
}

