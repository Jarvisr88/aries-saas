namespace DevExpress.Xpf.Docking.Base
{
    using DevExpress.Xpf.Docking;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class DockItemClosedEventArgs : ItemEventArgs
    {
        public DockItemClosedEventArgs(BaseLayoutItem item, IEnumerable<BaseLayoutItem> affectedItems) : base(item)
        {
            base.RoutedEvent = DockLayoutManager.DockItemClosedEvent;
            this.AffectedItems = affectedItems;
        }

        public IEnumerable<BaseLayoutItem> AffectedItems { get; private set; }
    }
}

