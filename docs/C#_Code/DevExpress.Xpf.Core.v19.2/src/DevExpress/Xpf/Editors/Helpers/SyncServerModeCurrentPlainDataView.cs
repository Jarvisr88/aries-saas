namespace DevExpress.Xpf.Editors.Helpers
{
    using System;
    using System.Collections.Generic;

    public class SyncServerModeCurrentPlainDataView : SyncServerModeCurrentDataView
    {
        public SyncServerModeCurrentPlainDataView(SyncListServerDefaultDataView view, object handle, string valueMember, string displayMember, IEnumerable<GroupingInfo> groups, IEnumerable<SortingInfo> sorts, string filterCriteria, string displayFilterCriteria) : base(view, handle, valueMember, displayMember, groups, sorts, filterCriteria, displayFilterCriteria)
        {
        }

        protected override DataAccessor CreateEditorsDataAccessor() => 
            this.DefaultView.DataAccessor;

        protected override DataControllerItemsCache CreateItemsCache(bool select) => 
            this.DefaultView.ItemsCache;

        protected override SyncListWrapper CreateWrapper(IListServerDataView view) => 
            view.Wrapper;

        protected override void InitializeView(object source)
        {
            base.SetView(this.DefaultView.View);
        }

        public override bool ProcessChangeSortFilter(IList<GroupingInfo> groups, IList<SortingInfo> sorts, string filterCriteria, string displayFilterCriteria) => 
            string.IsNullOrEmpty(filterCriteria) ? base.ProcessChangeSortFilter(groups, sorts, filterCriteria, displayFilterCriteria) : false;

        private SyncListServerDefaultDataView DefaultView =>
            (SyncListServerDefaultDataView) base.ListSource;
    }
}

