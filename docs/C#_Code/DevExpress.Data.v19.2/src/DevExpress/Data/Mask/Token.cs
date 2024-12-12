namespace DevExpress.Data.Mask
{
    using System;

    internal class Token
    {
        public const int CHAR = 0x101;
        public const int DIGIT = 0x102;
        public const int CHARClassDecimalDigit = 0x103;
        public const int CHARClassNonDecimalDigit = 260;
        public const int CHARClassWhiteSpace = 0x105;
        public const int CHARClassNonWhiteSpace = 0x106;
        public const int CHARClassWord = 0x107;
        public const int CHARClassNonWord = 0x108;
        public const int CHARClassUnicodeCategory = 0x109;
        public const int CHARClassUnicodeCategoryNot = 0x10a;
        public const int yyErrorCode = 0x100;
    }
}

