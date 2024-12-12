namespace Devart.Common
{
    using System;

    [Flags]
    public enum LexerBehavior
    {
        OmitBlank = 1,
        OmitComment = 2,
        QuotedString = 4,
        QuotedIdent = 8,
        UpperedIdent = 0x10,
        LoweredIdent = 0x20,
        HandleEscaping = 0x40,
        OmitTokenValue = 0x80,
        IdentDoubleQuote = 0x100,
        StringDoubleQuote = 0x200,
        BreakBlank = 0x400,
        OmitTokenStringValue = 0x800,
        IdentifierHasMinus = 0x1000,
        IdentifierHasFirstDigit = 0x2000,
        AlternativeQuotedString = 0x4000
    }
}

