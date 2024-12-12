namespace DevExpress.Utils.Filtering.Internal
{
    using System.Collections.Generic;

    public interface IEndUserFilteringMetricAttributesProvider
    {
        IEnumerable<IEndUserFilteringMetricAttributes> Attributes { get; }
    }
}

