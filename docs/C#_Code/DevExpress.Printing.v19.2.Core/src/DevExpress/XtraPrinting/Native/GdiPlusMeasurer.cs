namespace DevExpress.XtraPrinting.Native
{
    using System;
    using System.Drawing;
    using System.Runtime.InteropServices;

    public class GdiPlusMeasurer : Measurer
    {
        public override RectangleF GetRegionBounds(Region rgn, GraphicsUnit pageUnit);
        public override Region[] MeasureCharacterRanges(string text, Font font, RectangleF layoutRect, StringFormat stringFormat, GraphicsUnit pageUnit);
        public override SizeF MeasureString(string text, Font font, PointF location, StringFormat stringFormat, GraphicsUnit pageUnit);
        public override SizeF MeasureString(string text, Font font, SizeF size, StringFormat stringFormat, GraphicsUnit pageUnit);
        internal SizeF MeasureString(string text, Font font, SizeF size, StringFormat stringFormat, GraphicsUnit pageUnit, out int charactersFitted, out int linesFilled);
    }
}

