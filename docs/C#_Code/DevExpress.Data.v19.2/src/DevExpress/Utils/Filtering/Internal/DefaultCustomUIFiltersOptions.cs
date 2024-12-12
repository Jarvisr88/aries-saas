namespace DevExpress.Utils.Filtering.Internal
{
    using System;

    internal sealed class DefaultCustomUIFiltersOptions : CustomUIFiltersOptions, ICustomUIFiltersOptions
    {
        internal static readonly ICustomUIFiltersOptions Instance = new DefaultCustomUIFiltersOptions();

        private DefaultCustomUIFiltersOptions() : base(ExcelFilterOptions.Default.DefaultFilterType.GetValueOrDefault(CustomUIFilterType.Equals))
        {
        }
    }
}

