namespace DevExpress.Xpf.Editors.Helpers
{
    using System;
    using System.Collections.Generic;

    public class SyncServerModeCurrentFilteredSortedDataView : SyncServerModeCurrentDataView
    {
        public SyncServerModeCurrentFilteredSortedDataView(SyncListServerDefaultDataView view, object handle, string valueMember, string displayMember, IEnumerable<GroupingInfo> groups, IEnumerable<SortingInfo> sorts, string filterCriteria, string displayFilterCriteria) : base(view, handle, valueMember, displayMember, groups, sorts, filterCriteria, displayFilterCriteria)
        {
            base.Wrapper.Initialize(this);
        }

        protected override DataControllerItemsCache CreateItemsCache(bool select) => 
            new DataControllerItemsCache(this, select);

        protected override SyncListWrapper CreateWrapper(IListServerDataView view) => 
            new SyncListWrapper(view.Wrapper.Server);

        public override void InitializeSource()
        {
            this.ApplySortFilterForWrapper(base.Wrapper);
            base.InitializeSource();
        }

        protected override void InitializeView(object source)
        {
            base.SetView(this.CreateDataProxyViewCache(this));
            this.InitializeSource();
        }

        public override bool ProcessInconsistencyDetected()
        {
            bool flag = base.ProcessInconsistencyDetected();
            base.ProcessInconsistencyDetectedForVisibleListWrapper((SyncVisibleListWrapper) base.GetVisibleListInternal());
            return flag;
        }

        public override bool ProcessRefresh()
        {
            base.ProcessRefresh();
            base.Wrapper.Refresh();
            base.ProcessRefreshForVisibleListWrapper((SyncVisibleListWrapper) base.GetVisibleListInternal());
            return true;
        }
    }
}

