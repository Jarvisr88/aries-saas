namespace DevExpress.Xpf.Core.FilteringUI.Native
{
    using DevExpress.Data.Filtering;
    using DevExpress.Xpf.Core.FilteringUI;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    internal abstract class HierarchyFilterBuilderBase
    {
        private readonly Func<string, bool> GetUseDateRangeFilter;
        protected readonly string PropertyName;
        protected readonly Func<string, CriteriaOperator> GetBlanksFilter;

        public HierarchyFilterBuilderBase(string propertyName, Func<string, CriteriaOperator> getBlanksFilter, Func<string, bool> getUseDateRangeFilter)
        {
            this.PropertyName = propertyName;
            this.GetBlanksFilter = getBlanksFilter;
            this.GetUseDateRangeFilter = getUseDateRangeFilter;
        }

        public CriteriaOperator BuildSingleValueFilter(object value, bool invert, Func<CriteriaOperator> getBlanksFilter) => 
            !HierarchyFilterBuilderHelper.IsBlankElement(value) ? this.BuildSingleValueFilterCore(value, invert) : (invert ? !getBlanksFilter() : getBlanksFilter());

        protected abstract CriteriaOperator BuildSingleValueFilterCore(object value, bool invert);
        public CriteriaOperator BuildValuesFilter(FilterRestrictions filterRestrictions, CheckedValuesInfo checkedValuesInfo, bool nullOnNoUncheckedValues)
        {
            CriteriaOperator indeterminateFilter = this.GetIndeterminateFilter(checkedValuesInfo);
            return (checkedValuesInfo.CheckedValues.Any<object>() ? this.BuildValuesFilterCore(filterRestrictions, checkedValuesInfo, nullOnNoUncheckedValues, indeterminateFilter) : indeterminateFilter);
        }

        protected abstract CriteriaOperator BuildValuesFilterCore(FilterRestrictions filterRestrictions, CheckedValuesInfo checkedValuesInfo, bool nullOnNoUncheckedValues, CriteriaOperator indeterminateFilter);
        private CriteriaOperator GetIndeterminateFilter(CheckedValuesInfo checkedValuesInfo)
        {
            CriteriaOperator @operator = null;
            List<IndeterminateValueInfo> indeterminateValues = checkedValuesInfo.IndeterminateValues;
            return (!indeterminateValues.Any<IndeterminateValueInfo>() ? @operator : CriteriaOperator.Or((IEnumerable<CriteriaOperator>) (from x in indeterminateValues select this.BuildSingleValueFilter(x.Value, false, () => this.GetBlanksFilter(this.PropertyName)) & HierarchyFilterBuilderHelper.GetAppropriateBuilder(x.PropertyName, this.GetBlanksFilter, this.GetUseDateRangeFilter).BuildValuesFilter(FilterRestrictions.All(), x.Children, true))));
        }
    }
}

