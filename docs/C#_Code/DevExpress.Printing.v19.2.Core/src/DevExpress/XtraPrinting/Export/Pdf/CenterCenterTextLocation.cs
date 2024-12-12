namespace DevExpress.XtraPrinting.Export.Pdf
{
    using System;

    public class CenterCenterTextLocation : CenterTextLocation
    {
        public CenterCenterTextLocation(bool directionVertical) : base(directionVertical)
        {
        }

        protected override float CalculateTextX_Horizontal(PdfGraphicsRectangle pdfRect, float textWidth) => 
            pdfRect.Left + ((pdfRect.Width - textWidth) / 2f);

        protected override float CalculateTextX_Vertical(PdfGraphicsRectangle pdfRect, float textWidth) => 
            pdfRect.Left + ((pdfRect.Width - textWidth) / 2f);

        protected override float CalculateTextY_Vertical(PdfGraphicsRectangle pdfRect, float textHeight) => 
            pdfRect.Bottom + ((pdfRect.Height + textHeight) / 2f);
    }
}

