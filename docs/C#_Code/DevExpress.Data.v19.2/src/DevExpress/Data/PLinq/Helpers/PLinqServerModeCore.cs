namespace DevExpress.Data.PLinq.Helpers
{
    using DevExpress.Data;
    using DevExpress.Data.Filtering;
    using DevExpress.Data.Helpers;
    using DevExpress.Data.Linq;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Threading;

    public class PLinqServerModeCore : IListServer, IList, ICollection, IEnumerable, IDXCloneable
    {
        public string DefaultSorting;
        public static bool? DefaultForceCaseInsensitiveForAnySource;
        public bool ForceCaseInsensitiveForAnySource;
        private CriteriaOperator _filterCriteria;
        private ServerModeOrderDescriptor[] _sortInfo;
        private int _groupCount;
        private ServerModeSummaryDescriptor[] _groupSummaryInfo;
        private ServerModeSummaryDescriptor[] _totalSummaryInfo;
        private IEnumerable source;
        private IList _filtered;
        private IList _sorted;
        private int? degreeOfParallelism;
        private Type ListType;
        private PLinqServerModeCore.PLinqListSourceGroupInfo _RootGroup;
        internal const string OrderDescToken = "DevExpress;MAGIK const;ORDER DESCENDING";

        public event EventHandler<ServerModeExceptionThrownEventArgs> ExceptionThrown;

        public event EventHandler<ServerModeInconsistencyDetectedEventArgs> InconsistencyDetected;

        public PLinqServerModeCore(IEnumerable source, int? degreeOfParallelism = new int?());
        public int Add(object value);
        public void Apply(CriteriaOperator filterCriteria, ICollection<ServerModeOrderDescriptor[]> sortInfo, int groupCount, ICollection<ServerModeSummaryDescriptor> groupSummaryInfo, ICollection<ServerModeSummaryDescriptor> totalSummaryInfo);
        public void Clear();
        public bool Contains(object value);
        private ServerModeSummaryDescriptor[] Convert(ICollection<ServerModeSummaryDescriptor> original);
        private ServerModeOrderDescriptor[] Convert(ICollection<ServerModeOrderDescriptor[]> original_new, int groupCount);
        public void CopyTo(Array array, int index);
        protected virtual PLinqServerModeCore CreateDXClone();
        private PLinqServerModeCore.PLinqListSourceGroupInfo CreateRootGroup();
        object IDXCloneable.DXClone();
        IList IListServer.GetAllFilteredAndSortedRows();
        private IList DoFilter();
        private IList DoSort();
        protected virtual PLinqServerModeCore DXClone();
        public virtual CriteriaOperator ExtractExpression(CriteriaOperator d);
        public static Type ExtractGenericEnumerableType(IEnumerable enumerable);
        private static ServerModeOrderDescriptor ExtractSorting(CriteriaOperator op);
        public int FindIncremental(CriteriaOperator expression, string value, int startIndex, bool searchUp, bool ignoreStartRow, bool allowLoop);
        public IEnumerator GetEnumerator();
        public List<ListSourceGroupInfo> GetGroupInfo(ListSourceGroupInfo parentGroup);
        public int GetRowIndexByKey(object key);
        public object GetRowKey(int index);
        public static ServerModeOrderDescriptor[] GetSortingDescriptors(string sortingsString);
        public List<object> GetTotalSummary();
        public object[] GetUniqueColumnValues(CriteriaOperator valuesExpression, int maxCount, CriteriaOperator filterExpression, bool ignoreAppliedFilter);
        public int IndexOf(object value);
        public void Insert(int index, object value);
        public int LocateByExpression(CriteriaOperator expression, int startIndex, bool searchUp);
        private int LocateByExpressionCore(CriteriaOperator expression, int startIndex, bool searchUp);
        public int LocateByValue(CriteriaOperator expression, object value, int startIndex, bool searchUp);
        public virtual bool PrefetchRows(ListSourceGroupInfo[] groupsToPrefetch, CancellationToken cancellationToken);
        protected virtual void RaiseInconsistencyDetected();
        public void Refresh();
        public void Remove(object value);
        public void RemoveAt(int index);

        public static ICriteriaToExpressionConverter PLinqCriteriaToExpressionConverter { get; }

        protected IList Filtered { get; }

        protected IList Sorted { get; }

        protected PLinqServerModeCore.PLinqListSourceGroupInfo RootGroup { get; }

        protected List<object> TotalSummary { get; }

        public bool IsFixedSize { get; }

        public bool IsReadOnly { get; }

        public object this[int index] { get; set; }

        public int Count { get; }

        public bool IsSynchronized { get; }

        public object SyncRoot { get; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly PLinqServerModeCore.<>c <>9;
            public static Func<ServerModeOrderDescriptor[], bool> <>9__42_0;
            public static Func<ServerModeOrderDescriptor[], ServerModeOrderDescriptor> <>9__42_1;
            public static Predicate<OperandValue> <>9__45_1;
            public static Predicate<FunctionOperator> <>9__45_0;

            static <>c();
            internal bool <Convert>b__42_0(ServerModeOrderDescriptor[] sis);
            internal ServerModeOrderDescriptor <Convert>b__42_1(ServerModeOrderDescriptor[] sis);
            internal bool <ExtractSorting>b__45_0(FunctionOperator fop);
            internal bool <ExtractSorting>b__45_1(OperandValue maybeDescToken);
        }

        protected class PLinqListSourceGroupInfo : ListSourceGroupInfo
        {
            private ServerModeOrderDescriptor[] sortInfo;
            private int groupCount;
            private IEnumerable rows;
            private ServerModeSummaryDescriptor[] totalSummaryInfo;
            private ServerModeSummaryDescriptor[] groupSummaryInfo;
            private int topRowIndex;
            private List<object> _Summary;
            private List<ListSourceGroupInfo> _Children;
            private Type sourceType;
            private int? degreeOfParallelism;

            public PLinqListSourceGroupInfo(ServerModeOrderDescriptor[] sortInfo, int groupCount, IEnumerable rows, int level, ServerModeSummaryDescriptor[] totalSummaryInfo, ServerModeSummaryDescriptor[] groupSummaryInfo, int topRowIndex, object groupValue, Type sourseType, int? degreeOfParallelism);
            private int CalcRowCount();
            private List<ListSourceGroupInfo> CreateChildren();
            private List<object> CreateSummary(IEnumerable rows, ServerModeSummaryDescriptor[] serverModeSummaryDescriptor);
            public List<ListSourceGroupInfo> GetChildren();
            private Delegate GetDelegate(Type iGroupingType);

            public override List<object> Summary { get; }
        }
    }
}

