namespace DevExpress.Data.Filtering.Helpers
{
    using DevExpress.Data.Filtering;
    using System;
    using System.ComponentModel;

    [DXHelpExclude(true)]
    internal interface ICustomFunctionOperatorEvaluatableWithCaseSensitivityAnd3ValuedLogic : ICustomFunctionOperatorEvaluatableWithCaseSensitivity, ICustomFunctionOperator
    {
        object Evaluate(bool caseSensitive, bool is3ValuedLogic, params object[] operands);
    }
}

