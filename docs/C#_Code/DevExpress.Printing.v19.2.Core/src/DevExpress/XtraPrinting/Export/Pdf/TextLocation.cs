namespace DevExpress.XtraPrinting.Export.Pdf
{
    using System;
    using System.Drawing;

    public abstract class TextLocation
    {
        private bool directionVertical;

        protected TextLocation(bool directionVertical)
        {
            this.directionVertical = directionVertical;
        }

        public float CalculateTextX(PdfGraphicsRectangle pdfRect, float textWidth) => 
            this.directionVertical ? this.CalculateTextX_Vertical(pdfRect, textWidth) : this.CalculateTextX_Horizontal(pdfRect, textWidth);

        protected abstract float CalculateTextX_Horizontal(PdfGraphicsRectangle pdfRect, float textWidth);
        protected abstract float CalculateTextX_Vertical(PdfGraphicsRectangle pdfRect, float textWidth);
        public float CalculateTextY(PdfGraphicsRectangle pdfRect, float textHeight) => 
            this.directionVertical ? this.CalculateTextY_Vertical(pdfRect, textHeight) : this.CalculateTextY_Horizontal(pdfRect, textHeight);

        protected abstract float CalculateTextY_Horizontal(PdfGraphicsRectangle pdfRect, float textHeight);
        protected abstract float CalculateTextY_Vertical(PdfGraphicsRectangle pdfRect, float textHeight);
        public static TextLocation CreateInstance(StringFormat format)
        {
            StringAlignment alignment = format.Alignment;
            bool directionVertical = (format.FormatFlags & StringFormatFlags.DirectionVertical) != 0;
            switch (format.LineAlignment)
            {
                case StringAlignment.Near:
                    return NearTextLocation.CreateInstance(alignment, directionVertical);

                case StringAlignment.Center:
                    return CenterTextLocation.CreateInstance(alignment, directionVertical);

                case StringAlignment.Far:
                    return FarTextLocation.CreateInstance(alignment, directionVertical);
            }
            throw new ArgumentException();
        }
    }
}

