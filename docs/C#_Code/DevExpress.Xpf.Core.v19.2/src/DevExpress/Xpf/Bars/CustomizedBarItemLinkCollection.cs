namespace DevExpress.Xpf.Bars
{
    using System;
    using System.Collections.ObjectModel;

    public class CustomizedBarItemLinkCollection : ObservableCollection<BarItemLinkBase>
    {
        private ILinksHolder holder;

        public CustomizedBarItemLinkCollection(ILinksHolder holder);
        protected override void ClearItems();
        protected override void InsertItem(int index, BarItemLinkBase item);
        protected override void RemoveItem(int index);
    }
}

