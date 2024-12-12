namespace DevExpress.Xpf.Core.FilteringUI
{
    using DevExpress.Data.Filtering;
    using DevExpress.Mvvm.Native;
    using DevExpress.Utils;
    using DevExpress.Xpf.Core.FilteringUI.Native;
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.Editors.Settings;
    using DevExpress.Xpf.Grid;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.InteropServices;
    using System.Threading.Tasks;

    internal sealed class FilteringColumn
    {
        public readonly string Name;
        public readonly System.Type Type;
        public readonly Func<bool> GetRoundDateTimeFilter;
        public readonly Func<object, string> GetDisplayText;
        public readonly Func<FilterRestrictions> GetFilterRestrictions;
        public readonly Func<bool> ShowAllValuesInFilter;
        public readonly Func<SummaryFilterInfo[], object[]> GetSummaryValues;
        public readonly bool AllowHierarchicalFilterTree;
        private readonly ColumnFilterMode filterMode;
        private readonly Func<BaseEditSettings> getEditSettingsCore;
        private readonly Func<string> getGroupFields;
        private readonly Func<CriteriaOperator, bool, CountsIncludeMode, FilterValuesThrottleMode, Task<UniqueValues>> getUniqueValues;
        private readonly Func<PredefinedFilterCollection> getPredefinedFilters;

        public FilteringColumn(string name, System.Type type, ColumnFilterMode filterMode, bool allowHierarchicalFilterTree, Func<BaseEditSettings> getEditSettings, Func<bool> getRoundDateTimeFilter, Func<object, string> getDisplayText, Func<FilterRestrictions> getFilterRestrictions, Func<string> getGroupFields, Func<CriteriaOperator, bool, CountsIncludeMode, FilterValuesThrottleMode, Task<UniqueValues>> getUniqueValues, Func<bool> showAllValuesInFilter, Func<SummaryFilterInfo[], object[]> getSummaryValues, Func<PredefinedFilterCollection> getPredefinedFilters)
        {
            Guard.ArgumentNotNull(name, "name");
            Guard.ArgumentNotNull(type, "type");
            this.Name = name;
            this.filterMode = filterMode;
            this.Type = (filterMode == ColumnFilterMode.Value) ? type : typeof(string);
            this.GetRoundDateTimeFilter = getRoundDateTimeFilter;
            this.getEditSettingsCore = getEditSettings;
            this.GetDisplayText = getDisplayText;
            this.GetFilterRestrictions = getFilterRestrictions;
            this.getGroupFields = getGroupFields;
            this.getUniqueValues = getUniqueValues;
            this.ShowAllValuesInFilter = showAllValuesInFilter;
            this.GetSummaryValues = getSummaryValues;
            this.getPredefinedFilters = getPredefinedFilters;
            this.AllowHierarchicalFilterTree = allowHierarchicalFilterTree;
        }

        public BaseEditSettings GetEditSettings() => 
            (this.filterMode == ColumnFilterMode.Value) ? this.getEditSettingsCore() : null;

        public IEnumerable<PredefinedFilter> GetSustitutedPredefinedFilters() => 
            (this.getPredefinedFilters() ?? EmptyArray<PredefinedFilter>.Instance).Select<PredefinedFilter, PredefinedFilter>(delegate (PredefinedFilter y) {
                PredefinedFilter filter1 = new PredefinedFilter();
                filter1.Filter = PredefinedFiltersSubstituteHelper.SubstituteProperty(y.Filter, this.Name);
                filter1.Name = y.Name;
                return filter1;
            });

        public Task<UniqueValues> GetUniqueValues(CriteriaOperator filter, bool forceIsColumnFiltered, CountsIncludeMode countsIncludeMode, FilterValuesThrottleMode throttleMode = 0) => 
            this.getUniqueValues(filter, forceIsColumnFiltered, countsIncludeMode, throttleMode);

        public string[] GroupFields =>
            FilteringUIContextHelper.SplitGroupFields(this.Name, this.getGroupFields(), true);

        public string RootFilterValuesPropertyName =>
            FilteringUIContextHelper.SplitGroupFields(this.Name, this.getGroupFields(), false)[0];
    }
}

