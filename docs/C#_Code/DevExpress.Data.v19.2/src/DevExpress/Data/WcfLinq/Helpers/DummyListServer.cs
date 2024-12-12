namespace DevExpress.Data.WcfLinq.Helpers
{
    using DevExpress.Data;
    using DevExpress.Data.Filtering;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Threading;

    internal class DummyListServer : List<GetSourceNotHandledMessenger>, IListServer, IList, ICollection, IEnumerable
    {
        public event EventHandler<ServerModeExceptionThrownEventArgs> ExceptionThrown;

        public event EventHandler<ServerModeInconsistencyDetectedEventArgs> InconsistencyDetected;

        public DummyListServer();
        public void Apply(CriteriaOperator filterCriteria, ICollection<ServerModeOrderDescriptor[]> sortInfo, int groupCount, ICollection<ServerModeSummaryDescriptor> groupSummaryInfo, ICollection<ServerModeSummaryDescriptor> totalSummaryInfo);
        public int FindIncremental(CriteriaOperator expression, string value, int startIndex, bool searchUp, bool ignoreStartRow, bool allowLoop);
        public IList GetAllFilteredAndSortedRows();
        public List<ListSourceGroupInfo> GetGroupInfo(ListSourceGroupInfo parentGroup);
        public int GetRowIndexByKey(object key);
        public object GetRowKey(int index);
        public List<object> GetTotalSummary();
        public object[] GetUniqueColumnValues(CriteriaOperator valuesExpression, int maxCount, CriteriaOperator filterExpression, bool ignoreAppliedFilter);
        public int LocateByExpression(CriteriaOperator expression, int startIndex, bool searchUp);
        public int LocateByValue(CriteriaOperator expression, object value, int startIndex, bool searchUp);
        public virtual bool PrefetchRows(ListSourceGroupInfo[] groupsToPrefetch, CancellationToken cancellationToken);
        public void Refresh();
    }
}

