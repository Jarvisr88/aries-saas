namespace DevExpress.Utils.Filtering.Internal
{
    using System;
    using System.Runtime.InteropServices;

    public interface ISummaryMetricAttributes : IMetricAttributes
    {
        bool TryGetDataController(out object controller);
        bool TryGetSummaryValue(string member, out object value);
    }
}

