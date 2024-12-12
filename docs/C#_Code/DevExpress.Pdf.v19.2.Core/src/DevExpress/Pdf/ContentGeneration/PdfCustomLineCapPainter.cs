namespace DevExpress.Pdf.ContentGeneration
{
    using DevExpress.Emf;
    using DevExpress.Pdf;
    using DevExpress.Pdf.Native;
    using System;

    public class PdfCustomLineCapPainter : PdfLineCapPainter
    {
        private readonly PdfPoint[] points;
        private readonly double delta;
        private readonly double scaledWidth;
        private readonly bool fill;

        public PdfCustomLineCapPainter(DXCustomLineCap customLineCap, double penWidth)
        {
            double width = customLineCap.Width;
            double height = customLineCap.Height;
            this.scaledWidth = penWidth * customLineCap.WidthScale;
            double y = (this.scaledWidth * width) / 2.0;
            double x = this.scaledWidth * height;
            this.fill = customLineCap.Filled;
            if ((this.scaledWidth != 0.0) && (width != 0.0))
            {
                this.delta = (height / width) * this.scaledWidth;
                this.points = new PdfPoint[] { new PdfPoint(x, y), new PdfPoint(0.0, 0.0), new PdfPoint(x, -y) };
            }
        }

        protected override void PerformDraw(PdfCommandConstructor constructor, PdfTransformationMatrix lineTransform)
        {
            PdfPoint[] points = lineTransform.Transform(this.points);
            if (this.fill)
            {
                constructor.FillPolygon(points, true);
            }
            else
            {
                constructor.SaveGraphicsState();
                constructor.SetLineWidth(this.scaledWidth);
                constructor.DrawLines(points);
                constructor.RestoreGraphicsState();
            }
        }

        protected override double Delta =>
            this.delta;

        protected override bool ShouldPaint =>
            this.points != null;
    }
}

