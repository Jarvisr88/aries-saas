namespace DevExpress.Xpf.Grid
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public class FocusedViewChangedEventArgs : RoutedEventArgs
    {
        public FocusedViewChangedEventArgs(DataViewBase oldView, DataViewBase newView)
        {
            this.OldView = oldView;
            this.NewView = newView;
        }

        public DataViewBase OldView { get; private set; }

        public DataViewBase NewView { get; private set; }
    }
}

