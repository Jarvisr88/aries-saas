namespace DevExpress.Data.Linq
{
    using DevExpress.Data.Filtering;
    using DevExpress.Data.Linq.Helpers;
    using System;
    using System.Linq.Expressions;

    public class CriteriaToODataExpressionConverter : ICriteriaToExpressionConverter, ICriteriaToExpressionConverterCustomizable
    {
        public Expression Convert(ParameterExpression thisExpression, CriteriaOperator op);
        public Expression Convert(ParameterExpression thisExpression, CriteriaOperator op, CriteriaToExpressionConverterEventsHelper eventsHelper);
    }
}

