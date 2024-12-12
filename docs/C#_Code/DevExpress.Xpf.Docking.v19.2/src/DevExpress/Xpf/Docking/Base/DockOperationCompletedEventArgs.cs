namespace DevExpress.Xpf.Docking.Base
{
    using DevExpress.Xpf.Docking;
    using System;
    using System.Runtime.CompilerServices;

    public class DockOperationCompletedEventArgs : ItemEventArgs
    {
        public DockOperationCompletedEventArgs(BaseLayoutItem item, DevExpress.Xpf.Docking.DockOperation dockOperation) : base(item)
        {
            this.DockOperation = dockOperation;
            base.RoutedEvent = DockLayoutManager.DockOperationCompletedEvent;
        }

        public DevExpress.Xpf.Docking.DockOperation DockOperation { get; private set; }
    }
}

