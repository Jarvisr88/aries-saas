namespace DevExpress.Data.Filtering
{
    using System;

    [Serializable]
    public sealed class NullOperator : UnaryOperator
    {
        public NullOperator();
        public NullOperator(CriteriaOperator operand);
        public NullOperator(string operand);
    }
}

