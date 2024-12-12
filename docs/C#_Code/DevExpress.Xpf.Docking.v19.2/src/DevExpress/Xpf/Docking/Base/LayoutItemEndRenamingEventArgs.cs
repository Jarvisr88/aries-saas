namespace DevExpress.Xpf.Docking.Base
{
    using DevExpress.Xpf.Docking;
    using System;
    using System.Runtime.CompilerServices;

    public class LayoutItemEndRenamingEventArgs : ItemEventArgs
    {
        public LayoutItemEndRenamingEventArgs(BaseLayoutItem item, string oldCaption) : this(item, oldCaption, false)
        {
        }

        public LayoutItemEndRenamingEventArgs(BaseLayoutItem item, string oldCaption, bool canceled) : base(item)
        {
            base.RoutedEvent = DockLayoutManager.LayoutItemEndRenamingEvent;
            this.OldCaption = oldCaption;
            this.NewCaption = (item != null) ? (item.Caption as string) : null;
            this.Canceled = canceled;
        }

        public string OldCaption { get; private set; }

        public string NewCaption { get; private set; }

        public bool Canceled { get; private set; }
    }
}

