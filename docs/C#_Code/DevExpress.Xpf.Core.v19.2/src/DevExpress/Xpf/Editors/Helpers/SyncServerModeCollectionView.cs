namespace DevExpress.Xpf.Editors.Helpers
{
    using DevExpress.Xpf.Core;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.ComponentModel;
    using System.Windows.Data;

    public class SyncServerModeCollectionView : CollectionView, IServerModeCollectionView, ICollectionView, IEnumerable, INotifyCollectionChanged
    {
        private readonly SyncVisibleListWrapper syncVisibleListWrapper;
        private readonly Locker updateSelectionLocker;
        private UglyHackContainer container;

        public SyncServerModeCollectionView(SyncVisibleListWrapper syncVisibleListWrapper) : base(Enumerable.Empty<object>())
        {
            this.updateSelectionLocker = new Locker();
            this.syncVisibleListWrapper = syncVisibleListWrapper;
            syncVisibleListWrapper.Initialize(null);
            syncVisibleListWrapper.CollectionChanged += new NotifyCollectionChangedEventHandler(this.OnVisibleListCollectionChanged);
        }

        public override bool Contains(object item)
        {
            DataProxy proxy = item as DataProxy;
            if (proxy == null)
            {
                return false;
            }
            object valueFromProxy = this.Wrapper.GetValueFromProxy(proxy);
            return this.Wrapper.Contains(valueFromProxy);
        }

        public override IDisposable DeferRefresh() => 
            this.SourceCollection.DeferRefresh();

        void IServerModeCollectionView.CancelItem(int visibleIndex)
        {
        }

        void IServerModeCollectionView.FetchItem(int visibleIndex)
        {
        }

        object IServerModeCollectionView.GetItem(int visibleIndex) => 
            this.syncVisibleListWrapper[visibleIndex];

        bool IServerModeCollectionView.IsTempItem(int visibleIndex) => 
            false;

        protected override IEnumerator GetEnumerator() => 
            this.SourceCollection.GetEnumerator();

        public override object GetItemAt(int index) => 
            !this.updateSelectionLocker.IsLocked ? this.Wrapper[index] : this.container.GetItemAt(index);

        public override int IndexOf(object item)
        {
            DataProxy proxy = item as DataProxy;
            if (proxy == null)
            {
                return -1;
            }
            object valueFromProxy = this.Wrapper.GetValueFromProxy(proxy);
            return this.IndexOfValue(valueFromProxy);
        }

        public int IndexOfValue(object value) => 
            this.Wrapper.IndexOf(value);

        public IDisposable LockWhileUpdatingSelection(IEnumerable<int> allowedIndices)
        {
            this.container = new UglyHackContainer(allowedIndices, x => this.Wrapper[x]);
            return this.updateSelectionLocker.Lock();
        }

        private void OnVisibleListCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            this.OnCollectionChanged(e);
        }

        private SyncVisibleListWrapper Wrapper =>
            this.syncVisibleListWrapper;

        private ICollectionView SourceCollection =>
            this.syncVisibleListWrapper;

        public override IComparer Comparer =>
            null;

        public override int Count =>
            this.Wrapper.Count;

        public override bool IsEmpty =>
            this.SourceCollection.IsEmpty;

        public override object CurrentItem =>
            this.SourceCollection.CurrentItem;

        public override bool CanFilter =>
            this.SourceCollection.CanFilter;

        public override bool CanGroup =>
            this.SourceCollection.CanGroup;

        public override bool CanSort =>
            this.SourceCollection.CanSort;

        public override int CurrentPosition =>
            this.SourceCollection.CurrentPosition;
    }
}

