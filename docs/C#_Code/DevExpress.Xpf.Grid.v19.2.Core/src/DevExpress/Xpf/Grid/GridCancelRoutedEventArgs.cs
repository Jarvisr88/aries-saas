namespace DevExpress.Xpf.Grid
{
    using DevExpress.Xpf.Core;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public class GridCancelRoutedEventArgs : CancelRoutedEventArgs
    {
        public GridCancelRoutedEventArgs(DataControlBase dataControl, RoutedEvent routedEvent) : base(routedEvent)
        {
            this.Source = dataControl;
        }

        public DataControlBase Source { get; private set; }
    }
}

