namespace DevExpress.Utils.Filtering.Internal
{
    using System;

    public interface IGroupValueViewModel : IValueViewModel, IBaseCollectionValueViewModel
    {
        string[] Grouping { get; }

        IGroupValues GroupValues { get; }
    }
}

