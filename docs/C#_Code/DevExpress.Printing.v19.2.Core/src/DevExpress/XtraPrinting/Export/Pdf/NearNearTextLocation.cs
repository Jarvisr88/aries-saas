namespace DevExpress.XtraPrinting.Export.Pdf
{
    using System;

    public class NearNearTextLocation : NearTextLocation
    {
        public NearNearTextLocation(bool directionVertical) : base(directionVertical)
        {
        }

        protected override float CalculateTextX_Horizontal(PdfGraphicsRectangle pdfRect, float textWidth) => 
            pdfRect.Left;

        protected override float CalculateTextX_Vertical(PdfGraphicsRectangle pdfRect, float textWidth) => 
            pdfRect.Left;

        protected override float CalculateTextY_Vertical(PdfGraphicsRectangle pdfRect, float textHeight) => 
            pdfRect.Top + textHeight;
    }
}

