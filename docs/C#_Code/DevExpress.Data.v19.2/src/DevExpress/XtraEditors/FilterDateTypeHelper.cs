namespace DevExpress.XtraEditors
{
    using DevExpress.Data.Filtering;
    using DevExpress.Data.Filtering.Helpers;
    using DevExpress.XtraEditors.Helpers;
    using System;
    using System.Collections.Generic;
    using System.Runtime.InteropServices;

    public static class FilterDateTypeHelper
    {
        private static readonly List<FilterDateTypeHelper.IntervalTriplet> Intervals;
        private static readonly Dictionary<FilterDateType, FunctionOperatorType> Mappings;
        private static readonly Dictionary<FunctionOperatorType, FilterDateType> ReverseMappings;

        static FilterDateTypeHelper();
        private static void ExtractGreaterOrEquals(CriteriaOperator criteriaOperator, out DateTime? dts, out int? indexs);
        private static int ExtractIntervalIndex(FunctionOperatorType interval);
        private static void ExtractLess(CriteriaOperator criteriaOperator, out DateTime? dte, out int? indexe);
        public static DateFilterResult FromCriteria(CriteriaOperator currentFilter, string fieldName);
        private static void FromCriteriaCore(CriteriaOperator currentFilter, CriteriaOperator operand, bool[] arra, ref DateTime? startDate, ref DateTime? endDate, ref bool hasNullValue);
        private static void FromCriteriaRoot(CriteriaOperator currentFilter, CriteriaOperator operand, bool[] arra, ref DateTime? startDate, ref DateTime? endDate, ref bool hasNullValue);
        private static int GetMonthAgo(FilterDateType heap);
        public static DateTime GetMonthAgo(DateTime date, FilterDateType monthAgo);
        public static DateTime GetMonthAgo(DateTime date, int month);
        public static bool IsFilterValid(FilterDateType filterType);
        private static void PushInterval(FunctionOperatorType interval, FunctionOperatorType? intervalEnd, ref FunctionOperatorType? intervalStart);
        public static CriteriaOperator ToCriteria(CriteriaOperator property, FilterDateType heap);
        private static CriteriaOperator ToCriteriaAlt(CriteriaOperator property, FilterDateType heap);
        private static CriteriaOperator ToCriteriaCore(CriteriaOperator property, FilterDateType heap);
        public static CriteriaOperator ToTooltipCriteria(CriteriaOperator property, FilterDateType heap);

        private class ActualDatesProcessor : ClientCriteriaLazyPatcherBase.AggregatesCommonProcessingBase
        {
            private static readonly FilterDateTypeHelper.ActualDatesProcessor Instance;

            static ActualDatesProcessor();
            private ActualDatesProcessor();
            public static CriteriaOperator Do(CriteriaOperator op);
            public override CriteriaOperator Visit(FunctionOperator theOperator);
        }

        private class IntervalTriplet
        {
            public readonly FunctionOperatorType Interval;
            public readonly FunctionOperatorType? Start;
            public readonly FunctionOperatorType? End;

            public IntervalTriplet(FunctionOperatorType interval, FunctionOperatorType? start, FunctionOperatorType? end);
        }
    }
}

