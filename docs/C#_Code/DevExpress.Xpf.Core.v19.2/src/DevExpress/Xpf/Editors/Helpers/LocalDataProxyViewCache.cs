namespace DevExpress.Xpf.Editors.Helpers
{
    using DevExpress.Data.Filtering;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    public class LocalDataProxyViewCache : DataProxyViewCache
    {
        private List<DataProxy> view;

        public LocalDataProxyViewCache(DataAccessor accessor, IList listSource) : base(accessor)
        {
            this.view = (from x in listSource.Cast<object>() select base.DataAccessor.CreateProxy(x, -1)).ToList<DataProxy>();
        }

        public override void Add(int index, DataProxy item)
        {
            this.view.Insert(index, item);
        }

        public void ApplySortGroupFilter(Func<IList<DataProxy>, List<DataProxy>> performSortGroupFilter)
        {
            this.view = performSortGroupFilter(this.view);
        }

        public int BinarySearch(DataProxy proxy, int startIndex, int count, IComparer<object> comparer) => 
            this.view.BinarySearch(startIndex, count, proxy, (IComparer<DataProxy>) comparer);

        protected override int FindIndexByItem(DataProxy item)
        {
            int index = base.FindIndexByItem(item);
            if (((index < 0) || (index >= this.Count)) || (this.view[index] != item))
            {
                index = this.view.IndexOf(item);
                item.f_visibleIndex = index;
            }
            return index;
        }

        public override int FindIndexByText(CriteriaOperator compareOperand, CriteriaOperator compareOperator, string text, bool isCaseSensitive, int startItemIndex, bool searchNext, bool ignoreStartIndex) => 
            this.FindIndexByTextLocal(compareOperator, isCaseSensitive, this.view, startItemIndex, searchNext, ignoreStartIndex);

        public override int FindIndexByTextLocal(CriteriaOperator compareOperator, bool isCaseSensitive, IEnumerable<DataProxy> view, int startItemIndex, bool searchNext, bool ignoreStartIndex)
        {
            int num = 0;
            foreach (DataProxy proxy in view)
            {
                proxy.f_visibleIndex = num++;
            }
            return base.FindIndexByTextLocal(compareOperator, isCaseSensitive, view, startItemIndex, searchNext, ignoreStartIndex);
        }

        public override int FindIndexByValue(CriteriaOperator compareOperator, object value)
        {
            throw new NotImplementedException();
        }

        public override IEnumerator<DataProxy> GetEnumerator() => 
            this.view.GetEnumerator();

        public override void Remove(int index)
        {
            this.view.RemoveAt(index);
        }

        public override void Replace(int index, DataProxy item)
        {
            this.view[index] = item;
        }

        public override void Reset()
        {
        }

        public override DataProxy this[int index]
        {
            get => 
                this.view[index];
            set => 
                this.view[index] = value;
        }

        public override int Count =>
            this.view.Count;
    }
}

