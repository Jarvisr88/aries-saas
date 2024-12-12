namespace DevExpress.Xpf.Core.FilteringUI
{
    using System;
    using System.Runtime.InteropServices;

    internal sealed class FilterModelClient
    {
        public readonly string PropertyName;
        internal readonly Func<FilteringColumn> GetColumn;
        internal readonly Func<string, FilteringColumn> GetColumnByName;
        internal readonly Func<CountsIncludeMode, Task<UniqueValues>> GetUniqueValues;
        internal readonly Func<string, CriteriaOperator, CountsIncludeMode, string, Task<UniqueValues>> GetGroupUniqueValues;
        internal readonly Func<IEnumerable<CriteriaOperator>, int[]> GetCounts;
        internal readonly Action<Lazy<CriteriaOperator>> ApplyFilter;
        internal readonly Func<CriteriaOperator, CriteriaOperator> SubstituteFilter;
        internal readonly Func<Tuple<object, object>> GetMinMaxRange;
        internal readonly Func<IEnumerable<FormatConditionFilter>> GetFormatConditionFilters;

        public FilterModelClient(string propertyName, Func<string, FilteringColumn> getColumnByName, Func<CountsIncludeMode, Task<UniqueValues>> getUniqueValues, Func<string, CriteriaOperator, CountsIncludeMode, string, Task<UniqueValues>> getGroupUniqueValues, Func<IEnumerable<CriteriaOperator>, int[]> getCounts, Action<Lazy<CriteriaOperator>> applyFilter, Func<CriteriaOperator, CriteriaOperator> substituteFilter, Func<Tuple<object, object>> getMinMaxRange, Func<IEnumerable<FormatConditionFilter>> getFormatConditionFilters)
        {
            this.PropertyName = propertyName;
            this.GetColumnByName = getColumnByName;
            this.GetColumn = () => this.GetColumnByName(this.PropertyName);
            this.GetUniqueValues = getUniqueValues;
            this.GetGroupUniqueValues = getGroupUniqueValues;
            this.GetCounts = getCounts;
            this.ApplyFilter = applyFilter;
            this.SubstituteFilter = substituteFilter;
            this.GetMinMaxRange = getMinMaxRange;
            this.GetFormatConditionFilters = getFormatConditionFilters;
        }

        public FilterModelClient Update(Action<Lazy<CriteriaOperator>> applyFilter = null, Func<CountsIncludeMode, Task<UniqueValues>> getUniqueValues = null, Func<string, CriteriaOperator, CountsIncludeMode, string, Task<UniqueValues>> getGroupUniqueValues = null)
        {
            Func<CountsIncludeMode, Task<UniqueValues>> func1 = getUniqueValues;
            if (getUniqueValues == null)
            {
                Func<CountsIncludeMode, Task<UniqueValues>> local1 = getUniqueValues;
                func1 = this.GetUniqueValues;
            }
            Func<IEnumerable<CriteriaOperator>, int[]> getCounts = this.GetCounts;
            if (applyFilter == null)
            {
                Func<IEnumerable<CriteriaOperator>, int[]> local3 = this.GetCounts;
                getCounts = (Func<IEnumerable<CriteriaOperator>, int[]>) this.ApplyFilter;
            }
            return new FilterModelClient(this.PropertyName, this.GetColumnByName, func1, getGroupUniqueValues ?? this.GetGroupUniqueValues, (Func<IEnumerable<CriteriaOperator>, int[]>) applyFilter, getCounts, this.SubstituteFilter, this.GetMinMaxRange, this.GetFormatConditionFilters);
        }
    }
}

