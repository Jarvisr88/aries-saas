namespace DevExpress.Utils.Filtering.Internal
{
    using System;

    public interface ICustomUIFiltersOptions
    {
        CustomUIFilterType DefaultFilterType { get; }

        bool AllowFilters { get; }

        bool FilterByDisplayText { get; }

        bool ImmediateUpdate { get; }

        bool ShowComparisons { get; }

        bool ShowAggregates { get; }

        bool ShowSequences { get; }

        bool ShowBlanks { get; }

        bool ShowNulls { get; }

        bool ShowLikeFilters { get; }

        bool ShowCustomFilters { get; }

        bool ShowUserDefinedFilters { get; }
    }
}

