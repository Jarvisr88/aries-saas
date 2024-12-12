namespace DevExpress.Xpf.Docking.Base
{
    using DevExpress.Xpf.Docking;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public class BeforeItemAddedEventArgs : RoutedEventArgs
    {
        public BeforeItemAddedEventArgs(object item, BaseLayoutItem potentialTarget)
        {
            base.RoutedEvent = DockLayoutManager.BeforeItemAddedEvent;
            this.Item = item;
            this.Target = potentialTarget;
        }

        public BaseLayoutItem Target { get; set; }

        public object Item { get; private set; }

        public bool Cancel { get; set; }
    }
}

