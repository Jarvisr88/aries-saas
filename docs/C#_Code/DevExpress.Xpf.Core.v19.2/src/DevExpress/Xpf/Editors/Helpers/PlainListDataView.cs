namespace DevExpress.Xpf.Editors.Helpers
{
    using DevExpress.Data.Filtering;
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class PlainListDataView : DefaultDataView
    {
        private readonly string filterCriteria;
        private readonly IEnumerable<GroupingInfo> groups;
        private readonly IEnumerable<SortingInfo> sorts;
        private readonly bool lazyInitialization;

        public PlainListDataView(bool selectNullValue, IEnumerable serverSource, string valueMember, string displayMember, IEnumerable<GroupingInfo> groups, IEnumerable<SortingInfo> sorts, string filterCriteria, bool lazyInitialization) : base(selectNullValue, serverSource, valueMember, displayMember)
        {
            this.filterCriteria = filterCriteria;
            this.groups = groups;
            this.sorts = sorts;
            this.lazyInitialization = lazyInitialization;
        }

        public override CurrentDataView CreateCurrentDataView(object handle, IList<GroupingInfo> groups, IList<SortingInfo> sorts, string filterCriteria, string displayFilterCriteria, bool caseSensitiveFilter) => 
            (GetIsCurrentViewFIltered(groups, sorts, filterCriteria) || !string.IsNullOrEmpty(displayFilterCriteria)) ? ((CurrentDataView) new LocalCurrentFilteredSortedDataView(base.selectNullValue, this, handle, base.DataAccessor.ValueMember, base.DataAccessor.DisplayMember, groups, sorts, filterCriteria, displayFilterCriteria, caseSensitiveFilter)) : ((CurrentDataView) new LocalCurrentPlainDataView(base.selectNullValue, this, handle, base.DataAccessor.ValueMember, base.DataAccessor.DisplayMember, this.lazyInitialization));

        protected override DataProxyViewCache CreateDataProxyViewCache(object source) => 
            this.lazyInitialization ? new LazyLocalDataProxyViewCache(base.DataAccessor, (IList) source) : base.CreateDataProxyViewCache(source);

        protected override void FetchDescriptorsInternal(DataAccessor accessor)
        {
            base.FetchDescriptorsInternal(accessor);
            foreach (GroupingInfo info in this.groups)
            {
                accessor.Fetch(info.FieldName);
            }
            foreach (SortingInfo info2 in this.sorts)
            {
                accessor.Fetch(info2.FieldName);
            }
            CriteriaOperator @operator = CriteriaOperator.Parse(this.filterCriteria, new object[0]);
            if (@operator != null)
            {
                Visitor visitor = new Visitor();
                @operator.Accept(visitor);
                foreach (string str in visitor.RequestedProperties)
                {
                    accessor.Fetch(str);
                }
            }
        }

        public override IEnumerator<DataProxy> GetEnumerator() => 
            this.View.GetEnumerator();

        private IEnumerable<DataProxy> View =>
            base.View;
    }
}

