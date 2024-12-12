namespace DevExpress.Xpf.Docking.Base
{
    using DevExpress.Xpf.Docking;
    using DevExpress.Xpf.Layout.Core;
    using System;
    using System.Runtime.CompilerServices;

    public class LayoutItemMovedEventArgs : ItemEventArgs
    {
        public LayoutItemMovedEventArgs(BaseLayoutItem item, BaseLayoutItem target, MoveType type) : base(item)
        {
            this.Target = target;
            this.Type = type;
            base.RoutedEvent = DockLayoutManager.LayoutItemMovedEvent;
        }

        public BaseLayoutItem Target { get; private set; }

        public MoveType Type { get; private set; }
    }
}

