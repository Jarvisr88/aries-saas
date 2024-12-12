namespace DevExpress.XtraPrinting.Export.Pdf
{
    using System;

    public class FarCenterTextLocation : FarTextLocation
    {
        public FarCenterTextLocation(bool directionVertical) : base(directionVertical)
        {
        }

        protected override float CalculateTextX_Horizontal(PdfGraphicsRectangle pdfRect, float textWidth) => 
            pdfRect.Left + ((pdfRect.Width - textWidth) / 2f);

        protected override float CalculateTextX_Vertical(PdfGraphicsRectangle pdfRect, float textWidth) => 
            (pdfRect.Left + (pdfRect.Width / 2f)) - textWidth;

        protected override float CalculateTextY_Vertical(PdfGraphicsRectangle pdfRect, float textHeight) => 
            pdfRect.Bottom + (textHeight / 2f);
    }
}

