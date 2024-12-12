namespace DevExpress.Pdf.Native
{
    using DevExpress.Pdf;
    using System;

    public class PdfType1StandardAccentedGlyphInfo
    {
        private readonly int baseGlyph;
        private readonly int accentGlyph;
        private readonly PdfPoint accentDelta;
        private readonly double accentSidebearing;

        public PdfType1StandardAccentedGlyphInfo(int baseGlyph, int accentGlyph, PdfPoint accentDelta, double accentSidebearing)
        {
            this.baseGlyph = baseGlyph;
            this.accentGlyph = accentGlyph;
            this.accentDelta = accentDelta;
            this.accentSidebearing = accentSidebearing;
        }

        public int BaseGlyph =>
            this.baseGlyph;

        public int AccentGlyph =>
            this.accentGlyph;

        public PdfPoint AccentDelta =>
            this.accentDelta;

        public double AccentSidebearing =>
            this.accentSidebearing;
    }
}

