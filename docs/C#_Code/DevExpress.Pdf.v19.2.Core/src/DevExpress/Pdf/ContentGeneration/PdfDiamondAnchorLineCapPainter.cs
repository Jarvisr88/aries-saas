namespace DevExpress.Pdf.ContentGeneration
{
    using DevExpress.Pdf;
    using System;

    public class PdfDiamondAnchorLineCapPainter : PdfPolygonalLineCapPainter
    {
        public PdfDiamondAnchorLineCapPainter(double lineWidth) : base(lineWidth)
        {
        }

        protected override PdfPoint[] CreatePathData(double penWidth) => 
            new PdfPoint[] { new PdfPoint(-penWidth, 0.0), new PdfPoint(0.0, penWidth), new PdfPoint(penWidth, 0.0), new PdfPoint(0.0, -penWidth) };
    }
}

