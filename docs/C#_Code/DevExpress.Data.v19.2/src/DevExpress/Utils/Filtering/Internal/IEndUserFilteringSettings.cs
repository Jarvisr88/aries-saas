namespace DevExpress.Utils.Filtering.Internal
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Reflection;
    using System.Runtime.InteropServices;

    public interface IEndUserFilteringSettings : IEnumerable<IEndUserFilteringMetric>, IEnumerable
    {
        bool Ensure(string path, Type type, FilterType filterType, FilterValuesType valuesType, FilterGroupType groupType = 0);
        IEnumerable<KeyValuePair<string, TValue>> GetPairs<TValue>(Func<IEndUserFilteringMetric, TValue> accessor);

        IEnumerable<string> Paths { get; }

        IEndUserFilteringMetric this[string path] { get; }

        IEnumerable<IEndUserFilteringMetricAttributes> CustomAttributes { get; }
    }
}

