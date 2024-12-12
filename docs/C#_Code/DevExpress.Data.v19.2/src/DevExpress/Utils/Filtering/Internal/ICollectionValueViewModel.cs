namespace DevExpress.Utils.Filtering.Internal
{
    using System;
    using System.Collections.Generic;

    public interface ICollectionValueViewModel : IBaseCollectionValueViewModel, IValueViewModel, ILookupValuesViewModel
    {
        void LoadFewer();
        void LoadMore();

        object DataSource { get; }

        string ValueMember { get; }

        string DisplayMember { get; }

        bool IsLoadMoreAvailable { get; }

        bool IsLoadFewerAvailable { get; }

        bool FilterByDisplayText { get; }

        IReadOnlyCollection<int> DisplayIndexes { get; set; }

        bool UseBlanks { get; }

        bool? Blanks { get; set; }

        string BlanksName { get; }

        bool IsInverted { get; }
    }
}

