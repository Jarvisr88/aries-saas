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
    using System.Reflection;

    public class AsyncListServer2DatacontrollerProxy : IAsyncListServer, IListServerHints, IDisposable, ITypedList, IList, ICollection, IEnumerable, IDXCloneable
    {
        protected readonly IAsyncListServer Nested;

        public AsyncListServer2DatacontrollerProxy(IAsyncListServer nested);
        public virtual int Add(object value);
        public virtual CommandApply Apply(CriteriaOperator filterCriteria, ICollection<ServerModeOrderDescriptor[]> sortInfo, int groupCount, ICollection<ServerModeSummaryDescriptor> summaryInfo, ICollection<ServerModeSummaryDescriptor> totalSummaryInfo, params DictionaryEntry[] tags);
        public virtual void Cancel<T>() where T: Command;
        public virtual void Cancel(Command command);
        public virtual void Clear();
        public virtual bool Contains(object value);
        public virtual void CopyTo(Array array, int index);
        object IDXCloneable.DXClone();
        void IListServerHints.HintGridIsPaged(int pageSize);
        void IListServerHints.HintMaxVisibleRowsInGrid(int rowsInGrid);
        public virtual void Dispose();
        protected virtual AsyncListServer2DatacontrollerProxy DXClone();
        public virtual CommandFindIncremental FindIncremental(CriteriaOperator expression, string value, int startIndex, bool searchUp, bool ignoreStartRow, bool allowLoop, params DictionaryEntry[] tags);
        public virtual CommandGetAllFilteredAndSortedRows GetAllFilteredAndSortedRows(params DictionaryEntry[] tags);
        public virtual IEnumerator GetEnumerator();
        public virtual CommandGetGroupInfo GetGroupInfo(ListSourceGroupInfo parentGroup, params DictionaryEntry[] tags);
        public virtual PropertyDescriptorCollection GetItemProperties(PropertyDescriptor[] listAccessors);
        public virtual string GetListName(PropertyDescriptor[] listAccessors);
        public virtual CommandGetRow GetRow(int index, params DictionaryEntry[] tags);
        public virtual CommandGetRowIndexByKey GetRowIndexByKey(object key, params DictionaryEntry[] tags);
        public virtual CommandGetTotals GetTotals(params DictionaryEntry[] tags);
        public virtual CommandGetUniqueColumnValues GetUniqueColumnValues(CriteriaOperator valuesExpression, int maxCount, CriteriaOperator filterExpression, bool ignoreAppliedFilter, params DictionaryEntry[] tags);
        public virtual int IndexOf(object value);
        public virtual void Insert(int index, object value);
        public virtual CommandLocateByValue LocateByValue(CriteriaOperator expression, object value, int startIndex, bool searchUp, params DictionaryEntry[] tags);
        public virtual CommandPrefetchRows PrefetchRows(ListSourceGroupInfo[] groupsToPrefetch, params DictionaryEntry[] tags);
        public virtual T PullNext<T>() where T: Command;
        public virtual CommandRefresh Refresh(params DictionaryEntry[] tags);
        public virtual void Remove(object value);
        public virtual void RemoveAt(int index);
        public virtual void SetReceiver(IAsyncResultReceiver receiver);
        public virtual bool WaitFor(Command waitForCommand);
        public virtual void WeakCancel<T>() where T: Command;

        public virtual bool IsFixedSize { get; }

        public virtual bool IsReadOnly { get; }

        public virtual object this[int index] { get; set; }

        public virtual int Count { get; }

        public virtual bool IsSynchronized { get; }

        public virtual object SyncRoot { get; }
    }
}

