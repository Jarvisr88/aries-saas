namespace DevExpress.Utils.Filtering.Internal
{
    using DevExpress.Utils.Filtering;
    using System;
    using System.Collections.Generic;

    public interface IMetricAttributesQuery
    {
        IDictionary<string, object> InitializeValues(MetricAttributesData data);
        void QueryValues(IDictionary<string, object> values);

        string Path { get; }
    }
}

