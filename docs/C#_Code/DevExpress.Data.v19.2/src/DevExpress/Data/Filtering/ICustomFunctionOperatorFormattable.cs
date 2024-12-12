namespace DevExpress.Data.Filtering
{
    using System;

    public interface ICustomFunctionOperatorFormattable : ICustomFunctionOperator
    {
        string Format(Type providerType, params string[] operands);
    }
}

