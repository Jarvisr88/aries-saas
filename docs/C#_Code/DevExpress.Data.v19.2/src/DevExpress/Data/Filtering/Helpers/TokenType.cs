namespace DevExpress.Data.Filtering.Helpers
{
    using System;

    public enum TokenType
    {
        public const TokenType Property = TokenType.Property;,
        public const TokenType Constant = TokenType.Constant;,
        public const TokenType Group = TokenType.Group;,
        public const TokenType Aggregate = TokenType.Aggregate;,
        public const TokenType CompareOperator = TokenType.CompareOperator;,
        public const TokenType MathOperator = TokenType.MathOperator;,
        public const TokenType Function = TokenType.Function;,
        public const TokenType Predicate = TokenType.Predicate;,
        public const TokenType Not = TokenType.Not;,
        public const TokenType OpenParenthesis = TokenType.OpenParenthesis;,
        public const TokenType CloseParenthesis = TokenType.CloseParenthesis;,
        public const TokenType Dot = TokenType.Dot;,
        public const TokenType Unknown = TokenType.Unknown;
    }
}

