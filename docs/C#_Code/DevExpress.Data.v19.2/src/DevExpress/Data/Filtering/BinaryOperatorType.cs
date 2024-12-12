namespace DevExpress.Data.Filtering
{
    using System;

    [Serializable]
    public enum BinaryOperatorType
    {
        public const BinaryOperatorType Equal = BinaryOperatorType.Equal;,
        public const BinaryOperatorType NotEqual = BinaryOperatorType.NotEqual;,
        public const BinaryOperatorType Greater = BinaryOperatorType.Greater;,
        public const BinaryOperatorType Less = BinaryOperatorType.Less;,
        public const BinaryOperatorType LessOrEqual = BinaryOperatorType.LessOrEqual;,
        public const BinaryOperatorType GreaterOrEqual = BinaryOperatorType.GreaterOrEqual;,
        [Obsolete("Use StartsWith, EndsWith, Contains functions or Like custom function instead. See https://www.devexpress.com/Support/Center/Question/Details/T313960 for details.")]
        public const BinaryOperatorType Like = BinaryOperatorType.Like;,
        public const BinaryOperatorType BitwiseAnd = BinaryOperatorType.BitwiseAnd;,
        public const BinaryOperatorType BitwiseOr = BinaryOperatorType.BitwiseOr;,
        public const BinaryOperatorType BitwiseXor = BinaryOperatorType.BitwiseXor;,
        public const BinaryOperatorType Divide = BinaryOperatorType.Divide;,
        public const BinaryOperatorType Modulo = BinaryOperatorType.Modulo;,
        public const BinaryOperatorType Multiply = BinaryOperatorType.Multiply;,
        public const BinaryOperatorType Plus = BinaryOperatorType.Plus;,
        public const BinaryOperatorType Minus = BinaryOperatorType.Minus;
    }
}

