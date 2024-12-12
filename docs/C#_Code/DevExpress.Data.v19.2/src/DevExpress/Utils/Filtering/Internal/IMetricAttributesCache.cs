namespace DevExpress.Utils.Filtering.Internal
{
    using System;

    internal interface IMetricAttributesCache
    {
        IMetricAttributes GetValueOrCache(string path, Func<IMetricAttributes> create);
        Type GetValueOrCache(string path, Func<Type> create);
        void Reset();
    }
}

