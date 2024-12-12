namespace DevExpress.Xpf.Editors.Settings
{
    using DevExpress.Data.Linq;
    using System;
    using System.ComponentModel;

    public interface IItemsProviderCollectionViewSupport
    {
        void RaiseCurrentChanged(object currentItem);
        void SetCurrentItem(object currentItem);
        void SyncWithCurrentItem();

        ICollectionViewHelper DataSync { get; }

        ICollectionView ListSource { get; }

        bool IsSynchronizedWithCurrentItem { get; }
    }
}

