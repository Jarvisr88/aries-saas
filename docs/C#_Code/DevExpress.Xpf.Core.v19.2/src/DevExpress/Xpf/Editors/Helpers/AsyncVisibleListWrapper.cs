namespace DevExpress.Xpf.Editors.Helpers
{
    using DevExpress.Data;
    using DevExpress.Data.Async;
    using DevExpress.Data.Filtering;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Collections.Specialized;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Globalization;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Threading;

    public class AsyncVisibleListWrapper : VisibleListWrapper, ICollectionView, IEnumerable, INotifyCollectionChanged, IAsyncListServer, IAsyncListServerDataView, IDataControllerAdapter, IAsyncResultReceiverBusyChangedListener, ITypedList
    {
        private readonly AsyncServerModeCurrentDataView dataView;
        private readonly AsyncListWrapper wrapper;
        private AsyncSharedResultReceiver sharedReceiver;
        private readonly bool isClonedServer;
        private bool initialized;
        private IAsyncApplyServerWrapper applyServerWrapper;

        public event EventHandler CurrentChanged;

        public event CurrentChangingEventHandler CurrentChanging;

        public AsyncVisibleListWrapper(AsyncServerModeCurrentDataView dataView, AsyncListWrapper wrapper = null)
        {
            this.isClonedServer = ReferenceEquals(wrapper, null);
            AsyncListWrapper wrapper1 = wrapper;
            if (wrapper == null)
            {
                AsyncListWrapper local1 = wrapper;
                wrapper1 = new AsyncListWrapper(dataView.Wrapper.Server);
            }
            this.wrapper = wrapper1;
            this.dataView = dataView;
            dataView.InconsistencyDetected += new EventHandler(this.DataViewOnInconsistencyDetected);
            dataView.ListChanged += new ListChangedEventHandler(this.DataViewListChanged);
            this.<ItemsCache>k__BackingField = this.CreateDataProxyViewCache();
            this.<View>k__BackingField = new DataControllerItemsCache(this, false);
        }

        public CommandApply ApplyControlServerSettings(CriteriaOperator filterCriteria, ICollection<ServerModeOrderDescriptor[]> sortInfo, int groupCount, ICollection<ServerModeSummaryDescriptor> groupSummaryInfo, ICollection<ServerModeSummaryDescriptor> totalSummaryInfo, params DictionaryEntry[] tags) => 
            this.applyServerWrapper.ApplyControlSettings(filterCriteria, sortInfo, groupCount, groupSummaryInfo, totalSummaryInfo, tags);

        public CommandApply ApplyViewServerSettings(CriteriaOperator filterCriteria, ICollection<ServerModeOrderDescriptor[]> sortInfo, int groupCount, ICollection<ServerModeSummaryDescriptor> groupSummaryInfo, ICollection<ServerModeSummaryDescriptor> totalSummaryInfo, params DictionaryEntry[] tags) => 
            this.applyServerWrapper.ApplyViewSettings(filterCriteria, sortInfo, groupCount, groupSummaryInfo, totalSummaryInfo, tags);

        public void CancelItem(int visibleIndex)
        {
            this.wrapper.CancelRow(visibleIndex);
        }

        protected override bool ContainsInternal(object value) => 
            false;

        protected override void CopyToInternal(Array array, int index)
        {
            int count = base.Count;
            for (int i = index; i < count; i++)
            {
                array.SetValue(this.GetRow(i), i);
            }
        }

        protected virtual AsyncDataProxyViewCache CreateDataProxyViewCache() => 
            new AsyncDataProxyViewCache(this);

        private void DataViewListChanged(object sender, ListChangedEventArgs e)
        {
            base.RaiseListChanged(e);
        }

        private void DataViewOnInconsistencyDetected(object sender, EventArgs eventArgs)
        {
        }

        CommandApply IAsyncListServer.Apply(CriteriaOperator filterCriteria, ICollection<ServerModeOrderDescriptor[]> sortInfo, int groupCount, ICollection<ServerModeSummaryDescriptor> summaryInfo, ICollection<ServerModeSummaryDescriptor> totalSummaryInfo, params DictionaryEntry[] tags) => 
            this.Server.Apply(filterCriteria, sortInfo, groupCount, summaryInfo, totalSummaryInfo, tags);

        void IAsyncListServer.Cancel<T>() where T: Command
        {
            this.Server.Cancel<T>();
        }

        void IAsyncListServer.Cancel(Command command)
        {
            this.Server.Cancel(command);
        }

        CommandFindIncremental IAsyncListServer.FindIncremental(CriteriaOperator expression, string value, int startIndex, bool searchUp, bool ignoreStartRow, bool allowLoop, params DictionaryEntry[] tags) => 
            this.Server.FindIncremental(expression, value, startIndex, searchUp, ignoreStartRow, allowLoop, tags);

        CommandGetAllFilteredAndSortedRows IAsyncListServer.GetAllFilteredAndSortedRows(params DictionaryEntry[] tags) => 
            this.Server.GetAllFilteredAndSortedRows(tags);

        CommandGetGroupInfo IAsyncListServer.GetGroupInfo(ListSourceGroupInfo parentGroup, params DictionaryEntry[] tags) => 
            this.Server.GetGroupInfo(parentGroup, tags);

        CommandGetRow IAsyncListServer.GetRow(int index, params DictionaryEntry[] tags) => 
            this.Server.GetRow(index, tags);

        CommandGetRowIndexByKey IAsyncListServer.GetRowIndexByKey(object key, params DictionaryEntry[] tags) => 
            this.Server.GetRowIndexByKey(key, tags);

        CommandGetTotals IAsyncListServer.GetTotals(params DictionaryEntry[] tags) => 
            this.Server.GetTotals(tags);

        CommandGetUniqueColumnValues IAsyncListServer.GetUniqueColumnValues(CriteriaOperator valuesExpression, int maxCount, CriteriaOperator filterExpression, bool ignoreAppliedFilter, params DictionaryEntry[] tags) => 
            this.Server.GetUniqueColumnValues(valuesExpression, maxCount, filterExpression, ignoreAppliedFilter, tags);

        CommandLocateByValue IAsyncListServer.LocateByValue(CriteriaOperator expression, object value, int startIndex, bool searchUp, params DictionaryEntry[] tags) => 
            this.Server.LocateByValue(expression, value, startIndex, searchUp, tags);

        CommandPrefetchRows IAsyncListServer.PrefetchRows(ListSourceGroupInfo[] groupsToPrefetch, params DictionaryEntry[] tags) => 
            this.Server.PrefetchRows(groupsToPrefetch, tags);

        T IAsyncListServer.PullNext<T>() where T: Command => 
            this.Server.PullNext<T>();

        CommandRefresh IAsyncListServer.Refresh(params DictionaryEntry[] tags) => 
            this.Server.Refresh(tags);

        void IAsyncListServer.SetReceiver(IAsyncResultReceiver receiver)
        {
            this.sharedReceiver.AddReceiver(receiver);
        }

        bool IAsyncListServer.WaitFor(Command command) => 
            this.Server.WaitFor(command);

        void IAsyncListServer.WeakCancel<T>() where T: Command
        {
            this.Server.WeakCancel<T>();
        }

        void IAsyncListServerDataView.BusyChanged(bool busy)
        {
            this.ProcessBusyChanged(busy);
        }

        object IAsyncListServerDataView.GetValueFromProxy(DataProxy proxy) => 
            this.dataView.GetValueFromProxy(proxy);

        void IAsyncListServerDataView.NotifyApply()
        {
            this.ProcessApply();
        }

        void IAsyncListServerDataView.NotifyCountChanged()
        {
            this.ProcessCountChanged();
        }

        void IAsyncListServerDataView.NotifyExceptionThrown(ServerModeExceptionThrownEventArgs e)
        {
        }

        void IAsyncListServerDataView.NotifyFindIncrementalCompleted(string text, int startIndex, bool searchNext, bool ignoreStartIndex, int controllerIndex)
        {
            throw new NotImplementedException();
        }

        void IAsyncListServerDataView.NotifyInconsistencyDetected(ServerModeInconsistencyDetectedEventArgs e)
        {
            this.dataView.ProcessInconsistencyDetected();
        }

        void IAsyncListServerDataView.NotifyLoaded(int listSourceIndex)
        {
            this.ProcessRowLoaded(listSourceIndex);
        }

        void IAsyncResultReceiverBusyChangedListener.ProcessBusyChanged(bool busyChanged)
        {
        }

        int IDataControllerAdapter.GetListSourceIndex(object value) => 
            this.ItemsCache.FindIndexByValue(ListServerDataViewBase.CreateCriteriaForValueColumn(this.DataAccessor), value);

        object IDataControllerAdapter.GetRow(int listSourceIndex) => 
            (listSourceIndex > -1) ? this.ItemsCache[listSourceIndex].f_component : null;

        object IDataControllerAdapter.GetRowValue(int listSourceIndex)
        {
            DataProxy proxy = this.ItemsCache[listSourceIndex];
            return this.GetValueFromProxy(proxy);
        }

        object IDataControllerAdapter.GetRowValue(object item)
        {
            DataProxy proxy = this.DataAccessor.CreateProxy(item, -1);
            object valueFromProxy = this.GetValueFromProxy(proxy);
            return base.IndexOf(valueFromProxy);
        }

        protected override void DisposeInternal()
        {
            this.dataView.ListChanged -= new ListChangedEventHandler(this.DataViewListChanged);
            if (this.isClonedServer)
            {
                this.wrapper.Dispose();
            }
        }

        private void EndDefer()
        {
            base.RaiseCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
        }

        public void FetchItem(int visibleIndex)
        {
            this.wrapper.GetRow(visibleIndex, new OperationCompleted(this.FetchItemCompleted));
        }

        private void FetchItemCompleted(object arguments)
        {
            CommandGetRow row = (CommandGetRow) arguments;
            this.ProcessRowLoaded(row.Index);
        }

        protected override int GetCountInternal() => 
            this.wrapper.Count;

        [IteratorStateMachine(typeof(<GetEnumeratorInternal>d__24))]
        protected override IEnumerator GetEnumeratorInternal()
        {
            <GetEnumeratorInternal>d__24 d__1 = new <GetEnumeratorInternal>d__24(0);
            d__1.<>4__this = this;
            return d__1;
        }

        private object GetRow(int index) => 
            this.wrapper.IsRowLoaded(index) ? this.ItemsCache[index] : this.ItemsCache.TempProxy;

        public object GetValueFromItem(object item)
        {
            int num = this.View.IndexByItem(item);
            DataProxy proxy = this.ItemsCache[num];
            return this.GetValueFromProxy(proxy);
        }

        public object GetValueFromProxy(DataProxy proxy) => 
            this.DataAccessor.GetValue(proxy);

        protected override object IndexerGetInternal(int index) => 
            this.GetRow(index);

        protected override int IndexOfInternal(object value) => 
            this.View.IndexOfValue(value);

        public void Initialize(IAsyncApplyServerWrapper applyServerWrapper = null)
        {
            if (!this.initialized)
            {
                applyServerWrapper ??= new AsyncComboBoxApplyServerWrapper(this);
                this.applyServerWrapper = applyServerWrapper;
                this.wrapper.Initialize(this);
                this.SetupWrapper();
                this.initialized = true;
            }
            this.dataView.InitializeVisibleList();
        }

        public bool IsTempItem(int visibleIndex) => 
            !this.wrapper.IsRowLoaded(visibleIndex);

        private void ProcessApply()
        {
            this.ItemsCache.Reset();
            base.RaiseCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
        }

        private void ProcessBusyChanged(bool busy)
        {
            this.dataView.ProcessVisibleListBusyChanged(busy);
        }

        private void ProcessCountChanged()
        {
            base.RaiseCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
        }

        private void ProcessRowLoaded(int index)
        {
            this.View.UpdateItem(index);
            object row = this.GetRow(index);
            base.RaiseCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Replace, row, this.ItemsCache.TempProxy, index));
        }

        private void RaiseCurrentChanged()
        {
            if (this.CurrentChanged != null)
            {
                this.CurrentChanged(this, EventArgs.Empty);
            }
        }

        private void RaiseCurrentChanging()
        {
            if (this.CurrentChanging != null)
            {
                this.CurrentChanging(this, new CurrentChangingEventArgs());
            }
        }

        protected override void RefreshInternal()
        {
        }

        private void SetupWrapper()
        {
            if (this.isClonedServer)
            {
                this.sharedReceiver = new AsyncSharedResultReceiver(this.wrapper);
                this.sharedReceiver.AddReceiver(new AsyncNotifyBusyChangedResultReceiver(this));
                this.wrapper.SetReceiver(this.sharedReceiver);
                this.wrapper.Reset();
            }
        }

        IDisposable ICollectionView.DeferRefresh() => 
            new DeferHelper(this);

        bool ICollectionView.MoveCurrentTo(object item) => 
            false;

        bool ICollectionView.MoveCurrentToFirst() => 
            false;

        bool ICollectionView.MoveCurrentToLast() => 
            false;

        bool ICollectionView.MoveCurrentToNext() => 
            false;

        bool ICollectionView.MoveCurrentToPosition(int position) => 
            false;

        bool ICollectionView.MoveCurrentToPrevious() => 
            false;

        void ICollectionView.Refresh()
        {
        }

        PropertyDescriptorCollection ITypedList.GetItemProperties(PropertyDescriptor[] listAccessors) => 
            ((ITypedList) this.Server).GetItemProperties(listAccessors);

        string ITypedList.GetListName(PropertyDescriptor[] listAccessors) => 
            string.Empty;

        private IAsyncListServer Server =>
            this.wrapper.Server;

        private AsyncDataProxyViewCache ItemsCache { get; }

        private DataControllerItemsCache View { get; }

        private DevExpress.Xpf.Editors.Helpers.DataAccessor DataAccessor =>
            this.dataView.DataAccessor;

        public AsyncListWrapper Wrapper =>
            this.wrapper;

        public object TempProxy =>
            this.ItemsCache.TempProxy;

        int IDataControllerAdapter.VisibleRowCount =>
            this.ItemsCache.Count;

        bool IDataControllerAdapter.IsOwnSearchProcessing =>
            true;

        CultureInfo ICollectionView.Culture
        {
            get => 
                CultureInfo.CurrentCulture;
            set
            {
            }
        }

        IEnumerable ICollectionView.SourceCollection =>
            this;

        Predicate<object> ICollectionView.Filter
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        bool ICollectionView.CanFilter =>
            false;

        SortDescriptionCollection ICollectionView.SortDescriptions =>
            new SortDescriptionCollection();

        bool ICollectionView.CanSort =>
            false;

        bool ICollectionView.CanGroup =>
            false;

        ObservableCollection<GroupDescription> ICollectionView.GroupDescriptions =>
            new ObservableCollection<GroupDescription>();

        ReadOnlyObservableCollection<object> ICollectionView.Groups
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        bool ICollectionView.IsEmpty =>
            this.dataView.Wrapper.Count == 0;

        object ICollectionView.CurrentItem =>
            null;

        int ICollectionView.CurrentPosition =>
            -1;

        bool ICollectionView.IsCurrentAfterLast =>
            false;

        bool ICollectionView.IsCurrentBeforeFirst =>
            false;

        AsyncListWrapper IAsyncListServerDataView.Wrapper =>
            this.wrapper;

        DevExpress.Xpf.Editors.Helpers.DataAccessor IAsyncListServerDataView.DataAccessor =>
            this.dataView.DataAccessor;

        [CompilerGenerated]
        private sealed class <GetEnumeratorInternal>d__24 : IEnumerator<object>, IDisposable, IEnumerator
        {
            private int <>1__state;
            private object <>2__current;
            public AsyncVisibleListWrapper <>4__this;
            private int <i>5__1;
            private int <count>5__2;

            [DebuggerHidden]
            public <GetEnumeratorInternal>d__24(int <>1__state)
            {
                this.<>1__state = <>1__state;
            }

            private bool MoveNext()
            {
                int num = this.<>1__state;
                if (num == 0)
                {
                    this.<>1__state = -1;
                    this.<count>5__2 = this.<>4__this.wrapper.Count;
                    this.<i>5__1 = 0;
                }
                else
                {
                    if (num != 1)
                    {
                        return false;
                    }
                    this.<>1__state = -1;
                    int num2 = this.<i>5__1;
                    this.<i>5__1 = num2 + 1;
                }
                if (this.<i>5__1 >= this.<count>5__2)
                {
                    return false;
                }
                this.<>2__current = this.<>4__this.GetRow(this.<i>5__1);
                this.<>1__state = 1;
                return true;
            }

            [DebuggerHidden]
            void IEnumerator.Reset()
            {
                throw new NotSupportedException();
            }

            [DebuggerHidden]
            void IDisposable.Dispose()
            {
            }

            object IEnumerator<object>.Current =>
                this.<>2__current;

            object IEnumerator.Current =>
                this.<>2__current;
        }

        private class DeferHelper : IDisposable
        {
            private AsyncVisibleListWrapper collectionView;

            public DeferHelper(AsyncVisibleListWrapper collectionView)
            {
                this.collectionView = collectionView;
            }

            public void Dispose()
            {
                if (this.collectionView != null)
                {
                    this.collectionView.EndDefer();
                    this.collectionView = null;
                }
                GC.SuppressFinalize(this);
            }
        }
    }
}

