namespace DevExpress.Xpf.Bars
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public class LayoutUpgradingEventArgs : RoutedEventArgs
    {
        public LayoutUpgradingEventArgs(BarManager manager);

        public BarManagerActionCollection NewItems { get; protected internal set; }
    }
}

