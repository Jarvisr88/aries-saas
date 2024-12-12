namespace DevExpress.Data.Linq.Helpers
{
    using DevExpress.Data.Filtering;
    using DevExpress.Data.Linq;
    using System;
    using System.Linq.Expressions;

    public class CriteriaToODataExpressionConverterInternal : CriteriaToExpressionConverterInternal
    {
        public CriteriaToODataExpressionConverterInternal(ICriteriaToExpressionConverter owner, ParameterExpression thisExpression);
        protected override Expression VisitInternal(BinaryOperator theOperator);
    }
}

