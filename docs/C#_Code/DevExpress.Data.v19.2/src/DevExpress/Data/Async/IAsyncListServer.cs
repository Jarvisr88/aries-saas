namespace DevExpress.Data.Async
{
    using DevExpress.Data;
    using DevExpress.Data.Filtering;
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public interface IAsyncListServer
    {
        CommandApply Apply(CriteriaOperator filterCriteria, ICollection<ServerModeOrderDescriptor[]> sortInfo, int groupCount, ICollection<ServerModeSummaryDescriptor> summaryInfo, ICollection<ServerModeSummaryDescriptor> totalSummaryInfo, params DictionaryEntry[] tags);
        void Cancel<T>() where T: Command;
        void Cancel(Command command);
        CommandFindIncremental FindIncremental(CriteriaOperator expression, string value, int startIndex, bool searchUp, bool ignoreStartRow, bool allowLoop, params DictionaryEntry[] tags);
        CommandGetAllFilteredAndSortedRows GetAllFilteredAndSortedRows(params DictionaryEntry[] tags);
        CommandGetGroupInfo GetGroupInfo(ListSourceGroupInfo parentGroup, params DictionaryEntry[] tags);
        CommandGetRow GetRow(int index, params DictionaryEntry[] tags);
        CommandGetRowIndexByKey GetRowIndexByKey(object key, params DictionaryEntry[] tags);
        CommandGetTotals GetTotals(params DictionaryEntry[] tags);
        CommandGetUniqueColumnValues GetUniqueColumnValues(CriteriaOperator valuesExpression, int maxCount, CriteriaOperator filterExpression, bool ignoreAppliedFilter, params DictionaryEntry[] tags);
        CommandLocateByValue LocateByValue(CriteriaOperator expression, object value, int startIndex, bool searchUp, params DictionaryEntry[] tags);
        CommandPrefetchRows PrefetchRows(ListSourceGroupInfo[] groupsToPrefetch, params DictionaryEntry[] tags);
        T PullNext<T>() where T: Command;
        CommandRefresh Refresh(params DictionaryEntry[] tags);
        void SetReceiver(IAsyncResultReceiver receiver);
        bool WaitFor(Command command);
        void WeakCancel<T>() where T: Command;
    }
}

