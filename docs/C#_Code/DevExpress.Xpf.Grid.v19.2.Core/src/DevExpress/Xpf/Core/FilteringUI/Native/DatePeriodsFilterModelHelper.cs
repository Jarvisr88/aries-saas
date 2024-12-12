namespace DevExpress.Xpf.Core.FilteringUI.Native
{
    using DevExpress.Data.Filtering;
    using DevExpress.Xpf.Grid.Filtering;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public static class DatePeriodsFilterModelHelper
    {
        public static CriteriaOperator CompressFilter(CriteriaOperator filter)
        {
            Func<string, FilterDateType[], CriteriaOperator> getConvertedFilter = <>c.<>9__1_0;
            if (<>c.<>9__1_0 == null)
            {
                Func<string, FilterDateType[], CriteriaOperator> local1 = <>c.<>9__1_0;
                getConvertedFilter = <>c.<>9__1_0 = (fieldName, filterDateTypes) => filterDateTypes.ToCriteria(fieldName);
            }
            return ConvertFilterCore(filter, getConvertedFilter);
        }

        private static CriteriaOperator ConvertFilterCore(CriteriaOperator filter, Func<string, FilterDateType[], CriteriaOperator> getConvertedFilter)
        {
            if (filter == null)
            {
                return null;
            }
            CriteriaOperator @operator = null;
            Tuple<CriteriaOperator, IDictionary<string, CriteriaOperator>> tuple = CriteriaColumnAffinityResolver.SplitByColumnNames(filter, null);
            foreach (KeyValuePair<string, CriteriaOperator> pair in tuple.Item2)
            {
                FilterDateType[] typeArray = pair.Value.ToFilters(pair.Key, false);
                @operator &= (typeArray.Length == 0) ? pair.Value : getConvertedFilter(pair.Key, typeArray);
            }
            return (@operator & tuple.Item1);
        }

        public static CriteriaOperator ExtractFilter(CriteriaOperator filter) => 
            ConvertFilterCore(filter, new Func<string, FilterDateType[], CriteriaOperator>(DatePeriodsFilterModelHelper.GetFilterFromFilterDateType));

        private static CriteriaOperator GetFilterFromFilterDateType(string propertyName, FilterDateType[] filterDateTypes)
        {
            CriteriaOperator @operator = null;
            foreach (FilterDateType type in filterDateTypes)
            {
                @operator |= type.ToCriteria(propertyName);
            }
            return @operator;
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly DatePeriodsFilterModelHelper.<>c <>9 = new DatePeriodsFilterModelHelper.<>c();
            public static Func<string, FilterDateType[], CriteriaOperator> <>9__1_0;

            internal CriteriaOperator <CompressFilter>b__1_0(string fieldName, FilterDateType[] filterDateTypes) => 
                filterDateTypes.ToCriteria(fieldName);
        }
    }
}

