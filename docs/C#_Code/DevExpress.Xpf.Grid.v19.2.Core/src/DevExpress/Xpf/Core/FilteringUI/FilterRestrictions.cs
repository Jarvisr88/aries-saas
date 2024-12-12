namespace DevExpress.Xpf.Core.FilteringUI
{
    using DevExpress.Data.Filtering;
    using DevExpress.Data.Filtering.Helpers;
    using DevExpress.Mvvm.Native;
    using DevExpress.Xpf.Core.Native;
    using System;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    internal struct FilterRestrictions
    {
        public readonly DevExpress.Xpf.Core.FilteringUI.AllowedUnaryFilters AllowedUnaryFilters;
        public readonly DevExpress.Xpf.Core.FilteringUI.AllowedAnyOfFilters AllowedAnyOfFilters;
        public readonly DevExpress.Xpf.Core.FilteringUI.AllowedDateTimeFilters AllowedDateTimeFilters;
        public readonly DevExpress.Xpf.Core.FilteringUI.AllowedBinaryFilters AllowedBinaryFilters;
        public readonly DevExpress.Xpf.Core.FilteringUI.AllowedBetweenFilters AllowedBetweenFilters;
        public readonly DevExpress.Xpf.Core.FilteringUI.AllowedCustomDateFilters AllowedCustomDateFilters;
        public readonly DevExpress.Xpf.Core.FilteringUI.AllowedDataAnalysisFilters AllowedDataAnalysisFilters;
        public static FilterRestrictions None() => 
            new FilterRestrictions(DevExpress.Xpf.Core.FilteringUI.AllowedUnaryFilters.None, DevExpress.Xpf.Core.FilteringUI.AllowedAnyOfFilters.None, DevExpress.Xpf.Core.FilteringUI.AllowedDateTimeFilters.None, DevExpress.Xpf.Core.FilteringUI.AllowedBinaryFilters.None, DevExpress.Xpf.Core.FilteringUI.AllowedBetweenFilters.None, DevExpress.Xpf.Core.FilteringUI.AllowedCustomDateFilters.None, DevExpress.Xpf.Core.FilteringUI.AllowedDataAnalysisFilters.None);

        public static FilterRestrictions All() => 
            new FilterRestrictions(DevExpress.Xpf.Core.FilteringUI.AllowedUnaryFilters.All, DevExpress.Xpf.Core.FilteringUI.AllowedAnyOfFilters.All, DevExpress.Xpf.Core.FilteringUI.AllowedDateTimeFilters.All, DevExpress.Xpf.Core.FilteringUI.AllowedBinaryFilters.All, DevExpress.Xpf.Core.FilteringUI.AllowedBetweenFilters.All, DevExpress.Xpf.Core.FilteringUI.AllowedCustomDateFilters.All, DevExpress.Xpf.Core.FilteringUI.AllowedDataAnalysisFilters.All);

        public FilterRestrictions(DevExpress.Xpf.Core.FilteringUI.AllowedUnaryFilters allowedUnaryFilters, DevExpress.Xpf.Core.FilteringUI.AllowedAnyOfFilters allowedAnyOfFilters, DevExpress.Xpf.Core.FilteringUI.AllowedDateTimeFilters allowedDateTimeFilters, DevExpress.Xpf.Core.FilteringUI.AllowedBinaryFilters allowedBinaryFilters, DevExpress.Xpf.Core.FilteringUI.AllowedBetweenFilters allowedBetweenFilters, DevExpress.Xpf.Core.FilteringUI.AllowedCustomDateFilters allowedCustomDateFilters, DevExpress.Xpf.Core.FilteringUI.AllowedDataAnalysisFilters dataAnalysis)
        {
            this.AllowedUnaryFilters = allowedUnaryFilters;
            this.AllowedAnyOfFilters = allowedAnyOfFilters;
            this.AllowedDateTimeFilters = allowedDateTimeFilters;
            this.AllowedBinaryFilters = allowedBinaryFilters;
            this.AllowedBetweenFilters = allowedBetweenFilters;
            this.AllowedCustomDateFilters = allowedCustomDateFilters;
            this.AllowedDataAnalysisFilters = dataAnalysis;
        }

        public FilterRestrictions Update(DevExpress.Xpf.Core.FilteringUI.AllowedAnyOfFilters? anyOf = new DevExpress.Xpf.Core.FilteringUI.AllowedAnyOfFilters?(), DevExpress.Xpf.Core.FilteringUI.AllowedUnaryFilters? unary = new DevExpress.Xpf.Core.FilteringUI.AllowedUnaryFilters?(), DevExpress.Xpf.Core.FilteringUI.AllowedDateTimeFilters? dateTime = new DevExpress.Xpf.Core.FilteringUI.AllowedDateTimeFilters?(), DevExpress.Xpf.Core.FilteringUI.AllowedBinaryFilters? binary = new DevExpress.Xpf.Core.FilteringUI.AllowedBinaryFilters?(), DevExpress.Xpf.Core.FilteringUI.AllowedBetweenFilters? between = new DevExpress.Xpf.Core.FilteringUI.AllowedBetweenFilters?(), DevExpress.Xpf.Core.FilteringUI.AllowedCustomDateFilters? customDate = new DevExpress.Xpf.Core.FilteringUI.AllowedCustomDateFilters?(), DevExpress.Xpf.Core.FilteringUI.AllowedDataAnalysisFilters? dataAnalysis = new DevExpress.Xpf.Core.FilteringUI.AllowedDataAnalysisFilters?())
        {
            DevExpress.Xpf.Core.FilteringUI.AllowedUnaryFilters? nullable = unary;
            DevExpress.Xpf.Core.FilteringUI.AllowedAnyOfFilters? nullable2 = anyOf;
            DevExpress.Xpf.Core.FilteringUI.AllowedDateTimeFilters? nullable3 = dateTime;
            DevExpress.Xpf.Core.FilteringUI.AllowedBinaryFilters? nullable4 = binary;
            DevExpress.Xpf.Core.FilteringUI.AllowedBetweenFilters? nullable5 = between;
            DevExpress.Xpf.Core.FilteringUI.AllowedCustomDateFilters? nullable6 = customDate;
            DevExpress.Xpf.Core.FilteringUI.AllowedDataAnalysisFilters? nullable7 = dataAnalysis;
            return new FilterRestrictions((nullable != null) ? nullable.GetValueOrDefault() : this.AllowedUnaryFilters, (nullable2 != null) ? nullable2.GetValueOrDefault() : this.AllowedAnyOfFilters, (nullable3 != null) ? nullable3.GetValueOrDefault() : this.AllowedDateTimeFilters, (nullable4 != null) ? nullable4.GetValueOrDefault() : this.AllowedBinaryFilters, (nullable5 != null) ? nullable5.GetValueOrDefault() : this.AllowedBetweenFilters, (nullable6 != null) ? nullable6.GetValueOrDefault() : this.AllowedCustomDateFilters, (nullable7 != null) ? nullable7.GetValueOrDefault() : this.AllowedDataAnalysisFilters);
        }

        public FilterRestrictions Intersect(FilterRestrictions restrictions) => 
            new FilterRestrictions(this.AllowedUnaryFilters & restrictions.AllowedUnaryFilters, this.AllowedAnyOfFilters & restrictions.AllowedAnyOfFilters, this.AllowedDateTimeFilters & restrictions.AllowedDateTimeFilters, this.AllowedBinaryFilters & restrictions.AllowedBinaryFilters, this.AllowedBetweenFilters & restrictions.AllowedBetweenFilters, this.AllowedCustomDateFilters & restrictions.AllowedCustomDateFilters, this.AllowedDataAnalysisFilters & restrictions.AllowedDataAnalysisFilters);

        public bool Allow(FunctionOperatorType type)
        {
            if (type == FunctionOperatorType.IsNullOrEmpty)
            {
                return this.AllowedUnaryFilters.HasFlag(DevExpress.Xpf.Core.FilteringUI.AllowedUnaryFilters.IsNullOrEmpty);
            }
            switch (type)
            {
                case FunctionOperatorType.StartsWith:
                    return this.AllowedBinaryFilters.HasFlag(DevExpress.Xpf.Core.FilteringUI.AllowedBinaryFilters.BeginsWith);

                case FunctionOperatorType.EndsWith:
                    return this.AllowedBinaryFilters.HasFlag(DevExpress.Xpf.Core.FilteringUI.AllowedBinaryFilters.EndsWith);

                case FunctionOperatorType.Contains:
                    return this.AllowedBinaryFilters.HasFlag(DevExpress.Xpf.Core.FilteringUI.AllowedBinaryFilters.Contains);
            }
            switch (type)
            {
                case FunctionOperatorType.IsOutlookIntervalBeyondThisYear:
                    return this.AllowedDateTimeFilters.HasFlag(DevExpress.Xpf.Core.FilteringUI.AllowedDateTimeFilters.IsBeyondThisYear);

                case FunctionOperatorType.IsOutlookIntervalLaterThisYear:
                    return this.AllowedDateTimeFilters.HasFlag(DevExpress.Xpf.Core.FilteringUI.AllowedDateTimeFilters.IsLaterThisYear);

                case FunctionOperatorType.IsOutlookIntervalLaterThisMonth:
                    return this.AllowedDateTimeFilters.HasFlag(DevExpress.Xpf.Core.FilteringUI.AllowedDateTimeFilters.IsLaterThisMonth);

                case FunctionOperatorType.IsOutlookIntervalNextWeek:
                    return this.AllowedDateTimeFilters.HasFlag(DevExpress.Xpf.Core.FilteringUI.AllowedDateTimeFilters.IsNextWeek);

                case FunctionOperatorType.IsOutlookIntervalLaterThisWeek:
                    return this.AllowedDateTimeFilters.HasFlag(DevExpress.Xpf.Core.FilteringUI.AllowedDateTimeFilters.IsLaterThisWeek);

                case FunctionOperatorType.IsOutlookIntervalTomorrow:
                    return this.AllowedDateTimeFilters.HasFlag(DevExpress.Xpf.Core.FilteringUI.AllowedDateTimeFilters.IsTomorrow);

                case FunctionOperatorType.IsOutlookIntervalToday:
                    return this.AllowedDateTimeFilters.HasFlag(DevExpress.Xpf.Core.FilteringUI.AllowedDateTimeFilters.IsToday);

                case FunctionOperatorType.IsOutlookIntervalYesterday:
                    return this.AllowedDateTimeFilters.HasFlag(DevExpress.Xpf.Core.FilteringUI.AllowedDateTimeFilters.IsYesterday);

                case FunctionOperatorType.IsOutlookIntervalEarlierThisWeek:
                    return this.AllowedDateTimeFilters.HasFlag(DevExpress.Xpf.Core.FilteringUI.AllowedDateTimeFilters.IsEarlierThisWeek);

                case FunctionOperatorType.IsOutlookIntervalLastWeek:
                    return this.AllowedDateTimeFilters.HasFlag(DevExpress.Xpf.Core.FilteringUI.AllowedDateTimeFilters.IsLastWeek);

                case FunctionOperatorType.IsOutlookIntervalEarlierThisMonth:
                    return this.AllowedDateTimeFilters.HasFlag(DevExpress.Xpf.Core.FilteringUI.AllowedDateTimeFilters.IsEarlierThisMonth);

                case FunctionOperatorType.IsOutlookIntervalEarlierThisYear:
                    return this.AllowedDateTimeFilters.HasFlag(DevExpress.Xpf.Core.FilteringUI.AllowedDateTimeFilters.IsEarlierThisYear);

                case FunctionOperatorType.IsOutlookIntervalPriorThisYear:
                    return this.AllowedDateTimeFilters.HasFlag(DevExpress.Xpf.Core.FilteringUI.AllowedDateTimeFilters.IsPriorThisYear);
            }
            return false;
        }

        private bool AllowInverted(FunctionOperatorType type) => 
            (type == FunctionOperatorType.IsNullOrEmpty) ? this.AllowedUnaryFilters.HasFlag(DevExpress.Xpf.Core.FilteringUI.AllowedUnaryFilters.IsNotNullOrEmpty) : ((type == FunctionOperatorType.Contains) && this.AllowedBinaryFilters.HasFlag(DevExpress.Xpf.Core.FilteringUI.AllowedBinaryFilters.DoesNotContain));

        public bool Allow(BinaryOperatorType type)
        {
            switch (type)
            {
                case BinaryOperatorType.Equal:
                    return this.AllowedBinaryFilters.HasFlag(DevExpress.Xpf.Core.FilteringUI.AllowedBinaryFilters.Equals);

                case BinaryOperatorType.NotEqual:
                    return this.AllowedBinaryFilters.HasFlag(DevExpress.Xpf.Core.FilteringUI.AllowedBinaryFilters.DoesNotEqual);

                case BinaryOperatorType.Greater:
                    return this.AllowedBinaryFilters.HasFlag(DevExpress.Xpf.Core.FilteringUI.AllowedBinaryFilters.Greater);

                case BinaryOperatorType.Less:
                    return this.AllowedBinaryFilters.HasFlag(DevExpress.Xpf.Core.FilteringUI.AllowedBinaryFilters.Less);

                case BinaryOperatorType.LessOrEqual:
                    return this.AllowedBinaryFilters.HasFlag(DevExpress.Xpf.Core.FilteringUI.AllowedBinaryFilters.LessOrEqual);

                case BinaryOperatorType.GreaterOrEqual:
                    return this.AllowedBinaryFilters.HasFlag(DevExpress.Xpf.Core.FilteringUI.AllowedBinaryFilters.GreaterOrEqual);

                case BinaryOperatorType.Like:
                    return this.AllowedBinaryFilters.HasFlag(DevExpress.Xpf.Core.FilteringUI.AllowedBinaryFilters.Like);
            }
            return false;
        }

        private bool AllowInverted(BinaryOperatorType type) => 
            (type == BinaryOperatorType.Like) && this.AllowedBinaryFilters.HasFlag(DevExpress.Xpf.Core.FilteringUI.AllowedBinaryFilters.NotLike);

        public bool Allow(CriteriaOperator filter)
        {
            Predicate<UnaryOperator> condition = <>c.<>9__16_0;
            if (<>c.<>9__16_0 == null)
            {
                Predicate<UnaryOperator> local1 = <>c.<>9__16_0;
                condition = <>c.<>9__16_0 = unary => unary.OperatorType == UnaryOperatorType.Not;
            }
            return filter.Transform<UnaryOperator, bool>(condition, delegate (UnaryOperator unaryNot) {
                Func<string, ValueData[], FunctionOperatorType, Option<bool>> <>9__6;
                Func<string, ValueData, ValueData, Option<bool>> <>9__5;
                Func<string, ValueData[], Option<bool>> <>9__4;
                UnaryOperatorMapper<bool> <>9__3;
                Func<string, ValueData, BinaryOperatorType, Option<bool>> <>9__2;
                Func<string, ValueData, BinaryOperatorType, Option<bool>> binary = <>9__2;
                if (<>9__2 == null)
                {
                    Func<string, ValueData, BinaryOperatorType, Option<bool>> local1 = <>9__2;
                    binary = <>9__2 = (_, __, type) => this.AllowInverted(type).AsOption<bool>();
                }
                UnaryOperatorMapper<bool> unary = <>9__3;
                if (<>9__3 == null)
                {
                    UnaryOperatorMapper<bool> local2 = <>9__3;
                    unary = <>9__3 = (_, type) => (type == UnaryOperatorType.IsNull) && this.AllowedUnaryFilters.HasFlag(DevExpress.Xpf.Core.FilteringUI.AllowedUnaryFilters.IsNotNull);
                }
                Func<string, ValueData[], Option<bool>> @in = <>9__4;
                if (<>9__4 == null)
                {
                    Func<string, ValueData[], Option<bool>> local3 = <>9__4;
                    @in = <>9__4 = (_, __) => this.AllowedAnyOfFilters.HasFlag(DevExpress.Xpf.Core.FilteringUI.AllowedAnyOfFilters.NoneOf).AsOption<bool>();
                }
                Func<string, ValueData, ValueData, Option<bool>> between = <>9__5;
                if (<>9__5 == null)
                {
                    Func<string, ValueData, ValueData, Option<bool>> local4 = <>9__5;
                    between = <>9__5 = (_, __, ___) => this.AllowedBetweenFilters.HasFlag(DevExpress.Xpf.Core.FilteringUI.AllowedBetweenFilters.NotBetween).AsOption<bool>();
                }
                Func<string, ValueData[], FunctionOperatorType, Option<bool>> function = <>9__6;
                if (<>9__6 == null)
                {
                    Func<string, ValueData[], FunctionOperatorType, Option<bool>> local5 = <>9__6;
                    function = <>9__6 = delegate (string propertyName, ValueData[] values, FunctionOperatorType type) {
                        Attributed<ValueData[]> attributed = BetweenDatesHelper.TryGetPropertyValuesFromSubstituted<object>(propertyName, values, type);
                        return ((attributed == null) || (attributed.Value.Length != 1)) ? this.AllowInverted(type).AsOption<bool>() : this.AllowedCustomDateFilters.HasFlag(DevExpress.Xpf.Core.FilteringUI.AllowedCustomDateFilters.IsNotOnDate).AsOption<bool>();
                    };
                }
                FallbackMapper<bool> fallback = <>c.<>9__16_7;
                if (<>c.<>9__16_7 == null)
                {
                    FallbackMapper<bool> local6 = <>c.<>9__16_7;
                    fallback = <>c.<>9__16_7 = _ => false;
                }
                return unaryNot.Operand.MapExtended<bool>(binary, unary, @in, between, function, null, null, null, fallback, null);
            }, delegate (CriteriaOperator op) {
                Func<string, ValueData[], FunctionOperatorType, Option<bool>> <>9__13;
                Func<string, ValueData, ValueData, Option<bool>> <>9__12;
                Func<string, ValueData[], Option<bool>> <>9__11;
                UnaryOperatorMapper<bool> <>9__10;
                Func<string, ValueData, BinaryOperatorType, Option<bool>> <>9__9;
                Func<string, ValueData, BinaryOperatorType, Option<bool>> binary = <>9__9;
                if (<>9__9 == null)
                {
                    Func<string, ValueData, BinaryOperatorType, Option<bool>> local1 = <>9__9;
                    binary = <>9__9 = (_, __, type) => this.Allow(type).AsOption<bool>();
                }
                UnaryOperatorMapper<bool> unary = <>9__10;
                if (<>9__10 == null)
                {
                    UnaryOperatorMapper<bool> local2 = <>9__10;
                    unary = <>9__10 = (_, type) => (type == UnaryOperatorType.IsNull) && this.AllowedUnaryFilters.HasFlag(DevExpress.Xpf.Core.FilteringUI.AllowedUnaryFilters.IsNull);
                }
                Func<string, ValueData[], Option<bool>> @in = <>9__11;
                if (<>9__11 == null)
                {
                    Func<string, ValueData[], Option<bool>> local3 = <>9__11;
                    @in = <>9__11 = (_, __) => this.AllowedAnyOfFilters.HasFlag(DevExpress.Xpf.Core.FilteringUI.AllowedAnyOfFilters.AnyOf).AsOption<bool>();
                }
                Func<string, ValueData, ValueData, Option<bool>> between = <>9__12;
                if (<>9__12 == null)
                {
                    Func<string, ValueData, ValueData, Option<bool>> local4 = <>9__12;
                    between = <>9__12 = (_, __, ___) => this.AllowedBetweenFilters.HasFlag(DevExpress.Xpf.Core.FilteringUI.AllowedBetweenFilters.Between).AsOption<bool>();
                }
                Func<string, ValueData[], FunctionOperatorType, Option<bool>> function = <>9__13;
                if (<>9__13 == null)
                {
                    Func<string, ValueData[], FunctionOperatorType, Option<bool>> local5 = <>9__13;
                    function = <>9__13 = delegate (string propertyName, ValueData[] values, FunctionOperatorType type) {
                        FormatConditionFilterInfo topBottomFilterInfo = FormatConditionFiltersHelper.GetTopBottomFilterInfo((FunctionOperator) filter);
                        if ((topBottomFilterInfo != null) && topBottomFilterInfo.Type.IsPredefinedFormatCondition())
                        {
                            return this.AllowedDataAnalysisFilters.HasFlag(topBottomFilterInfo.Type.ToPredefinedFormatConditionType().ToAllowedDataAnalysisFilters()).AsOption<bool>();
                        }
                        if (BetweenDatesHelper.TryGetRangeFromSubstituted<object>(propertyName, values, type) != null)
                        {
                            return this.AllowedCustomDateFilters.HasFlag(DevExpress.Xpf.Core.FilteringUI.AllowedCustomDateFilters.BetweenDates).AsOption<bool>();
                        }
                        Attributed<ValueData[]> attributed = BetweenDatesHelper.TryGetPropertyValuesFromSubstituted<object>(propertyName, values, type);
                        return (attributed == null) ? this.Allow(type).AsOption<bool>() : (this.AllowedCustomDateFilters.HasFlag(DevExpress.Xpf.Core.FilteringUI.AllowedCustomDateFilters.IsOnDates) || ((attributed.Value.Length == 1) && this.AllowedCustomDateFilters.HasFlag(DevExpress.Xpf.Core.FilteringUI.AllowedCustomDateFilters.IsOnDate))).AsOption<bool>();
                    };
                }
                FallbackMapper<bool> fallback = <>c.<>9__16_14;
                if (<>c.<>9__16_14 == null)
                {
                    FallbackMapper<bool> local6 = <>c.<>9__16_14;
                    fallback = <>c.<>9__16_14 = _ => false;
                }
                return op.MapExtended<bool>(binary, unary, @in, between, function, null, null, null, fallback, null);
            });
        }

        public bool AllowCustomDateFilters =>
            this.AllowedBinaryFilters.HasFlag(DevExpress.Xpf.Core.FilteringUI.AllowedBinaryFilters.GreaterOrEqual) && this.AllowedBinaryFilters.HasFlag(DevExpress.Xpf.Core.FilteringUI.AllowedBinaryFilters.Less);
        internal bool Allow(PredefinedFormatConditionType predefinedFormatConditionType)
        {
            switch (predefinedFormatConditionType)
            {
                case PredefinedFormatConditionType.Top:
                    return this.AllowedDataAnalysisFilters.HasFlag(DevExpress.Xpf.Core.FilteringUI.AllowedDataAnalysisFilters.Top);

                case PredefinedFormatConditionType.Bottom:
                    return this.AllowedDataAnalysisFilters.HasFlag(DevExpress.Xpf.Core.FilteringUI.AllowedDataAnalysisFilters.Bottom);

                case PredefinedFormatConditionType.AboveAverage:
                    return this.AllowedDataAnalysisFilters.HasFlag(DevExpress.Xpf.Core.FilteringUI.AllowedDataAnalysisFilters.AboveAverage);

                case PredefinedFormatConditionType.BelowAverage:
                    return this.AllowedDataAnalysisFilters.HasFlag(DevExpress.Xpf.Core.FilteringUI.AllowedDataAnalysisFilters.BelowAverage);

                case PredefinedFormatConditionType.Unique:
                    return this.AllowedDataAnalysisFilters.HasFlag(DevExpress.Xpf.Core.FilteringUI.AllowedDataAnalysisFilters.Unique);

                case PredefinedFormatConditionType.Duplicate:
                    return this.AllowedDataAnalysisFilters.HasFlag(DevExpress.Xpf.Core.FilteringUI.AllowedDataAnalysisFilters.Duplicate);
            }
            return false;
        }
        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly FilterRestrictions.<>c <>9 = new FilterRestrictions.<>c();
            public static Predicate<UnaryOperator> <>9__16_0;
            public static FallbackMapper<bool> <>9__16_7;
            public static FallbackMapper<bool> <>9__16_14;

            internal bool <Allow>b__16_0(UnaryOperator unary) => 
                unary.OperatorType == UnaryOperatorType.Not;

            internal bool <Allow>b__16_14(CriteriaOperator _) => 
                false;

            internal bool <Allow>b__16_7(CriteriaOperator _) => 
                false;
        }
    }
}

