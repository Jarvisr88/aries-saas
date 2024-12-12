namespace DevExpress.Data.Mask
{
    using System;
    using System.Globalization;
    using System.IO;

    internal class Yylex : yyInput
    {
        private readonly PokeableReader reader;
        private readonly CultureInfo parseCulture;
        private int remembered_token;
        private object remembered_value;
        private bool inBracketExpression;
        private bool inBracketExpressionStart;
        private bool inDupCount;

        public Yylex(TextReader reader, CultureInfo parseCulture);
        public bool advance();
        private static char ReadCharCharCode(PokeableReader reader, int digitsWanted);
        private static string ReadUnicodeCategoryName(PokeableReader reader);
        public int token();
        public object value();
    }
}

