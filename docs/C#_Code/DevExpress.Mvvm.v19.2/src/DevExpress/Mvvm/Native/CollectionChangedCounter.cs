namespace DevExpress.Mvvm.Native
{
    using System;
    using System.Collections.Specialized;

    public class CollectionChangedCounter : EventFireCounter<INotifyCollectionChanged, NotifyCollectionChangedEventArgs>
    {
        public CollectionChangedCounter(INotifyCollectionChanged collection) : base(delegate (EventHandler h) {
            collection.CollectionChanged += (o, e) => h(o, e);
        }, null)
        {
        }
    }
}

