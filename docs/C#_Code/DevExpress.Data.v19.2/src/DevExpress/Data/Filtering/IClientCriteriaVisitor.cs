namespace DevExpress.Data.Filtering
{
    using System;

    public interface IClientCriteriaVisitor : ICriteriaVisitor
    {
        void Visit(AggregateOperand theOperand);
        void Visit(JoinOperand theOperand);
        void Visit(OperandProperty theOperand);
    }
}

