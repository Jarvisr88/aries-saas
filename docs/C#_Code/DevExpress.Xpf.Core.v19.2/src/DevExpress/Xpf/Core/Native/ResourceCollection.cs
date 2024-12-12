namespace DevExpress.Xpf.Core.Native
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.Specialized;

    internal abstract class ResourceCollection : IEnumerable<string>, IEnumerable, INotifyCollectionChanged
    {
        private NotifyCollectionChangedEventHandler changedHandler;

        event NotifyCollectionChangedEventHandler INotifyCollectionChanged.CollectionChanged;

        protected ResourceCollection();
        protected abstract IEnumerable<string> EnumerateResourceKeys();
        protected void RaiseChanged();
        IEnumerator<string> IEnumerable<string>.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator();
    }
}

