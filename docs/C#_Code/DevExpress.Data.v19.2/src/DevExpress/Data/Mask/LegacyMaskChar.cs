namespace DevExpress.Data.Mask
{
    using System;

    public class LegacyMaskChar : LegacyMaskPrimitive
    {
        private string capturing;
        private int minMatches;
        private int maxMatches;

        public LegacyMaskChar(string capturing, char caseConversion, int minMatches, int maxMatches);
        public override string GetDisplayText(string elementValue, char blank);
        public override string GetEditText(string elementValue, char blank, bool saveLiteral);
        public void PatchMatches(int min, int max);

        public override bool IsLiteral { get; }

        public override string CapturingExpression { get; }

        public override int MinMatches { get; }

        public override int MaxMatches { get; }
    }
}

