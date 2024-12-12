namespace DevExpress.Xpf.Bars
{
    using System;
    using System.Runtime.CompilerServices;

    public class ReadOnlyLinkCollection : SimpleLinkCollection
    {
        protected override void ClearItems();
        protected override void InsertItem(int index, BarItemLinkBase item);
        protected override void RemoveItem(int index);
        protected override void SetItem(int index, BarItemLinkBase item);

        internal bool AllowModifyCollection { get; set; }
    }
}

