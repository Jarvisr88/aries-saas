namespace DevExpress.Utils.Filtering.Internal
{
    using System;

    public interface ILookupMetricAttributes : IBaseLookupMetricAttributes, ICollectionMetricAttributes, IMetricAttributes, IUniqueValuesMetricAttributes
    {
        int? Top { get; }

        int? MaxCount { get; }

        object DataSource { get; }

        string DisplayMember { get; }

        string ValueMember { get; }

        bool UseBlanks { get; }

        string BlanksName { get; }
    }
}

