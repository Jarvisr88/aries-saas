namespace DevExpress.Xpf.Core
{
    using System;
    using System.Collections.ObjectModel;
    using System.Collections.Specialized;

    public class VersionedObservableCollection<T> : ObservableCollection<T>, ISupportCacheVersion
    {
        private Guid cacheVersion;

        public VersionedObservableCollection(Guid cacheVersion)
        {
            this.cacheVersion = cacheVersion;
        }

        public void RaiseCollectionChanged()
        {
            this.OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
        }

        Guid ISupportCacheVersion.CacheVersion =>
            this.cacheVersion;
    }
}

