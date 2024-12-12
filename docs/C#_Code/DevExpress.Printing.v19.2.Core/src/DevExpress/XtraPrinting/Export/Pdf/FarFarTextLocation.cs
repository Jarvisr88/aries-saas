namespace DevExpress.XtraPrinting.Export.Pdf
{
    using System;

    public class FarFarTextLocation : FarTextLocation
    {
        public FarFarTextLocation(bool directionVertical) : base(directionVertical)
        {
        }

        protected override float CalculateTextX_Horizontal(PdfGraphicsRectangle pdfRect, float textWidth) => 
            pdfRect.Right - textWidth;

        protected override float CalculateTextX_Vertical(PdfGraphicsRectangle pdfRect, float textWidth) => 
            pdfRect.Right - textWidth;

        protected override float CalculateTextY_Vertical(PdfGraphicsRectangle pdfRect, float textHeight) => 
            pdfRect.Bottom;
    }
}

