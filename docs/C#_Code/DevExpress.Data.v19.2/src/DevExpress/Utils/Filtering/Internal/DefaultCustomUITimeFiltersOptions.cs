namespace DevExpress.Utils.Filtering.Internal
{
    using System;

    internal sealed class DefaultCustomUITimeFiltersOptions : CustomUIFiltersOptions, ICustomUIFiltersOptions
    {
        internal static readonly ICustomUIFiltersOptions Instance = new DefaultCustomUITimeFiltersOptions();

        private DefaultCustomUITimeFiltersOptions() : base(ExcelFilterOptions.Default.DefaultTimeFilterType.GetValueOrDefault(CustomUIFilterType.Equals))
        {
        }
    }
}

