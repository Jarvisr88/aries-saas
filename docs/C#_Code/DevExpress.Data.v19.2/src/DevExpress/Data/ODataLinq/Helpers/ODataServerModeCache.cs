﻿namespace DevExpress.Data.ODataLinq.Helpers
{
    using DevExpress.Data;
    using DevExpress.Data.Filtering;
    using DevExpress.Data.Helpers;
    using System;
    using System.Collections;
    using System.Linq;
    using System.Runtime.InteropServices;

    public class ODataServerModeCache : ServerModeKeyedCacheExtendable
    {
        private readonly IQueryable source;
        private readonly CriteriaOperator externalCriteria;

        public ODataServerModeCache(IQueryable source, string selectExpression, CriteriaOperator filterCriteria, CriteriaOperator[] keysCriteria, ServerModeOrderDescriptor[][] sortInfo, int groupCount, ServerModeSummaryDescriptor[] summary, ServerModeSummaryDescriptor[] totalSummary);
        protected override void FetchKeysCore(object source, int skip, int take, out IEnumerable keys, out IEnumerable rows);
        protected override object FetchPrepare(CriteriaOperator where, ServerModeOrderDescriptor[] order);
        protected override IEnumerable FetchRowsByKeysCore(object[] keys);
        protected override IEnumerable FetchRowsCore(object source, int skip, int take);
        protected override int GetCountInternal(CriteriaOperator criteriaOperator);
        protected override Func<object, object> GetOnInstanceEvaluator(CriteriaOperator subj);
        protected override ServerModeGroupInfoData[] PrepareChildrenInternal(CriteriaOperator groupWhere, ServerModeOrderDescriptor groupByDescriptor, ServerModeSummaryDescriptor[] summaries);
        protected override ServerModeGroupInfoData PrepareTopGroupInfoInternal(ServerModeSummaryDescriptor[] summaries);
        protected override Type ResolveKeyType(CriteriaOperator singleKeyCritterion);
        protected override Type ResolveRowType();

        protected override CriteriaOperator ExternalCriteria { get; }

        protected override int MagicNumberTakeKeysUpperLimitAfterSkip { get; }
    }
}

