namespace DevExpress.Data.Filtering
{
    using DevExpress.Data.Linq;
    using System;
    using System.Linq.Expressions;

    public interface ICustomAggregateConvertibleToExpression : ICustomAggregate
    {
        Expression Convert(ICriteriaToExpressionConverter converter, Expression collectionProperty, ParameterExpression elementParameter, params Expression[] operands);
    }
}

