namespace DevExpress.Office.Drawing
{
    using DevExpress.Office;
    using DevExpress.Office.Utils;
    using DevExpress.Utils.Internal;
    using DevExpress.XtraPrinting.Native;
    using System;
    using System.Drawing;

    public class PrecalculatedMetricsFontInfo : FontInfo
    {
        private System.Drawing.Font font;
        private DevExpress.Utils.Internal.FontDescriptor fontDescriptor;

        public PrecalculatedMetricsFontInfo(FontInfoMeasurer measurer, string fontName, int doubleFontSize, bool fontBold, bool fontItalic, bool fontStrikeout, bool fontUnderline, bool allowCjkCorrection, FontInfo baselineFontInfo) : base(measurer, fontName, doubleFontSize, fontBold, fontItalic, fontStrikeout, fontUnderline, allowCjkCorrection, true, baselineFontInfo)
        {
        }

        public PrecalculatedMetricsFontInfo(FontInfoMeasurer measurer, string fontName, int doubleFontSize, bool fontBold, bool fontItalic, bool fontStrikeout, bool fontUnderline, bool allowCjkCorrection, int textRotation, FontInfo baselineFontInfo) : base(measurer, fontName, doubleFontSize, fontBold, fontItalic, fontStrikeout, fontUnderline, allowCjkCorrection, textRotation, true, baselineFontInfo)
        {
        }

        protected internal override int CalculateFontCharset(FontInfoMeasurer measurer) => 
            (this.Name != "Symbol") ? 1 : 2;

        protected internal override float CalculateFontSizeInPoints() => 
            FontSizeHelper.GetSizeInPoints(this.font);

        protected internal override void CalculateFontVerticalParameters(FontInfoMeasurer measurer, bool allowCjkCorrection)
        {
            float sizeInPoints = FontSizeHelper.GetSizeInPoints(this.Font);
            float num2 = measurer.UnitConverter.PixelsToLayoutUnitsF(Units.DocumentsToPixelsF(Units.PointsToDocumentsF(sizeInPoints), DocumentModelDpi.Dpi), DocumentModelDpi.Dpi);
            TTFontInfo fontInfo = this.fontDescriptor.FontInfo;
            base.Ascent = fontInfo.GetAscent((double) num2);
            base.Descent = fontInfo.GetDescent((double) num2);
            base.LineSpacing = (base.Ascent + base.Descent) + fontInfo.GetLineGap((double) num2);
            base.CalculatedLineSpacing = base.LineSpacing;
        }

        protected internal override void CalculateUnderlineAndStrikeoutParameters(FontInfoMeasurer measurer)
        {
            float sizeInPoints = FontSizeHelper.GetSizeInPoints(this.Font);
            float num2 = measurer.UnitConverter.PixelsToLayoutUnitsF(Units.DocumentsToPixelsF(Units.PointsToDocumentsF(sizeInPoints), DocumentModelDpi.Dpi), DocumentModelDpi.Dpi);
            TTFontInfo fontInfo = this.fontDescriptor.FontInfo;
            base.UnderlinePosition = fontInfo.GetUnderlinePosition((double) num2);
            base.UnderlineThickness = fontInfo.GetUnderlineSize((double) num2);
            base.StrikeoutPosition = fontInfo.GetStrikeOutPosition((double) num2);
            base.StrikeoutThickness = fontInfo.GetStrikeOutSize((double) num2);
            base.SubscriptSize = new System.Drawing.Size(fontInfo.GetSubscriptXSize((double) num2), fontInfo.GetSubscriptYSize((double) num2));
            base.SubscriptOffset = new Point(fontInfo.GetSubscriptXOffset((double) num2), fontInfo.GetSubscriptYOffset((double) num2));
            base.SuperscriptSize = new System.Drawing.Size(fontInfo.GetSuperscriptXSize((double) num2), fontInfo.GetSuperscriptYSize((double) num2));
            base.SuperscriptOffset = new Point(fontInfo.GetSuperscriptXOffset((double) num2), fontInfo.GetSuperscriptYOffset((double) num2));
        }

        protected internal override void CreateFont(FontInfoMeasurer measurer, string fontName, int doubleFontSize, bool fontBold, bool fontItalic, bool fontStrikeout, bool fontUnderline)
        {
            this.font = ((PrecalculatedMetricsFontInfoMeasurer) measurer).CreateFont(fontName, ((float) doubleFontSize) / 2f, fontBold, fontItalic, fontStrikeout, fontUnderline);
            this.fontDescriptor = FontManager.GetFontDescriptor(fontName, fontBold, fontItalic);
        }

        protected override void Dispose(bool disposing)
        {
            try
            {
                if (disposing)
                {
                    if (this.font != null)
                    {
                        this.font.Dispose();
                        this.font = null;
                    }
                    this.fontDescriptor = null;
                }
            }
            finally
            {
                base.Dispose(disposing);
            }
        }

        protected internal override void Initialize(FontInfoMeasurer measurer, bool useSystemFontQuality)
        {
        }

        public override bool Bold =>
            this.font.Bold;

        public override bool Italic =>
            this.font.Italic;

        public override bool Underline =>
            this.font.Underline;

        public override bool Strikeout =>
            this.font.Strikeout;

        public override float Size =>
            this.font.Size;

        public override string Name =>
            this.font.Name;

        public override string FontFamilyName =>
            this.font.FontFamily.Name;

        public override System.Drawing.Font Font =>
            this.font;

        public DevExpress.Utils.Internal.FontDescriptor FontDescriptor =>
            this.fontDescriptor;
    }
}

