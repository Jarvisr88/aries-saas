namespace DevExpress.Data.Filtering
{
    using System;

    public interface ICustomAggregateBrowsable : ICustomAggregate
    {
        bool IsValidOperandCount(int count);
        bool IsValidOperandType(int operandIndex, int operandCount, Type type);

        int MinOperandCount { get; }

        int MaxOperandCount { get; }

        string Description { get; }
    }
}

