namespace DevExpress.Office.Drawing
{
    using System;
    using System.Drawing;

    public abstract class GdiPlusPainterBase : Painter
    {
        private System.Drawing.StringFormat sf = CreateStringFormat();

        protected GdiPlusPainterBase()
        {
        }

        protected virtual unsafe Rectangle CorrectTextDrawingBounds(FontInfo fontInfo, Rectangle textBounds)
        {
            Rectangle rectangle = textBounds;
            Rectangle* rectanglePtr1 = &rectangle;
            rectanglePtr1.Y += fontInfo.Free - fontInfo.DrawingOffset;
            return rectangle;
        }

        protected static System.Drawing.StringFormat CreateStringFormat()
        {
            System.Drawing.StringFormat format = (System.Drawing.StringFormat) System.Drawing.StringFormat.GenericTypographic.Clone();
            format.FormatFlags |= StringFormatFlags.NoClip | StringFormatFlags.NoWrap | StringFormatFlags.MeasureTrailingSpaces;
            format.FormatFlags &= ~StringFormatFlags.LineLimit;
            return format;
        }

        protected override void Dispose(bool disposing)
        {
            try
            {
                if (disposing && (this.sf != null))
                {
                    this.sf.Dispose();
                    this.sf = null;
                }
            }
            finally
            {
                base.Dispose(disposing);
            }
        }

        public override void DrawSpacesString(string text, FontInfo fontInfo, Rectangle bounds)
        {
            this.DrawString(text, fontInfo, bounds);
        }

        public System.Drawing.StringFormat StringFormat =>
            this.sf;
    }
}

