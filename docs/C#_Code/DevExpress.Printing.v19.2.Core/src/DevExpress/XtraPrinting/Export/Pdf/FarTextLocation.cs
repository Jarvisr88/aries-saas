namespace DevExpress.XtraPrinting.Export.Pdf
{
    using System;
    using System.Drawing;

    public abstract class FarTextLocation : TextLocation
    {
        protected FarTextLocation(bool directionVertical) : base(directionVertical)
        {
        }

        protected override float CalculateTextY_Horizontal(PdfGraphicsRectangle pdfRect, float textHeight) => 
            pdfRect.Bottom + textHeight;

        public static FarTextLocation CreateInstance(StringAlignment alignment, bool directionVertical)
        {
            switch (alignment)
            {
                case StringAlignment.Near:
                    return new FarNearTextLocation(directionVertical);

                case StringAlignment.Center:
                    return new FarCenterTextLocation(directionVertical);

                case StringAlignment.Far:
                    return new FarFarTextLocation(directionVertical);
            }
            throw new ArgumentException();
        }
    }
}

