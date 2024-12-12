namespace DevExpress.Data.Filtering
{
    using System;

    public interface ICustomFunctionOperatorBrowsable : ICustomFunctionOperator
    {
        bool IsValidOperandCount(int count);
        bool IsValidOperandType(int operandIndex, int operandCount, Type type);

        int MinOperandCount { get; }

        int MaxOperandCount { get; }

        string Description { get; }

        FunctionCategory Category { get; }
    }
}

