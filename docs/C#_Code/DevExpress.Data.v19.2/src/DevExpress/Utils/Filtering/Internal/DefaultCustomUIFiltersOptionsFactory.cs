namespace DevExpress.Utils.Filtering.Internal
{
    using System;

    internal sealed class DefaultCustomUIFiltersOptionsFactory : ICustomUIFiltersOptionsFactory
    {
        internal static readonly ICustomUIFiltersOptionsFactory Instance = new DefaultCustomUIFiltersOptionsFactory();

        private DefaultCustomUIFiltersOptionsFactory()
        {
        }

        ICustomUIFiltersOptions ICustomUIFiltersOptionsFactory.Create(IEndUserFilteringMetric metric)
        {
            if (MetricAttributes.IsTimeSpan(metric.Type))
            {
                return DefaultCustomUITimeFiltersOptions.Instance;
            }
            if (metric.Type == typeof(string))
            {
                return DefaultCustomUITextFiltersOptions.Instance;
            }
            IRangeMetricAttributes attributes = metric.Attributes as IRangeMetricAttributes;
            return (((attributes == null) || attributes.IsNumericRange) ? DefaultCustomUIFiltersOptions.Instance : DefaultCustomUIDateFiltersOptions.Instance);
        }
    }
}

