namespace DevExpress.Xpf.Core.Native
{
    using DevExpress.Data.Filtering;
    using System;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    public static class CriteriaRangeHelper
    {
        internal static Attributed<T> Attribute<T>(this T value, string propertyName);
        private static ValueData FromOperatorWithDateTime(CriteriaOperator theOperator);
        private static ValueData FromRegularOperator(CriteriaOperator theOperator);
        private static ValueData FromRoundDateOperator(CriteriaOperator theOperator);
        private static Attributed<CriteriaRangeHelper.ValueSide> GetPropertySide<T>(CriteriaOperator criteria, bool includeRightBoundary, Func<CriteriaOperator, ValueData> leftBoundaryParser, Func<CriteriaOperator, ValueData> rightBoundaryParser) where T: struct;
        private static BinaryOperatorType GetRevertedOperatorType(this BinaryOperatorType trackingOperatorType);
        private static bool IsRoundDate(ValueData valueData);
        public static bool IsSingleDay(this ValueDataRange range);
        private static ValueData ModifyDate(ValueData valueData, Func<DateTime, DateTime> update);
        public static Tuple<object, object> TryConvertToValueRange(this Attributed<ValueDataRange> range);
        private static bool TryGetPropertySide<T>(CriteriaOperator criteria, BinaryOperatorType trackingOperatorType, CriteriaRangeHelper.Side side, Func<CriteriaOperator, ValueData> boundaryParser, out Attributed<CriteriaRangeHelper.ValueSide> valueSide) where T: struct;
        private static Attributed<ValueDataRange> TryGetRange<T>(GroupOperator group, bool includeRightBoundary, Func<CriteriaOperator, ValueData> leftBoundaryParser, Func<CriteriaOperator, ValueData> rightBoundaryParser) where T: struct, IComparable<T>;
        public static Attributed<ValueDataRange> TryGetRegularRange<T>(GroupOperator group, bool includeRightBoundary) where T: struct, IComparable<T>;
        public static Attributed<ValueDataRange> TryGetRoundedDateTimeRange(GroupOperator group, bool passNonRoundDates);
        public static object TryGetValue(this ValueData valueData);
        private static bool TryParseBinaryOperator(this BinaryOperator theOperator, BinaryOperatorType trackingOperatorType, out CriteriaOperator leftOperand, out CriteriaOperator rightOperand);
        private static Attributed<ValueDataRange> ValidateDateRange(this Attributed<ValueDataRange> range, bool passNonRoundDates);

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly CriteriaRangeHelper.<>c <>9;
            public static Func<object, object, bool> <>9__2_0;
            public static Func<string, string, bool> <>9__2_1;
            public static Func<string, string, bool> <>9__2_2;
            public static Func<LocalDateTimeFunction, LocalDateTimeFunction, bool> <>9__2_3;
            public static Func<bool> <>9__2_4;
            public static Func<object, bool> <>9__8_0;
            public static Func<bool> <>9__8_1;
            public static Func<object, bool> <>9__9_0;
            public static Func<LocalDateTimeFunction, bool> <>9__9_1;
            public static Func<bool> <>9__9_2;
            public static Func<DateTime, DateTime> <>9__11_4;
            public static Func<ValueData, ValueData> <>9__11_3;
            public static Func<DateTime, DateTime> <>9__11_6;
            public static Func<ValueData, ValueData> <>9__11_5;
            public static Func<ValueDataRange, ValueDataRange> <>9__11_2;
            public static Func<Attributed<ValueDataRange>, Attributed<ValueDataRange>> <>9__11_1;
            public static Func<object, bool> <>9__12_0;
            public static Func<bool> <>9__12_1;
            public static Func<object, object> <>9__17_0;
            public static Func<object> <>9__17_1;

            static <>c();
            internal bool <FromOperatorWithDateTime>b__8_0(object value);
            internal bool <FromOperatorWithDateTime>b__8_1();
            internal bool <FromRoundDateOperator>b__9_0(object _);
            internal bool <FromRoundDateOperator>b__9_1(LocalDateTimeFunction _);
            internal bool <FromRoundDateOperator>b__9_2();
            internal bool <IsRoundDate>b__12_0(object value);
            internal bool <IsRoundDate>b__12_1();
            internal bool <IsSingleDay>b__2_0(object left, object right);
            internal bool <IsSingleDay>b__2_1(string left, string right);
            internal bool <IsSingleDay>b__2_2(string left, string right);
            internal bool <IsSingleDay>b__2_3(LocalDateTimeFunction left, LocalDateTimeFunction right);
            internal bool <IsSingleDay>b__2_4();
            internal object <TryGetValue>b__17_0(object value);
            internal object <TryGetValue>b__17_1();
            internal Attributed<ValueDataRange> <ValidateDateRange>b__11_1(Attributed<ValueDataRange> r);
            internal ValueDataRange <ValidateDateRange>b__11_2(ValueDataRange x);
            internal ValueData <ValidateDateRange>b__11_3(ValueData valueData);
            internal DateTime <ValidateDateRange>b__11_4(DateTime date);
            internal ValueData <ValidateDateRange>b__11_5(ValueData valueData);
            internal DateTime <ValidateDateRange>b__11_6(DateTime date);
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c__10<T> where T: struct, IComparable<T>
        {
            public static readonly CriteriaRangeHelper.<>c__10<T> <>9;
            public static Func<T?> <>9__10_0;
            public static Func<T?> <>9__10_1;

            static <>c__10();
            internal T? <TryGetRange>b__10_0();
            internal T? <TryGetRange>b__10_1();
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c__5<T> where T: struct
        {
            public static readonly CriteriaRangeHelper.<>c__5<T> <>9;
            public static Func<OperandProperty, string> <>9__5_0;
            public static Func<CriteriaOperator, string> <>9__5_1;

            static <>c__5();
            internal string <TryGetPropertySide>b__5_0(OperandProperty prop);
            internal string <TryGetPropertySide>b__5_1(CriteriaOperator _);
        }

        private enum Side
        {
            public const CriteriaRangeHelper.Side Left = CriteriaRangeHelper.Side.Left;,
            public const CriteriaRangeHelper.Side Right = CriteriaRangeHelper.Side.Right;
        }

        private class ValueSide
        {
            public readonly DevExpress.Xpf.Core.Native.CriteriaRangeHelper.Side Side;
            public readonly DevExpress.Xpf.Core.Native.ValueData ValueData;

            public ValueSide(DevExpress.Xpf.Core.Native.CriteriaRangeHelper.Side side, DevExpress.Xpf.Core.Native.ValueData valueData);
        }
    }
}

