namespace DevExpress.Utils.Filtering.Internal
{
    using DevExpress.Utils.Filtering;
    using System;

    public interface IMetricAttributes
    {
        void UpdateMemberBinding(string unboundMemberName, object value);
        void UpdateMemberBindings(MetricAttributesData data, IMetricAttributesQuery query);
        void UpdateMemberBindings(object viewModel, string propertyName, IMetricAttributesQuery query);
    }
}

