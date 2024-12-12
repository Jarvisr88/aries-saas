namespace DevExpress.Data.Filtering
{
    using DevExpress.Data.Linq;
    using System;
    using System.Linq.Expressions;

    public interface ICustomFunctionOperatorConvertibleToExpression : ICustomFunctionOperator
    {
        Expression Convert(ICriteriaToExpressionConverter converter, params Expression[] operands);
    }
}

