namespace DevExpress.Xpf.Docking.Base
{
    using DevExpress.Xpf.Docking;
    using DevExpress.Xpf.Layout.Core;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public class DockItemDockingEventArgs : ItemCancelEventArgs
    {
        public DockItemDockingEventArgs(BaseLayoutItem item, Point pt, BaseLayoutItem target, DevExpress.Xpf.Layout.Core.DockType type, bool isHiding) : this(false, item, pt, target, type, isHiding)
        {
        }

        public DockItemDockingEventArgs(bool cancel, BaseLayoutItem item, Point pt, BaseLayoutItem target, DevExpress.Xpf.Layout.Core.DockType type, bool isHiding) : base(cancel, item)
        {
            this.DragPoint = pt;
            this.DockTarget = target;
            this.DockType = type;
            this.IsHiding = isHiding;
        }

        public Point DragPoint { get; private set; }

        public BaseLayoutItem DockTarget { get; private set; }

        public DevExpress.Xpf.Layout.Core.DockType DockType { get; private set; }

        public bool IsHiding { get; private set; }
    }
}

