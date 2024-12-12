namespace DevExpress.Xpf.Core.FilteringUI.Native
{
    using DevExpress.Data.Filtering;
    using DevExpress.Xpf.Core.FilteringUI;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;

    internal class HierarchyFilterBuilderSimple : HierarchyFilterBuilderBase
    {
        public HierarchyFilterBuilderSimple(string propertyName, Func<string, CriteriaOperator> getBlanksFilter, Func<string, bool> getUseDateRangeFilter) : base(propertyName, getBlanksFilter, getUseDateRangeFilter)
        {
        }

        private CriteriaOperator BuildInFilter(List<object> values, bool invert, FilterRestrictions filterRestrictions)
        {
            Func<CriteriaOperator, CriteriaOperator> func = <>c.<>9__3_0 ??= x => ((x == null) ? null : !x);
            CriteriaOperator arg = null;
            if (HierarchyFilterBuilderHelper.IsFirstElementBlank(values))
            {
                arg = base.GetBlanksFilter(base.PropertyName);
                values = values.Skip<object>(1).ToList<object>();
            }
            CriteriaOperator operator2 = this.TryCreateSingleValueFilter(values, invert, filterRestrictions);
            return ((operator2 == null) ? (values.Any<object>() ? (invert ? !(arg | new InOperator(base.PropertyName, values)) : (arg | new InOperator(base.PropertyName, values))) : (invert ? func(arg) : arg)) : (invert ? (func(arg) & operator2) : (arg | operator2)));
        }

        protected override CriteriaOperator BuildSingleValueFilterCore(object value, bool invert) => 
            new BinaryOperator(base.PropertyName, value, invert ? BinaryOperatorType.NotEqual : BinaryOperatorType.Equal);

        protected override CriteriaOperator BuildValuesFilterCore(FilterRestrictions filterRestrictions, CheckedValuesInfo checkedValuesInfo, bool nullOnNoUncheckedValues, CriteriaOperator indeterminateFilter)
        {
            List<object> checkedValues = checkedValuesInfo.CheckedValues;
            if ((checkedValues.Count == 1) && filterRestrictions.Allow(BinaryOperatorType.Equal))
            {
                return (base.BuildSingleValueFilter(checkedValues.Single<object>(), false, () => base.GetBlanksFilter(base.PropertyName)) | indeterminateFilter);
            }
            List<object> source = checkedValuesInfo.GetUncheckedValues();
            List<IndeterminateValueInfo> indeterminateValues = checkedValuesInfo.IndeterminateValues;
            if (nullOnNoUncheckedValues && ((source.Count == 0) && (indeterminateValues.Count == 0)))
            {
                return null;
            }
            if ((source.Count == 1) && ((indeterminateValues.Count == 0) && filterRestrictions.Allow(BinaryOperatorType.NotEqual)))
            {
                return base.BuildSingleValueFilter(source.Single<object>(), true, () => base.GetBlanksFilter(base.PropertyName));
            }
            bool flag2 = filterRestrictions.AllowedAnyOfFilters.HasFlag(AllowedAnyOfFilters.NoneOf);
            if (filterRestrictions.AllowedAnyOfFilters.HasFlag(AllowedAnyOfFilters.AnyOf) && ((!flag2 || ((source.Count == 0) && (indeterminateValues.Count == 0))) || (checkedValues.Count <= source.Count)))
            {
                return (this.BuildInFilter(checkedValues, false, filterRestrictions) | indeterminateFilter);
            }
            if (!flag2)
            {
                return null;
            }
            List<object> first = source;
            if (indeterminateValues.Any<IndeterminateValueInfo>())
            {
                Func<IndeterminateValueInfo, object> selector = <>c.<>9__2_2;
                if (<>c.<>9__2_2 == null)
                {
                    Func<IndeterminateValueInfo, object> local1 = <>c.<>9__2_2;
                    selector = <>c.<>9__2_2 = x => x.Value;
                }
                first = first.Concat<object>(indeterminateValues.Select<IndeterminateValueInfo, object>(selector)).ToList<object>();
            }
            return (this.BuildInFilter(first, true, filterRestrictions) | indeterminateFilter);
        }

        private CriteriaOperator TryCreateSingleValueFilter(List<object> values, bool invert, FilterRestrictions filterRestrictions) => 
            (values.Count == 1) ? (!invert ? (filterRestrictions.Allow(BinaryOperatorType.Equal) ? base.BuildSingleValueFilter(values.Single<object>(), false, null) : null) : (filterRestrictions.Allow(BinaryOperatorType.NotEqual) ? base.BuildSingleValueFilter(values.Single<object>(), true, null) : null)) : null;

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly HierarchyFilterBuilderSimple.<>c <>9 = new HierarchyFilterBuilderSimple.<>c();
            public static Func<IndeterminateValueInfo, object> <>9__2_2;
            public static Func<CriteriaOperator, CriteriaOperator> <>9__3_0;

            internal CriteriaOperator <BuildInFilter>b__3_0(CriteriaOperator x) => 
                (x == null) ? null : !x;

            internal object <BuildValuesFilterCore>b__2_2(IndeterminateValueInfo x) => 
                x.Value;
        }
    }
}

