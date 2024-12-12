namespace DevExpress.Xpf.Editors.Helpers
{
    using DevExpress.Data;
    using DevExpress.Data.Async;
    using DevExpress.Data.Filtering;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Runtime.CompilerServices;

    public class AsyncListServerDefaultDataView : ListServerDataViewBase, IAsyncListServerDataView
    {
        private string filterCriteria;
        private readonly IEnumerable<SortingInfo> actualSorting;
        private readonly int groupCount;

        public AsyncListServerDefaultDataView(bool selectNullValue, IAsyncListServer server, string valueMember, string displayMember, IEnumerable<GroupingInfo> groups, IEnumerable<SortingInfo> sorts, string filter) : base(selectNullValue, new AsyncListWrapper(server), valueMember, displayMember)
        {
            List<SortingInfo> list1;
            this.filterCriteria = filter;
            if (groups == null)
            {
                list1 = new List<SortingInfo>();
            }
            else
            {
                Func<GroupingInfo, SortingInfo> selector = <>c.<>9__13_0;
                if (<>c.<>9__13_0 == null)
                {
                    Func<GroupingInfo, SortingInfo> local1 = <>c.<>9__13_0;
                    selector = <>c.<>9__13_0 = x => new SortingInfo(x.FieldName, ListSortDirection.Ascending);
                }
                list1 = groups.Select<GroupingInfo, SortingInfo>(selector).ToList<SortingInfo>();
            }
            List<SortingInfo> list = list1;
            list.AddRange(sorts);
            this.actualSorting = list;
            this.groupCount = groups.Count<GroupingInfo>();
            this.Wrapper.Initialize(this);
        }

        public override CurrentDataView CreateCurrentDataView(object handle, IList<GroupingInfo> groups, IList<SortingInfo> sorts, string filterCriteria, string displayFilterCriteria, bool caseSensitiveFilter) => 
            new AsyncServerModeCurrentPlainDataView(this, handle, base.DataAccessor.ValueMember, base.DataAccessor.DisplayMember, groups, sorts, filterCriteria, displayFilterCriteria);

        protected override DataProxyViewCache CreateDataProxyViewCache(object source) => 
            new AsyncDataProxyViewCache(this);

        protected override object DataControllerAdapterGetRowInternal(int listSourceIndex) => 
            this.GetRow(listSourceIndex);

        void IAsyncListServerDataView.BusyChanged(bool isBusy)
        {
            if (this.IsBusy != isBusy)
            {
                this.IsBusy = isBusy;
                this.RaiseOnBusyChanged(new ItemsProviderOnBusyChangedEventArgs(isBusy));
            }
        }

        void IAsyncListServerDataView.NotifyApply()
        {
            base.ItemsCache.Reset();
            base.RaiseRefreshNeeded();
        }

        void IAsyncListServerDataView.NotifyCountChanged()
        {
        }

        void IAsyncListServerDataView.NotifyExceptionThrown(ServerModeExceptionThrownEventArgs e)
        {
        }

        void IAsyncListServerDataView.NotifyFindIncrementalCompleted(string text, int startIndex, bool searchNext, bool ignoreStartIndex, int controllerIndex)
        {
            this.NotifyFindIncrementalInternal(text, startIndex, searchNext, ignoreStartIndex, controllerIndex);
        }

        void IAsyncListServerDataView.NotifyInconsistencyDetected(ServerModeInconsistencyDetectedEventArgs e)
        {
            base.RaiseInconsistencyDetected();
        }

        void IAsyncListServerDataView.NotifyLoaded(int listSourceIndex)
        {
            this.ProcessChangeItem(listSourceIndex);
            object valueFromProxy = base.GetValueFromProxy(base[listSourceIndex]);
            this.RaiseRowLoaded(new ItemsProviderRowLoadedEventArgs(null, listSourceIndex, valueFromProxy));
        }

        protected override void DisposeInternal()
        {
            base.DisposeInternal();
            this.Wrapper.Dispose();
        }

        public void FetchCount()
        {
            this.View.FetchCount();
        }

        public void FetchItem(object value)
        {
            base.IndexOfValue(value);
        }

        protected override int FindListSourceIndexByValue(object value) => 
            this.View.FindIndexByValue(CreateCriteriaForValueColumn(base.DataAccessor), value);

        protected internal override bool GetIsAsyncServerMode() => 
            true;

        protected internal override bool GetIsOwnSearchProcessing() => 
            true;

        private object GetRow(int listSourceIndex)
        {
            GetRowCompleter completer = new GetRowCompleter(this);
            return this.Wrapper.GetRow(listSourceIndex, new OperationCompleted(completer.Completed));
        }

        protected virtual void InitializeSource()
        {
            Func<SortingInfo, ServerModeOrderDescriptor[]> selector = <>c.<>9__18_0;
            if (<>c.<>9__18_0 == null)
            {
                Func<SortingInfo, ServerModeOrderDescriptor[]> local1 = <>c.<>9__18_0;
                selector = <>c.<>9__18_0 = x => new ServerModeOrderDescriptor[] { new ServerModeOrderDescriptor(new OperandProperty(x.FieldName), x.OrderBy == ListSortDirection.Descending) };
            }
            this.Wrapper.ApplySortGroupFilter(CriteriaOperator.Parse(this.filterCriteria, new object[0]), this.actualSorting.Select<SortingInfo, ServerModeOrderDescriptor[]>(selector).ToList<ServerModeOrderDescriptor[]>(), this.groupCount, null, null, new DictionaryEntry[0]);
            this.Wrapper.Reset();
        }

        protected override void InitializeView(object source)
        {
            base.SetView(this.CreateDataProxyViewCache(source));
            this.InitializeSource();
        }

        protected virtual void NotifyFindIncrementalInternal(string text, int startIndex, bool searchNext, bool ignoreStartIndex, int controllerIndex)
        {
            if (!this.Wrapper.IsRowLoaded(controllerIndex))
            {
                GetRowOnFindIncrementalCompleter completer = new GetRowOnFindIncrementalCompleter(this, text, startIndex, searchNext, ignoreStartIndex);
                this.Wrapper.GetRow(controllerIndex, new OperationCompleted(completer.Completed));
            }
            else
            {
                DataProxy proxy = this.View[controllerIndex];
                object valueFromProxy = base.GetValueFromProxy(proxy);
                this.RaiseFindIncrementalCompleted(new ItemsProviderFindIncrementalCompletedEventArgs(text, startIndex, searchNext, ignoreStartIndex, valueFromProxy));
            }
        }

        public override bool ProcessChangeFilter(string filterCriteria)
        {
            this.filterCriteria = filterCriteria;
            this.InitializeSource();
            return true;
        }

        public override void ProcessFindIncremental(ItemsProviderFindIncrementalCompletedEventArgs e)
        {
            base.ProcessFindIncremental(e);
            if (base.IndexOfValue(e.Value) >= 0)
            {
                this.RaiseFindIncrementalCompleted(e);
            }
            else
            {
                FindRowIndexByValueOnFindIncrementalCompleter completer = new FindRowIndexByValueOnFindIncrementalCompleter(this, e.Text, e.StartIndex, e.SearchNext, e.IgnoreStartIndex);
                this.View.FindIndexByValue(CreateCriteriaForValueColumn(base.DataAccessor), e.Value, new OperationCompleted(completer.Completed));
            }
        }

        public bool IsBusy { get; private set; }

        private AsyncDataProxyViewCache View =>
            (AsyncDataProxyViewCache) base.View;

        public AsyncListWrapper Wrapper =>
            this.ListSource as AsyncListWrapper;

        private IBindingList ListSource =>
            base.ListSource as IBindingList;

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly AsyncListServerDefaultDataView.<>c <>9 = new AsyncListServerDefaultDataView.<>c();
            public static Func<GroupingInfo, SortingInfo> <>9__13_0;
            public static Func<SortingInfo, ServerModeOrderDescriptor[]> <>9__18_0;

            internal SortingInfo <.ctor>b__13_0(GroupingInfo x) => 
                new SortingInfo(x.FieldName, ListSortDirection.Ascending);

            internal ServerModeOrderDescriptor[] <InitializeSource>b__18_0(SortingInfo x) => 
                new ServerModeOrderDescriptor[] { new ServerModeOrderDescriptor(new OperandProperty(x.FieldName), x.OrderBy == ListSortDirection.Descending) };
        }
    }
}

