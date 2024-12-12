namespace DevExpress.Xpf.Editors.Helpers
{
    using DevExpress.Data;
    using DevExpress.Data.Filtering;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    public abstract class AsyncServerModeCurrentDataView : CurrentDataView, IAsyncListServerDataView
    {
        private string filterCriteria;
        private string displayFilterCriteria;
        private IList<SortingInfo> actualSorting;
        private int groupCount;
        private readonly AsyncListWrapper wrapper;

        protected AsyncServerModeCurrentDataView(AsyncListServerDefaultDataView view, object handle, string valueMember, string displayMember, IEnumerable<GroupingInfo> groups, IEnumerable<SortingInfo> sorts, string filterCriteria, string displayFilterCriteria) : base(false, view, handle, valueMember, displayMember)
        {
            List<SortingInfo> list1;
            this.filterCriteria = filterCriteria;
            this.displayFilterCriteria = displayFilterCriteria;
            if (groups == null)
            {
                list1 = new List<SortingInfo>();
            }
            else
            {
                Func<GroupingInfo, SortingInfo> selector = <>c.<>9__19_0;
                if (<>c.<>9__19_0 == null)
                {
                    Func<GroupingInfo, SortingInfo> local1 = <>c.<>9__19_0;
                    selector = <>c.<>9__19_0 = x => new SortingInfo(x.FieldName, ListSortDirection.Ascending);
                }
                list1 = groups.Select<GroupingInfo, SortingInfo>(selector).ToList<SortingInfo>();
            }
            List<SortingInfo> list = list1;
            list.AddRange(sorts);
            this.actualSorting = list;
            this.groupCount = groups.Count<GroupingInfo>();
            this.wrapper = this.CreateWrapper(view);
        }

        protected virtual void ApplySortFilterForVisibleListWrapper(AsyncVisibleListWrapper visibleListWrapper)
        {
            if (visibleListWrapper != null)
            {
                CriteriaOperator[] operands = new CriteriaOperator[] { CriteriaOperator.Parse(this.filterCriteria, new object[0]), CriteriaOperator.Parse(this.displayFilterCriteria, new object[0]) };
                CriteriaOperator filterCriteria = CriteriaOperator.And(operands);
                Func<SortingInfo, ServerModeOrderDescriptor[]> selector = <>c.<>9__51_0;
                if (<>c.<>9__51_0 == null)
                {
                    Func<SortingInfo, ServerModeOrderDescriptor[]> local1 = <>c.<>9__51_0;
                    selector = <>c.<>9__51_0 = x => new ServerModeOrderDescriptor[] { new ServerModeOrderDescriptor(new OperandProperty(x.FieldName), x.OrderBy == ListSortDirection.Descending) };
                }
                visibleListWrapper.ApplyViewServerSettings(filterCriteria, this.actualSorting.Select<SortingInfo, ServerModeOrderDescriptor[]>(selector).ToList<ServerModeOrderDescriptor[]>(), this.groupCount, null, null, new DictionaryEntry[0]);
            }
        }

        protected virtual void ApplySortFilterForWrapper(AsyncListWrapper wrapper)
        {
            Func<SortingInfo, ServerModeOrderDescriptor[]> selector = <>c.<>9__50_0;
            if (<>c.<>9__50_0 == null)
            {
                Func<SortingInfo, ServerModeOrderDescriptor[]> local1 = <>c.<>9__50_0;
                selector = <>c.<>9__50_0 = x => new ServerModeOrderDescriptor[] { new ServerModeOrderDescriptor(new OperandProperty(x.FieldName), x.OrderBy == ListSortDirection.Descending) };
            }
            wrapper.ApplySortGroupFilter(CriteriaOperator.Parse(this.filterCriteria, new object[0]), this.actualSorting.Select<SortingInfo, ServerModeOrderDescriptor[]>(selector).ToList<ServerModeOrderDescriptor[]>(), this.groupCount, null, null, new DictionaryEntry[0]);
        }

        public override void CancelAsyncOperations()
        {
            base.CancelAsyncOperations();
            this.Wrapper.CancelAllGetRows();
            this.ProcessBusyChanged(false, false);
        }

        public void CancelItem(int visibleIndex)
        {
            this.View.CancelItem(visibleIndex);
        }

        protected override object CreateVisibleListWrapper() => 
            new AsyncVisibleListWrapper(this, null);

        protected abstract AsyncListWrapper CreateWrapper(AsyncListServerDefaultDataView view);
        protected override object DataControllerAdapterGetRowInternal(int visibleIndex)
        {
            DataProxy proxyByIndex = base.GetProxyByIndex(visibleIndex);
            return ((proxyByIndex != null) ? base.GetItemByProxy(proxyByIndex) : -1);
        }

        void IAsyncListServerDataView.BusyChanged(bool busy)
        {
            this.ProcessBusyChanged(busy, this.IsVisibleListBusy);
        }

        void IAsyncListServerDataView.NotifyApply()
        {
            this.ResetDisplayTextCache();
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
            if (listSourceIndex >= 0)
            {
                this.NotifyLoadedInternal(listSourceIndex);
            }
        }

        protected override void DisposeInternal()
        {
            base.DisposeInternal();
            this.Wrapper.Dispose();
        }

        public void FetchItem(object value)
        {
            base.IndexOfValue(value);
        }

        public void FetchItem(int visibleIndex, OperationCompleted action = null)
        {
            this.View.FetchItem(visibleIndex, action);
        }

        protected override int FindItemIndexByTextInternal(string text, bool isCaseSensitive, bool autoComplete, int startItemIndex, bool searchNext, bool ignoreStartIndex)
        {
            if (string.IsNullOrEmpty(text))
            {
                return -1;
            }
            OperandProperty compareOperand = (OperandProperty) ListServerDataViewBase.CreateCriteriaForDisplayColumn(base.DataAccessor);
            return this.View.FindIndexByText(compareOperand, base.GetCompareCriteriaOperator(autoComplete, compareOperand, new OperandValue(text)), text, isCaseSensitive, startItemIndex, searchNext, ignoreStartIndex);
        }

        protected override int FindListSourceIndexByValue(object value) => 
            this.View.FindIndexByValue(ListServerDataViewBase.CreateCriteriaForValueColumn(base.DataAccessor), value);

        protected internal override bool GetIsAsyncServerMode() => 
            true;

        protected internal override bool GetIsOwnSearchProcessing() => 
            true;

        public object GetTempProxy(int listSourceIndex) => 
            this.View.TempProxy;

        public virtual void InitializeSource()
        {
            this.ApplySortFilterForWrapper(this.Wrapper);
        }

        public virtual void InitializeVisibleList()
        {
            this.ApplySortFilterForVisibleListWrapper((AsyncVisibleListWrapper) base.GetVisibleListInternal());
        }

        public bool IsRowLoaded(int visibleIndex) => 
            this.View.IsRowLoaded(visibleIndex);

        protected virtual void NotifyFindIncrementalInternal(string text, int startIndex, bool searchNext, bool ignoreStartIndex, int controllerIndex)
        {
            this.UpdateDisplayTextCache(text, true, startIndex, searchNext, controllerIndex, ignoreStartIndex);
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

        protected virtual void NotifyLoadedInternal(int controllerIndex)
        {
            this.ProcessChangeItem(controllerIndex);
            this.RaiseRowLoaded(new ItemsProviderRowLoadedEventArgs(base.Handle, controllerIndex, null));
        }

        private void ProcessBusyChanged(bool busy, bool visibleListBusy)
        {
            bool flag = this.IsBusy || this.IsVisibleListBusy;
            bool isBusy = busy | visibleListBusy;
            this.IsBusy = busy;
            this.IsVisibleListBusy = visibleListBusy;
            if (flag != isBusy)
            {
                this.RaiseOnBusyChanged(new ItemsProviderOnBusyChangedEventArgs(isBusy));
            }
        }

        public override void ProcessFindIncrementalCompleted(string text, int startIndex, bool searchNext, bool ignoreStartIndex, object value)
        {
            int controllerIndex = base.ItemsCache.IndexOfValue(value);
            if (controllerIndex > -1)
            {
                ((IAsyncListServerDataView) this).NotifyFindIncrementalCompleted(text, startIndex, searchNext, ignoreStartIndex, controllerIndex);
            }
        }

        public override void ProcessRefreshed()
        {
            base.ProcessRefreshed();
            this.ResetDisplayTextCache();
            base.ItemsCache.Reset();
        }

        public override void ProcessRowLoaded(object value)
        {
            this.FetchItem(value);
        }

        public void ProcessVisibleListBusyChanged(bool busy)
        {
            this.ProcessBusyChanged(this.IsBusy, busy);
        }

        protected virtual void SetupGroupSortFilter(IList<GroupingInfo> groups, IList<SortingInfo> sorts, string filterCriteria, string displayFilterCriteria)
        {
            List<SortingInfo> list1;
            this.filterCriteria = filterCriteria;
            this.displayFilterCriteria = displayFilterCriteria;
            if (groups == null)
            {
                list1 = new List<SortingInfo>();
            }
            else
            {
                Func<GroupingInfo, SortingInfo> selector = <>c.<>9__49_0;
                if (<>c.<>9__49_0 == null)
                {
                    Func<GroupingInfo, SortingInfo> local1 = <>c.<>9__49_0;
                    selector = <>c.<>9__49_0 = x => new SortingInfo(x.FieldName, ListSortDirection.Ascending);
                }
                list1 = groups.Select<GroupingInfo, SortingInfo>(selector).ToList<SortingInfo>();
            }
            List<SortingInfo> list = list1;
            list.AddRange(sorts);
            this.actualSorting = list;
            this.groupCount = groups.Count<GroupingInfo>();
        }

        public bool IsBusy { get; private set; }

        public bool IsVisibleListBusy { get; private set; }

        private AsyncDataProxyViewCache View =>
            (AsyncDataProxyViewCache) base.View;

        public AsyncListWrapper Wrapper =>
            this.wrapper;

        private AsyncListServerDefaultDataView DefaultView =>
            (AsyncListServerDefaultDataView) base.ListSource;

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly AsyncServerModeCurrentDataView.<>c <>9 = new AsyncServerModeCurrentDataView.<>c();
            public static Func<GroupingInfo, SortingInfo> <>9__19_0;
            public static Func<GroupingInfo, SortingInfo> <>9__49_0;
            public static Func<SortingInfo, ServerModeOrderDescriptor[]> <>9__50_0;
            public static Func<SortingInfo, ServerModeOrderDescriptor[]> <>9__51_0;

            internal SortingInfo <.ctor>b__19_0(GroupingInfo x) => 
                new SortingInfo(x.FieldName, ListSortDirection.Ascending);

            internal ServerModeOrderDescriptor[] <ApplySortFilterForVisibleListWrapper>b__51_0(SortingInfo x) => 
                new ServerModeOrderDescriptor[] { new ServerModeOrderDescriptor(new OperandProperty(x.FieldName), x.OrderBy == ListSortDirection.Descending) };

            internal ServerModeOrderDescriptor[] <ApplySortFilterForWrapper>b__50_0(SortingInfo x) => 
                new ServerModeOrderDescriptor[] { new ServerModeOrderDescriptor(new OperandProperty(x.FieldName), x.OrderBy == ListSortDirection.Descending) };

            internal SortingInfo <SetupGroupSortFilter>b__49_0(GroupingInfo x) => 
                new SortingInfo(x.FieldName, ListSortDirection.Ascending);
        }
    }
}

