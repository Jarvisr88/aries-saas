namespace DevExpress.Xpf.Editors.Helpers
{
    using DevExpress.Data.Filtering;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Reflection;

    public class LazyLocalDataProxyViewCache : DataProxyViewCache
    {
        private LazyDataProxyListWrapper view;

        public LazyLocalDataProxyViewCache(DataAccessor accessor, IList listSource) : base(accessor)
        {
            this.view = new LazyDataProxyListWrapper(accessor, listSource);
        }

        public override void Add(int index, DataProxy item)
        {
            this.view.Insert(index, item);
        }

        protected override int FindIndexByItem(DataProxy item)
        {
            int index = base.FindIndexByItem(item);
            if (((index < 0) || (index >= this.Count)) || !ReferenceEquals(this.view[index], item))
            {
                index = this.view.IndexOf(item);
                item.f_visibleIndex = index;
            }
            return index;
        }

        public override int FindIndexByText(CriteriaOperator compareOperand, CriteriaOperator compareOperator, string text, bool isCaseSensitive, int startItemIndex, bool searchNext, bool ignoreStartIndex) => 
            this.FindIndexByTextLocal(compareOperator, isCaseSensitive, this.view, startItemIndex, searchNext, ignoreStartIndex);

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

