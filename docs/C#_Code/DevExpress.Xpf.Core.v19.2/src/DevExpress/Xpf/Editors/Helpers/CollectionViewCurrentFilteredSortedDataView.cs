namespace DevExpress.Xpf.Editors.Helpers
{
    using DevExpress.Data;
    using DevExpress.Data.Filtering;
    using DevExpress.Data.Linq;
    using DevExpress.Mvvm.Native;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Windows.Data;

    public class CollectionViewCurrentFilteredSortedDataView : LocalCurrentFilteredSortedDataView
    {
        private ICollectionViewHelper server;
        private readonly string filterCriteria;
        private readonly string displayFilterCriteria;
        private readonly IEnumerable<SortingInfo> actualSorting;
        private readonly int groupCount;

        public CollectionViewCurrentFilteredSortedDataView(bool selectNullValue, object source, object handle, string valueMember, string displayMember, IEnumerable<GroupingInfo> groups, IEnumerable<SortingInfo> sorts, string filterCriteria, string displayFilterCriteria, bool caseSensitiveFilter) : base(selectNullValue, source, handle, valueMember, displayMember, groups, sorts, filterCriteria, displayFilterCriteria, caseSensitiveFilter)
        {
            List<SortingInfo> list1;
            ICollectionView collection = ((ICollectionViewHelper) ((DefaultDataView) source).ListSource).Collection;
            this.filterCriteria = filterCriteria;
            this.displayFilterCriteria = displayFilterCriteria;
            Func<IEnumerable<GroupingInfo>, bool> evaluator = <>c.<>9__5_0;
            if (<>c.<>9__5_0 == null)
            {
                Func<IEnumerable<GroupingInfo>, bool> local1 = <>c.<>9__5_0;
                evaluator = <>c.<>9__5_0 = x => x.Count<GroupingInfo>() != 0;
            }
            if (groups.If<IEnumerable<GroupingInfo>>(evaluator).ReturnSuccess<IEnumerable<GroupingInfo>>())
            {
                Func<GroupingInfo, SortingInfo> selector = <>c.<>9__5_1;
                if (<>c.<>9__5_1 == null)
                {
                    Func<GroupingInfo, SortingInfo> local4 = <>c.<>9__5_1;
                    selector = <>c.<>9__5_1 = x => new SortingInfo(x.FieldName, ListSortDirection.Ascending);
                }
                list1 = groups.Select<GroupingInfo, SortingInfo>(selector).ToList<SortingInfo>();
            }
            else
            {
                Func<PropertyGroupDescription, bool> predicate = <>c.<>9__5_2;
                if (<>c.<>9__5_2 == null)
                {
                    Func<PropertyGroupDescription, bool> local2 = <>c.<>9__5_2;
                    predicate = <>c.<>9__5_2 = x => !string.IsNullOrEmpty(x.PropertyName);
                }
                Func<PropertyGroupDescription, SortingInfo> selector = <>c.<>9__5_3;
                if (<>c.<>9__5_3 == null)
                {
                    Func<PropertyGroupDescription, SortingInfo> local3 = <>c.<>9__5_3;
                    selector = <>c.<>9__5_3 = x => new SortingInfo(x.PropertyName, ListSortDirection.Ascending);
                }
                list1 = collection.GroupDescriptions.OfType<PropertyGroupDescription>().Where<PropertyGroupDescription>(predicate).Select<PropertyGroupDescription, SortingInfo>(selector).ToList<SortingInfo>();
            }
            List<SortingInfo> list = list1;
            list.AddRange(sorts);
            this.actualSorting = list;
            this.groupCount = list.Count;
        }

        protected override object CreateVisibleListWrapper() => 
            this.server.Collection;

        protected override void InitializeView(object source)
        {
            base.InitializeView(source);
            ICollectionViewHelper helper = new ICollectionViewHelper(CollectionViewSource.GetDefaultView(((ICollectionViewHelper) ((DefaultDataView) source).ListSource).Collection.Cast<object>().ToList<object>()), CollectionView.NewItemPlaceholder);
            helper.Initialize();
            CriteriaOperator[] operands = new CriteriaOperator[] { CriteriaOperator.Parse(this.filterCriteria, new object[0]), CriteriaOperator.Parse(this.displayFilterCriteria, new object[0]) };
            Func<SortingInfo, ServerModeOrderDescriptor[]> selector = <>c.<>9__6_0;
            if (<>c.<>9__6_0 == null)
            {
                Func<SortingInfo, ServerModeOrderDescriptor[]> local1 = <>c.<>9__6_0;
                selector = <>c.<>9__6_0 = x => new ServerModeOrderDescriptor[] { new ServerModeOrderDescriptor(new OperandProperty(x.FieldName), x.OrderBy == ListSortDirection.Descending) };
            }
            helper.Apply(CriteriaOperator.And(operands), this.actualSorting.Select<SortingInfo, ServerModeOrderDescriptor[]>(selector).ToList<ServerModeOrderDescriptor[]>(), this.groupCount, null, null);
            this.server = helper;
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly CollectionViewCurrentFilteredSortedDataView.<>c <>9 = new CollectionViewCurrentFilteredSortedDataView.<>c();
            public static Func<IEnumerable<GroupingInfo>, bool> <>9__5_0;
            public static Func<GroupingInfo, SortingInfo> <>9__5_1;
            public static Func<PropertyGroupDescription, bool> <>9__5_2;
            public static Func<PropertyGroupDescription, SortingInfo> <>9__5_3;
            public static Func<SortingInfo, ServerModeOrderDescriptor[]> <>9__6_0;

            internal bool <.ctor>b__5_0(IEnumerable<GroupingInfo> x) => 
                x.Count<GroupingInfo>() != 0;

            internal SortingInfo <.ctor>b__5_1(GroupingInfo x) => 
                new SortingInfo(x.FieldName, ListSortDirection.Ascending);

            internal bool <.ctor>b__5_2(PropertyGroupDescription x) => 
                !string.IsNullOrEmpty(x.PropertyName);

            internal SortingInfo <.ctor>b__5_3(PropertyGroupDescription x) => 
                new SortingInfo(x.PropertyName, ListSortDirection.Ascending);

            internal ServerModeOrderDescriptor[] <InitializeView>b__6_0(SortingInfo x) => 
                new ServerModeOrderDescriptor[] { new ServerModeOrderDescriptor(new OperandProperty(x.FieldName), x.OrderBy == ListSortDirection.Descending) };
        }
    }
}

