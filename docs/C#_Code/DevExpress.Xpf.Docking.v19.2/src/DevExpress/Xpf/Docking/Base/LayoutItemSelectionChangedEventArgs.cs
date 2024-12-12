namespace DevExpress.Xpf.Docking.Base
{
    using DevExpress.Xpf.Docking;
    using System;
    using System.Runtime.CompilerServices;

    public class LayoutItemSelectionChangedEventArgs : ItemEventArgs
    {
        public LayoutItemSelectionChangedEventArgs(BaseLayoutItem item, bool selected) : base(item)
        {
            base.RoutedEvent = DockLayoutManager.LayoutItemSelectionChangedEvent;
            this.Selected = selected;
        }

        public bool Selected { get; private set; }
    }
}

