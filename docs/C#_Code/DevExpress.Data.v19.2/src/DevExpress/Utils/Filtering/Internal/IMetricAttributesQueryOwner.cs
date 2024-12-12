namespace DevExpress.Utils.Filtering.Internal
{
    using System;

    public interface IMetricAttributesQueryOwner
    {
        void RaiseMetricAttributesQuery<TEventArgs, TData>(TEventArgs e) where TEventArgs: QueryDataEventArgs<TData> where TData: MetricAttributesData;
        void RegisterContext(IMetricAttributesQueryOwner context);
        void UnregisterContext();
    }
}

