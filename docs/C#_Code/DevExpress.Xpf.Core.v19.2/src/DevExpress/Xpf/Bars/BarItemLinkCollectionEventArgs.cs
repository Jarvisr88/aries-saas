namespace DevExpress.Xpf.Bars
{
    using System;
    using System.Runtime.CompilerServices;

    public class BarItemLinkCollectionEventArgs : EventArgs
    {
        public BarItemLinkCollectionEventArgs(BarItem item, int itemIndex);

        public BarItem Item { get; protected set; }

        public int ItemIndex { get; protected set; }
    }
}

