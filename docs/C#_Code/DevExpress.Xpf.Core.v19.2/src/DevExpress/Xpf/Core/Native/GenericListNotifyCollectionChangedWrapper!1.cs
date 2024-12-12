namespace DevExpress.Xpf.Core.Native
{
    using DevExpress.Data.Helpers;
    using System;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public class GenericListNotifyCollectionChangedWrapper<T> : GenericListWrapper<T>, INotifyCollectionChanged, IWeakEventListener, IRefreshable
    {
        [CompilerGenerated]
        private NotifyCollectionChangedEventHandler CollectionChanged;

        public event NotifyCollectionChangedEventHandler CollectionChanged;

        public GenericListNotifyCollectionChangedWrapper(IList<T> list);
        void IRefreshable.Refresh();
        private void OnInnerCollectionChanged(NotifyCollectionChangedEventArgs e);
        bool IWeakEventListener.ReceiveWeakEvent(Type managerType, object sender, EventArgs e);
    }
}

