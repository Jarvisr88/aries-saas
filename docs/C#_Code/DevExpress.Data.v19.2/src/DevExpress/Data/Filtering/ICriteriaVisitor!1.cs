namespace DevExpress.Data.Filtering
{
    public interface ICriteriaVisitor<T>
    {
        T Visit(BetweenOperator theOperator);
        T Visit(BinaryOperator theOperator);
        T Visit(FunctionOperator theOperator);
        T Visit(GroupOperator theOperator);
        T Visit(InOperator theOperator);
        T Visit(OperandValue theOperand);
        T Visit(UnaryOperator theOperator);
    }
}

