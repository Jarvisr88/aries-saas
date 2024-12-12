namespace DevExpress.Utils.Filtering.Internal
{
    using System;

    public interface IRangeMetricAttributes<T> : IMetricAttributes<T>, IMetricAttributes, IRangeMetricAttributes, IUniqueValuesMetricAttributes, ISummaryMetricAttributes where T: struct
    {
        T? Minimum { get; }

        T? Maximum { get; }

        T? Average { get; }
    }
}

