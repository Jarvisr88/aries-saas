namespace DevExpress.Data.Filtering.Helpers
{
    using DevExpress.Data.Filtering;
    using System;
    using System.IO;
    using System.Runtime.InteropServices;
    using System.Text;

    public class CriteriaLexer : yyInput
    {
        private readonly TextReader InputReader;
        public int CurrentToken;
        public object CurrentValue;
        private bool isAfterColumn;
        private int _line;
        private int _col;
        private int _currentLine;
        private int _currentCol;
        private int _pos;
        private int _currentTokenPos;
        private char wasChar;
        private bool preread;
        private int valuepreread;
        internal bool RecogniseSortings;
        public static bool ParseVeryWideIntegerToDoubleInsteadOfDecimalCompatibilityFallback;
        private StringBuilder _stringBuilder;

        public CriteriaLexer(TextReader inputReader);
        public bool Advance();
        public static bool CanContinueColumn(char value);
        public static bool CanStartColumn(char value);
        private void CatchAll(char firstChar);
        bool yyInput.advance();
        int yyInput.token();
        object yyInput.value();
        private void DoAtColumn();
        private void DoConstGuid();
        private void DoDateTimeConst();
        private void DoDotOrNumber();
        private void DoEnclosedColumn();
        private string DoneWithStringBuilder(StringBuilder sb);
        private void DoNumber(char firstSymbol);
        private void DoParam();
        private void DoString();
        private void DoUserObject();
        private object ExtractNumericValue(string str, string numericCode, out int token);
        protected virtual object ExtractUserValue(string tag, string data);
        protected virtual void ExtractUserValueLastChance(UserValueProcessingEventArgs e);
        private string GetNumericCode();
        private StringBuilder GetStringBuilder();
        public static bool IsGoodUnescapedName(string fnName);
        protected int PeekNext2Char();
        protected int PeekNextChar();
        protected int ReadNextChar();
        private string ReadToLoneSharp();
        public void SkipBlanks();
        private static void ToTokenAndValue(string str, out int currentToken, out object currentValue);
        private static void ToTokenAndValue(string str, out int currentToken, out object currentValue, bool allowSortings);
        public virtual void YYError(string message, params object[] args);

        public int Line { get; }

        public int Col { get; }

        public int Position { get; }

        public int CurrentTokenPosition { get; }
    }
}

