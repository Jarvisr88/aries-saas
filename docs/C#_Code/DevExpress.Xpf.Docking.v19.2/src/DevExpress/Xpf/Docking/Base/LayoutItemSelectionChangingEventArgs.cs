namespace DevExpress.Xpf.Docking.Base
{
    using DevExpress.Xpf.Docking;
    using System;
    using System.Runtime.CompilerServices;

    public class LayoutItemSelectionChangingEventArgs : ItemCancelEventArgs
    {
        public LayoutItemSelectionChangingEventArgs(BaseLayoutItem item, bool selected) : base(false, item)
        {
            base.RoutedEvent = DockLayoutManager.LayoutItemSelectionChangingEvent;
            this.Selected = selected;
        }

        public bool Selected { get; private set; }
    }
}

