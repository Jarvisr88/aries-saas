namespace DevExpress.Data.Helpers
{
    using DevExpress.Data;
    using DevExpress.Data.Filtering;
    using DevExpress.Data.Linq;
    using System;
    using System.Linq;
    using System.Runtime.CompilerServices;

    public class ServerModeCoreExtender
    {
        private static readonly ICriteriaToExpressionConverter Converter;

        public event EventHandler<CustomFetchKeysEventArgs> CustomFetchKeys;

        public event EventHandler<CustomGetCountEventArgs> CustomGetCount;

        public event EventHandler<CustomGetUniqueValuesEventArgs> CustomGetUniqueValues;

        public event EventHandler<CustomPrepareChildrenEventArgs> CustomPrepareChildren;

        public event EventHandler<CustomPrepareTopGroupInfoEventArgs> CustomPrepareTopGroupInfo;

        static ServerModeCoreExtender();
        public static object[] FetchKeys(IQueryable q, CriteriaOperator[] keysCriteria, CriteriaOperator where, ServerModeOrderDescriptor[] order, int skip, int take);
        public static object[] FetchKeys(IQueryable q, ICriteriaToExpressionConverter converter, CriteriaOperator[] keysCriteria, CriteriaOperator where, ServerModeOrderDescriptor[] order, int skip, int take);
        public static int GetCount(IQueryable q, CriteriaOperator where);
        public static int GetCount(IQueryable q, ICriteriaToExpressionConverter converter, CriteriaOperator where);
        public static object[] GetUniqueValues(IQueryable q, CriteriaOperator expression, int maxCount, CriteriaOperator where);
        public static object[] GetUniqueValues(IQueryable q, ICriteriaToExpressionConverter converter, CriteriaOperator expression, int maxCount, CriteriaOperator where);
        private void OnCustomFetchKeys(object sender, CustomFetchKeysEventArgs e);
        private void OnCustomGetCount(object sender, CustomGetCountEventArgs e);
        private void OnCustomGetUniqueValues(object sender, CustomGetUniqueValuesEventArgs e);
        private void OnCustomPrepareChildren(object sender, CustomPrepareChildrenEventArgs e);
        protected virtual void OnCustomPrepareTopGroupInfo(object sender, CustomPrepareTopGroupInfoEventArgs e);
        public static ServerModeGroupInfoData[] PrepareChildren(IQueryable q, CriteriaOperator groupWhere, ServerModeOrderDescriptor groupByDescriptor, ServerModeSummaryDescriptor[] summaries);
        public static ServerModeGroupInfoData[] PrepareChildren(IQueryable q, ICriteriaToExpressionConverter converter, CriteriaOperator groupWhere, ServerModeOrderDescriptor groupByDescriptor, ServerModeSummaryDescriptor[] summaries);
        public static ServerModeGroupInfoData PrepareTopGroupInfo(IQueryable q, CriteriaOperator where, ServerModeSummaryDescriptor[] summaries);
        public static ServerModeGroupInfoData PrepareTopGroupInfo(IQueryable q, ICriteriaToExpressionConverter converter, CriteriaOperator where, ServerModeSummaryDescriptor[] summaries);
        internal void Subscribe(ServerModeCoreExtendable serverModeCore);
        internal void Subscribe(ServerModeKeyedCacheExtendable serverModeCache);
    }
}

