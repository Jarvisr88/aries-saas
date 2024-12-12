namespace DevExpress.Data.Mask
{
    using System;

    public abstract class LegacyMaskPrimitive
    {
        private char CaseConversion;

        protected LegacyMaskPrimitive(char caseConversion);
        public char GetAcceptableChar(char input);
        public abstract string GetDisplayText(string elementValue, char blank);
        public abstract string GetEditText(string elementValue, char blank, bool saveLiteral);
        public bool IsAcceptable(char input);
        public bool IsAcceptableStrong(char input);

        public abstract bool IsLiteral { get; }

        public abstract string CapturingExpression { get; }

        public abstract int MinMatches { get; }

        public abstract int MaxMatches { get; }
    }
}

