namespace DevExpress.Utils.Filtering.Internal
{
    using System;
    using System.Collections.Generic;

    public interface ICollectionValueViewModel<T> : ICollectionValueViewModel, IBaseCollectionValueViewModel, IValueViewModel, ILookupValuesViewModel
    {
        IReadOnlyCollection<T> Values { get; set; }

        int? Top { get; }

        int? MaxCount { get; }
    }
}

