namespace DevExpress.Xpf.Bars
{
    using System;
    using System.Collections.ObjectModel;

    public class GalleryCollection : ObservableCollection<T>
    {
        protected virtual void ClearItem(T item);
        protected override void ClearItems();
        protected override void InsertItem(int index, T item);
        protected virtual void PrepareItem(T item);
        protected override void RemoveItem(int index);
        protected override void SetItem(int index, T item);
    }
}

