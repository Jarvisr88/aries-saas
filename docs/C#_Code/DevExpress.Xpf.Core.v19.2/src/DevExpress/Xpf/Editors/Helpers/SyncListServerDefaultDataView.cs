namespace DevExpress.Xpf.Editors.Helpers
{
    using DevExpress.Data;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Runtime.CompilerServices;

    public class SyncListServerDefaultDataView : ListServerDataViewBase, IListServerDataView
    {
        private string filterCriteria;
        private readonly IEnumerable<SortingInfo> actualSorting;
        private readonly int groupCount;

        public SyncListServerDefaultDataView(bool selectNullValue, IListServer server, string valueMember, string displayMember, IEnumerable<GroupingInfo> groups, IEnumerable<SortingInfo> sorts, string filter) : base(selectNullValue, new SyncListWrapper(server), valueMember, displayMember)
        {
            List<SortingInfo> list1;
            this.filterCriteria = filter;
            if (groups == null)
            {
                list1 = new List<SortingInfo>();
            }
            else
            {
                Func<GroupingInfo, SortingInfo> selector = <>c.<>9__7_0;
                if (<>c.<>9__7_0 == null)
                {
                    Func<GroupingInfo, SortingInfo> local1 = <>c.<>9__7_0;
                    selector = <>c.<>9__7_0 = x => new SortingInfo(x.FieldName, ListSortDirection.Ascending);
                }
                list1 = groups.Select<GroupingInfo, SortingInfo>(selector).ToList<SortingInfo>();
            }
            List<SortingInfo> list = list1;
            list.AddRange(sorts);
            this.actualSorting = list;
            this.groupCount = groups.Count<GroupingInfo>();
            this.Wrapper.Initialize(this);
        }

        protected virtual void ApplySortFilterForWrapper(SyncListWrapper wrapper)
        {
            wrapper.ApplySortGroupFilter(null, null, 0, null, null);
        }

        public override CurrentDataView CreateCurrentDataView(object handle, IList<GroupingInfo> groups, IList<SortingInfo> sorts, string filterCriteria, string displayFilterCriteria, bool caseSensitiveFilter) => 
            GetIsCurrentViewFIltered(groups, sorts, filterCriteria) ? ((CurrentDataView) new SyncServerModeCurrentFilteredSortedDataView(this, handle, base.DataAccessor.ValueMember, base.DataAccessor.DisplayMember, groups, sorts, filterCriteria, displayFilterCriteria)) : ((CurrentDataView) new SyncServerModeCurrentPlainDataView(this, handle, base.DataAccessor.ValueMember, base.DataAccessor.DisplayMember, groups, sorts, filterCriteria, displayFilterCriteria));

        protected override DataProxyViewCache CreateDataProxyViewCache(object source) => 
            new SyncDataProxyViewCache(this);

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
            base.RaiseInconsistencyDetected();
        }

        void IListServerDataView.Reset()
        {
            base.ItemsCache.Reset();
            this.View.Reset();
        }

        protected override void DisposeInternal()
        {
            base.DisposeInternal();
            this.Wrapper.Dispose();
        }

        protected override int FindListSourceIndexByValue(object value) => 
            this.View.FindIndexByValue(CreateCriteriaForValueColumn(base.DataAccessor), value);

        protected internal override bool GetIsAsyncServerMode() => 
            false;

        protected internal override bool GetIsOwnSearchProcessing() => 
            true;

        protected virtual void InitializeSource()
        {
            this.ApplySortFilterForWrapper(this.Wrapper);
        }

        protected override void InitializeView(object source)
        {
            base.SetView(this.CreateDataProxyViewCache(source));
            this.InitializeSource();
        }

        public override bool ProcessChangeFilter(string filterCriteria)
        {
            this.filterCriteria = filterCriteria;
            this.InitializeSource();
            return true;
        }

        public override bool ProcessInconsistencyDetected()
        {
            this.InitializeSource();
            return base.ProcessInconsistencyDetected();
        }

        public override bool ProcessRefresh()
        {
            this.Wrapper.Refresh();
            this.View.Reset();
            base.ItemsCache.Reset();
            return true;
        }

        private SyncDataProxyViewCache View =>
            (SyncDataProxyViewCache) base.View;

        public SyncListWrapper Wrapper =>
            base.ListSource as SyncListWrapper;

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly SyncListServerDefaultDataView.<>c <>9 = new SyncListServerDefaultDataView.<>c();
            public static Func<GroupingInfo, SortingInfo> <>9__7_0;

            internal SortingInfo <.ctor>b__7_0(GroupingInfo x) => 
                new SortingInfo(x.FieldName, ListSortDirection.Ascending);
        }
    }
}

