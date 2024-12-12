namespace DevExpress.Office.Drawing
{
    using DevExpress.Office.Layout;
    using DevExpress.Office.Utils;
    using System;
    using System.Drawing;

    public class GdiPlusFontInfoMeasurer : FontInfoMeasurer
    {
        private Font defaultFont;
        private Graphics measureGraphics;
        private GraphicsToLayoutUnitsModifier graphicsModifier;
        private StringFormat measureStringFormat;
        private static char[] digits = new char[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };

        public GdiPlusFontInfoMeasurer(DocumentLayoutUnitConverter unitConverter) : base(unitConverter)
        {
        }

        protected internal virtual Font CreateDefaultFont() => 
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

        protected virtual Graphics CreateMeasureGraphics() => 
            Graphics.FromHwnd(IntPtr.Zero);

        protected internal virtual StringFormat CreateMeasureStringFormat()
        {
            StringFormat format = (StringFormat) StringFormat.GenericTypographic.Clone();
            format.FormatFlags |= StringFormatFlags.NoWrap | StringFormatFlags.MeasureTrailingSpaces;
            return format;
        }

        protected override void Dispose(bool disposing)
        {
            try
            {
                if (disposing)
                {
                    if (this.graphicsModifier != null)
                    {
                        this.graphicsModifier.Dispose();
                        this.graphicsModifier = null;
                    }
                    if (this.measureStringFormat != null)
                    {
                        this.measureStringFormat.Dispose();
                        this.measureStringFormat = null;
                    }
                    if (this.measureGraphics != null)
                    {
                        this.measureGraphics.Dispose();
                        this.measureGraphics = null;
                    }
                    if (this.defaultFont != null)
                    {
                        this.defaultFont.Dispose();
                        this.defaultFont = null;
                    }
                }
            }
            finally
            {
                base.Dispose(disposing);
            }
        }

        protected override void Initialize()
        {
            this.defaultFont = this.CreateDefaultFont();
            this.measureGraphics = this.CreateMeasureGraphics();
            this.graphicsModifier = new GraphicsToLayoutUnitsModifier(this.measureGraphics, base.UnitConverter);
            this.measureStringFormat = this.CreateMeasureStringFormat();
        }

        public override float MeasureCharacterWidthF(char character, FontInfo fontInfo) => 
            this.measureGraphics.MeasureString(new string(character, 1), fontInfo.Font, 0x7fffffff, this.measureStringFormat).Width;

        public override float MeasureMaxDigitWidthF(FontInfo fontInfo)
        {
            int length = digits.Length;
            float num2 = this.MeasureCharacterWidthF(digits[0], fontInfo);
            for (int i = 1; i < length; i++)
            {
                num2 = Math.Max(num2, this.MeasureCharacterWidthF(digits[i], fontInfo));
            }
            return num2;
        }

        public override Size MeasureMultilineText(string text, FontInfo fontInfo, int availableWidth) => 
            Size.Ceiling(this.measureGraphics.MeasureString(text, fontInfo.Font, availableWidth, StringFormat.GenericTypographic));

        public override Size MeasureString(string text, FontInfo fontInfo) => 
            Size.Ceiling(this.measureGraphics.MeasureString(text, fontInfo.Font, 0x7fffffff, this.measureStringFormat));

        public Graphics MeasureGraphics =>
            this.measureGraphics;

        internal GraphicsToLayoutUnitsModifier GraphicsModifier =>
            this.graphicsModifier;

        internal StringFormat MeasureStringFormat =>
            this.measureStringFormat;

        internal Font DefaultFont =>
            this.defaultFont;
    }
}

