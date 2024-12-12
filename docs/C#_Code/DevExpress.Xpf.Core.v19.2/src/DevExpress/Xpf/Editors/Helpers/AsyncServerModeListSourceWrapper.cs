namespace DevExpress.Xpf.Editors.Helpers
{
    using DevExpress.Data;
    using DevExpress.Data.Async;
    using DevExpress.Data.Filtering;
    using DevExpress.Data.Helpers;
    using DevExpress.Mvvm.Native;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Threading;

    public class AsyncServerModeListSourceWrapper : IAsyncListServer, ITypedList, IDisposable, IList, ICollection, IEnumerable
    {
        public event EventHandler<ServerModeExceptionThrownEventArgs> ExceptionThrown;

        public event EventHandler<ServerModeInconsistencyDetectedEventArgs> InconsistencyDetected;

        public AsyncServerModeListSourceWrapper(AsyncVisibleListWrapper wrapper)
        {
            this.Server = wrapper;
            this.Wrapper = wrapper;
            this.List = wrapper;
            this.Disposable = wrapper;
            this.SubscribeServer();
        }

        public void Cancel<T>() where T: Command
        {
            this.Server.Cancel<T>();
        }

        public void Cancel(Command command)
        {
            this.Server.Cancel(command);
        }

        CommandApply IAsyncListServer.Apply(CriteriaOperator filterCriteria, ICollection<ServerModeOrderDescriptor[]> sortInfo, int groupCount, ICollection<ServerModeSummaryDescriptor> groupSummaryInfo, ICollection<ServerModeSummaryDescriptor> totalSummaryInfo, params DictionaryEntry[] tags) => 
            this.Wrapper.ApplyControlServerSettings(filterCriteria, sortInfo, groupCount, groupSummaryInfo, totalSummaryInfo, tags);

        CommandFindIncremental IAsyncListServer.FindIncremental(CriteriaOperator expression, string value, int startIndex, bool searchUp, bool ignoreStartRow, bool allowLoop, params DictionaryEntry[] tags) => 
            this.Server.FindIncremental(expression, value, startIndex, searchUp, ignoreStartRow, allowLoop, tags);

        CommandGetAllFilteredAndSortedRows IAsyncListServer.GetAllFilteredAndSortedRows(params DictionaryEntry[] tags) => 
            this.Server.GetAllFilteredAndSortedRows(tags);

        CommandGetGroupInfo IAsyncListServer.GetGroupInfo(ListSourceGroupInfo parentGroup, params DictionaryEntry[] tags) => 
            this.Server.GetGroupInfo(parentGroup, tags);

        CommandGetRow IAsyncListServer.GetRow(int index, params DictionaryEntry[] tags) => 
            this.Server.GetRow(index, tags);

        CommandGetRowIndexByKey IAsyncListServer.GetRowIndexByKey(object key, params DictionaryEntry[] tags) => 
            this.Server.GetRowIndexByKey(key, tags);

        CommandGetTotals IAsyncListServer.GetTotals(params DictionaryEntry[] tags) => 
            this.Server.GetTotals(tags);

        CommandGetUniqueColumnValues IAsyncListServer.GetUniqueColumnValues(CriteriaOperator valuesExpression, int maxCount, CriteriaOperator filterExpression, bool ignoreAppliedFilter, params DictionaryEntry[] tags) => 
            this.Server.GetUniqueColumnValues(valuesExpression, maxCount, filterExpression, ignoreAppliedFilter, tags);

        CommandLocateByValue IAsyncListServer.LocateByValue(CriteriaOperator expression, object value, int startIndex, bool searchUp, params DictionaryEntry[] tags) => 
            this.Server.LocateByValue(expression, value, startIndex, searchUp, tags);

        CommandPrefetchRows IAsyncListServer.PrefetchRows(ListSourceGroupInfo[] groupsToPrefetch, params DictionaryEntry[] tags) => 
            this.Server.PrefetchRows(groupsToPrefetch, tags);

        CommandRefresh IAsyncListServer.Refresh(params DictionaryEntry[] tags) => 
            this.Server.Refresh(tags);

        public T PullNext<T>() where T: Command => 
            this.Server.PullNext<T>();

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

        public void SetReceiver(IAsyncResultReceiver receiver)
        {
            this.Server.SetReceiver(receiver);
        }

        private void SubscribeServer()
        {
        }

        void ICollection.CopyTo(Array array, int index)
        {
            throw new NotSupportedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotSupportedException();
        }

        int IList.Add(object value)
        {
            throw new NotSupportedException();
        }

        void IList.Clear()
        {
        }

        bool IList.Contains(object value) => 
            false;

        int IList.IndexOf(object value) => 
            -1;

        void IList.Insert(int index, object value)
        {
            throw new NotSupportedException();
        }

        void IList.Remove(object value)
        {
            throw new NotSupportedException();
        }

        void IList.RemoveAt(int index)
        {
            throw new NotSupportedException();
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
        }

        public bool WaitFor(Command command) => 
            this.Server.WaitFor(command);

        public void WeakCancel<T>() where T: Command
        {
            this.Server.WeakCancel<T>();
        }

        private IAsyncListServer Server { get; set; }

        private AsyncVisibleListWrapper Wrapper { get; set; }

        private IListServerHints Hints { get; set; }

        private ITypedList List { get; set; }

        private IDisposable Disposable { get; set; }

        bool IList.IsReadOnly =>
            true;

        bool IList.IsFixedSize =>
            true;

        int ICollection.Count =>
            0;

        object ICollection.SyncRoot =>
            this;

        bool ICollection.IsSynchronized =>
            false;

        object IList.this[int index]
        {
            get => 
                null;
            set
            {
            }
        }
    }
}

