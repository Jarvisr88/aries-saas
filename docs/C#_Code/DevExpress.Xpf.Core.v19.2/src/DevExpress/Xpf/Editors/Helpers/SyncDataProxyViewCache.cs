namespace DevExpress.Xpf.Editors.Helpers
{
    using DevExpress.Data.Filtering;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Runtime.CompilerServices;

    public class SyncDataProxyViewCache : DataProxyViewCache
    {
        private readonly IListServerDataView dataView;
        private readonly SortedDictionary<int, DataProxy> view;

        public SyncDataProxyViewCache(IListServerDataView dataView) : base(dataView.DataAccessor)
        {
            this.dataView = dataView;
            this.view = new SortedDictionary<int, DataProxy>();
        }

        public override void Add(int index, DataProxy proxy)
        {
            this.SetProxy(base.DataAccessor.CreateProxy(this.Wrapper.GetRow(index), index));
        }

        public override int FindIndexByText(CriteriaOperator criteriaOperand, CriteriaOperator compareOperator, string text, bool isCaseSensitive, int startItemIndex, bool searchNext, bool ignoreStartIndex)
        {
            Func<int, int> keySelector = <>c.<>9__12_0;
            if (<>c.<>9__12_0 == null)
            {
                Func<int, int> local1 = <>c.<>9__12_0;
                keySelector = <>c.<>9__12_0 = x => x;
            }
            int num = this.FindIndexByTextLocal(compareOperator, isCaseSensitive, from x in this.view.Keys.OrderBy<int, int>(keySelector) select this.view[x], startItemIndex, searchNext, ignoreStartIndex);
            return ((num <= -1) ? this.Wrapper.FindRowByText(criteriaOperand, text, startItemIndex, searchNext, ignoreStartIndex) : num);
        }

        public override int FindIndexByValue(CriteriaOperator compareOperator, object value) => 
            this.Wrapper.FindRowByValue(compareOperator, value);

        public override IEnumerator<DataProxy> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        private bool IndexInRange(int index) => 
            (index >= 0) && (index < this.Count);

        public override void Remove(int index)
        {
            if (this.view.ContainsKey(index))
            {
                this.view.Remove(index);
            }
        }

        public override void Replace(int index, DataProxy item)
        {
            this.SetProxy(base.DataAccessor.CreateProxy(this.Wrapper.GetRow(index), index));
        }

        public override void Reset()
        {
            this.view.Clear();
        }

        private void SetProxy(DataProxy proxy)
        {
            int index = proxy.f_visibleIndex;
            this.view[index] = proxy;
            proxy.f_RowKey = base.DataAccessor.HasValueMember ? this.dataView.GetValueFromProxy(proxy) : this.Wrapper.GetLoadedRowKey(index);
        }

        private SyncListWrapper Wrapper =>
            this.dataView.Wrapper;

        public override DataProxy this[int index]
        {
            get
            {
                if (!this.IndexInRange(index))
                {
                    return null;
                }
                if (!this.view.ContainsKey(index))
                {
                    this.SetProxy(base.DataAccessor.CreateProxy(this.Wrapper.GetRow(index), index));
                }
                return this.view[index];
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public override int Count =>
            this.Wrapper.Count;

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly SyncDataProxyViewCache.<>c <>9 = new SyncDataProxyViewCache.<>c();
            public static Func<int, int> <>9__12_0;

            internal int <FindIndexByText>b__12_0(int x) => 
                x;
        }
    }
}

