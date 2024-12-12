namespace DevExpress.Xpf.Bars
{
    using System;
    using System.Collections.ObjectModel;

    public class SimpleBarCollection<TBar> : ObservableCollection<TBar> where TBar: IBar
    {
        protected override void InsertItem(int index, TBar item);
        protected override void RemoveItem(int index);
        protected override void SetItem(int index, TBar item);
        private void UpdateIndex(int index, TBar item, bool isInsert);
    }
}

