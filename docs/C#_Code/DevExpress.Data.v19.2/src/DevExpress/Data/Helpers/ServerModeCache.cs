namespace DevExpress.Data.Helpers
{
    using DevExpress.Data;
    using DevExpress.Data.Filtering;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;
    using System.Threading;

    public abstract class ServerModeCache
    {
        public readonly CriteriaOperator[] KeysCriteria;
        public readonly ServerModeOrderDescriptor[][] SortDescription;
        public readonly int GroupByCount;
        public readonly ServerModeSummaryDescriptor[] SummaryInfo;
        public readonly ServerModeSummaryDescriptor[] TotalSummaryInfo;
        public readonly Dictionary<object, object> SomethingCache;
        private bool _landed;
        private bool _DeathProof;
        private ServerModeCache.ServerModeGroupInfo _topGroupInfo;
        private static bool dontThrowNotImplementedAnymore;
        public static bool DefaultForceStaSafeForReentryProtected;
        private ServerModeCache.ReentrancyAndThreadsWatch _ReenterancyUndThreadsWatch;
        private Func<object, object>[][] _GetSortExpressionValueFromRowCache;
        private static readonly CriteriaOperator FalseMarker;

        public event EventHandler<ServerModeExceptionThrownEventArgs> ExceptionThrown;

        public event EventHandler<ServerModeInconsistencyDetectedEventArgs> InconsistencyDetected;

        static ServerModeCache();
        protected ServerModeCache(CriteriaOperator[] keyCriteria, ServerModeOrderDescriptor[][] sortInfo, int groupCount, ServerModeSummaryDescriptor[] summary, ServerModeSummaryDescriptor[] totalSummary);
        internal Func<object, object> _GetOnInstanceEvaluator(CriteriaOperator toEvaluate);
        public void CanResetCache();
        private bool CanTrickCreateTopGroupFromNextGroups();
        private static void CloneFromOldCache(ServerModeCache.ServerModeGroupInfo newGroupInfo, ServerModeCache.ServerModeGroupInfo oldGroupInfo, ServerModeOrderDescriptor[][] newDescriptors, ServerModeOrderDescriptor[][] oldDescriptors, int currentDepth, int maxCloneDepth);
        public abstract bool Contains(object value);
        public int Count();
        private ServerModeCache.ServerModeGroupInfo CreateTopGroupInfo();
        protected virtual void Fatal(Exception e);
        private void FillChildrenIfNeeded(ServerModeCache.ServerModeGroupInfo myGroupInfo);
        internal void FillFromOldCacheWhateverMakesSence(ServerModeCache oldCache);
        protected abstract object FindFirstKeyByCriteriaOperator(CriteriaOperator filterCriteria, bool isReversed);
        public int FindIncremental(CriteriaOperator columnExpression, string value, int startIndex, bool searchUp, bool ignoreStartRow, Func<object, string> stringValueExtractor, ref bool? useToLower);
        public List<ListSourceGroupInfo> GetGroupInfo(ListSourceGroupInfo parentGroup);
        protected CriteriaOperator GetGroupWhere(ServerModeCache.ServerModeGroupInfo myGroupInfo);
        protected abstract Func<object, object> GetOnInstanceEvaluator(CriteriaOperator toEvaluate);
        public abstract int GetRowIndexByKey(object key);
        public abstract object GetRowKey(int index);
        protected object GetSortExpressionValueFromRow(object row, int sortIndex, int sortSubIndex);
        public List<object> GetTotalSummary();
        public abstract object Indexer(int index);
        public abstract int IndexOf(object value);
        public static bool IsNothingButCount(ServerModeSummaryDescriptor[] summaries);
        private CriteriaOperator LimitSearchByRow(object startRow, bool searchUp, bool ignoreStartRow);
        public int LocateByExpressionCore(CriteriaOperator extractedExpression, int startIndex, bool searchUp);
        private void PerformDeathProofAction(Action action);
        public abstract bool PrefetchRows(IEnumerable<ListSourceGroupInfo> groupsToPrefetch, CancellationToken cancellationToken);
        protected virtual ServerModeGroupInfoData[] PrepareChildren(CriteriaOperator groupWhere, CriteriaOperator[] groupByCriteria, CriteriaOperator[] orderByCriteria, bool[] isDescOrder, ServerModeSummaryDescriptor[] summaries);
        protected virtual ServerModeGroupInfoData[] PrepareChildren(CriteriaOperator groupWhere, CriteriaOperator groupByCriterion, CriteriaOperator orderByCriterion, bool isDesc, ServerModeSummaryDescriptor[] summaries);
        [IteratorStateMachine(typeof(ServerModeCache.<PrepareChildrenFallback>d__38))]
        private IEnumerable<ServerModeGroupInfoData> PrepareChildrenFallback(int currentLvlIndex, CriteriaOperator currentGroupWhere, CriteriaOperator[] groupByCriteria, CriteriaOperator[] orderByCriteria, bool[] isDescOrder, ServerModeSummaryDescriptor[] summaries);
        protected virtual int PrepareTopGroupCount();
        protected abstract ServerModeGroupInfoData PrepareTopGroupInfo(ServerModeSummaryDescriptor[] summaries);
        private ServerModeGroupInfoData PrepareTopGroupInfoWithTrick(ServerModeSummaryDescriptor[] summaries);
        protected void RaiseInconsistencyDetected(IEnumerable<string> messages);
        protected void RaiseInconsistencyDetected(string message);
        protected void RaiseInconsistencyDetected(string format, params object[] args);
        internal static int ValidateIncrementalSearchRowIndex(int rowFound, int startRow, bool searchUp);
        protected internal T WithReentryProtection<T>(Func<T> action);

        protected virtual bool IsLanded { get; }

        protected ServerModeCache.ServerModeGroupInfo TotalGroupInfo { get; }

        protected virtual bool ForceStaSafeForReentryProtected { get; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly ServerModeCache.<>c <>9;
            public static Func<object> <>9__17_0;
            public static Func<ServerModeGroupInfoData, object> <>9__35_3;
            public static Func<object, int, object> <>9__46_0;
            public static Func<object, int, object> <>9__46_1;

            static <>c();
            internal object <CanResetCache>b__17_0();
            internal object <FillChildrenIfNeeded>b__35_3(ServerModeGroupInfoData info);
            internal object <GetGroupWhere>b__46_0(object o, int i);
            internal object <GetGroupWhere>b__46_1(object o, int i);
        }

        [CompilerGenerated]
        private sealed class <PrepareChildrenFallback>d__38 : IEnumerable<ServerModeGroupInfoData>, IEnumerable, IEnumerator<ServerModeGroupInfoData>, IDisposable, IEnumerator
        {
            private int <>1__state;
            private ServerModeGroupInfoData <>2__current;
            private int <>l__initialThreadId;
            private int currentLvlIndex;
            public int <>3__currentLvlIndex;
            private CriteriaOperator[] groupByCriteria;
            public CriteriaOperator[] <>3__groupByCriteria;
            public ServerModeCache <>4__this;
            private CriteriaOperator currentGroupWhere;
            public CriteriaOperator <>3__currentGroupWhere;
            private CriteriaOperator[] orderByCriteria;
            public CriteriaOperator[] <>3__orderByCriteria;
            private bool[] isDescOrder;
            public bool[] <>3__isDescOrder;
            private ServerModeSummaryDescriptor[] summaries;
            public ServerModeSummaryDescriptor[] <>3__summaries;
            private int <valuesLength>5__1;
            private ServerModeGroupInfoData <cg>5__2;
            private ServerModeGroupInfoData[] <>7__wrap1;
            private int <>7__wrap2;
            private IEnumerator<ServerModeGroupInfoData> <>7__wrap3;

            [DebuggerHidden]
            public <PrepareChildrenFallback>d__38(int <>1__state);
            private void <>m__Finally1();
            private bool MoveNext();
            [DebuggerHidden]
            IEnumerator<ServerModeGroupInfoData> IEnumerable<ServerModeGroupInfoData>.GetEnumerator();
            [DebuggerHidden]
            IEnumerator IEnumerable.GetEnumerator();
            [DebuggerHidden]
            void IEnumerator.Reset();
            [DebuggerHidden]
            void IDisposable.Dispose();

            ServerModeGroupInfoData IEnumerator<ServerModeGroupInfoData>.Current { [DebuggerHidden] get; }

            object IEnumerator.Current { [DebuggerHidden] get; }
        }

        private class ReentrancyAndThreadsWatch
        {
            private readonly ServerModeCache Watched;
            private bool dontThrow;

            public ReentrancyAndThreadsWatch(ServerModeCache watched);
            public void ExceptionAlreadyThrown();
            public void Exit();
            private void Throw();
        }

        private protected class ServerModeGroupInfo : ListSourceGroupInfo
        {
            private List<object> _Summary;
            public List<ListSourceGroupInfo> ChildrenGroups;
            public readonly ServerModeCache.ServerModeGroupInfo Parent;
            internal readonly object OwnershipMarker;
            public int TopRecordIndex;

            public ServerModeGroupInfo(ServerModeCache.ServerModeGroupInfo parent, object ownershipMarker);

            public override List<object> Summary { get; }
        }
    }
}

