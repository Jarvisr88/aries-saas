namespace DevExpress.Xpf.Core.Native
{
    using DevExpress.Data.Filtering;
    using DevExpress.Data.Filtering.Helpers;
    using DevExpress.Mvvm.Native;
    using DevExpress.Xpf.Editors.Settings;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public static class BetweenDatesHelper
    {
        public static readonly string BetweenDatesCustomFunctionName;
        public static readonly string IsOnDatesCustomFunctionName;
        private static readonly DateTime probe;

        static BetweenDatesHelper();
        public static bool CanSubstitute(TextEditSettings settings);
        public static CriteriaOperator CreateBetweenDatesFunction(OperandProperty property, ValueData from, ValueData to);
        private static CriteriaOperator CreateDateGreaterCriteria(OperandProperty property, ValueData valueData);
        public static CriteriaOperator CreateIsOnDatesFunction(OperandProperty property, IEnumerable<ValueData> valuesData);
        public static bool DateFormatHasHoursComponent(string format);
        public static bool IsBetweenDatesFunction(ValueData[] values, FunctionOperatorType operatorType);
        private static bool IsDatesFunction(ValueData value, FunctionOperatorType operatorType, string customFunctionName);
        public static bool IsIsOnDatesFunction(ValueData[] values, FunctionOperatorType operatorType);
        internal static bool IsMaxDate(this ValueData valueData);
        public static CriteriaOperator RemoveDateInRange(CriteriaOperator criteria);
        public static CriteriaOperator SubstituteDateInRange(CriteriaOperator criteria);
        public static Attributed<ValueData[]> TryGetPropertyValuesFromSubstituted<T>(FunctionOperator criteria);
        public static Attributed<ValueData[]> TryGetPropertyValuesFromSubstituted<T>(string propertyName, ValueData[] values, FunctionOperatorType operatorType);
        public static Attributed<ValueDataRange> TryGetRangeFromSubstituted<T>(FunctionOperator criteria);
        public static Attributed<ValueDataRange> TryGetRangeFromSubstituted<T>(string propertyName, ValueData[] values, FunctionOperatorType operatorType);

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly BetweenDatesHelper.<>c <>9;
            public static Func<bool> <>9__6_1;
            public static Func<ValueData, CriteriaOperator> <>9__10_0;
            public static Func<object, bool> <>9__12_0;
            public static Func<bool> <>9__12_1;
            public static Func<CriteriaOperator, CriteriaOperator> <>9__15_0;
            public static Func<CriteriaOperator, CriteriaOperator> <>9__16_0;

            static <>c();
            internal CriteriaOperator <CreateIsOnDatesFunction>b__10_0(ValueData x);
            internal bool <IsDatesFunction>b__6_1();
            internal bool <IsMaxDate>b__12_0(object value);
            internal bool <IsMaxDate>b__12_1();
            internal CriteriaOperator <RemoveDateInRange>b__16_0(CriteriaOperator x);
            internal CriteriaOperator <SubstituteDateInRange>b__15_0(CriteriaOperator x);
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c__2<T>
        {
            public static readonly BetweenDatesHelper.<>c__2<T> <>9;
            public static Func<string, ValueData[], FunctionOperatorType, Option<Attributed<ValueDataRange>>> <>9__2_0;
            public static FallbackMapper<Attributed<ValueDataRange>> <>9__2_1;

            static <>c__2();
            internal Option<Attributed<ValueDataRange>> <TryGetRangeFromSubstituted>b__2_0(string name, ValueData[] values, FunctionOperatorType type);
            internal Attributed<ValueDataRange> <TryGetRangeFromSubstituted>b__2_1(CriteriaOperator _);
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c__3<T>
        {
            public static readonly BetweenDatesHelper.<>c__3<T> <>9;
            public static Func<ValueData, bool> <>9__3_0;

            static <>c__3();
            internal bool <TryGetRangeFromSubstituted>b__3_0(ValueData x);
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c__7<T>
        {
            public static readonly BetweenDatesHelper.<>c__7<T> <>9;
            public static Func<string, ValueData[], FunctionOperatorType, Option<Attributed<ValueData[]>>> <>9__7_0;
            public static FallbackMapper<Attributed<ValueData[]>> <>9__7_1;

            static <>c__7();
            internal Option<Attributed<ValueData[]>> <TryGetPropertyValuesFromSubstituted>b__7_0(string name, ValueData[] values, FunctionOperatorType type);
            internal Attributed<ValueData[]> <TryGetPropertyValuesFromSubstituted>b__7_1(CriteriaOperator _);
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c__8<T>
        {
            public static readonly BetweenDatesHelper.<>c__8<T> <>9;
            public static Func<ValueData, bool> <>9__8_0;

            static <>c__8();
            internal bool <TryGetPropertyValuesFromSubstituted>b__8_0(ValueData x);
        }

        private class DateInRangeSubstituter : ClientCriteriaLazyPatcherBase
        {
            private readonly BetweenDatesHelper.Mode mode;

            public DateInRangeSubstituter(BetweenDatesHelper.Mode mode);
            private static CriteriaOperator CreateLeftBorderCriteria(ValueData valueData);
            private static CriteriaOperator CreateRightBorderCriteria(ValueData valueData);
            private static GroupOperator MakeRange(OperandProperty property, ValueData from, ValueData to);
            public override CriteriaOperator Visit(AggregateOperand theOperand);
            public override CriteriaOperator Visit(FunctionOperator theOperator);
            public override CriteriaOperator Visit(GroupOperator theOperator);
            public override CriteriaOperator Visit(JoinOperand theOperand);

            [Serializable, CompilerGenerated]
            private sealed class <>c
            {
                public static readonly BetweenDatesHelper.DateInRangeSubstituter.<>c <>9;
                public static Func<CriteriaOperator, bool> <>9__2_0;
                public static Func<GroupOperator, Attributed<ValueDataRange>> <>9__2_1;
                public static Func<Attributed<ValueDataRange>, bool> <>9__2_3;
                public static Func<Attributed<ValueDataRange>, Attributed<ValueDataRange>, bool> <>9__2_4;
                public static Func<Attributed<ValueDataRange>[], bool> <>9__2_2;
                public static Func<Attributed<ValueDataRange>, bool> <>9__2_5;
                public static Func<Attributed<ValueDataRange>, ValueDataRange> <>9__2_6;
                public static Func<Attributed<ValueDataRange>, bool> <>9__2_7;
                public static Func<Attributed<ValueDataRange>, ValueData> <>9__2_8;
                public static Func<object, CriteriaOperator> <>9__6_0;

                static <>c();
                internal CriteriaOperator <CreateRightBorderCriteria>b__6_0(object value);
                internal bool <Visit>b__2_0(CriteriaOperator x);
                internal Attributed<ValueDataRange> <Visit>b__2_1(GroupOperator x);
                internal bool <Visit>b__2_2(Attributed<ValueDataRange>[] rangesCandidate);
                internal bool <Visit>b__2_3(Attributed<ValueDataRange> x);
                internal bool <Visit>b__2_4(Attributed<ValueDataRange> x, Attributed<ValueDataRange> y);
                internal bool <Visit>b__2_5(Attributed<ValueDataRange> x);
                internal ValueDataRange <Visit>b__2_6(Attributed<ValueDataRange> x);
                internal bool <Visit>b__2_7(Attributed<ValueDataRange> x);
                internal ValueData <Visit>b__2_8(Attributed<ValueDataRange> x);
            }
        }

        private enum Mode
        {
            public const BetweenDatesHelper.Mode Substitute = BetweenDatesHelper.Mode.Substitute;,
            public const BetweenDatesHelper.Mode Remove = BetweenDatesHelper.Mode.Remove;
        }
    }
}

