namespace DevExpress.Pdf.ContentGeneration
{
    using DevExpress.Pdf;
    using System;

    public class PdfRoundAnchorLineCapPainter : PdfRoundLineCapPainter
    {
        public PdfRoundAnchorLineCapPainter(double penWidth) : base(penWidth)
        {
        }

        protected override PdfRectangle GetBounds(double penWidth) => 
            new PdfRectangle(-penWidth, -penWidth, -penWidth + (2.0 * penWidth), -penWidth + (2.0 * penWidth));
    }
}

