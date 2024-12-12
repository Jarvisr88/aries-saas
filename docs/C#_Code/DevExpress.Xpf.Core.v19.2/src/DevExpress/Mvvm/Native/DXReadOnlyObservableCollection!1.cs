namespace DevExpress.Mvvm.Native
{
    using System;
    using System.Collections.ObjectModel;
    using System.Collections.Specialized;
    using System.ComponentModel;

    public class DXReadOnlyObservableCollection<T> : ReadOnlyObservableCollection<T>
    {
        public event NotifyCollectionChangedEventHandler CollectionChanged
        {
            add
            {
                base.CollectionChanged += value;
            }
            remove
            {
                base.CollectionChanged -= value;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged
        {
            add
            {
                base.PropertyChanged += value;
            }
            remove
            {
                base.PropertyChanged -= value;
            }
        }

        public DXReadOnlyObservableCollection(ObservableCollection<T> list) : base(list)
        {
        }
    }
}

