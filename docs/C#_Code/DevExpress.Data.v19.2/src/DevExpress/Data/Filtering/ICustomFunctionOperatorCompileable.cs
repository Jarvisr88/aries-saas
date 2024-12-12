namespace DevExpress.Data.Filtering
{
    using System;
    using System.Linq.Expressions;

    public interface ICustomFunctionOperatorCompileable : ICustomFunctionOperator
    {
        Expression Create(params Expression[] operands);
    }
}

