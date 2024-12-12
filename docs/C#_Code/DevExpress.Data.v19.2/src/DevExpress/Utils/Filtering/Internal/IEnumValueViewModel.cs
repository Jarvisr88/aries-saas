namespace DevExpress.Utils.Filtering.Internal
{
    using System;

    public interface IEnumValueViewModel : IBaseCollectionValueViewModel, IValueViewModel
    {
        bool UseFlags { get; }

        Type EnumType { get; }
    }
}

