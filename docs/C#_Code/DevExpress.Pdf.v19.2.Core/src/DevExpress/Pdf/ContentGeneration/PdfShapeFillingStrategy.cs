namespace DevExpress.Pdf.ContentGeneration
{
    using DevExpress.Pdf;
    using DevExpress.Pdf.Native;
    using System;
    using System.Drawing;

    public abstract class PdfShapeFillingStrategy
    {
        protected PdfShapeFillingStrategy()
        {
        }

        public abstract void Clip(PdfCommandConstructor constructor);
        protected static PdfPoint[] GetRectanglePoints(RectangleF rectangle)
        {
            double left = rectangle.Left;
            double bottom = rectangle.Bottom;
            double right = rectangle.Right;
            double top = rectangle.Top;
            return new PdfPoint[] { new PdfPoint(left, bottom), new PdfPoint(right, bottom), new PdfPoint(right, top), new PdfPoint(left, top) };
        }

        public abstract PdfPoint[] ShapePoints { get; }
    }
}

