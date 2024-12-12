namespace DevExpress.Data.Filtering
{
    using System;

    public interface ICustomFunctionOperatorEvaluatableWithCaseSensitivity : ICustomFunctionOperator
    {
        object Evaluate(bool caseSensitive, params object[] operands);
    }
}

