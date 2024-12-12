namespace DevExpress.Utils.Filtering.Internal
{
    using System;

    internal sealed class DefaultCustomUITextFiltersOptions : CustomUIFiltersOptions, ICustomUIFiltersOptions
    {
        internal static readonly ICustomUIFiltersOptions Instance = new DefaultCustomUITextFiltersOptions();

        private DefaultCustomUITextFiltersOptions() : base(ExcelFilterOptions.Default.DefaultTextFilterType.GetValueOrDefault(CustomUIFilterType.BeginsWith))
        {
        }
    }
}

