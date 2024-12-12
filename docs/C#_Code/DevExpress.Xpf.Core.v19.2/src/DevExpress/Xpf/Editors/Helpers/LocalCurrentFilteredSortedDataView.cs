namespace DevExpress.Xpf.Editors.Helpers
{
    using DevExpress.Data.Filtering;
    using DevExpress.Data.Filtering.Helpers;
    using DevExpress.Xpf.Core.Internal;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Reflection;
    using System.Runtime.CompilerServices;

    public class LocalCurrentFilteredSortedDataView : LocalCurrentDataView
    {
        private readonly string filterCriteria;
        private readonly string displayFilterCriteria;
        private readonly IList<SortingInfo> actualSorts;
        private CriteriaCompiledContextDescriptorFlatTyped cachedDescriptor;
        private Func<object, bool> cachedFilterPredicate;
        private Func<object, object>[] cachedSortHandlers;
        private readonly bool caseSensitiveFilter;

        public LocalCurrentFilteredSortedDataView(bool selectNullValue, object view, object handle, string valueMember, string displayMember, IEnumerable<GroupingInfo> groups, IEnumerable<SortingInfo> sorts, string filterCriteria, string displayFilterCriteria, bool caseSensitiveFilter) : base(selectNullValue, view, handle, valueMember, displayMember, false)
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
                Func<GroupingInfo, SortingInfo> selector = <>c.<>9__11_0;
                if (<>c.<>9__11_0 == null)
                {
                    Func<GroupingInfo, SortingInfo> local1 = <>c.<>9__11_0;
                    selector = <>c.<>9__11_0 = x => new SortingInfo(x.FieldName, ListSortDirection.Ascending);
                }
                list1 = groups.Select<GroupingInfo, SortingInfo>(selector).ToList<SortingInfo>();
            }
            List<SortingInfo> list = list1;
            list.AddRange(sorts);
            this.actualSorts = list;
            this.caseSensitiveFilter = caseSensitiveFilter;
        }

        protected override ListChangedEventArgs ConvertListChangedEventArgs(ListChangedEventArgs e) => 
            new ListChangedEventArgs(ListChangedType.Reset, -1);

        protected override DataProxyViewCache CreateDataProxyViewCache(object source) => 
            new LocalDataProxyViewCache(base.DataAccessor, (IList) source);

        protected override void FetchDescriptorsInternal(DataAccessor accessor)
        {
            base.FetchDescriptorsInternal(accessor);
            foreach (SortingInfo info in this.actualSorts)
            {
                accessor.Fetch(info.FieldName);
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
            CriteriaOperator operator2 = CriteriaOperator.Parse(this.displayFilterCriteria, new object[0]);
            if (operator2 != null)
            {
                Visitor visitor = new Visitor();
                operator2.Accept(visitor);
                foreach (string str2 in visitor.RequestedProperties)
                {
                    accessor.Fetch(str2);
                }
            }
        }

        protected override void InitializeView(object source)
        {
            DefaultDataViewAsListWrapper wrapper = new DefaultDataViewAsListWrapper((DefaultDataView) source);
            base.SetView(this.CreateDataProxyViewCache(wrapper));
            this.cachedDescriptor = new CriteriaCompiledContextDescriptorFlatTyped(base.DataAccessor.ElementType);
            CriteriaOperator[] operands = new CriteriaOperator[] { CriteriaOperator.Parse(this.filterCriteria, new object[0]), CriteriaOperator.Parse(this.displayFilterCriteria, new object[0]) };
            CriteriaOperator expression = CriteriaOperator.And(operands);
            CriteriaCompilerAuxSettings settings = new CriteriaCompilerAuxSettings(this.caseSensitiveFilter);
            this.cachedFilterPredicate = CriteriaCompiler.ToUntypedPredicate(expression, this.cachedDescriptor, settings);
            this.cachedSortHandlers = ((this.actualSorts.Count <= 0) || !this.View.Any<DataProxy>()) ? null : (from x in this.actualSorts select ReflectionHelper.CreateInstanceMethodHandler<DataProxy, Func<object, object>>(this.View.First<DataProxy>(), "get_" + x.FieldName, BindingFlags.Public | BindingFlags.Instance, null, null, true)).ToArray<Func<object, object>>();
            this.View.ApplySortGroupFilter(new Func<IList<DataProxy>, List<DataProxy>>(this.PerformSortGroupFilter));
            base.ItemsCache.Reset();
            this.ResetDisplayTextCache();
        }

        private List<DataProxy> PerformSortGroupFilter(IList<DataProxy> view)
        {
            IEnumerable<DataProxy> collection = view;
            if (this.cachedFilterPredicate != null)
            {
                collection = from x in collection
                    where this.cachedFilterPredicate(x)
                    select x;
            }
            if (this.actualSorts.Count > 0)
            {
                if (view.FirstOrDefault<DataProxy>() == null)
                {
                    return new List<DataProxy>();
                }
                IOrderedEnumerable<DataProxy> source = (this.actualSorts.First<SortingInfo>().OrderBy == ListSortDirection.Ascending) ? (from x in collection
                    orderby this.cachedSortHandlers[0](x)
                    select x) : (from x in collection
                    orderby this.cachedSortHandlers[0](x) descending
                    select x);
                int num = 1;
                while (true)
                {
                    if (num >= this.actualSorts.Count)
                    {
                        collection = source;
                        break;
                    }
                    int index = num;
                    SortingInfo info2 = this.actualSorts[num];
                    source = (info2.OrderBy != ListSortDirection.Ascending) ? source.ThenByDescending<DataProxy, object>(x => this.cachedSortHandlers[index](x)) : source.ThenBy<DataProxy, object>(x => this.cachedSortHandlers[index](x));
                    num++;
                }
            }
            return new List<DataProxy>(collection);
        }

        public override bool ProcessAddItem(int index)
        {
            this.InitializeView(this.ListSource);
            return true;
        }

        public override bool ProcessChangeItem(int index)
        {
            this.InitializeView(this.ListSource);
            return true;
        }

        public override bool ProcessDeleteItem(int index)
        {
            this.InitializeView(this.ListSource);
            return true;
        }

        public override bool ProcessMoveItem(int oldIndex, int newIndex)
        {
            this.InitializeView(this.ListSource);
            return true;
        }

        public override bool ProcessReset()
        {
            this.InitializeView(this.ListSource);
            return true;
        }

        private DefaultDataView ListSource =>
            base.ListSource as DefaultDataView;

        private LocalDataProxyViewCache View =>
            base.View as LocalDataProxyViewCache;

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly LocalCurrentFilteredSortedDataView.<>c <>9 = new LocalCurrentFilteredSortedDataView.<>c();
            public static Func<GroupingInfo, SortingInfo> <>9__11_0;

            internal SortingInfo <.ctor>b__11_0(GroupingInfo x) => 
                new SortingInfo(x.FieldName, ListSortDirection.Ascending);
        }

        private class SearchInFilteredSortedListComparer : IComparer<object>
        {
            private readonly DefaultDataView defaultView;
            private readonly CurrentDataView currentView;

            public SearchInFilteredSortedListComparer(DefaultDataView defaultView, CurrentDataView currentView)
            {
                this.defaultView = defaultView;
                this.currentView = currentView;
            }

            public int Compare(object x, object y)
            {
                object valueFromProxy = this.currentView.GetValueFromProxy((DataProxy) x);
                return this.defaultView.IndexOfValue(valueFromProxy).CompareTo(((DataProxy) y).f_visibleIndex);
            }
        }

        private class SearchInSortedListComparer : IComparer<object>
        {
            private readonly Func<object, object> sortHandler;

            public SearchInSortedListComparer(Func<object, object> sortHandler)
            {
                this.sortHandler = sortHandler;
            }

            public int Compare(object x, object y)
            {
                object obj2 = this.sortHandler(x);
                object obj3 = this.sortHandler(y);
                IComparable comparable = obj2 as IComparable;
                if (comparable != null)
                {
                    return comparable.CompareTo(obj3);
                }
                IComparable comparable2 = obj3 as IComparable;
                return ((comparable2 == null) ? 0 : (-1 * comparable2.CompareTo(obj2)));
            }
        }
    }
}

