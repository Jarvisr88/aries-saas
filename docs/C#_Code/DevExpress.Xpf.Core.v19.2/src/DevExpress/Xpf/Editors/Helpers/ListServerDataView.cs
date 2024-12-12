namespace DevExpress.Xpf.Editors.Helpers
{
    using DevExpress.Data;
    using DevExpress.Data.Filtering;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Runtime.CompilerServices;

    public class ListServerDataView : ListServerDataViewBase
    {
        private readonly IListServer server;
        private string filterCriteria;
        private readonly IEnumerable<SortingInfo> actualSorting;
        private readonly int groupCount;

        public ListServerDataView(bool selectNullValue, IListServer server, string valueMember, string displayMember, IEnumerable<GroupingInfo> groups, IEnumerable<SortingInfo> sorts, string filter) : base(selectNullValue, server, valueMember, displayMember)
        {
            List<SortingInfo> list1;
            this.server = server;
            this.filterCriteria = filter;
            if (groups == null)
            {
                list1 = new List<SortingInfo>();
            }
            else
            {
                Func<GroupingInfo, SortingInfo> selector = <>c.<>9__4_0;
                if (<>c.<>9__4_0 == null)
                {
                    Func<GroupingInfo, SortingInfo> local1 = <>c.<>9__4_0;
                    selector = <>c.<>9__4_0 = x => new SortingInfo(x.FieldName, ListSortDirection.Ascending);
                }
                list1 = groups.Select<GroupingInfo, SortingInfo>(selector).ToList<SortingInfo>();
            }
            List<SortingInfo> list = list1;
            list.AddRange(sorts);
            this.actualSorting = list;
            this.groupCount = groups.Count<GroupingInfo>();
            server.InconsistencyDetected += new EventHandler<ServerModeInconsistencyDetectedEventArgs>(this.list_InconsistencyDetected);
            server.ExceptionThrown += new EventHandler<ServerModeExceptionThrownEventArgs>(this.list_ExceptionThrown);
        }

        public override CurrentDataView CreateCurrentDataView(object handle, IList<GroupingInfo> groups, IList<SortingInfo> sorts, string filterCriteria, string displayFilterCriteria, bool caseSensitiveFilter) => 
            (GetIsCurrentViewFIltered(groups, sorts, filterCriteria) || !string.IsNullOrEmpty(displayFilterCriteria)) ? ((CurrentDataView) new LocalCurrentFilteredSortedDataView(base.selectNullValue, this, handle, base.DataAccessor.ValueMember, base.DataAccessor.DisplayMember, groups, sorts, filterCriteria, displayFilterCriteria, caseSensitiveFilter)) : ((CurrentDataView) new LocalCurrentPlainDataView(base.selectNullValue, this, handle, base.DataAccessor.ValueMember, base.DataAccessor.DisplayMember, false));

        protected override void DisposeInternal()
        {
            base.DisposeInternal();
            this.server.InconsistencyDetected -= new EventHandler<ServerModeInconsistencyDetectedEventArgs>(this.list_InconsistencyDetected);
            this.server.ExceptionThrown -= new EventHandler<ServerModeExceptionThrownEventArgs>(this.list_ExceptionThrown);
        }

        protected override int FindListSourceIndexByValue(object value) => 
            this.server.LocateByValue(base.GetCriteriaForValueColumn(), value, -1, false);

        protected virtual void InitializeSource()
        {
            Func<SortingInfo, ServerModeOrderDescriptor[]> selector = <>c.<>9__7_0;
            if (<>c.<>9__7_0 == null)
            {
                Func<SortingInfo, ServerModeOrderDescriptor[]> local1 = <>c.<>9__7_0;
                selector = <>c.<>9__7_0 = x => new ServerModeOrderDescriptor[] { new ServerModeOrderDescriptor(new OperandProperty(x.FieldName), x.OrderBy == ListSortDirection.Descending) };
            }
            this.server.Apply(CriteriaOperator.Parse(this.filterCriteria, new object[0]), this.actualSorting.Select<SortingInfo, ServerModeOrderDescriptor[]>(selector).ToList<ServerModeOrderDescriptor[]>(), this.groupCount, null, null);
        }

        protected override void InitializeView(object source)
        {
            this.InitializeSource();
        }

        private void list_ExceptionThrown(object sender, ServerModeExceptionThrownEventArgs e)
        {
        }

        private void list_InconsistencyDetected(object sender, ServerModeInconsistencyDetectedEventArgs e)
        {
        }

        public override bool ProcessChangeFilter(string filterCriteria)
        {
            this.filterCriteria = filterCriteria;
            this.InitializeSource();
            return true;
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly ListServerDataView.<>c <>9 = new ListServerDataView.<>c();
            public static Func<GroupingInfo, SortingInfo> <>9__4_0;
            public static Func<SortingInfo, ServerModeOrderDescriptor[]> <>9__7_0;

            internal SortingInfo <.ctor>b__4_0(GroupingInfo x) => 
                new SortingInfo(x.FieldName, ListSortDirection.Ascending);

            internal ServerModeOrderDescriptor[] <InitializeSource>b__7_0(SortingInfo x) => 
                new ServerModeOrderDescriptor[] { new ServerModeOrderDescriptor(new OperandProperty(x.FieldName), x.OrderBy == ListSortDirection.Descending) };
        }
    }
}

