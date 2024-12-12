namespace DevExpress.Utils.Filtering.Internal
{
    using System;

    public interface ICollectionMetricAttributes : IMetricAttributes
    {
        bool UseRadioSelection { get; }

        bool UseSelectAll { get; }

        string SelectAllName { get; }

        string NullName { get; }
    }
}

