namespace DevExpress.Xpf.Editors.Helpers
{
    using DevExpress.Data;
    using DevExpress.Data.Async;
    using DevExpress.Data.Filtering;
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using System.Runtime.InteropServices;

    public class AsyncDataProxyViewCache : DataProxyViewCache
    {
        private readonly IAsyncListServerDataView dataView;
        private readonly SortedDictionary<int, DataProxy> view;
        private readonly DataProxy tempProxy;

        public AsyncDataProxyViewCache(IAsyncListServerDataView dataView) : base(dataView.DataAccessor)
        {
            this.dataView = dataView;
            this.view = new SortedDictionary<int, DataProxy>();
            this.tempProxy = dataView.DataAccessor.CreateProxy(null, -1);
        }

        public override void Add(int index, DataProxy proxy)
        {
            if (this.Wrapper.IsRowLoaded(index))
            {
                this.SetProxy(base.DataAccessor.CreateProxy(this.Wrapper.GetRow(index, null), index));
            }
            else
            {
                this.SetProxy(base.DataAccessor.CreateProxy(this.Wrapper.GetRow(index, new OperationCompleted(this.LoadRowCompleted)), index));
            }
        }

        public void CancelItem(int listSourceIndex)
        {
            this.Wrapper.CancelRow(listSourceIndex);
        }

        public void FetchCount()
        {
            this.Wrapper.Invalidate();
        }

        public void FetchItem(int controllerIndex, OperationCompleted action = null)
        {
            this.Wrapper.GetRow(controllerIndex, action);
        }

        public override int FindIndexByText(CriteriaOperator criteriaOperand, CriteriaOperator compareOperator, string text, bool isCaseSensitive, int startItemIndex, bool searchNext, bool ignoreStartIndex)
        {
            int num = this.FindIndexByTextLocal(compareOperator, isCaseSensitive, this.view.Values, startItemIndex, searchNext, ignoreStartIndex);
            if (num > -1)
            {
                return num;
            }
            FindRowByTextCompleter completer = new FindRowByTextCompleter(this.dataView, text, startItemIndex, searchNext, ignoreStartIndex);
            this.Wrapper.FindRowByText(criteriaOperand, text, startItemIndex, searchNext, ignoreStartIndex, new OperationCompleted(completer.Completed));
            return -2147483638;
        }

        public override int FindIndexByValue(CriteriaOperator compareOperator, object value)
        {
            this.Wrapper.FindRowByValue(compareOperator, value, new OperationCompleted(this.FindRowByValueCompleted));
            return -2147483638;
        }

        public virtual int FindIndexByValue(CriteriaOperator compareOperator, object value, OperationCompleted completed)
        {
            this.Wrapper.FindRowByValue(compareOperator, value, completed);
            return -2147483638;
        }

        private void FindRowByValueCompleted(object arguments)
        {
            CommandLocateByValue value2 = (CommandLocateByValue) arguments;
            int rowIndex = value2.RowIndex;
            if ((value2.RowIndex >= 0) && !AsyncServerModeDataController.IsNoValue(this.Wrapper.GetRow(rowIndex, new OperationCompleted(this.LoadRowCompleted))))
            {
                this.dataView.NotifyLoaded(rowIndex);
            }
        }

        public override IEnumerator<DataProxy> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        public bool IsRowLoaded(int listSourceIndex) => 
            this.Wrapper.IsRowLoaded(listSourceIndex);

        private void LoadRowCompleted(object arguments)
        {
            CommandGetRow row = (CommandGetRow) arguments;
            if (row.Index >= 0)
            {
                this.LoadRowCompletedInternal(row.Index);
            }
        }

        private void LoadRowCompletedInternal(int listSourceIndex)
        {
            this.dataView.NotifyLoaded(listSourceIndex);
        }

        public override void Remove(int index)
        {
            if (this.view.ContainsKey(index))
            {
                this.view.Remove(index);
            }
        }

        public override void Replace(int index, DataProxy item)
        {
            if (this.Wrapper.IsRowLoaded(index))
            {
                this.SetProxy(base.DataAccessor.CreateProxy(this.Wrapper.GetRow(index, null), index));
            }
            else
            {
                DataProxy proxy = base.DataAccessor.CreateProxy(null, index);
                proxy.f_component = this.Wrapper.GetRow(index, new OperationCompleted(this.LoadRowCompleted));
                this.SetProxy(proxy);
            }
        }

        public override void Reset()
        {
            this.Wrapper.Invalidate();
            this.view.Clear();
        }

        private void SetProxy(DataProxy proxy)
        {
            int index = proxy.f_visibleIndex;
            this.view[index] = proxy;
            proxy.f_RowKey = base.DataAccessor.HasValueMember ? this.dataView.GetValueFromProxy(proxy) : this.Wrapper.GetLoadedRowKey(index);
        }

        private AsyncListWrapper Wrapper =>
            this.dataView.Wrapper;

        public DataProxy TempProxy =>
            this.tempProxy;

        public override DataProxy this[int index]
        {
            get
            {
                if (!this.Wrapper.IsRowLoaded(index))
                {
                    this.Wrapper.GetRow(index, new OperationCompleted(this.LoadRowCompleted));
                    return this.tempProxy;
                }
                if (!this.view.ContainsKey(index))
                {
                    this.SetProxy(base.DataAccessor.CreateProxy(this.Wrapper.GetRow(index, null), index));
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
    }
}

