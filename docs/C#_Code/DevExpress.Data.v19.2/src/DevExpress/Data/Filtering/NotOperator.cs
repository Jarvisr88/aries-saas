namespace DevExpress.Data.Filtering
{
    using System;

    [Serializable]
    public sealed class NotOperator : UnaryOperator
    {
        public NotOperator();
        public NotOperator(CriteriaOperator operand);
    }
}

