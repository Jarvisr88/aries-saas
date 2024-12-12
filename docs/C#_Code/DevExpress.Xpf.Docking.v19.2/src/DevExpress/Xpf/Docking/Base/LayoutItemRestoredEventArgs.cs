namespace DevExpress.Xpf.Docking.Base
{
    using DevExpress.Xpf.Docking;
    using System;

    public class LayoutItemRestoredEventArgs : ItemEventArgs
    {
        public LayoutItemRestoredEventArgs(BaseLayoutItem item) : base(item)
        {
            base.RoutedEvent = DockLayoutManager.LayoutItemRestoredEvent;
        }
    }
}

