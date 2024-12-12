namespace DevExpress.Utils.Filtering.Internal
{
    using System;
    using System.Collections.Generic;

    public interface IEnumValueViewModel<T> : IEnumValueViewModel, IBaseCollectionValueViewModel, IValueViewModel, ISimpleValueViewModel<T> where T: struct
    {
        IReadOnlyCollection<T> Values { get; set; }
    }
}

