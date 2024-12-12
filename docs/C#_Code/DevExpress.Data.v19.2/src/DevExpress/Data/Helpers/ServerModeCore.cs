namespace DevExpress.Data.Helpers
{
    using DevExpress.Data;
    using DevExpress.Data.Filtering;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Threading;

    public abstract class ServerModeCore : IListServer, IList, ICollection, IEnumerable, IListServerHints, IDXCloneable
    {
        protected readonly CriteriaOperator[] KeysCriteria;
        protected CriteriaOperator FilterClause;
        protected ServerModeOrderDescriptor[][] SortInfo;
        protected int GroupCount;
        protected ServerModeSummaryDescriptor[] SummaryInfo;
        protected ServerModeSummaryDescriptor[] TotalSummaryInfo;
        public string DefaultSorting;
        public static bool? DefaultForceCaseInsensitiveForAnySource;
        public bool ForceCaseInsensitiveForAnySource;
        private ServerModeCache _cache;
        protected static readonly CriteriaOperator LogicalFalseStub;
        protected static readonly CriteriaOperator ValueFalseStub;
        internal const string OrderDescToken = "DevExpress;MAGIK const;ORDER DESCENDING";
        private bool? useToLower;
        private Tuple<CriteriaOperator, CriteriaOperator, Func<object, object>> _OnInstanceEvaluatorCache;
        private int _hintGridIsPagedPageSize;
        private int _hintMaxVisibleRowsInGrid;

        public event EventHandler<ServerModeExceptionThrownEventArgs> ExceptionThrown;

        public event EventHandler<ServerModeInconsistencyDetectedEventArgs> InconsistencyDetected;

        static ServerModeCore();
        protected ServerModeCore(CriteriaOperator[] key);
        private void _cache_ExceptionThrown(object sender, ServerModeExceptionThrownEventArgs e);
        private void _cache_InconsistencyDetected(object sender, ServerModeInconsistencyDetectedEventArgs e);
        public int Add(object value);
        public void Apply(CriteriaOperator filterCriteria, ICollection<ServerModeOrderDescriptor[]> sortInfo, int groupCount, ICollection<ServerModeSummaryDescriptor> summaryInfo, ICollection<ServerModeSummaryDescriptor> totalSummaryInfo);
        private void ApplyHints();
        public static bool AreEqualsOrder(ServerModeOrderDescriptor[][] a, ServerModeOrderDescriptor[][] b);
        internal static bool AreEqualsOrderCore(ServerModeOrderDescriptor[] a, ServerModeOrderDescriptor[] b);
        public static bool AreEqualsSummary(ServerModeSummaryDescriptor[] a, ServerModeSummaryDescriptor[] b);
        public void Clear();
        public bool Contains(object value);
        private ServerModeSummaryDescriptor[] Convert(ICollection<ServerModeSummaryDescriptor> original, ref int exceptionsThrown);
        private ServerModeOrderDescriptor[][] Convert(ICollection<ServerModeOrderDescriptor[]> original, int groupCount, ref int exceptionsThrown);
        public void CopyTo(Array array, int index);
        private ServerModeCache CreateCache();
        protected abstract ServerModeCache CreateCacheCore();
        object IDXCloneable.DXClone();
        void IListServerHints.HintGridIsPaged(int pageSize);
        void IListServerHints.HintMaxVisibleRowsInGrid(int rowsInGrid);
        protected virtual ServerModeCore DXClone();
        protected abstract ServerModeCore DXCloneCreate();
        protected object EvaluateOnInstance(object row, CriteriaOperator dirtyExpression, CriteriaOperator extractedExpression);
        private bool EvaluateOnInstanceLogical(object row, CriteriaOperator dirtyExpression, CriteriaOperator extractedExpression);
        private CriteriaOperator ExtractExpression(CriteriaOperator d, ref int exceptionsThrown, CriteriaOperator onExceptionValue);
        protected virtual CriteriaOperator ExtractExpressionCore(CriteriaOperator d);
        public CriteriaOperator ExtractExpressionLogical(CriteriaOperator d, ref int exceptionsThrown);
        public CriteriaOperator ExtractExpressionValue(CriteriaOperator d, ref int exceptionsThrown);
        private static ServerModeOrderDescriptor ExtractSorting(CriteriaOperator op);
        public int FindIncremental(CriteriaOperator expression, string value, int startIndex, bool searchUp, bool ignoreStartRow, bool allowLoop);
        public abstract IList GetAllFilteredAndSortedRows();
        public IEnumerator GetEnumerator();
        public List<ListSourceGroupInfo> GetGroupInfo(ListSourceGroupInfo parentGroup);
        protected virtual Func<object, object> GetOnInstanceEvaluator(CriteriaOperator dirtyExpression, CriteriaOperator extractedExpression);
        public int GetRowIndexByKey(object key);
        public object GetRowKey(int index);
        public static ServerModeOrderDescriptor[] GetSortingDescriptors(string sortingsString);
        public List<object> GetTotalSummary();
        public object[] GetUniqueColumnValues(CriteriaOperator valuesExpression, int maxCount, CriteriaOperator filterExpression, bool ignoreAppliedFilter);
        protected abstract object[] GetUniqueValues(CriteriaOperator expression, int maxCount, CriteriaOperator filter);
        public int IndexOf(object value);
        public void Insert(int index, object value);
        private bool IsIndexFit(int index, CriteriaOperator dirtyExpression, CriteriaOperator extractedExpression);
        public int LocateByExpression(CriteriaOperator expression, int startIndex, bool searchUp);
        public int LocateByValue(CriteriaOperator expression, object value, int startIndex, bool searchUp);
        public virtual bool PrefetchRows(ListSourceGroupInfo[] groupsToPrefetch, CancellationToken cancellationToken);
        protected virtual void RaiseExceptionThrown(ServerModeExceptionThrownEventArgs e);
        protected virtual void RaiseInconsistencyDetected(ServerModeInconsistencyDetectedEventArgs e);
        public virtual void Refresh();
        private void RefreshCacheOnApply(bool sameFilter);
        public void Remove(object value);
        public void RemoveAt(int index);
        protected virtual void SoftReset();
        protected T WithReentryProtection<T>(Func<T> action);

        protected ServerModeCache Cache { get; }

        public int Count { get; }

        public object this[int index] { get; set; }

        public bool IsSynchronized { get; }

        public object SyncRoot { get; }

        public bool IsFixedSize { get; }

        public bool IsReadOnly { get; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly ServerModeCore.<>c <>9;
            public static Func<CriteriaOperator, ServerModeOrderDescriptor[]> <>9__18_0;

            static <>c();
            internal ServerModeOrderDescriptor[] <.ctor>b__18_0(CriteriaOperator c);
        }

        private class GetUniqueColumnValuesCacheKey
        {
            public readonly CriteriaOperator ValuesExpression;
            public readonly int MaxCount;
            public readonly CriteriaOperator FilterExpression;
            public readonly bool IgnoreAppliedFilter;

            public GetUniqueColumnValuesCacheKey(CriteriaOperator columnExpression, int maxCount, CriteriaOperator filterExpression, bool ignoreAppliedFilter);
            public override bool Equals(object obj);
            public override int GetHashCode();
        }
    }
}

