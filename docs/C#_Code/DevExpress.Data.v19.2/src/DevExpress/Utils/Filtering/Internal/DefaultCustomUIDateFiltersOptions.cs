namespace DevExpress.Utils.Filtering.Internal
{
    using System;

    internal sealed class DefaultCustomUIDateFiltersOptions : CustomUIFiltersOptions, ICustomUIFiltersOptions
    {
        internal static readonly ICustomUIFiltersOptions Instance = new DefaultCustomUIDateFiltersOptions();

        private DefaultCustomUIDateFiltersOptions() : base(ExcelFilterOptions.Default.DefaultDateFilterType.GetValueOrDefault(CustomUIFilterType.DatePeriods))
        {
        }
    }
}

