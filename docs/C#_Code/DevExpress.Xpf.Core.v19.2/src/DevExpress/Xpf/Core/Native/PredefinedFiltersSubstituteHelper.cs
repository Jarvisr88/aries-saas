namespace DevExpress.Xpf.Core.Native
{
    using DevExpress.Data.Filtering;
    using DevExpress.Data.Filtering.Helpers;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public static class PredefinedFiltersSubstituteHelper
    {
        public static CriteriaOperator Substitute(CriteriaOperator filter, ICollection<Attributed<IPredefinedFilter>> predefinedFilters, Func<ICollection<FormatConditionFilter>> formatConditionFilters, Func<FormatConditionFilterInfo, CriteriaOperator> substituteTopBottomFilter);
        public static CriteriaOperator SubstituteProperty(CriteriaOperator filter, string propertyName);
        private static TFilter TrySubstitute<TFilter, TInfo>(TInfo info, Lazy<ICollection<TFilter>> filters, Func<TFilter, TInfo, bool> condition) where TFilter: class;

        private class PredefinedFilterPropertySubstituter : ClientCriteriaLazyPatcherBase
        {
            private readonly string propertyName;

            public PredefinedFilterPropertySubstituter(string propertyName);
            public override CriteriaOperator Visit(AggregateOperand theOperand);
            public override CriteriaOperator Visit(JoinOperand theOperand);
            public override CriteriaOperator Visit(OperandValue theOperand);
        }

        private class PredefinedFiltersSubstituter : ClientCriteriaLazyPatcherBase
        {
            private readonly ICollection<Attributed<IPredefinedFilter>> predefinedFilters;
            private readonly Lazy<ICollection<FormatConditionFilter>> formatConditionFilters;
            private readonly Func<FormatConditionFilterInfo, CriteriaOperator> substituteTopBottomFilter;

            public PredefinedFiltersSubstituter(ICollection<Attributed<IPredefinedFilter>> predefinedFilters, Func<ICollection<FormatConditionFilter>> formatConditionFilters, Func<FormatConditionFilterInfo, CriteriaOperator> substituteTopBottomFilter);
            public override CriteriaOperator Visit(AggregateOperand theOperand);
            public override CriteriaOperator Visit(FunctionOperator theOperator);
            public override CriteriaOperator Visit(JoinOperand theOperand);

            [Serializable, CompilerGenerated]
            private sealed class <>c
            {
                public static readonly PredefinedFiltersSubstituteHelper.PredefinedFiltersSubstituter.<>c <>9;
                public static Func<Attributed<IPredefinedFilter>, PredefinedFiltersHelper.PredefinedFilterInfo, bool> <>9__4_1;
                public static Func<FormatConditionFilter, AppliedFormatConditionFilterInfo, bool> <>9__4_2;

                static <>c();
                internal bool <Visit>b__4_1(Attributed<IPredefinedFilter> filter, PredefinedFiltersHelper.PredefinedFilterInfo info);
                internal bool <Visit>b__4_2(FormatConditionFilter filter, AppliedFormatConditionFilterInfo info);
            }
        }
    }
}

