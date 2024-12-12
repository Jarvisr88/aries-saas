namespace DevExpress.Xpf.Editors
{
    using System;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.ComponentModel;

    internal class ReadOnlyItemsSource<T> : List<T>, INotifyPropertyChanged, INotifyCollectionChanged
    {
        event NotifyCollectionChangedEventHandler INotifyCollectionChanged.CollectionChanged
        {
            add
            {
            }
            remove
            {
            }
        }

        event PropertyChangedEventHandler INotifyPropertyChanged.PropertyChanged
        {
            add
            {
            }
            remove
            {
            }
        }
    }
}

