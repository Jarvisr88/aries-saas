namespace DevExpress.XtraPrinting.Export.Pdf
{
    using System;
    using System.Drawing;

    public abstract class NearTextLocation : TextLocation
    {
        protected NearTextLocation(bool directionVertical) : base(directionVertical)
        {
        }

        protected override float CalculateTextY_Horizontal(PdfGraphicsRectangle pdfRect, float textHeight) => 
            pdfRect.Top;

        public static NearTextLocation CreateInstance(StringAlignment alignment, bool directionVertical)
        {
            switch (alignment)
            {
                case StringAlignment.Near:
                    return new NearNearTextLocation(directionVertical);

                case StringAlignment.Center:
                    return new NearCenterTextLocation(directionVertical);

                case StringAlignment.Far:
                    return new NearFarTextLocation(directionVertical);
            }
            throw new ArgumentException();
        }
    }
}

