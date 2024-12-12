namespace DevExpress.Xpf.Core.Native
{
    using DevExpress.Data.Filtering;
    using DevExpress.Mvvm.Native;
    using System;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    public class ValueData
    {
        private static ValueData nullValue;
        private readonly object arg;
        private readonly ValueDataKind kind;

        private ValueData(object arg, ValueDataKind kind);
        private bool Equals(ValueData other);
        public override bool Equals(object obj);
        public static ValueData FromLocalDateTimeFunction(LocalDateTimeFunction function);
        public static ValueData FromOperator(CriteriaOperator operandValue);
        public static ValueData FromParameter(string parameter);
        public static ValueData FromProperty(string propertyName);
        public static ValueData FromValue(object value);
        public override int GetHashCode();
        public T Match<T>(Func<object, T> value = null, Func<string, T> propertyName = null, Func<string, T> parameter = null, Func<LocalDateTimeFunction, T> localDateTimeFunction = null, Func<T> fallback = null);
        public static T MatchForEqualKinds<T>(ValueData left, ValueData right, Func<object, object, T> values = null, Func<string, string, T> propertyNames = null, Func<string, string, T> parameters = null, Func<LocalDateTimeFunction, LocalDateTimeFunction, T> localDateTimeFunctions = null, Func<T> fallback = null);
        private static Option<LocalDateTimeFunction> ToLocalDateTimeFunction(FunctionOperatorType type);

        public static ValueData NullValue { get; }

        public bool IsNull { get; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly ValueData.<>c <>9;
            public static Func<OperandParameter, ValueData> <>9__1_0;
            public static Func<OperandProperty, ValueData> <>9__1_2;
            public static Func<OperandValue, ValueData> <>9__1_4;
            public static Predicate<FunctionOperator> <>9__1_6;
            public static Func<FunctionOperator, ValueData> <>9__1_7;
            public static Func<CriteriaOperator, ValueData> <>9__1_8;
            public static Func<CriteriaOperator, ValueData> <>9__1_5;
            public static Func<CriteriaOperator, ValueData> <>9__1_3;
            public static Func<CriteriaOperator, ValueData> <>9__1_1;

            static <>c();
            internal ValueData <FromOperator>b__1_0(OperandParameter parameter);
            internal ValueData <FromOperator>b__1_1(CriteriaOperator o1);
            internal ValueData <FromOperator>b__1_2(OperandProperty property);
            internal ValueData <FromOperator>b__1_3(CriteriaOperator o2);
            internal ValueData <FromOperator>b__1_4(OperandValue value);
            internal ValueData <FromOperator>b__1_5(CriteriaOperator o3);
            internal bool <FromOperator>b__1_6(FunctionOperator func);
            internal ValueData <FromOperator>b__1_7(FunctionOperator func);
            internal ValueData <FromOperator>b__1_8(CriteriaOperator o4);
        }
    }
}

