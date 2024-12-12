namespace DevExpress.Utils.Filtering.Internal
{
    using System;

    public interface IEnumChoiceMetricAttributes : IBaseLookupMetricAttributes, ICollectionMetricAttributes, IMetricAttributes, IUniqueValuesMetricAttributes
    {
        Type EnumType { get; }

        bool UseFlags { get; }

        bool UseContainsForFlags { get; }
    }
}

