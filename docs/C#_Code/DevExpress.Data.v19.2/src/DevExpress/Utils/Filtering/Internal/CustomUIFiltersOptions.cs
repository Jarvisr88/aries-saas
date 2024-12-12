namespace DevExpress.Utils.Filtering.Internal
{
    using DevExpress.Utils.Filtering;
    using System;
    using System.Runtime.CompilerServices;

    public abstract class CustomUIFiltersOptions : ICustomUIFiltersOptions
    {
        protected CustomUIFiltersOptions(CustomUIFilterType defaultFilterType)
        {
            this.DefaultFilterType = defaultFilterType;
        }

        public static CustomUIFilterType GetDefaultFilterType(CustomUIFiltersType filtersType, Type type) => 
            (filtersType != CustomUIFiltersType.Text) ? ((filtersType != CustomUIFiltersType.DateTime) ? ExcelFilterOptions.Default.DefaultFilterType.GetValueOrDefault(CustomUIFilterType.Equals) : (!MetricAttributes.IsTimeSpan(type) ? ExcelFilterOptions.Default.DefaultDateFilterType.GetValueOrDefault(CustomUIFilterType.DatePeriods) : ExcelFilterOptions.Default.DefaultTimeFilterType.GetValueOrDefault(CustomUIFilterType.Equals))) : ExcelFilterOptions.Default.DefaultTextFilterType.GetValueOrDefault(CustomUIFilterType.BeginsWith);

        public CustomUIFilterType DefaultFilterType { get; private set; }

        public virtual bool AllowFilters =>
            true;

        public virtual bool FilterByDisplayText =>
            false;

        public virtual bool ImmediateUpdate =>
            true;

        public virtual bool ShowComparisons =>
            ExcelFilterOptions.Default.ShowComparisons.GetValueOrDefault(true);

        public virtual bool ShowAggregates =>
            ExcelFilterOptions.Default.ShowAggregates.GetValueOrDefault(true);

        public virtual bool ShowSequences =>
            ExcelFilterOptions.Default.ShowSequences.GetValueOrDefault(true);

        public virtual bool ShowBlanks =>
            ExcelFilterOptions.Default.ShowBlanks.GetValueOrDefault(true);

        public virtual bool ShowNulls =>
            ExcelFilterOptions.Default.ShowNulls.GetValueOrDefault(true);

        public virtual bool ShowLikeFilters =>
            ExcelFilterOptions.Default.ShowLikeFilters.GetValueOrDefault(false);

        public virtual bool ShowCustomFilters =>
            ExcelFilterOptions.Default.ShowCustomFilters.GetValueOrDefault(true);

        public virtual bool ShowUserDefinedFilters =>
            ExcelFilterOptions.Default.ShowPredefinedFilters.GetValueOrDefault(true);
    }
}

