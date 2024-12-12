namespace DevExpress.Xpf.Core.Native
{
    using DevExpress.Data.Filtering;
    using DevExpress.Xpf.Core.ConditionalFormatting;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public static class FormatConditionFiltersHelper
    {
        private static bool AreMatchedValues(object val1, object val2);
        public static FunctionOperator CreateFilter(string propertyName, FormatConditionFilterInfo filter, bool applyToRow, TopBottomFilterKind filterKind);
        public static AppliedFormatConditionFilterInfo GetAppliedFormatConditionFilterInfo(FunctionOperator theOperator);
        private static AppliedFormatConditionFilterInfo GetAppliedFormatConditionFilterInfo(FunctionOperator theOperator, TopBottomFilterKind filterKind);
        private static string GetFunctionName(ConditionFilterType type, TopBottomFilterKind kind);
        public static FormatConditionFilterInfo GetTopBottomFilterInfo(FunctionOperator theOperator);
        public static bool IsAverage(this ConditionFilterType type);
        public static bool IsConditionFormatFilter(CriteriaOperator filter, TopBottomFilterKind filterKind);
        public static bool IsMatchedInfo(this FormatConditionFilter @this, AppliedFormatConditionFilterInfo info);
        public static bool IsTopBottom(this ConditionFilterType type);
        public static bool IsTopBottomItems(this ConditionFilterType type);
        public static bool IsTopBottomOrAverageOrUniqueDuplicate(this ConditionFilterType type);
        public static bool IsTopBottomPercent(this ConditionFilterType type);
        public static bool IsUniqueDiplicate(this ConditionFilterType type);
        public static ICollection<T> ParseGroupOrOperator<T>(CriteriaOperator filter, Func<CriteriaOperator, T> mapper) where T: class;
        public static CriteriaOperator RemoveConditionFormatFilters(CriteriaOperator filter, Func<AppliedFormatConditionFilterInfo, bool> shouldRemove);
        public static ConditionFilterType ToFilterType(this ConditionRule rule);
        public static ConditionFilterType ToFilterType(this TopBottomRule rule);
        public static ConditionFilterType ToFilterType(this UniqueDuplicateRule rule);

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly FormatConditionFiltersHelper.<>c <>9;
            public static Func<AppliedFormatConditionFilterInfo, bool> <>9__1_0;
            public static Func<OperandValue, object> <>9__2_0;
            public static Func<CriteriaOperator, bool> <>9__18_2;

            static <>c();
            internal object <GetAppliedFormatConditionFilterInfo>b__2_0(OperandValue x);
            internal bool <GetTopBottomFilterInfo>b__1_0(AppliedFormatConditionFilterInfo x);
            internal bool <RemoveConditionFormatFilters>b__18_2(CriteriaOperator x);
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c__7<T> where T: class
        {
            public static readonly FormatConditionFiltersHelper.<>c__7<T> <>9;
            public static Predicate<GroupOperator> <>9__7_0;
            public static Func<T, bool> <>9__7_3;

            static <>c__7();
            internal bool <ParseGroupOrOperator>b__7_0(GroupOperator x);
            internal bool <ParseGroupOrOperator>b__7_3(T x);
        }
    }
}

