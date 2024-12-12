namespace DevExpress.Xpf.Editors.Helpers
{
    using DevExpress.Xpf.Core;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.ComponentModel;
    using System.Windows.Data;

    public class AsyncServerModeCollectionView : CollectionView, IServerModeCollectionView, ICollectionView, IEnumerable, INotifyCollectionChanged
    {
        private readonly AsyncVisibleListWrapper asyncVisibleListWrapper;
        private readonly Locker updateSelectionLocker;
        private UglyHackContainer container;

        public AsyncServerModeCollectionView(AsyncVisibleListWrapper asyncVisibleListWrapper) : base(Enumerable.Empty<object>())
        {
            this.updateSelectionLocker = new Locker();
            this.asyncVisibleListWrapper = asyncVisibleListWrapper;
            asyncVisibleListWrapper.Initialize(null);
            asyncVisibleListWrapper.CollectionChanged += new NotifyCollectionChangedEventHandler(this.OnVisibleListCollectionChanged);
        }

        public void CancelItem(int visibleIndex)
        {
            this.Wrapper.CancelItem(visibleIndex);
        }

        public override bool Contains(object item) => 
            this.Wrapper.Contains(item);

        public override IDisposable DeferRefresh() => 
            this.SourceCollection.DeferRefresh();

        public void FetchItem(int visibleIndex)
        {
            this.Wrapper.FetchItem(visibleIndex);
        }

        public object GetItem(int visibleIndex) => 
            this.asyncVisibleListWrapper[visibleIndex];

        public override object GetItemAt(int index) => 
            this.Wrapper[index];

        public override int IndexOf(object item)
        {
            DataProxy proxy = item as DataProxy;
            if ((proxy == null) || (proxy.f_component == null))
            {
                return -1;
            }
            object valueFromItem = this.Wrapper.GetValueFromItem(proxy.f_component);
            return this.IndexOfValue(valueFromItem);
        }

        public int IndexOfValue(object value) => 
            this.Wrapper.IndexOf(value);

        public bool IsTempItem(int visibleIndex) => 
            this.Wrapper.IsTempItem(visibleIndex);

        public IDisposable LockWhileUpdatingSelection(IEnumerable<int> allowedIndices)
        {
            this.container = new UglyHackContainer(allowedIndices, x => this.Wrapper[x]);
            return this.updateSelectionLocker;
        }

        private void OnVisibleListCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            this.OnCollectionChanged(e);
        }

        private AsyncVisibleListWrapper Wrapper =>
            this.asyncVisibleListWrapper;

        private ICollectionView SourceCollection =>
            this.asyncVisibleListWrapper;

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

        public override IComparer Comparer =>
            null;

        public override int Count =>
            this.Wrapper.Count;

        public override int CurrentPosition =>
            this.SourceCollection.CurrentPosition;
    }
}

