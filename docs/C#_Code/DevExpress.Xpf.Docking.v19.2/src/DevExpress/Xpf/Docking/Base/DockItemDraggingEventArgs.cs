namespace DevExpress.Xpf.Docking.Base
{
    using DevExpress.Xpf.Docking;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public class DockItemDraggingEventArgs : ItemCancelEventArgs
    {
        public DockItemDraggingEventArgs(Point screenPoint, BaseLayoutItem item) : base(item)
        {
            this.ScreenPoint = screenPoint;
            base.RoutedEvent = DockLayoutManager.DockItemDraggingEvent;
        }

        public Point ScreenPoint { get; private set; }
    }
}

