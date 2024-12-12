namespace DevExpress.Xpf.Editors.Helpers
{
    using System;
    using System.Collections.Generic;

    public class AsyncServerModeCurrentPlainDataView : AsyncServerModeCurrentDataView
    {
        public AsyncServerModeCurrentPlainDataView(AsyncListServerDefaultDataView view, object handle, string valueMember, string displayMember, IEnumerable<GroupingInfo> groups, IEnumerable<SortingInfo> sorts, string filterCriteria, string displayFilterCriteria) : base(view, handle, valueMember, displayMember, groups, sorts, filterCriteria, displayFilterCriteria)
        {
        }

        protected override DataAccessor CreateEditorsDataAccessor() => 
            this.DefaultView.DataAccessor;

        protected override DataControllerItemsCache CreateItemsCache(bool select) => 
            this.DefaultView.ItemsCache;

        protected override AsyncListWrapper CreateWrapper(AsyncListServerDefaultDataView view) => 
            view.Wrapper;

        protected override void InitializeView(object source)
        {
            base.SetView(this.DefaultView.View);
        }

        public override bool ProcessChangeSortFilter(IList<GroupingInfo> groups, IList<SortingInfo> sorts, string filterCriteria, string displayFilterCriteria)
        {
            base.ProcessChangeSortFilter(groups, sorts, filterCriteria, displayFilterCriteria);
            this.SetupGroupSortFilter(groups, sorts, filterCriteria, displayFilterCriteria);
            this.ApplySortFilterForVisibleListWrapper((AsyncVisibleListWrapper) base.GetVisibleListInternal());
            return true;
        }

        private AsyncVisibleListWrapper VisibleList =>
            (AsyncVisibleListWrapper) base.VisibleList;

        private AsyncListServerDefaultDataView DefaultView =>
            (AsyncListServerDefaultDataView) base.ListSource;
    }
}

