namespace DevExpress.Data.Filtering
{
    using DevExpress.Xpo.DB;

    public interface IQueryCriteriaVisitor<T> : ICriteriaVisitor<T>
    {
        T Visit(QueryOperand theOperand);
        T Visit(QuerySubQueryContainer theOperand);
    }
}

