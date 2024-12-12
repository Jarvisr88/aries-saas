namespace DevExpress.Utils.Filtering.Internal
{
    using System;

    public interface IUniqueValuesMetricAttributes : IMetricAttributes
    {
        bool HasUniqueValues { get; }

        object UniqueValues { get; }
    }
}

