namespace DevExpress.Data.WcfLinq.Helpers
{
    using DevExpress.Data.Filtering;
    using DevExpress.Data.Filtering.Helpers;
    using System;

    public class CriteriaForFilter : ClientCriteriaVisitorBase
    {
        private int inIsNull;

        private CriteriaOperator CheckGetDateFunction(BinaryOperatorType originalOperatorType, CriteriaOperator leftResult, CriteriaOperator rightResult);
        public static CriteriaOperator Prepare(CriteriaOperator criteria);
        private BinaryOperatorType RotateBinaryOperatorTypeForDateTime(BinaryOperatorType originalOperatorType);
        protected override CriteriaOperator Visit(BinaryOperator theOperator);
        protected override CriteriaOperator Visit(FunctionOperator theOperator);
        protected override CriteriaOperator Visit(UnaryOperator theOperator);
    }
}

