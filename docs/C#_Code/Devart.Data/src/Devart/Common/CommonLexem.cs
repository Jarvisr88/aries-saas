namespace Devart.Common
{
    using System;
    using System.Collections;

    public class CommonLexem
    {
        public const int StringQuote = 0x2711;
        public const int IdentQuote = 0x2712;
        public const int IdentQuoteBegin = 0x2713;
        public const int IdentQuoteEnd = 0x2714;
        public const int InlineComment = 0x2715;
        public const int CommentBegin = 0x2716;
        public const int CommentEnd = 0x2717;
        public const int IdentPrefix = 0x2718;
        public const int CommentExtBegin = 0x2719;
        public static Hashtable symbols = new Hashtable();
        public static Hashtable keywords = new Hashtable();
    }
}

