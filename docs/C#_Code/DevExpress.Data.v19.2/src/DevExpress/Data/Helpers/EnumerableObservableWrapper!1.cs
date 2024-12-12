namespace DevExpress.Data.Helpers
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public class EnumerableObservableWrapper<T> : List<T>, INotifyCollectionChanged, IWeakEventListener, IRefreshable
    {
        private readonly IEnumerable enumerable;
        [CompilerGenerated]
        private NotifyCollectionChangedEventHandler CollectionChanged;

        public event NotifyCollectionChangedEventHandler CollectionChanged;

        public EnumerableObservableWrapper(IEnumerable enumerable);
        void IRefreshable.Refresh();
        private void OnInnerCollectionChanged(NotifyCollectionChangedEventArgs e);
        private void Repopulate();
        bool IWeakEventListener.ReceiveWeakEvent(Type managerType, object sender, EventArgs e);
    }
}

