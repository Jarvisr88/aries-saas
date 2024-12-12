namespace DevExpress.Pdf.ContentGeneration
{
    using DevExpress.Pdf;
    using DevExpress.Pdf.Native;
    using System;

    public abstract class PdfLineCapPainter : IPdfLineCapPainter
    {
        protected PdfLineCapPainter()
        {
        }

        public void DrawLineCap(PdfCommandConstructor constructor, PdfPoint startPoint, PdfPoint endPoint)
        {
            if (this.ShouldPaint)
            {
                double a = endPoint.X - startPoint.X;
                double b = endPoint.Y - startPoint.Y;
                double num3 = Math.Sqrt((a * a) + (b * b));
                if (num3 != 0.0)
                {
                    a /= num3;
                    b /= num3;
                    this.PerformDraw(constructor, new PdfTransformationMatrix(a, b, -b, a, startPoint.X, startPoint.Y));
                }
            }
        }

        protected abstract void PerformDraw(PdfCommandConstructor constructor, PdfTransformationMatrix lineTransform);
        public PdfPoint TranslatePoint(PdfPoint point, PdfPoint endPoint)
        {
            double delta = this.Delta;
            if (delta == 0.0)
            {
                return point;
            }
            double num2 = endPoint.X - point.X;
            double num3 = endPoint.Y - point.Y;
            double num4 = Math.Sqrt((num2 * num2) + (num3 * num3));
            return ((num4 == 0.0) ? point : new PdfPoint(point.X + ((delta * num2) / num4), point.Y + ((delta * num3) / num4)));
        }

        protected virtual bool ShouldPaint =>
            true;

        protected virtual double Delta =>
            0.0;
    }
}

