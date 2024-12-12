namespace DevExpress.Data.Filtering
{
    using System;

    public interface ICustomFunctionOperator
    {
        object Evaluate(params object[] operands);
        Type ResultType(params Type[] operands);

        string Name { get; }
    }
}

