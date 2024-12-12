namespace DevExpress.Xpf.Editors.Helpers
{
    using DevExpress.Data.Filtering;
    using DevExpress.Xpf.Editors;
    using System;
    using System.Windows.Threading;

    public interface IItemsProviderOwner
    {
        bool IsLoaded { get; set; }

        IItemsProvider2 ItemsProvider { get; }

        System.Windows.Threading.Dispatcher Dispatcher { get; }

        object ItemsSource { get; set; }

        CriteriaOperator FilterCriteria { get; }

        string DisplayMember { get; set; }

        string ValueMember { get; set; }

        ListItemCollection Items { get; }

        bool IsSynchronizedWithCurrentItem { get; }

        bool IsInLookUpMode { get; }

        bool AllowCollectionView { get; }

        bool AllowLiveDataShaping { get; }

        bool IsCaseSensitiveFilter { get; }

        bool SelectItemWithNullValue { get; }
    }
}

