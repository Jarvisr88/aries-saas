namespace DevExpress.Xpf.Editors.Helpers
{
    using System;
    using System.Collections.Generic;

    public class AsyncServerModeCurrentFilteredSortedDataView : AsyncServerModeCurrentDataView
    {
        public AsyncServerModeCurrentFilteredSortedDataView(AsyncListServerDefaultDataView view, object handle, string valueMember, string displayMember, IEnumerable<GroupingInfo> groups, IEnumerable<SortingInfo> sorts, string filterCriteria, string displayFilterCriteria) : base(view, handle, valueMember, displayMember, groups, sorts, filterCriteria, displayFilterCriteria)
        {
        }

        protected override DataProxyViewCache CreateDataProxyViewCache(object source) => 
            new AsyncDataProxyViewCache(this);

        protected override DataAccessor CreateEditorsDataAccessorInstance() => 
            new DataAccessor();

        protected override DataControllerItemsCache CreateItemsCache(bool select) => 
            new DataControllerItemsCache(this, false);

        protected override AsyncListWrapper CreateWrapper(AsyncListServerDefaultDataView view)
        {
            AsyncListWrapper receiver = new AsyncListWrapper(view.Wrapper.Server);
            receiver.Initialize(this);
            receiver.SetReceiver(receiver);
            return receiver;
        }

        protected override void InitializeView(object source)
        {
            base.SetView(this.CreateDataProxyViewCache(source));
            this.InitializeSource();
        }

        public override bool ProcessChangeSortFilter(IList<GroupingInfo> groups, IList<SortingInfo> sorts, string filterCriteria, string displayFilterCriteria)
        {
            this.SetupGroupSortFilter(groups, sorts, filterCriteria, displayFilterCriteria);
            this.ApplySortFilterForWrapper(base.Wrapper);
            this.ApplySortFilterForVisibleListWrapper((AsyncVisibleListWrapper) base.GetVisibleListInternal());
            return true;
        }

        private AsyncListServerDefaultDataView DefaultView =>
            (AsyncListServerDefaultDataView) base.ListSource;
    }
}

