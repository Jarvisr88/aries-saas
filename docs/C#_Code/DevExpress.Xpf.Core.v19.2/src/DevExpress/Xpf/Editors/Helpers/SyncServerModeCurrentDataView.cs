namespace DevExpress.Xpf.Editors.Helpers
{
    using DevExpress.Data;
    using DevExpress.Data.Filtering;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Runtime.CompilerServices;

    public abstract class SyncServerModeCurrentDataView : CurrentDataView, IListServerDataView
    {
        private string filterCriteria;
        private string displayFilterCriteria;
        private IList<SortingInfo> actualSorting;
        private int groupCount;
        private readonly SyncListWrapper wrapper;

        protected SyncServerModeCurrentDataView(SyncListServerDefaultDataView view, object handle, string valueMember, string displayMember, IEnumerable<GroupingInfo> groups, IEnumerable<SortingInfo> sorts, string filterCriteria, string displayFilterCriteria) : base(false, view, handle, valueMember, displayMember)
        {
            this.SetupGroupSortFilter(groups, sorts, filterCriteria, displayFilterCriteria);
            this.wrapper = this.CreateWrapper(view);
        }

        protected virtual void ApplySortFilterForVisibleListWrapper(SyncVisibleListWrapper visibleListWrapper)
        {
            if (visibleListWrapper != null)
            {
                CriteriaOperator[] operands = new CriteriaOperator[] { CriteriaOperator.Parse(this.filterCriteria, new object[0]), CriteriaOperator.Parse(this.displayFilterCriteria, new object[0]) };
                CriteriaOperator filterCriteria = CriteriaOperator.And(operands);
                Func<SortingInfo, ServerModeOrderDescriptor[]> selector = <>c.<>9__33_0;
                if (<>c.<>9__33_0 == null)
                {
                    Func<SortingInfo, ServerModeOrderDescriptor[]> local1 = <>c.<>9__33_0;
                    selector = <>c.<>9__33_0 = x => new ServerModeOrderDescriptor[] { new ServerModeOrderDescriptor(new OperandProperty(x.FieldName), x.OrderBy == ListSortDirection.Descending) };
                }
                visibleListWrapper.ApplyViewServerSettings(filterCriteria, this.actualSorting.Select<SortingInfo, ServerModeOrderDescriptor[]>(selector).ToList<ServerModeOrderDescriptor[]>(), this.groupCount, null, null);
            }
        }

        protected virtual void ApplySortFilterForWrapper(SyncListWrapper wrapper)
        {
            CriteriaOperator[] operands = new CriteriaOperator[] { CriteriaOperator.Parse(this.filterCriteria, new object[0]), CriteriaOperator.Parse(this.displayFilterCriteria, new object[0]) };
            CriteriaOperator filterCriteria = CriteriaOperator.And(operands);
            Func<SortingInfo, ServerModeOrderDescriptor[]> selector = <>c.<>9__32_0;
            if (<>c.<>9__32_0 == null)
            {
                Func<SortingInfo, ServerModeOrderDescriptor[]> local1 = <>c.<>9__32_0;
                selector = <>c.<>9__32_0 = x => new ServerModeOrderDescriptor[] { new ServerModeOrderDescriptor(new OperandProperty(x.FieldName), x.OrderBy == ListSortDirection.Descending) };
            }
            wrapper.ApplySortGroupFilter(filterCriteria, this.actualSorting.Select<SortingInfo, ServerModeOrderDescriptor[]>(selector).ToList<ServerModeOrderDescriptor[]>(), this.groupCount, null, null);
        }

        protected override DataProxyViewCache CreateDataProxyViewCache(object source) => 
            new SyncDataProxyViewCache(this);

        protected override object CreateVisibleListWrapper() => 
            new SyncVisibleListWrapper(this, null);

        protected abstract SyncListWrapper CreateWrapper(IListServerDataView view);
        protected override object DataControllerAdapterGetRowInternal(int visibleIndex)
        {
            DataProxy proxyByIndex = base.GetProxyByIndex(visibleIndex);
            return ((proxyByIndex != null) ? base.GetItemByProxy(proxyByIndex) : -1);
        }

        void IListServerDataView.NotifyExceptionThrown(ServerModeExceptionThrownEventArgs e)
        {
        }

        void IListServerDataView.NotifyInconsistencyDetected(ServerModeInconsistencyDetectedEventArgs e)
        {
            this.ProcessInconsistencyDetected();
            base.RaiseInconsistencyDetected();
        }

        void IListServerDataView.Reset()
        {
            this.ResetDisplayTextCache();
            base.ItemsCache.Reset();
            this.View.Reset();
        }

        protected override void DisposeInternal()
        {
            base.DisposeInternal();
        }

        protected override int FindItemIndexByTextInternal(string text, bool isCaseSensitive, bool autoComplete, int startItemIndex, bool searchNext, bool ignoreStartIndex)
        {
            if (string.IsNullOrEmpty(text))
            {
                return -1;
            }
            OperandProperty compareOperand = (OperandProperty) ListServerDataViewBase.CreateCriteriaForDisplayColumn(base.DataAccessor);
            return this.View.FindIndexByText(compareOperand, this.GetCompareCriteriaOperator(autoComplete && !string.IsNullOrEmpty(text), compareOperand, new OperandValue(text)), text, isCaseSensitive, startItemIndex, searchNext, ignoreStartIndex);
        }

        protected override int FindListSourceIndexByValue(object value) => 
            this.View.FindIndexByValue(ListServerDataViewBase.CreateCriteriaForValueColumn(base.DataAccessor), value);

        protected internal override bool GetIsAsyncServerMode() => 
            true;

        protected internal override bool GetIsOwnSearchProcessing() => 
            true;

        public virtual void InitializeSource()
        {
            this.InitializeVisibleList();
        }

        public virtual void InitializeVisibleList()
        {
            this.ApplySortFilterForVisibleListWrapper((SyncVisibleListWrapper) base.GetVisibleListInternal());
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

        public override bool ProcessChangeSortFilter(IList<GroupingInfo> groups, IList<SortingInfo> sorts, string filterCriteria, string displayFilterCriteria)
        {
            base.ProcessChangeSortFilter(groups, sorts, filterCriteria, displayFilterCriteria);
            this.SetupGroupSortFilter(groups, sorts, filterCriteria, displayFilterCriteria);
            this.InitializeSource();
            return true;
        }

        public override bool ProcessInconsistencyDetected()
        {
            bool flag = base.ProcessInconsistencyDetected();
            base.ItemsCache.Reset();
            this.Wrapper.Reset();
            this.View.Reset();
            return flag;
        }

        protected void ProcessInconsistencyDetectedForVisibleListWrapper(SyncVisibleListWrapper visibleListWrapper)
        {
            if (visibleListWrapper != null)
            {
                visibleListWrapper.ProcessInconsistencyDetected();
            }
        }

        public override bool ProcessRefresh()
        {
            this.Wrapper.Refresh();
            this.View.Reset();
            base.ItemsCache.Reset();
            this.ProcessRefreshForVisibleListWrapper((SyncVisibleListWrapper) base.GetVisibleListInternal());
            return true;
        }

        public override void ProcessRefreshed()
        {
            base.ProcessRefreshed();
            base.ItemsCache.Reset();
        }

        protected void ProcessRefreshForVisibleListWrapper(SyncVisibleListWrapper visibleListWrapper)
        {
            if (visibleListWrapper != null)
            {
                visibleListWrapper.Refresh();
            }
        }

        public void ProcessVisibleListBusyChanged(bool busy)
        {
            this.ProcessBusyChanged(this.IsBusy, busy);
        }

        protected virtual void SetupGroupSortFilter(IEnumerable<GroupingInfo> groups, IEnumerable<SortingInfo> sorts, string filterCriteria, string displayFilterCriteria)
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
                Func<GroupingInfo, SortingInfo> selector = <>c.<>9__31_0;
                if (<>c.<>9__31_0 == null)
                {
                    Func<GroupingInfo, SortingInfo> local1 = <>c.<>9__31_0;
                    selector = <>c.<>9__31_0 = x => new SortingInfo(x.FieldName, ListSortDirection.Ascending);
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

        private SyncDataProxyViewCache View =>
            (SyncDataProxyViewCache) base.View;

        public SyncListWrapper Wrapper =>
            this.wrapper;

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly SyncServerModeCurrentDataView.<>c <>9 = new SyncServerModeCurrentDataView.<>c();
            public static Func<GroupingInfo, SortingInfo> <>9__31_0;
            public static Func<SortingInfo, ServerModeOrderDescriptor[]> <>9__32_0;
            public static Func<SortingInfo, ServerModeOrderDescriptor[]> <>9__33_0;

            internal ServerModeOrderDescriptor[] <ApplySortFilterForVisibleListWrapper>b__33_0(SortingInfo x) => 
                new ServerModeOrderDescriptor[] { new ServerModeOrderDescriptor(new OperandProperty(x.FieldName), x.OrderBy == ListSortDirection.Descending) };

            internal ServerModeOrderDescriptor[] <ApplySortFilterForWrapper>b__32_0(SortingInfo x) => 
                new ServerModeOrderDescriptor[] { new ServerModeOrderDescriptor(new OperandProperty(x.FieldName), x.OrderBy == ListSortDirection.Descending) };

            internal SortingInfo <SetupGroupSortFilter>b__31_0(GroupingInfo x) => 
                new SortingInfo(x.FieldName, ListSortDirection.Ascending);
        }
    }
}

