namespace DevExpress.Office.Drawing
{
    using DevExpress.Office.Layout;
    using System;
    using System.Runtime.CompilerServices;

    public class GdiFontCache : GdiPlusFontCache
    {
        public GdiFontCache(DocumentLayoutUnitConverter unitConverter, bool allowCjkCorrection, bool useSystemFontQuality) : base(unitConverter, allowCjkCorrection, useSystemFontQuality)
        {
        }

        protected internal override FontInfo CreateFontInfoCore(string fontName, int doubleFontSize, bool fontBold, bool fontItalic, bool fontStrikeout, bool fontUnderline, int textRotation, FontInfo baselineFontInfo) => 
            new GdiFontInfo(base.Measurer, fontName, doubleFontSize, fontBold, fontItalic, fontStrikeout, fontUnderline, base.AllowCjkCorrection, textRotation, base.UseSystemFontQuality, this.UseGdiFontMetrics, baselineFontInfo);

        protected internal override FontInfoMeasurer CreateFontInfoMeasurer(DocumentLayoutUnitConverter unitConverter) => 
            new GdiFontInfoMeasurer(unitConverter);

        public bool UseGdiFontMetrics { get; set; }
    }
}

