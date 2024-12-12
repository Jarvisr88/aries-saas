namespace DevExpress.Data.Filtering.Exceptions
{
    using System;

    public sealed class FilteringExceptionsText
    {
        public const string LexerInvalidInputCharacter = "Invalid input character \"{0}\".";
        public const string LexerNonClosedElement = "Malformed {0}: missing closing \"{1}\".";
        public const string LexerInvalidElement = "Invalid {0} value: \"{1}\".";
        public const string LexerElementPropertyName = "property name";
        public const string LexerElementStringLiteral = "string literal";
        public const string LexerElementDateTimeLiteral = "date/time literal";
        public const string LexerElementDateTimeOrUserTypeLiteral = "date/time or user type literal";
        public const string LexerElementGuidLiteral = "guid literal";
        public const string LexerElementNumberLiteral = "numeric literal";
        public const string LexerElementComment = "comment";
        public const string GrammarCatchAllErrorMessage = "Parser error at line {0}, character {1}: {2}; (\"{3}\")";
        public const string ErrorPointer = "{FAILED HERE}";
        public const string ExpressionEvaluatorOperatorSubtypeNotImplemented = "ICriteriaProcessor.ProcessOperator({0} '{1}') not implemented";
        public const string ExpressionEvaluatorAnalyzePatternInvalidPattern = "Invalid argument '{0}'";
        public const string ExpressionEvaluatorInvalidPropertyPath = "Can't find property '{0}'";
        public const string ExpressionEvaluatorOperatorSubtypeNotSupportedForSpecificOperandType = "{0} {1} not supported for type {2}";
        public const string ExpressionEvaluatorNotACollectionPath = "'{0}' doesn't implement ITypedList";
        public const string ExpressionEvaluatorJoinOperandNotSupported = "JoinOperand not supported";
    }
}

