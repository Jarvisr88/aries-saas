namespace DevExpress.Data.Filtering
{
    using DevExpress.Data.Filtering.Helpers;
    using System;

    internal class DxFtsContainsProcessor : ClientCriteriaLazyPatcherBase.AggregatesAsIsBase
    {
        private readonly CriteriaOperator[] Columns;

        private DxFtsContainsProcessor(params CriteriaOperator[] columns);
        public static CriteriaOperator Convert(CriteriaOperator criteria, CriteriaOperator[] ftsColumns);
        private CriteriaOperator DoContains(CriteriaOperator value);
        private CriteriaOperator DoLike(CriteriaOperator value);
        public override CriteriaOperator Visit(FunctionOperator theOperator);
    }
}

