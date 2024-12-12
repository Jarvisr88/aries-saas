namespace DevExpress.Data.Filtering
{
    using System;
    using System.Linq.Expressions;

    public interface ICustomFunctionOperatorCompileableWithCaseSensitivity : ICustomFunctionOperatorCompileable, ICustomFunctionOperator
    {
        Expression Create(bool caseSensitive, params Expression[] operands);
    }
}

