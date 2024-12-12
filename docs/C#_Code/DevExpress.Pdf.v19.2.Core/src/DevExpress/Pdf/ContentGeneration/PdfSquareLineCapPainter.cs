namespace DevExpress.Pdf.ContentGeneration
{
    using DevExpress.Pdf;
    using System;

    public class PdfSquareLineCapPainter : PdfPolygonalLineCapPainter
    {
        public PdfSquareLineCapPainter(double penWidth) : base(penWidth)
        {
        }

        protected override PdfPoint[] CreatePathData(double penWidth)
        {
            double x = penWidth / 2.0;
            return new PdfPoint[] { new PdfPoint(x, -x), new PdfPoint(-x, -x), new PdfPoint(-x, x), new PdfPoint(x, x) };
        }
    }
}

