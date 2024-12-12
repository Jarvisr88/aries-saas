namespace DevExpress.Data.Filtering
{
    using DevExpress.Xpo.DB;
    using System;

    public interface IQueryCriteriaVisitor : ICriteriaVisitor
    {
        void Visit(QueryOperand theOperand);
        void Visit(QuerySubQueryContainer theOperand);
    }
}

