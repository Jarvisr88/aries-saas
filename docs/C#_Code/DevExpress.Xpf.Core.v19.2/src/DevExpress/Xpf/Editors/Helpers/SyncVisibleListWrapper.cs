namespace DevExpress.Xpf.Editors.Helpers
{
    using DevExpress.Data;
    using DevExpress.Data.Filtering;
    using DevExpress.Data.Helpers;
    using DevExpress.Mvvm.Native;
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

    public class SyncVisibleListWrapper : VisibleListWrapper, ICollectionView, IEnumerable, INotifyCollectionChanged, IListServerDataView, IListServer, IList, ICollection, IListServerHints, IDataControllerAdapter, ITypedList
    {
        private readonly SyncServerModeCurrentDataView dataView;
        private readonly SyncListWrapper wrapper;
        private readonly bool isClonedServer;
        private bool initialized;
        private IApplyServerWrapper applyServerWrapper;

        public event EventHandler CurrentChanged;

        public event CurrentChangingEventHandler CurrentChanging;

        public event EventHandler<ServerModeExceptionThrownEventArgs> ExceptionThrown;

        public event EventHandler<ServerModeInconsistencyDetectedEventArgs> InconsistencyDetected;

        public SyncVisibleListWrapper(SyncServerModeCurrentDataView dataView, SyncListWrapper wrapper = null)
        {
            this.isClonedServer = ReferenceEquals(wrapper, null);
            SyncListWrapper wrapper1 = wrapper;
            if (wrapper == null)
            {
                SyncListWrapper local1 = wrapper;
                wrapper1 = new SyncListWrapper(dataView.Wrapper.Server);
            }
            this.wrapper = wrapper1;
            this.dataView = dataView;
            dataView.ListChanged += new ListChangedEventHandler(this.DataViewListChanged);
            this.ItemsCache = new SyncDataProxyViewCache(this);
            this.View = new DataControllerItemsCache(this, dataView.selectNullValue);
        }

        public void ApplyControlServerSettings(CriteriaOperator filterCriteria, ICollection<ServerModeOrderDescriptor[]> sortInfo, int groupCount, ICollection<ServerModeSummaryDescriptor> groupSummaryInfo, ICollection<ServerModeSummaryDescriptor> totalSummaryInfo)
        {
            this.applyServerWrapper.ApplyControlSettings(filterCriteria, sortInfo, groupCount, groupSummaryInfo, totalSummaryInfo);
        }

        public void ApplyViewServerSettings(CriteriaOperator filterCriteria, ICollection<ServerModeOrderDescriptor[]> sortInfo, int groupCount, ICollection<ServerModeSummaryDescriptor> groupSummaryInfo, ICollection<ServerModeSummaryDescriptor> totalSummaryInfo)
        {
            this.applyServerWrapper.ApplyViewSettings(filterCriteria, sortInfo, groupCount, groupSummaryInfo, totalSummaryInfo);
        }

        protected override bool ContainsInternal(object value) => 
            base.IndexOf(value) > -1;

        protected override void CopyToInternal(Array array, int index)
        {
            int count = base.Count;
            for (int i = index; i < count; i++)
            {
                array.SetValue(this.GetRow(i), i);
            }
        }

        private void DataViewListChanged(object sender, ListChangedEventArgs e)
        {
            base.RaiseListChanged(e);
        }

        void IListServerHints.HintGridIsPaged(int pageSize)
        {
            (this.Server as IListServerHints).Do<IListServerHints>(x => x.HintGridIsPaged(pageSize));
        }

        void IListServerHints.HintMaxVisibleRowsInGrid(int rowsInGrid)
        {
            (this.Server as IListServerHints).Do<IListServerHints>(x => x.HintMaxVisibleRowsInGrid(rowsInGrid));
        }

        void IListServer.Apply(CriteriaOperator filterCriteria, ICollection<ServerModeOrderDescriptor[]> sortInfo, int groupCount, ICollection<ServerModeSummaryDescriptor> groupSummaryInfo, ICollection<ServerModeSummaryDescriptor> totalSummaryInfo)
        {
            throw new InvalidOperationException();
        }

        int IListServer.FindIncremental(CriteriaOperator expression, string value, int startIndex, bool searchUp, bool ignoreStartRow, bool allowLoop) => 
            this.Server.FindIncremental(expression, value, startIndex, searchUp, ignoreStartRow, allowLoop);

        IList IListServer.GetAllFilteredAndSortedRows() => 
            this.Server.GetAllFilteredAndSortedRows();

        List<ListSourceGroupInfo> IListServer.GetGroupInfo(ListSourceGroupInfo parentGroup) => 
            this.Server.GetGroupInfo(parentGroup);

        int IListServer.GetRowIndexByKey(object key) => 
            this.Server.GetRowIndexByKey(key);

        object IListServer.GetRowKey(int index) => 
            this.Server.GetRowKey(index);

        List<object> IListServer.GetTotalSummary() => 
            this.Server.GetTotalSummary();

        object[] IListServer.GetUniqueColumnValues(CriteriaOperator valuesExpression, int maxCount, CriteriaOperator filterExpression, bool ignoreAppliedFilter) => 
            this.Server.GetUniqueColumnValues(valuesExpression, maxCount, filterExpression, ignoreAppliedFilter);

        int IListServer.LocateByExpression(CriteriaOperator expression, int startIndex, bool searchUp) => 
            this.Server.LocateByExpression(expression, startIndex, searchUp);

        int IListServer.LocateByValue(CriteriaOperator expression, object value, int startIndex, bool searchUp) => 
            this.Server.LocateByValue(expression, value, startIndex, searchUp);

        bool IListServer.PrefetchRows(ListSourceGroupInfo[] groupsToPrefetch, CancellationToken cancellationToken) => 
            this.Server.PrefetchRows(groupsToPrefetch, cancellationToken);

        void IListServer.Refresh()
        {
            this.Server.Refresh();
            this.ItemsCache.Reset();
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

        void IListServerDataView.NotifyExceptionThrown(ServerModeExceptionThrownEventArgs e)
        {
            this.RaiseExceptionThrown(e);
        }

        void IListServerDataView.NotifyInconsistencyDetected(ServerModeInconsistencyDetectedEventArgs e)
        {
            this.Reset();
            this.RaiseInconsistencyDetected(e);
        }

        protected override void DisposeInternal()
        {
            this.dataView.ListChanged -= new ListChangedEventHandler(this.DataViewListChanged);
        }

        private void EndDefer()
        {
            base.RaiseCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
        }

        protected override int GetCountInternal() => 
            this.wrapper.Count;

        [IteratorStateMachine(typeof(<GetEnumeratorInternal>d__21))]
        protected override IEnumerator GetEnumeratorInternal()
        {
            <GetEnumeratorInternal>d__21 d__1 = new <GetEnumeratorInternal>d__21(0);
            d__1.<>4__this = this;
            return d__1;
        }

        private object GetRow(int index) => 
            this.ItemsCache[index];

        public object GetValueFromItem(object item)
        {
            int num = this.View.IndexByItem(item);
            if (num < 0)
            {
                return null;
            }
            DataProxy proxy = this.ItemsCache[num];
            return ((proxy != null) ? this.GetValueFromProxy(proxy) : null);
        }

        public object GetValueFromProxy(DataProxy proxy) => 
            this.DataAccessor.GetValue(proxy);

        protected override object IndexerGetInternal(int index) => 
            this.GetRow(index);

        protected override int IndexOfInternal(object value) => 
            this.View.IndexOfValue(value);

        public void Initialize(IApplyServerWrapper applyServerWrapper = null)
        {
            if (!this.initialized)
            {
                applyServerWrapper ??= new ComboBoxApplyServerWrapper(this);
                this.applyServerWrapper = applyServerWrapper;
                this.wrapper.Initialize(this);
                this.SetupWrapper();
                this.initialized = true;
            }
            this.dataView.InitializeVisibleList();
        }

        public void ProcessInconsistencyDetected()
        {
            this.Reset();
            base.RaiseCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
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

        private void RaiseExceptionThrown(ServerModeExceptionThrownEventArgs args)
        {
            EventHandler<ServerModeExceptionThrownEventArgs> exceptionThrown = this.ExceptionThrown;
            if (exceptionThrown != null)
            {
                exceptionThrown(this, args);
            }
        }

        private void RaiseInconsistencyDetected(ServerModeInconsistencyDetectedEventArgs args)
        {
            EventHandler<ServerModeInconsistencyDetectedEventArgs> inconsistencyDetected = this.InconsistencyDetected;
            if (inconsistencyDetected != null)
            {
                inconsistencyDetected(this, args);
            }
        }

        protected override void RefreshInternal()
        {
            this.wrapper.Refresh();
            this.Reset();
        }

        public void Reset()
        {
            this.ItemsCache.Reset();
            this.View.Reset();
            base.RaiseCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
        }

        private void SetupWrapper()
        {
            if (this.isClonedServer)
            {
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

        private IListServer Server =>
            this.wrapper.Server;

        private SyncDataProxyViewCache ItemsCache { get; set; }

        private DataControllerItemsCache View { get; set; }

        public DevExpress.Xpf.Editors.Helpers.DataAccessor DataAccessor =>
            this.dataView.DataAccessor;

        public SyncListWrapper Wrapper =>
            this.wrapper;

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

        [CompilerGenerated]
        private sealed class <GetEnumeratorInternal>d__21 : IEnumerator<object>, IDisposable, IEnumerator
        {
            private int <>1__state;
            private object <>2__current;
            public SyncVisibleListWrapper <>4__this;
            private int <i>5__1;
            private int <count>5__2;

            [DebuggerHidden]
            public <GetEnumeratorInternal>d__21(int <>1__state)
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
            private SyncVisibleListWrapper collectionView;

            public DeferHelper(SyncVisibleListWrapper collectionView)
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

