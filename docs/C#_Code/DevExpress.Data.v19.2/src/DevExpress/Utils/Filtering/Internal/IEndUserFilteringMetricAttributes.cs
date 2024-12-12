namespace DevExpress.Utils.Filtering.Internal
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public interface IEndUserFilteringMetricAttributes : IEnumerable<Attribute>, IEnumerable
    {
        string Path { get; }

        System.Type Type { get; }

        AttributesMergeMode MergeMode { get; }
    }
}

