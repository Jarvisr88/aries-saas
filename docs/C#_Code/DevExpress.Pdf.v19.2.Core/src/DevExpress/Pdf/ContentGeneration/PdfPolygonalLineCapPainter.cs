namespace DevExpress.Pdf.ContentGeneration
{
    using DevExpress.Pdf;
    using DevExpress.Pdf.Native;
    using System;

    public abstract class PdfPolygonalLineCapPainter : PdfLineCapPainter
    {
        private readonly PdfPoint[] lineCapData;

        protected PdfPolygonalLineCapPainter(double penWidth)
        {
            this.lineCapData = this.CreatePathData(penWidth);
        }

        protected abstract PdfPoint[] CreatePathData(double penWidth);
        protected override void PerformDraw(PdfCommandConstructor constructor, PdfTransformationMatrix lineTransform)
        {
            constructor.FillPolygon(lineTransform.Transform(this.lineCapData), true);
        }

        protected override bool ShouldPaint =>
            this.lineCapData.Length > 2;
    }
}

