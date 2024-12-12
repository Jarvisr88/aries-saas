namespace DevExpress.Pdf.ContentGeneration
{
    using DevExpress.Pdf;
    using System;

    public class PdfArrowAnchorLineCapPainter : PdfPolygonalLineCapPainter
    {
        private readonly double delta;

        public PdfArrowAnchorLineCapPainter(double penWidth) : base(penWidth)
        {
            this.delta = penWidth * 1.6;
        }

        protected override PdfPoint[] CreatePathData(double penWidth)
        {
            double x = penWidth * 1.7;
            return new PdfPoint[] { new PdfPoint(0.0, 0.0), new PdfPoint(x, penWidth), new PdfPoint(x, -penWidth) };
        }

        protected override double Delta =>
            this.delta;
    }
}

