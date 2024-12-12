namespace DevExpress.Data.Filtering
{
    public interface IClientCriteriaVisitor<T> : ICriteriaVisitor<T>
    {
        T Visit(AggregateOperand theOperand);
        T Visit(JoinOperand theOperand);
        T Visit(OperandProperty theOperand);
    }
}

