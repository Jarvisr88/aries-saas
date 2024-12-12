namespace DevExpress.Xpf.Docking.Base
{
    using DevExpress.Xpf.Docking;
    using System;
    using System.Runtime.CompilerServices;

    public class DockOperationStartingEventArgs : ItemCancelEventArgs
    {
        public DockOperationStartingEventArgs(BaseLayoutItem item, DevExpress.Xpf.Docking.DockOperation dockOperation) : this(item, null, dockOperation)
        {
        }

        public DockOperationStartingEventArgs(BaseLayoutItem item, BaseLayoutItem dockTarget, DevExpress.Xpf.Docking.DockOperation dockOperation) : base(item)
        {
            this.DockOperation = dockOperation;
            base.RoutedEvent = DockLayoutManager.DockOperationStartingEvent;
            this.DockTarget = dockTarget;
        }

        public DevExpress.Xpf.Docking.DockOperation DockOperation { get; private set; }

        public BaseLayoutItem DockTarget { get; private set; }
    }
}

