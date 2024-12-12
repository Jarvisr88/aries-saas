namespace DevExpress.Xpf.Docking
{
    using DevExpress.Xpf.Bars;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public class BarMergeEventArgs : RoutedEventArgs
    {
        public BarMergeEventArgs(DevExpress.Xpf.Bars.BarManager manager, DevExpress.Xpf.Bars.BarManager childManager)
        {
            this.BarManager = manager;
            this.ChildBarManager = childManager;
        }

        public DevExpress.Xpf.Bars.BarManager BarManager { get; private set; }

        public DevExpress.Xpf.Bars.BarManager ChildBarManager { get; private set; }

        public bool Cancel { get; set; }
    }
}

