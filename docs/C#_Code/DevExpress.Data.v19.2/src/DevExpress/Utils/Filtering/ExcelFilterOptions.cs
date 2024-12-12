namespace DevExpress.Utils.Filtering
{
    using System;
    using System.Runtime.CompilerServices;

    public class ExcelFilterOptions
    {
        public static readonly ExcelFilterOptions Default = new ExcelFilterOptions();

        private ExcelFilterOptions()
        {
        }

        public CustomUIFilterType? DefaultFilterType { get; set; }

        public CustomUIFilterType? DefaultTextFilterType { get; set; }

        public CustomUIFilterType? DefaultDateFilterType { get; set; }

        public CustomUIFilterType? DefaultTimeFilterType { get; set; }

        public bool? ShowComparisons { get; set; }

        public bool? ShowComparisonsForEnums { get; set; }

        public bool? ShowComparisonsForText { get; set; }

        public bool? ShowAggregates { get; set; }

        public bool? ShowSequences { get; set; }

        public bool? ShowBlanks { get; set; }

        public bool? ShowNulls { get; set; }

        public bool? ShowLikeFilters { get; set; }

        public bool? ShowCustomFilters { get; set; }

        public bool? ShowPredefinedFilters { get; set; }

        public TabType PreferredTabType { get; set; }

        public NumericValuesTabFilterType PreferredNumericValuesTabFilterType { get; set; }

        public DateTimeValuesTabFilterType PreferredDateTimeValuesTabFilterType { get; set; }

        public bool? ShowScrollAnnotations { get; set; }

        public bool? UseAnimationForTabs { get; set; }

        public DateTimeValuesTreeFilterType PreferredDateTimeValuesTreeFilterType { get; set; }

        public enum DateTimeValuesTabFilterType
        {
            Default,
            Tree,
            List
        }

        public enum DateTimeValuesTreeFilterType
        {
            Default,
            TreeView,
            TreeListBox
        }

        public enum NumericValuesTabFilterType
        {
            Default,
            Range,
            List
        }

        public enum TabType
        {
            Default,
            Values,
            Filters
        }
    }
}

