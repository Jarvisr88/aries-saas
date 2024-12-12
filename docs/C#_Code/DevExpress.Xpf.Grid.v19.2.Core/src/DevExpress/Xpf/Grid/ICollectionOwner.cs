namespace DevExpress.Xpf.Grid
{
    using System;

    public interface ICollectionOwner
    {
        void OnInsertItem(object item);
        void OnRemoveItem(object item);
    }
}

