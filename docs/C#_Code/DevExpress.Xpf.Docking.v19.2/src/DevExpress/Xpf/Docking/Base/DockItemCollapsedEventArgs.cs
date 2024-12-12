namespace DevExpress.Xpf.Docking.Base
{
    using DevExpress.Xpf.Docking;
    using System;

    public class DockItemCollapsedEventArgs : ItemEventArgs
    {
        public DockItemCollapsedEventArgs(BaseLayoutItem item) : base(item)
        {
            base.RoutedEvent = DockLayoutManager.DockItemCollapsedEvent;
        }
    }
}

