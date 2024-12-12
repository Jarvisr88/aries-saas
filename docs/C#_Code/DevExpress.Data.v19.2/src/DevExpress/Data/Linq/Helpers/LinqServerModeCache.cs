namespace DevExpress.Data.Linq.Helpers
{
    using DevExpress.Data;
    using DevExpress.Data.Filtering;
    using DevExpress.Data.Helpers;
    using DevExpress.Data.Linq;
    using System;
    using System.Linq;
    using System.Runtime.CompilerServices;

    public class LinqServerModeCache : ServerModeKeyedCache
    {
        public readonly IQueryable Q;
        private ICriteriaToExpressionConverter converter;

        public LinqServerModeCache(IQueryable q, CriteriaOperator[] keyCriteria, ServerModeOrderDescriptor[][] sortInfo, int groupCount, ServerModeSummaryDescriptor[] summary, ServerModeSummaryDescriptor[] totalSummary);
        protected override object[] FetchKeys(CriteriaOperator where, ServerModeOrderDescriptor[] order, int skip, int take);
        internal static object[] FetchKeysStatic(IQueryable q, ICriteriaToExpressionConverter converter, CriteriaOperator[] keysCriteria, CriteriaOperator where, ServerModeOrderDescriptor[] order, int skip, int take);
        protected override object[] FetchRows(CriteriaOperator where, ServerModeOrderDescriptor[] order, int take);
        protected override int GetCount(CriteriaOperator criteriaOperator);
        internal static int GetCountStatic(IQueryable q, ICriteriaToExpressionConverter converter, CriteriaOperator criteriaOperator);
        protected override Func<object, object> GetOnInstanceEvaluator(CriteriaOperator subj);
        protected override ServerModeGroupInfoData[] PrepareChildren(CriteriaOperator groupWhere, CriteriaOperator groupByCriterion, CriteriaOperator orderByCriterion, bool isDesc, ServerModeSummaryDescriptor[] summaries);
        internal static ServerModeGroupInfoData[] PrepareChildrenStatic(IQueryable q, ICriteriaToExpressionConverter converter, CriteriaOperator groupWhere, ServerModeOrderDescriptor groupByDescriptor, ServerModeSummaryDescriptor[] summaries);
        protected override ServerModeGroupInfoData PrepareTopGroupInfo(ServerModeSummaryDescriptor[] summaries);
        internal static ServerModeGroupInfoData PrepareTopGroupInfoStatic(IQueryable q, ICriteriaToExpressionConverter converter, CriteriaOperator where, ServerModeSummaryDescriptor[] summaries);
        protected override Type ResolveKeyType(CriteriaOperator singleKeyToResolve);
        protected override Type ResolveRowType();
        public static bool StaticKeyEquals(object a, object b);

        protected virtual ICriteriaToExpressionConverter Converter { get; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly LinqServerModeCache.<>c <>9;
            public static Func<object[], object> <>9__12_0;
            public static Func<object[], ServerModeCompoundKey> <>9__12_1;

            static <>c();
            internal object <FetchKeysStatic>b__12_0(object[] oa);
            internal ServerModeCompoundKey <FetchKeysStatic>b__12_1(object[] oa);
        }
    }
}

