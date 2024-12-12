namespace DevExpress.Utils.Filtering.Internal
{
    using System;

    public interface IBaseCollectionValueViewModel : IValueViewModel
    {
        bool UseSelectAll { get; }

        bool UseRadioSelection { get; }

        string SelectAllName { get; }

        string NullName { get; }
    }
}

