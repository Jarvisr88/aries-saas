namespace DevExpress.Office.Drawing
{
    using DevExpress.Office;
    using DevExpress.Office.Layout;
    using DevExpress.Office.Utils;
    using DevExpress.Utils.Internal;
    using DevExpress.XtraPrinting.Native;
    using System;
    using System.Drawing;

    public class PrecalculatedMetricsFontInfoMeasurer : FontInfoMeasurer
    {
        private Font defaultFont;
        private readonly bool roundMaxDigitWidth;

        public PrecalculatedMetricsFontInfoMeasurer(DocumentLayoutUnitConverter unitConverter) : this(unitConverter, false)
        {
        }

        public PrecalculatedMetricsFontInfoMeasurer(DocumentLayoutUnitConverter unitConverter, bool roundMaxDigitWidth) : base(unitConverter)
        {
            this.roundMaxDigitWidth = roundMaxDigitWidth;
        }

        private Font CreateDefaultFont() => 
            new Font("Arial", (float) base.UnitConverter.PointsToFontUnits(10), FontStyle.Regular, (GraphicsUnit) base.UnitConverter.FontUnit);

        public virtual Font CreateFont(string familyName, float emSize, bool fontBold, bool fontItalic, bool fontStrikeout, bool fontUnderline)
        {
            try
            {
                FontStyle regular = FontStyle.Regular;
                if (fontBold)
                {
                    regular |= FontStyle.Bold;
                }
                if (fontItalic)
                {
                    regular |= FontStyle.Italic;
                }
                if (fontStrikeout)
                {
                    regular |= FontStyle.Strikeout;
                }
                if (fontUnderline)
                {
                    regular |= FontStyle.Underline;
                }
                return new Font(familyName, emSize, regular, (GraphicsUnit) base.UnitConverter.FontUnit);
            }
            catch
            {
                return (Font) this.DefaultFont.Clone();
            }
        }

        private float GetFontSizeInPixels(Font font) => 
            Units.DocumentsToPixelsF(Units.PointsToDocumentsF(FontSizeHelper.GetSizeInPoints(font)), DocumentModelDpi.Dpi);

        protected override void Initialize()
        {
            this.defaultFont = this.CreateDefaultFont();
        }

        public Rectangle[] MeasureCharactersBounds(string s, PrecalculatedMetricsFontInfo fontInfo)
        {
            FontDescriptor fontDescriptor = fontInfo.FontDescriptor;
            if (fontDescriptor.FontInfo == null)
            {
                throw new ArgumentException("font.Name");
            }
            float sizeInPoints = fontInfo.SizeInPoints;
            float num2 = base.UnitConverter.PixelsToLayoutUnitsF(Units.DocumentsToPixelsF(Units.PointsToDocumentsF(sizeInPoints), DocumentModelDpi.Dpi), DocumentModelDpi.Dpi);
            return fontDescriptor.FontInfo.MeasureCharacterBounds(s, (double) num2);
        }

        public override float MeasureCharacterWidthF(char character, FontInfo fontInfo) => 
            (float) this.MeasureString(character.ToString(), fontInfo).Width;

        public override float MeasureMaxDigitWidthF(FontInfo fontInfo)
        {
            float num = this.MeasureMaxDigitWidthF("0", fontInfo);
            for (int i = 1; i < 10; i++)
            {
                num = Math.Max(num, this.MeasureMaxDigitWidthF(i.ToString(), fontInfo));
            }
            return num;
        }

        private float MeasureMaxDigitWidthF(string s, FontInfo fontInfo) => 
            this.MeasureStringCore(s, fontInfo, this.roundMaxDigitWidth).Width;

        public override Size MeasureMultilineText(string text, FontInfo fontInfo, int availableWidth)
        {
            PrecalculatedMetricsFontInfo info = (PrecalculatedMetricsFontInfo) fontInfo;
            FontDescriptor fontDescriptor = info.FontDescriptor;
            if (fontDescriptor.FontInfo == null)
            {
                throw new ArgumentException("font.Name");
            }
            float fontSizeInPixels = this.GetFontSizeInPixels(info.Font);
            SizeF ef = fontDescriptor.FontInfo.MeasureMultilineText(text, (double) base.UnitConverter.LayoutUnitsToPixelsF((float) availableWidth), (double) fontSizeInPixels);
            SizeF size = base.UnitConverter.PixelsToLayoutUnitsF(ef, DocumentModelDpi.DpiX, DocumentModelDpi.DpiY);
            return this.RoundSize(size);
        }

        public override Size MeasureString(string s, FontInfo fontInfo)
        {
            SizeF size = this.MeasureStringCore(s, fontInfo, false);
            return this.RoundSize(size);
        }

        private SizeF MeasureStringCore(string s, FontInfo fontInfo, bool roundSize)
        {
            PrecalculatedMetricsFontInfo info = (PrecalculatedMetricsFontInfo) fontInfo;
            TTFontInfo info2 = info.FontDescriptor.FontInfo;
            if (info2 == null)
            {
                throw new ArgumentException("font.Name");
            }
            float fontSizeInPixels = this.GetFontSizeInPixels(info.Font);
            SizeF size = info2.MeasureText(s, (double) fontSizeInPixels);
            return base.UnitConverter.PixelsToLayoutUnitsF(roundSize ? ((SizeF) this.RoundSize(size)) : size, DocumentModelDpi.DpiX, DocumentModelDpi.DpiY);
        }

        private Size RoundSize(SizeF size) => 
            new Size((int) Math.Round((double) size.Width), (int) Math.Round((double) size.Height));

        internal Font DefaultFont =>
            this.defaultFont;

        protected bool RoundMaxDigitWidth =>
            this.roundMaxDigitWidth;
    }
}

