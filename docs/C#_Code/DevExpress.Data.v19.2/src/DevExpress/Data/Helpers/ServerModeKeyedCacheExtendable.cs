namespace DevExpress.Data.Helpers
{
    using DevExpress.Data;
    using DevExpress.Data.Filtering;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    public abstract class ServerModeKeyedCacheExtendable : ServerModeKeyedCache
    {
        private int? fetchRowsSize;
        private readonly Dictionary<object, object> keyObjectCache;
        private int maxInSize;

        public event EventHandler<CustomFetchKeysEventArgs> CustomFetchKeys;

        public event EventHandler<CustomGetCountEventArgs> CustomGetCount;

        public event EventHandler<CustomPrepareChildrenEventArgs> CustomPrepareChildren;

        public event EventHandler<CustomPrepareTopGroupInfoEventArgs> CustomPrepareTopGroupInfo;

        protected ServerModeKeyedCacheExtendable(CriteriaOperator[] keysCriteria, ServerModeOrderDescriptor[][] sortInfo, int groupCount, ServerModeSummaryDescriptor[] summary, ServerModeSummaryDescriptor[] totalSummary);
        protected sealed override object[] FetchKeys(CriteriaOperator where, ServerModeOrderDescriptor[] order, int skip, int take);
        protected abstract void FetchKeysCore(object source, int skip, int take, out IEnumerable keys, out IEnumerable rows);
        protected abstract object FetchPrepare(CriteriaOperator where, ServerModeOrderDescriptor[] order);
        protected sealed override object[] FetchRows(CriteriaOperator where, ServerModeOrderDescriptor[] order, int take);
        protected sealed override object[] FetchRowsByKeys(object[] keys);
        protected abstract IEnumerable FetchRowsByKeysCore(object[] keys);
        protected abstract IEnumerable FetchRowsCore(object source, int skip, int take);
        protected sealed override int GetCount(CriteriaOperator criteriaOperator);
        protected abstract int GetCountInternal(CriteriaOperator criteriaOperator);
        private int GetCurrentTake(int take, int position);
        private static bool NeedBreak(int currentTake, int currentCounter);
        protected sealed override ServerModeGroupInfoData[] PrepareChildren(CriteriaOperator groupWhere, CriteriaOperator groupByCriterion, CriteriaOperator orderByCriterion, bool isDesc, ServerModeSummaryDescriptor[] summaries);
        protected abstract ServerModeGroupInfoData[] PrepareChildrenInternal(CriteriaOperator groupWhere, ServerModeOrderDescriptor groupByDescriptor, ServerModeSummaryDescriptor[] summaries);
        protected sealed override ServerModeGroupInfoData PrepareTopGroupInfo(ServerModeSummaryDescriptor[] summaries);
        protected abstract ServerModeGroupInfoData PrepareTopGroupInfoInternal(ServerModeSummaryDescriptor[] summaries);
        private bool ProcessException(int take);

        protected virtual CriteriaOperator ExternalCriteria { get; }

        protected override int MagicNumberTakeKeysUpperLimitAfterSkip { get; }

        protected override int MaxInSize { get; }

        protected override bool ForceStaSafeForReentryProtected { get; }
    }
}

