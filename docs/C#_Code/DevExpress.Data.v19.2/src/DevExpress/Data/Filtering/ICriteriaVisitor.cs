namespace DevExpress.Data.Filtering
{
    using System;

    public interface ICriteriaVisitor
    {
        void Visit(BetweenOperator theOperator);
        void Visit(BinaryOperator theOperator);
        void Visit(FunctionOperator theOperator);
        void Visit(GroupOperator theOperator);
        void Visit(InOperator theOperator);
        void Visit(OperandValue theOperand);
        void Visit(UnaryOperator theOperator);
    }
}

