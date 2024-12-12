namespace DevExpress.Xpf.Docking.Base
{
    using DevExpress.Xpf.Docking;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public class MDIItemActivatedEventArgs : RoutedEventArgs
    {
        public MDIItemActivatedEventArgs(DocumentPanel item, DocumentPanel oldItem) : base(DockLayoutManager.MDIItemActivatedEvent)
        {
            this.Item = item;
            this.OldItem = oldItem;
        }

        public DocumentPanel Item { get; private set; }

        public DocumentPanel OldItem { get; private set; }
    }
}

