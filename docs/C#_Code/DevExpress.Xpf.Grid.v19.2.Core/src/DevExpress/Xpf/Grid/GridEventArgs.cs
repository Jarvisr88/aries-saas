namespace DevExpress.Xpf.Grid
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public class GridEventArgs : RoutedEventArgs
    {
        public GridEventArgs(DataControlBase dataControl, RoutedEvent routedEvent) : base(routedEvent)
        {
            this.Source = dataControl;
        }

        public DataControlBase Source { get; private set; }
    }
}

