namespace DevExpress.Utils.Filtering.Internal
{
    using System;

    public interface ICustomUIFiltersFactory
    {
        ICustomUIFilters Create(IEndUserFilteringMetric metric, IMetricAttributesQuery query, Func<IServiceProvider> getServiceProvider);
    }
}

