namespace DevExpress.XtraPrinting.Export.Pdf
{
    using System;

    public class FarNearTextLocation : FarTextLocation
    {
        public FarNearTextLocation(bool directionVertical) : base(directionVertical)
        {
        }

        protected override float CalculateTextX_Horizontal(PdfGraphicsRectangle pdfRect, float textWidth) => 
            pdfRect.Left;

        protected override float CalculateTextX_Vertical(PdfGraphicsRectangle pdfRect, float textWidth) => 
            pdfRect.Left - textWidth;

        protected override float CalculateTextY_Vertical(PdfGraphicsRectangle pdfRect, float textHeight) => 
            pdfRect.Bottom + textHeight;
    }
}

