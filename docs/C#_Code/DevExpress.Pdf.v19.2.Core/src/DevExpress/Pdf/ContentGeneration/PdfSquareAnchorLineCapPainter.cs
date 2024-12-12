namespace DevExpress.Pdf.ContentGeneration
{
    using DevExpress.Pdf;
    using System;

    public class PdfSquareAnchorLineCapPainter : PdfPolygonalLineCapPainter
    {
        public PdfSquareAnchorLineCapPainter(double lineWidth) : base(lineWidth)
        {
        }

        protected override PdfPoint[] CreatePathData(double penWidth)
        {
            double x = -penWidth * 0.7;
            double y = -x;
            return new PdfPoint[] { new PdfPoint(x, x), new PdfPoint(x, y), new PdfPoint(y, y), new PdfPoint(y, x) };
        }
    }
}

