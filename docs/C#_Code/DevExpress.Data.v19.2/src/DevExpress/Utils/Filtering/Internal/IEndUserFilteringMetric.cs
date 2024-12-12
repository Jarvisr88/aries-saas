namespace DevExpress.Utils.Filtering.Internal
{
    using System;

    public interface IEndUserFilteringMetric : IEndUserFilteringElement
    {
        System.Type Type { get; }

        System.Type AttributesTypeDefinition { get; }

        IMetricAttributes Attributes { get; }
    }
}

