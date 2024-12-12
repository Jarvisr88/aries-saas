namespace DevExpress.Data.Linq
{
    using DevExpress.Data.Filtering;
    using DevExpress.Data.Helpers;
    using DevExpress.Data.Linq.Helpers;
    using System;
    using System.Linq;

    public class EntityServerModeCore : LinqServerModeCore
    {
        private ICriteriaToExpressionConverter converter;

        public EntityServerModeCore(IQueryable queryable, CriteriaOperator[] keys);
        protected override ServerModeCache CreateCacheCore();
        protected override ServerModeCore DXCloneCreate();

        protected override ICriteriaToExpressionConverter Converter { get; }
    }
}

