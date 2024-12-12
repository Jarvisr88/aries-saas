namespace DevExpress.Pdf.ContentGeneration
{
    using DevExpress.Pdf;
    using System;

    public class PdfTriangleLineCapPainter : PdfPolygonalLineCapPainter
    {
        public PdfTriangleLineCapPainter(double lineWidth) : base(lineWidth)
        {
        }

        protected override PdfPoint[] CreatePathData(double penWidth)
        {
            double y = penWidth / 2.0;
            double x = 0.1 * penWidth;
            return new PdfPoint[] { new PdfPoint(x, y), new PdfPoint(x, -y), new PdfPoint(0.0, -y), new PdfPoint(-y, 0.0), new PdfPoint(0.0, y) };
        }
    }
}

