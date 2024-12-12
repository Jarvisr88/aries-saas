namespace DevExpress.Data.Filtering
{
    using System;

    [Serializable]
    public enum UnaryOperatorType
    {
        public const UnaryOperatorType BitwiseNot = UnaryOperatorType.BitwiseNot;,
        public const UnaryOperatorType Plus = UnaryOperatorType.Plus;,
        public const UnaryOperatorType Minus = UnaryOperatorType.Minus;,
        public const UnaryOperatorType Not = UnaryOperatorType.Not;,
        public const UnaryOperatorType IsNull = UnaryOperatorType.IsNull;
    }
}

