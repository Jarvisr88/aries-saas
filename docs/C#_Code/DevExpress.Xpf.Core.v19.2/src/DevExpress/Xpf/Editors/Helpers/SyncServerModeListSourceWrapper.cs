namespace DevExpress.Xpf.Editors.Helpers
{
    using DevExpress.Data;
    using DevExpress.Data.Filtering;
    using DevExpress.Data.Helpers;
    using DevExpress.Mvvm.Native;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Threading;

    public class SyncServerModeListSourceWrapper : IListServer, IList, ICollection, IEnumerable, IListServerHints, ITypedList, IDisposable
    {
        public event EventHandler<ServerModeExceptionThrownEventArgs> ExceptionThrown;

        public event EventHandler<ServerModeInconsistencyDetectedEventArgs> InconsistencyDetected;

        public SyncServerModeListSourceWrapper(SyncVisibleListWrapper wrapper)
        {
            this.Server = wrapper;
            this.Wrapper = wrapper;
            this.Hints = wrapper;
            this.List = wrapper;
            this.Disposable = wrapper;
            this.SubscribeServer();
        }

        void IListServerHints.HintGridIsPaged(int pageSize)
        {
            this.Hints.HintGridIsPaged(pageSize);
        }

        void IListServerHints.HintMaxVisibleRowsInGrid(int rowsInGrid)
        {
            this.Hints.HintMaxVisibleRowsInGrid(rowsInGrid);
        }

        void IListServer.Apply(CriteriaOperator filterCriteria, ICollection<ServerModeOrderDescriptor[]> sortInfo, int groupCount, ICollection<ServerModeSummaryDescriptor> groupSummaryInfo, ICollection<ServerModeSummaryDescriptor> totalSummaryInfo)
        {
            this.Wrapper.ApplyControlServerSettings(filterCriteria, sortInfo, groupCount, groupSummaryInfo, totalSummaryInfo);
        }

        int IListServer.FindIncremental(CriteriaOperator expression, string value, int startIndex, bool searchUp, bool ignoreStartRow, bool allowLoop) => 
            this.Server.FindIncremental(expression, value, startIndex, searchUp, ignoreStartRow, allowLoop);

        IList IListServer.GetAllFilteredAndSortedRows() => 
            this.Server.GetAllFilteredAndSortedRows();

        List<ListSourceGroupInfo> IListServer.GetGroupInfo(ListSourceGroupInfo parentGroup) => 
            this.Server.GetGroupInfo(parentGroup);

        int IListServer.GetRowIndexByKey(object key) => 
            this.Server.GetRowIndexByKey(key);

        object IListServer.GetRowKey(int index) => 
            this.Server.GetRowKey(index);

        List<object> IListServer.GetTotalSummary() => 
            this.Server.GetTotalSummary();

        object[] IListServer.GetUniqueColumnValues(CriteriaOperator valuesExpression, int maxCount, CriteriaOperator filterExpression, bool ignoreAppliedFilter) => 
            this.Server.GetUniqueColumnValues(valuesExpression, maxCount, filterExpression, ignoreAppliedFilter);

        int IListServer.LocateByExpression(CriteriaOperator expression, int startIndex, bool searchUp) => 
            this.Server.LocateByExpression(expression, startIndex, searchUp);

        int IListServer.LocateByValue(CriteriaOperator expression, object value, int startIndex, bool searchUp) => 
            this.Server.LocateByValue(expression, value, startIndex, searchUp);

        bool IListServer.PrefetchRows(ListSourceGroupInfo[] groupsToPrefetch, CancellationToken cancellationToken) => 
            this.Server.PrefetchRows(groupsToPrefetch, cancellationToken);

        void IListServer.Refresh()
        {
            this.Server.Refresh();
        }

        private void RaiseExceptionThrown(ServerModeExceptionThrownEventArgs args)
        {
            EventHandler<ServerModeExceptionThrownEventArgs> exceptionThrown = this.ExceptionThrown;
            if (exceptionThrown != null)
            {
                exceptionThrown(this, args);
            }
        }

        private void RaiseInconsistencyDetected(ServerModeInconsistencyDetectedEventArgs args)
        {
            EventHandler<ServerModeInconsistencyDetectedEventArgs> inconsistencyDetected = this.InconsistencyDetected;
            if (inconsistencyDetected != null)
            {
                inconsistencyDetected(this, args);
            }
        }

        private void ServerOnExceptionThrown(object sender, ServerModeExceptionThrownEventArgs args)
        {
            this.RaiseExceptionThrown(args);
        }

        private void ServerOnInconsistencyDetected(object sender, ServerModeInconsistencyDetectedEventArgs args)
        {
            this.RaiseInconsistencyDetected(args);
        }

        private void SubscribeServer()
        {
            this.Server.InconsistencyDetected += new EventHandler<ServerModeInconsistencyDetectedEventArgs>(this.ServerOnInconsistencyDetected);
            this.Server.ExceptionThrown += new EventHandler<ServerModeExceptionThrownEventArgs>(this.ServerOnExceptionThrown);
        }

        void ICollection.CopyTo(Array array, int index)
        {
            this.Wrapper.CopyTo(array, index);
        }

        IEnumerator IEnumerable.GetEnumerator() => 
            this.Wrapper.GetEnumerator();

        int IList.Add(object value) => 
            this.Wrapper.Add(value);

        void IList.Clear()
        {
            this.Wrapper.Clear();
        }

        bool IList.Contains(object value) => 
            this.Wrapper.Contains(value);

        int IList.IndexOf(object value) => 
            this.Wrapper.IndexOf(value);

        void IList.Insert(int index, object value)
        {
            this.Wrapper.Insert(index, value);
        }

        void IList.Remove(object value)
        {
            this.Wrapper.Remove(value);
        }

        void IList.RemoveAt(int index)
        {
            this.Wrapper.RemoveAt(index);
        }

        PropertyDescriptorCollection ITypedList.GetItemProperties(PropertyDescriptor[] listAccessors) => 
            this.List.GetItemProperties(listAccessors);

        string ITypedList.GetListName(PropertyDescriptor[] listAccessors) => 
            this.List.GetListName(listAccessors);

        void IDisposable.Dispose()
        {
            this.Disposable.Do<IDisposable>(delegate (IDisposable x) {
                this.UnsubscribeServer();
                x.Dispose();
            });
            this.Wrapper = null;
            this.Server = null;
            this.Hints = null;
            this.List = null;
            this.Disposable = null;
        }

        private void UnsubscribeServer()
        {
            this.Server.InconsistencyDetected -= new EventHandler<ServerModeInconsistencyDetectedEventArgs>(this.ServerOnInconsistencyDetected);
            this.Server.ExceptionThrown -= new EventHandler<ServerModeExceptionThrownEventArgs>(this.ServerOnExceptionThrown);
        }

        private IListServer Server { get; set; }

        private SyncVisibleListWrapper Wrapper { get; set; }

        private IListServerHints Hints { get; set; }

        private ITypedList List { get; set; }

        private IDisposable Disposable { get; set; }

        int ICollection.Count =>
            this.Wrapper.Count;

        object ICollection.SyncRoot =>
            this.Wrapper.SyncRoot;

        bool ICollection.IsSynchronized =>
            this.Wrapper.IsSynchronized;

        object IList.this[int index]
        {
            get => 
                ((DataProxy) this.Wrapper[index]).f_component;
            set
            {
                throw new NotImplementedException();
            }
        }

        bool IList.IsReadOnly =>
            this.Wrapper.IsReadOnly;

        bool IList.IsFixedSize =>
            this.Wrapper.IsFixedSize;
    }
}

