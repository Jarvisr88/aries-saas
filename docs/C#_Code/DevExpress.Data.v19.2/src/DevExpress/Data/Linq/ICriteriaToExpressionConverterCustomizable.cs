namespace DevExpress.Data.Linq
{
    using DevExpress.Data.Filtering;
    using DevExpress.Data.Linq.Helpers;
    using System.Linq.Expressions;

    public interface ICriteriaToExpressionConverterCustomizable
    {
        Expression Convert(ParameterExpression thisExpression, CriteriaOperator op, CriteriaToExpressionConverterEventsHelper eventsHelper);
    }
}

