namespace DevExpress.Xpf.Docking.Base
{
    using DevExpress.Xpf.Docking;
    using System;

    public class LayoutItemHiddenEventArgs : ItemEventArgs
    {
        public LayoutItemHiddenEventArgs(BaseLayoutItem item) : base(item)
        {
            base.RoutedEvent = DockLayoutManager.LayoutItemHiddenEvent;
        }
    }
}

