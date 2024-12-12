namespace DevExpress.Xpf.Core.FilteringUI.Native
{
    using DevExpress.Data.Filtering;
    using DevExpress.Data.Helpers;
    using DevExpress.Mvvm.Native;
    using DevExpress.Xpf.Core.FilteringUI;
    using System;
    using System.Linq;

    internal class HierarchyFilterBuilderDateRange : HierarchyFilterBuilderBase
    {
        public HierarchyFilterBuilderDateRange(string propretyName, Func<string, CriteriaOperator> getBlanksFilter, Func<string, bool> getUseDateRangeFilter) : base(propretyName, getBlanksFilter, getUseDateRangeFilter)
        {
        }

        protected override CriteriaOperator BuildSingleValueFilterCore(object value, bool invert)
        {
            if (invert)
            {
                throw new InvalidOperationException();
            }
            return MultiselectRoundedDateTimeFilterHelper.DatesToCriteria(base.PropertyName, ((DateTime) value).Yield<DateTime>());
        }

        protected override CriteriaOperator BuildValuesFilterCore(FilterRestrictions filterRestrictions, CheckedValuesInfo checkedValuesInfo, bool nullOnNoUncheckedValues, CriteriaOperator indeterminateFilter)
        {
            CriteriaOperator @operator = null;
            if (HierarchyFilterBuilderHelper.IsFirstElementBlank(checkedValuesInfo.CheckedValues))
            {
                @operator = base.GetBlanksFilter(base.PropertyName);
            }
            return ((@operator | MultiselectRoundedDateTimeFilterHelper.DatesToCriteria(base.PropertyName, checkedValuesInfo.CheckedValues.OfType<DateTime>())) | indeterminateFilter);
        }
    }
}

