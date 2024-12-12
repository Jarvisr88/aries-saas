namespace DevExpress.Data
{
    using DevExpress.Data.Filtering;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Threading;

    public interface IListServer : IList, ICollection, IEnumerable
    {
        event EventHandler<ServerModeExceptionThrownEventArgs> ExceptionThrown;

        event EventHandler<ServerModeInconsistencyDetectedEventArgs> InconsistencyDetected;

        void Apply(CriteriaOperator filterCriteria, ICollection<ServerModeOrderDescriptor[]> sortInfo, int groupCount, ICollection<ServerModeSummaryDescriptor> groupSummaryInfo, ICollection<ServerModeSummaryDescriptor> totalSummaryInfo);
        int FindIncremental(CriteriaOperator expression, string value, int startIndex, bool searchUp, bool ignoreStartRow, bool allowLoop);
        IList GetAllFilteredAndSortedRows();
        List<ListSourceGroupInfo> GetGroupInfo(ListSourceGroupInfo parentGroup);
        int GetRowIndexByKey(object key);
        object GetRowKey(int index);
        List<object> GetTotalSummary();
        object[] GetUniqueColumnValues(CriteriaOperator valuesExpression, int maxCount, CriteriaOperator filterExpression, bool ignoreAppliedFilter);
        int LocateByExpression(CriteriaOperator expression, int startIndex, bool searchUp);
        int LocateByValue(CriteriaOperator expression, object value, int startIndex, bool searchUp);
        bool PrefetchRows(ListSourceGroupInfo[] groupsToPrefetch, CancellationToken cancellationToken);
        void Refresh();
    }
}

