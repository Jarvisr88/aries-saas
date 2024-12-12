namespace DevExpress.Data.Mask
{
    using System;

    public class MaskExceptionsTexts
    {
        public const string IncorrectMaskNonclosedQuotaWithMaskPattern = "Incorrect mask ({0}): closing quote expected";
        public const string IncorrectMaskBackslashBeforeEndOfMaskWithMaskPattern = @"Incorrect mask ({0}): character expected after '\'";
        public const string IncorrectMaskBackslashBeforeEndOfMask = @"Incorrect mask: character expected after '\'";
        public const string IncorrectMaskUnknownNamedMask = "IncorrectMask: unknown named mask '{0}'";
        public const string IncorrectNumericMaskSignedMaskNotMatchMaxDigitsBeforeDecimalSeparator = "Incorrect mask: the max number of digits before the decimal separator in the positive and negative patterns must match";
        public const string IncorrectNumericMaskSignedMaskNotMatchMaxDigitsAfterDecimalSeparator = "Incorrect mask: the max number of digits after the decimal separator in the positive and negative patterns must match";
        public const string IncorrectNumericMaskSignedMaskNotMatchMinDigitsBeforeDecimalSeparator = "Incorrect mask: the min number of digits before the decimal separator in the positive and negative patterns must match";
        public const string IncorrectNumericMaskSignedMaskNotMatchMinDigitsAfterDecimalSeparator = "Incorrect mask: the min number of digits after the decimal separator in the positive and negative patterns must match";
        public const string IncorrectNumericMaskSignedMaskNotMatchIs100Multiplied = "Incorrect mask: the percent type (% or %%) in the positive and negative patterns must match";
        public const string IncorrectMaskInvalidUnicodeCategory = "Incorrect mask: unsupported unicode category '{0}'";
        public const string IncorrectMaskCurveBracketAfterPpExpected = @"Incorrect mask: '{' expected after '\p' or '\P'";
        public const string IncorrectMaskClosingBracketAfterPpExpected = @"Incorrect mask: '}' expected after '\p{unicode_category_name' or '\P{unicode_category_name'";
        public const string IncorrectMaskBackslashRBeforeEndOfMask = @"Incorrect mask: character expected after '\R'";
        public const string IncorrectMaskInvalidCharAfterBackslashR = @"Incorrect mask: only '.', ':', '/' and '{' are allowed after '\R'";
        public const string IncorrectMaskClosingBracketAfterRExpected = @"Incorrect mask: '}' expected after '\R{pattern_name'";
        public const string IncorrectMaskClosingSquareBracketExpected = "Incorrect mask: ']' expected after '['";
        public const string IncorrectMaskClosingCurveBracketExpected = "Incorrect mask: '}' expected after '{'";
        public const string IncorrectMaskInvalidQuantifierFormat = "Incorrect mask: invalid quantifier format";
        public const string CreateManagerReturnsNull = "The manager cannot be created for the mask {1} of {0} type. Specify another type of mask or implement the TextEdit.CreateManager method.";
        public const string InternalErrorNonCoveredCase = "Internal error: non-covered case '{0}'";
        public const string InternalErrorGetSampleCharForEmpty = "Internal error: GetSampleChar() called for empty transition";
        public const string InternalErrorNonSpecific = "Internal error";
    }
}

