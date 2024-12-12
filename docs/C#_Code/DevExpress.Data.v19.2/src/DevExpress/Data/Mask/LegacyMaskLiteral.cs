namespace DevExpress.Data.Mask
{
    using System;

    public class LegacyMaskLiteral : LegacyMaskPrimitive
    {
        private char literal;

        public LegacyMaskLiteral(char literal);
        public override string GetDisplayText(string elementValue, char blank);
        public override string GetEditText(string elementValue, char blank, bool saveLiteral);

        public override bool IsLiteral { get; }

        public override string CapturingExpression { get; }

        public override int MinMatches { get; }

        public override int MaxMatches { get; }
    }
}

