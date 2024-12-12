namespace DevExpress.Pdf.ContentGeneration
{
    using DevExpress.Pdf.Native;
    using DevExpress.Text.Fonts;
    using System;
    using System.Collections.Generic;
    using System.Drawing;

    public class PdfExportPartialTrustFont : PdfExportFont
    {
        private const float shapingFontSize = 72f;
        private readonly GDIPlusMeasuringContext gdiContext;
        private readonly Font shapingFont;
        private readonly PdfExportModelFont modelFont;

        public PdfExportPartialTrustFont(IPdfExportPlatformFont platformFont, GDIPlusMeasuringContext gdiContext, PdfExportModelFont modelFont) : base(platformFont, modelFont)
        {
            this.gdiContext = gdiContext;
            this.shapingFont = platformFont.CreateGDIPlusFont(72f);
            this.modelFont = modelFont;
        }

        public override void Dispose()
        {
            this.shapingFont.Dispose();
        }

        protected override double GetCharacterWidth(char ch) => 
            (double) ((this.gdiContext.GetCharWidth(ch, this.shapingFont) / 72f) * 1000f);

        public override IList<DXCluster> GetTextRuns(string text, bool directionRightToLeft, float fontSizeInPoints, bool useKerning)
        {
            List<DXCluster> list = new List<DXCluster>();
            IList<DXBreakCondition> breakPoints = PdfLineBreakAnalyzer.GetBreakPoints(text);
            for (int i = 0; i < text.Length; i++)
            {
                char ch = text[i];
                float num2 = this.gdiContext.GetCharWidth(ch, this.shapingFont) / 72f;
                DXGlyph glyph = this.modelFont.CreateGlyph(0, ch, num2 * 1000f, num2 * fontSizeInPoints);
                DXGlyph[] glyphs = new DXGlyph[] { glyph };
                list.Add(new DXCluster(glyphs, new StringView(text, i, 1), new DXLineBreakpoint(breakPoints[i], ch), 0, ch == '\t'));
            }
            return list;
        }
    }
}

