namespace DevExpress.Utils.Filtering.Internal
{
    using System;

    public interface ICustomUIFiltersBox
    {
        void EnsureFiltersType();

        IEndUserFilteringMetric Metric { get; }

        ICustomUIFilters Filters { get; }
    }
}

