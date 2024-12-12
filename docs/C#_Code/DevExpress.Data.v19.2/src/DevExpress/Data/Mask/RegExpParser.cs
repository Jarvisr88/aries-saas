namespace DevExpress.Data.Mask
{
    using System;
    using System.Globalization;
    using System.IO;

    internal class RegExpParser
    {
        private Dfa result;
        private int yyMax;
        private static short[] yyLhs;
        private static short[] yyLen;
        private static short[] yyDefRed;
        protected static short[] yyDgoto;
        protected static int yyFinal;
        protected static short[] yySindex;
        protected static short[] yyRindex;
        protected static short[] yyGindex;
        protected static short[] yyTable;
        protected static short[] yyCheck;
        private Yylex lexer;
        private bool reverseAutomate;

        static RegExpParser();
        private Dfa Parse(TextReader reader, bool reverseAutomate, CultureInfo parseCulture);
        public static Dfa Parse(string regExp, bool reverseAutomate, CultureInfo parseCulture);
        private void yyerror(string message);
        private void yyerror(string message, string[] expected);
        private object yyparse(yyInput yyLex);

        public Dfa Result { get; }
    }
}

