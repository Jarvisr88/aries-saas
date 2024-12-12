namespace DevExpress.Xpf.Grid
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public class ItemsSourceChangedEventArgs : RoutedEventArgs
    {
        public ItemsSourceChangedEventArgs(DataControlBase source, object oldDataSource, object newDataSource)
        {
            this.OldItemsSource = oldDataSource;
            this.NewItemsSource = newDataSource;
            this.Source = source;
        }

        [Obsolete("Instead use the OldItemsSource property.")]
        public object OldDataSource =>
            this.OldItemsSource;

        [Obsolete("Instead use the NewItemsSource property.")]
        public object NewDataSource =>
            this.NewItemsSource;

        public object OldItemsSource { get; private set; }

        public object NewItemsSource { get; private set; }

        public DataControlBase Source { get; private set; }
    }
}

