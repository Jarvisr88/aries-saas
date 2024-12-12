namespace DevExpress.Data.Async.Helpers
{
    using DevExpress.Data;
    using DevExpress.Data.Async;
    using DevExpress.Data.Filtering;
    using DevExpress.Data.Helpers;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Threading;

    public class AsyncListServerCore : IAsyncListServer, IListServerHints, IDisposable, ITypedList, IDXCloneable
    {
        private bool isDisposed;
        public readonly System.Threading.SynchronizationContext SynchronizationContext;
        public EventHandler<ListServerGetOrFreeEventArgs> ListServerGet;
        public EventHandler<ListServerGetOrFreeEventArgs> ListServerFree;
        public EventHandler<GetTypeInfoEventArgs> GetTypeInfo;
        public EventHandler<GetWorkerThreadRowInfoEventArgs> GetWorkerThreadRowInfo;
        public EventHandler<GetPropertyDescriptorsEventArgs> GetPropertyDescriptors;
        public EventHandler<GetUIThreadRowEventArgs> GetUIThreadRow;
        private IAsyncResultReceiver ResultsReceiver;
        private CommandQueue _Worker;
        private bool isBusy;

        public AsyncListServerCore();
        public AsyncListServerCore(System.Threading.SynchronizationContext context);
        public AsyncListServerCore(System.Threading.SynchronizationContext context, EventHandler<ListServerGetOrFreeEventArgs> listServerGet);
        public AsyncListServerCore(System.Threading.SynchronizationContext context, IAsyncResultReceiver resultsReceiver, EventHandler<ListServerGetOrFreeEventArgs> listServerCreation);
        public CommandApply Apply(CriteriaOperator filterCriteria, ICollection<ServerModeOrderDescriptor[]> sortInfo, int groupCount, ICollection<ServerModeSummaryDescriptor> summaryInfo, ICollection<ServerModeSummaryDescriptor> totalSummaryInfo, params DictionaryEntry[] tags);
        public void Cancel<T>() where T: Command;
        public void Cancel(Command command);
        private void CheckDescriptorsRenew();
        protected virtual CommandQueue CreateCommandQueue(System.Threading.SynchronizationContext context, SendOrPostCallback somethingInTheOutputQueueCallback, EventHandler<ListServerGetOrFreeEventArgs> listServerGet, EventHandler<ListServerGetOrFreeEventArgs> listServerFree, EventHandler<GetTypeInfoEventArgs> getTypeInfo, EventHandler<GetWorkerThreadRowInfoEventArgs> getWorkerThreadRowInfo);
        protected virtual AsyncListServerCore CreateDXClone();
        object IDXCloneable.DXClone();
        void IListServerHints.HintGridIsPaged(int pageSize);
        void IListServerHints.HintMaxVisibleRowsInGrid(int rowsInGrid);
        private void DispatchOutputQueue();
        public virtual void Dispose();
        private void DoAfterDispatch(Command result);
        private void DoBeforeDispatch(Command result);
        protected virtual AsyncListServerCore DXClone();
        private object ExtractUIRowFromRowInfo(object rowInfo);
        public CommandFindIncremental FindIncremental(CriteriaOperator expression, string value, int startIndex, bool searchUp, bool ignoreStartRow, bool allowLoop, params DictionaryEntry[] tags);
        public CommandGetAllFilteredAndSortedRows GetAllFilteredAndSortedRows(params DictionaryEntry[] tags);
        public CommandGetGroupInfo GetGroupInfo(ListSourceGroupInfo parentGroup, params DictionaryEntry[] tags);
        public CommandGetRow GetRow(int index, params DictionaryEntry[] tags);
        public CommandGetRowIndexByKey GetRowIndexByKey(object key, params DictionaryEntry[] tags);
        public CommandGetTotals GetTotals(params DictionaryEntry[] tags);
        public CommandGetUniqueColumnValues GetUniqueColumnValues(CriteriaOperator valuesExpression, int maxCount, CriteriaOperator filterExpression, bool ignoreAppliedFilter, params DictionaryEntry[] tags);
        private bool IsDoAfterDispatchRequired(Command nextCommand);
        public CommandLocateByValue LocateByValue(CriteriaOperator expression, object value, int startIndex, bool searchUp, params DictionaryEntry[] tags);
        private T PostCommand<T>(T command) where T: Command;
        public CommandPrefetchRows PrefetchRows(ListSourceGroupInfo[] groupsToPrefetch, params DictionaryEntry[] tags);
        private void ProcessNewBusy(bool newBusy);
        public T PullNext<T>() where T: Command;
        public CommandRefresh Refresh(params DictionaryEntry[] tags);
        public void SetReceiver(IAsyncResultReceiver receiver);
        private void ShutDown();
        private void somethingInTheOutputQueueCallback(object arg);
        PropertyDescriptorCollection ITypedList.GetItemProperties(PropertyDescriptor[] listAccessors);
        string ITypedList.GetListName(PropertyDescriptor[] listAccessors);
        public bool WaitFor(Command command);
        public void WeakCancel<T>() where T: Command;

        private CommandQueue Worker { get; }

        private class GetAllFilteredAndSortedRowsResult : List<object>, ITypedList
        {
            private readonly ITypedList TypedListInfoSource;

            public GetAllFilteredAndSortedRowsResult(int count, ITypedList typedListInfoSource);
            PropertyDescriptorCollection ITypedList.GetItemProperties(PropertyDescriptor[] listAccessors);
            string ITypedList.GetListName(PropertyDescriptor[] listAccessors);
        }
    }
}

