namespace DevExpress.XtraPrinting.Export.Pdf
{
    using System;
    using System.Drawing;

    public abstract class CenterTextLocation : TextLocation
    {
        protected CenterTextLocation(bool directionVertical) : base(directionVertical)
        {
        }

        protected override float CalculateTextY_Horizontal(PdfGraphicsRectangle pdfRect, float textHeight) => 
            pdfRect.Bottom + ((pdfRect.Height + textHeight) / 2f);

        public static CenterTextLocation CreateInstance(StringAlignment alignment, bool directionVertical)
        {
            switch (alignment)
            {
                case StringAlignment.Near:
                    return new CenterNearTextLocation(directionVertical);

                case StringAlignment.Center:
                    return new CenterCenterTextLocation(directionVertical);

                case StringAlignment.Far:
                    return new CenterFarTextLocation(directionVertical);
            }
            throw new ArgumentException();
        }
    }
}

