namespace DevExpress.Data.Linq
{
    using DevExpress.Data;
    using DevExpress.Data.Filtering;
    using DevExpress.Data.Linq.Helpers;
    using System;
    using System.Linq;

    public class EntityServerModeCache : LinqServerModeCache
    {
        private ICriteriaToExpressionConverter converter;

        public EntityServerModeCache(IQueryable q, CriteriaOperator[] keysCriteria, ServerModeOrderDescriptor[][] sortInfo, int groupCount, ServerModeSummaryDescriptor[] summary, ServerModeSummaryDescriptor[] totalSummary);

        protected override ICriteriaToExpressionConverter Converter { get; }
    }
}

