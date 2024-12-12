namespace DevExpress.Data.PLinq.Helpers
{
    using DevExpress.Data;
    using DevExpress.Data.Filtering;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Reflection;
    using System.Threading;

    public class PLinqListServerDatacontrollerProxy : IListServer, IList, ICollection, IEnumerable, ITypedList
    {
        protected readonly IListServer Nested;

        public event EventHandler<ServerModeExceptionThrownEventArgs> ExceptionThrown;

        public event EventHandler<ServerModeInconsistencyDetectedEventArgs> InconsistencyDetected;

        public PLinqListServerDatacontrollerProxy(IListServer nested);
        public int Add(object value);
        public void Apply(CriteriaOperator filterCriteria, ICollection<ServerModeOrderDescriptor[]> sortInfo, int groupCount, ICollection<ServerModeSummaryDescriptor> groupSummaryInfo, ICollection<ServerModeSummaryDescriptor> totalSummaryInfo);
        public void Clear();
        public bool Contains(object value);
        public void CopyTo(Array array, int index);
        public int FindIncremental(CriteriaOperator expression, string value, int startIndex, bool searchUp, bool ignoreStartRow, bool allowLoop);
        public IList GetAllFilteredAndSortedRows();
        public IEnumerator GetEnumerator();
        public List<ListSourceGroupInfo> GetGroupInfo(ListSourceGroupInfo parentGroup);
        public PropertyDescriptorCollection GetItemProperties(PropertyDescriptor[] listAccessors);
        public string GetListName(PropertyDescriptor[] listAccessors);
        public int GetRowIndexByKey(object key);
        public object GetRowKey(int index);
        public List<object> GetTotalSummary();
        public object[] GetUniqueColumnValues(CriteriaOperator valuesExpression, int maxCount, CriteriaOperator filterExpression, bool ignoreAppliedFilter);
        public int IndexOf(object value);
        public void Insert(int index, object value);
        public int LocateByExpression(CriteriaOperator expression, int startIndex, bool searchUp);
        public int LocateByValue(CriteriaOperator expression, object value, int startIndex, bool searchUp);
        public virtual bool PrefetchRows(ListSourceGroupInfo[] groupsToPrefetch, CancellationToken cancellationToken);
        public void Refresh();
        public void Remove(object value);
        public void RemoveAt(int index);

        public bool IsFixedSize { get; }

        public bool IsReadOnly { get; }

        public object this[int index] { get; set; }

        public int Count { get; }

        public bool IsSynchronized { get; }

        public object SyncRoot { get; }
    }
}

