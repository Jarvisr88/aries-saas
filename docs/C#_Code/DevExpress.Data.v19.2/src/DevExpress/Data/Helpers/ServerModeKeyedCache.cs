namespace DevExpress.Data.Helpers
{
    using DevExpress.Data;
    using DevExpress.Data.Filtering;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Threading;

    public abstract class ServerModeKeyedCache : ServerModeCache, IListServerHints
    {
        public static readonly BooleanSwitch ExplainSkipTake;
        private IByIntDictionary _RowsByIndex;
        private IByIntDictionary _KeysByIndex;
        private Func<object, object> keyFromRowGetter;
        public static int DefaultMagicNumberFetchRowsInSize;
        public static int DefaultMagicNumberFetchRowsAllThreshold;
        public static int DefaultMagicNumberFetchKeysAllThreshold;
        public static int DefaultMagicNumberFetchRowsTopThreshold;
        public static int DefaultMagicNumberFetchRowsTop;
        public static int DefaultMagicNumberFetchKeysModulo;
        public static int DefaultMagicNumberScanKeysBase;
        public static int DefaultMagicNumberTakeKeysUpperLimitAfterSkip;
        public static double DefaultMagicNumberTakeIncreaseStepMultiplier;
        public static bool WebPagingPrefetchNeighbourPage;
        public static bool? FetchRowsIsGood;
        private bool? ActualFetchRowsIsGood;
        public CriteriaOperator IsFetchRowsGoodIdeaForSureHint_FullestPossibleCriteria;
        private static readonly ServerModeServerAndChannelModel SafeModel;
        private bool? UseTakeEnforcer;
        public static bool MinimiseSingleRequestTransferSizeInsteadOfOverallOptimisation;
        public static bool? ForceTake;
        private readonly ServerModeServerAndChannelModelBuilder regressor;
        public static bool SuppressInconsistencyCheckForIncreasedGroupRowCountInFetchRowsAll;
        private static readonly CriteriaOperator FalseMarker;
        private int _hintPageSize;
        private int _hintMaxVisibleRows;

        static ServerModeKeyedCache();
        protected ServerModeKeyedCache(CriteriaOperator[] keyCriteria, ServerModeOrderDescriptor[][] sortInfo, int groupCount, ServerModeSummaryDescriptor[] summary, ServerModeSummaryDescriptor[] totalSummary);
        private ServerModeKeyedCache.SkipTakeParamsSkip CalculateSkipTakeParams(ServerModeCache.ServerModeGroupInfo gi, int index);
        private ServerModeKeyedCache.SkipTakeParamsTake CalculateTakeParams(ServerModeCache.ServerModeGroupInfo gi, bool isFromBottom, int fetchCount);
        public override bool Contains(object value);
        private static bool DecideIsFetchRowsGoodIdea(ServerModeOrderDescriptor[][] order, CriteriaOperator criteria);
        protected virtual bool DecideIsFetchRowsGoodIdeaForSure();
        void IListServerHints.HintGridIsPaged(int pageSize);
        void IListServerHints.HintMaxVisibleRowsInGrid(int rowsInGrid);
        private void DoFetchKeys(ServerModeCache.ServerModeGroupInfo gi, bool isFromBottom, int skip, int take);
        private void DoFetchKeysSkipWithTakeBackup(ServerModeCache.ServerModeGroupInfo gi, bool skipIsFromBottom, int skipSkip, int skipTake, bool pureTakeIsFromBottom, int pureTake);
        [Obsolete("Override and use GetOnInstanceEvaluator instead")]
        protected virtual object EvaluateOnInstance(object row, CriteriaOperator criteriaOperator);
        private object FetchInIndexerCore(int index);
        protected abstract object[] FetchKeys(CriteriaOperator where, ServerModeOrderDescriptor[] order, int skip, int take);
        private object[] FetchKeysTimed(CriteriaOperator where, ServerModeOrderDescriptor[] order, int skip, int take);
        protected abstract object[] FetchRows(CriteriaOperator where, ServerModeOrderDescriptor[] order, int take);
        private void FetchRows(CriteriaOperator where, int take, int validateCount, int firstRecord, bool isFromBottom);
        private void FetchRowsAll(ServerModeCache.ServerModeGroupInfo gi);
        protected virtual object[] FetchRowsByKeys(object[] keys);
        private void FetchRowsTop(ServerModeCache.ServerModeGroupInfo gi, bool isFromBottom, int top);
        private void FillKeys(ServerModeCache.ServerModeGroupInfo gi, bool isFromBottom, int skip, int take, object[] keys);
        private void FillKeysToFetchList(int index, IList<string> inconsistencies, out IList<object> keysToFetch, out IDictionary<object, int> indicesByKeys);
        private void FillKeysToFetchListWeb(int index, IList<string> inconsistencies, out IList<object> keysToFetch, out IDictionary<object, int> indicesByKeys);
        protected sealed override object FindFirstKeyByCriteriaOperator(CriteriaOperator filterCriteria, bool isReversed);
        [IteratorStateMachine(typeof(ServerModeKeyedCache.<FlattenGroups>d__109))]
        private IEnumerable<ServerModeCache.ServerModeGroupInfo> FlattenGroups(IEnumerable<ListSourceGroupInfo> groupsToPrefetch, CancellationToken cancellationToken);
        protected abstract int GetCount(CriteriaOperator criteriaOperator);
        protected CriteriaOperator GetFetchRowsByKeysCondition(object[] keys);
        private static int GetGlobalIndex(ServerModeCache.ServerModeGroupInfo gi, int pos, bool isReversed);
        private ServerModeCache.ServerModeGroupInfo GetGroupForKeysFetchingAround(int index);
        protected virtual Func<object, object> GetKeyComponentFromRowGetter(CriteriaOperator keyComponent);
        protected object GetKeyFromRow(object row);
        private ServerModeOrderDescriptor[] GetOrder(bool isReversed);
        private static ServerModeOrderDescriptor[] GetOrder(ServerModeOrderDescriptor[][] src, bool isReversed);
        public override int GetRowIndexByKey(object key);
        public override object GetRowKey(int index);
        public override object Indexer(int index);
        public override int IndexOf(object value);
        protected bool IsFetchRowsGoodIdeaForSure();
        public bool KeyEquals(object a, object b);
        public bool KeyGroupEquals(object a, object b);
        private static CriteriaOperator MakeEqClause(ServerModeOrderDescriptor od, OperandValue ov);
        private static CriteriaOperator MakeStrongClause(ServerModeOrderDescriptor od, OperandValue ov);
        private void PrefetchRows(ServerModeCache.ServerModeGroupInfo gri);
        public override bool PrefetchRows(IEnumerable<ListSourceGroupInfo> groupsToPrefetch, CancellationToken cancellationToken);
        protected override int PrepareTopGroupCount();
        protected abstract Type ResolveKeyType(CriteriaOperator singleKeyToResolve);
        protected abstract Type ResolveRowType();

        protected IByIntDictionary RowsByIndex { get; }

        protected IByIntDictionary KeysByIndex { get; }

        public virtual IEqualityComparer<object> KeysComparer { get; }

        protected virtual int MaxInSize { get; }

        protected virtual int MagicNumberFetchRowsInSize { get; }

        protected virtual int MagicNumberFetchRowsAllThreshold { get; }

        protected virtual int MagicNumberFetchKeysAllThreshold { get; }

        protected virtual int MagicNumberFetchRowsTopThreshold { get; }

        protected virtual int MagicNumberFetchRowsTop { get; }

        protected virtual int MagicNumberFetchKeysTopPenaltyGap { get; }

        protected virtual int MagicNumberFetchKeysModulo { get; }

        protected virtual int MagicNumberTakeKeysBase { get; }

        protected virtual int MagicNumberScanKeysBase { get; }

        protected virtual int MagicNumberTakeKeysUpperLimitAfterSkip { get; }

        protected virtual double MagicNumberAllowedSlowerThenBase { get; }

        protected virtual double MagicNumberAllowedSlowerThenBaseVariance { get; }

        protected virtual double MagicNumberTakeIncreaseStepMultiplier { get; }

        [Obsolete("Not needed anymore. If you want to force Take, use ForceTake instead. If you want to chop server responses to smallest possible chunks use MinimiseSingleRequestTransferSizeInsteadOfOverallOptimisation instead")]
        public static bool? ForceSkip { get; set; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly ServerModeKeyedCache.<>c <>9;
            public static Func<ServerModeOrderDescriptor[], IEnumerable<ServerModeOrderDescriptor>> <>9__68_0;
            public static Func<string, bool> <>9__68_1;
            public static Func<ServerModeOrderDescriptor[], IEnumerable<ServerModeOrderDescriptor>> <>9__93_0;
            public static Func<ServerModeOrderDescriptor, ServerModeOrderDescriptor> <>9__93_1;
            public static Func<ServerModeOrderDescriptor[], IEnumerable<ServerModeOrderDescriptor>> <>9__93_2;
            public static Func<ServerModeOrderDescriptor[], int, IEnumerable<<>f__AnonymousType2<ServerModeOrderDescriptor, int, int>>> <>9__97_2;
            public static Func<object, OperandValue> <>9__103_0;

            static <>c();
            internal IEnumerable<ServerModeOrderDescriptor> <DecideIsFetchRowsGoodIdea>b__68_0(ServerModeOrderDescriptor[] o);
            internal bool <DecideIsFetchRowsGoodIdea>b__68_1(string op);
            internal OperandValue <GetFetchRowsByKeysCondition>b__103_0(object key);
            internal IEnumerable<ServerModeOrderDescriptor> <GetOrder>b__93_0(ServerModeOrderDescriptor[] descr);
            internal ServerModeOrderDescriptor <GetOrder>b__93_1(ServerModeOrderDescriptor descr);
            internal IEnumerable<ServerModeOrderDescriptor> <GetOrder>b__93_2(ServerModeOrderDescriptor[] descr);
            internal IEnumerable<<>f__AnonymousType2<ServerModeOrderDescriptor, int, int>> <GetRowIndexByKey>b__97_2(ServerModeOrderDescriptor[] sd, int sdIndex);
        }

        [CompilerGenerated]
        private sealed class <FlattenGroups>d__109 : IEnumerable<ServerModeCache.ServerModeGroupInfo>, IEnumerable, IEnumerator<ServerModeCache.ServerModeGroupInfo>, IDisposable, IEnumerator
        {
            private int <>1__state;
            private ServerModeCache.ServerModeGroupInfo <>2__current;
            private int <>l__initialThreadId;
            private IEnumerable<ListSourceGroupInfo> groupsToPrefetch;
            public IEnumerable<ListSourceGroupInfo> <>3__groupsToPrefetch;
            private CancellationToken cancellationToken;
            public CancellationToken <>3__cancellationToken;
            public ServerModeKeyedCache <>4__this;
            private ServerModeCache.ServerModeGroupInfo <sgi>5__1;
            private IEnumerator<ListSourceGroupInfo> <>7__wrap1;
            private IEnumerator<ServerModeCache.ServerModeGroupInfo> <>7__wrap2;

            [DebuggerHidden]
            public <FlattenGroups>d__109(int <>1__state);
            private void <>m__Finally1();
            private void <>m__Finally2();
            private bool MoveNext();
            [DebuggerHidden]
            IEnumerator<ServerModeCache.ServerModeGroupInfo> IEnumerable<ServerModeCache.ServerModeGroupInfo>.GetEnumerator();
            [DebuggerHidden]
            IEnumerator IEnumerable.GetEnumerator();
            [DebuggerHidden]
            void IEnumerator.Reset();
            [DebuggerHidden]
            void IDisposable.Dispose();

            ServerModeCache.ServerModeGroupInfo IEnumerator<ServerModeCache.ServerModeGroupInfo>.Current { [DebuggerHidden] get; }

            object IEnumerator.Current { [DebuggerHidden] get; }
        }

        private class SkipTakeParamsSkip
        {
            public int Skip;
            public int Take;
            public bool IsFromBottom;
        }

        private class SkipTakeParamsTake
        {
            public int Take;
            public bool IsFromBottom;
        }
    }
}

