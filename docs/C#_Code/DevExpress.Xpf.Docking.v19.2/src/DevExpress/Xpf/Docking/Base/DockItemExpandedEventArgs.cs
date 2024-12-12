namespace DevExpress.Xpf.Docking.Base
{
    using DevExpress.Xpf.Docking;
    using System;

    public class DockItemExpandedEventArgs : ItemEventArgs
    {
        public DockItemExpandedEventArgs(BaseLayoutItem item) : base(item)
        {
            base.RoutedEvent = DockLayoutManager.DockItemExpandedEvent;
        }
    }
}

