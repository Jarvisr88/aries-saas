namespace DevExpress.Xpf.Core.FilteringUI
{
    using DevExpress.Data.Filtering;
    using DevExpress.Xpf.Core.Native;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;

    public class FormatConditionFilterModel : CustomFiltersModelBase<FormatConditionFilter>
    {
        internal FormatConditionFilterModel(FilterModelClient client) : base(client)
        {
        }

        protected override FunctionOperator CreateFilter(FormatConditionFilter filter) => 
            FormatConditionFiltersHelper.CreateFilter(base.PropertyName, filter.Info, filter.ApplyToRow, TopBottomFilterKind.Conditional);

        internal static Func<FormatConditionFilter, bool> GetFormatConditionFilterChecker(CriteriaOperator filter)
        {
            Func<FunctionOperator, AppliedFormatConditionFilterInfo> mapper = <>c.<>9__4_0;
            if (<>c.<>9__4_0 == null)
            {
                Func<FunctionOperator, AppliedFormatConditionFilterInfo> local1 = <>c.<>9__4_0;
                mapper = <>c.<>9__4_0 = x => FormatConditionFiltersHelper.GetAppliedFormatConditionFilterInfo(x);
            }
            ICollection<AppliedFormatConditionFilterInfo> filterInfos = ParseCustomFunctions<AppliedFormatConditionFilterInfo>(filter, mapper);
            return f => filterInfos.Any<AppliedFormatConditionFilterInfo>(new Func<AppliedFormatConditionFilterInfo, bool>(FormatConditionFiltersHelper.IsMatchedInfo));
        }

        protected override Func<FormatConditionFilter, bool> GetIsSelectedFilterChecker(CriteriaOperator filter) => 
            GetFormatConditionFilterChecker(filter);

        internal override void UpdateFormatConditionFilters()
        {
            base.UpdateFormatConditionFilters();
            base.Update();
        }

        protected override IEnumerable<FormatConditionFilter> CoreFilters =>
            base.client.GetFormatConditionFilters();

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly FormatConditionFilterModel.<>c <>9 = new FormatConditionFilterModel.<>c();
            public static Func<FunctionOperator, AppliedFormatConditionFilterInfo> <>9__4_0;

            internal AppliedFormatConditionFilterInfo <GetFormatConditionFilterChecker>b__4_0(FunctionOperator x) => 
                FormatConditionFiltersHelper.GetAppliedFormatConditionFilterInfo(x);
        }
    }
}

