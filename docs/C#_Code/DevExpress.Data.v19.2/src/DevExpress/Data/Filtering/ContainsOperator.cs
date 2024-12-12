namespace DevExpress.Data.Filtering
{
    using System;

    [Serializable]
    public sealed class ContainsOperator : AggregateOperand
    {
        public ContainsOperator();
        public ContainsOperator(OperandProperty collectionProperty, CriteriaOperator condition);
        public ContainsOperator(string collectionProperty, CriteriaOperator condition);
    }
}

