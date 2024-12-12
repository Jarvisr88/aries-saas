namespace DevExpress.Data.Filtering
{
    using System;
    using System.ComponentModel;
    using System.Linq.Expressions;

    [DXHelpExclude(true)]
    public interface ICustomFunctionOperatorCompileableWithSettings : ICustomFunctionOperatorCompileable, ICustomFunctionOperator
    {
        Expression Create(CriteriaCompilerAuxSettings settings, params Expression[] operands);
    }
}

