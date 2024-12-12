namespace DevExpress.Utils.Svg
{
    using System;
    using System.Drawing;
    using System.Drawing.Drawing2D;

    public class GdiFontWrapper : IDisposable
    {
        private Font fontCore;
        private static float DpiY = 96f;

        public GdiFontWrapper(Font font)
        {
            this.fontCore = font;
        }

        public void AddStringToPath(GraphicsPath path, string text, PointF location)
        {
            path.AddString(text, this.fontCore.FontFamily, (int) this.fontCore.Style, this.fontCore.Size, location, StringFormat.GenericTypographic);
        }

        public float CalcFontHeight() => 
            (this.fontCore.GetHeight(DpiY) / ((float) this.fontCore.FontFamily.GetLineSpacing(this.fontCore.Style))) * this.fontCore.FontFamily.GetCellAscent(this.fontCore.Style);

        public void Dispose()
        {
            this.fontCore.Dispose();
        }

        public SizeF MeasureString(ISvgGraphics g, string text)
        {
            StringFormat genericTypographic = StringFormat.GenericTypographic;
            CharacterRange[] ranges = new CharacterRange[] { new CharacterRange(0, text.Length) };
            genericTypographic.SetMeasurableCharacterRanges(ranges);
            genericTypographic.FormatFlags |= StringFormatFlags.MeasureTrailingSpaces;
            using (SvgGraphicsWrapper wrapper = new SvgGraphicsWrapper(g))
            {
                return new SizeF(wrapper.MeasureString(text, this.fontCore, PointF.Empty, genericTypographic).Width, this.CalcFontHeight());
            }
        }

        public float Size =>
            this.fontCore.Size;

        public float SizeInPoints =>
            this.fontCore.SizeInPoints;
    }
}

