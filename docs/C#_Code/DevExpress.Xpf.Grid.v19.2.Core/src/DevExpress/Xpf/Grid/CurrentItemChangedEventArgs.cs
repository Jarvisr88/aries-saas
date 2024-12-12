namespace DevExpress.Xpf.Grid
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public class CurrentItemChangedEventArgs : RoutedEventArgs
    {
        public CurrentItemChangedEventArgs(DataControlBase source, object oldItem, object newItem)
        {
            this.OldItem = oldItem;
            this.NewItem = newItem;
            this.Source = source;
        }

        public object OldItem { get; private set; }

        public object NewItem { get; private set; }

        public DataControlBase Source { get; private set; }
    }
}

