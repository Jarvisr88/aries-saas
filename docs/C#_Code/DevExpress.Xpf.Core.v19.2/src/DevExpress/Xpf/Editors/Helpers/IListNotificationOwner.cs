namespace DevExpress.Xpf.Editors.Helpers
{
    using System;

    public interface IListNotificationOwner
    {
        void OnCollectionChanged(NotifyItemsProviderChangedEventArgs e);
        void OnCollectionChanged(NotifyItemsProviderSelectionChangedEventArgs e);
    }
}

