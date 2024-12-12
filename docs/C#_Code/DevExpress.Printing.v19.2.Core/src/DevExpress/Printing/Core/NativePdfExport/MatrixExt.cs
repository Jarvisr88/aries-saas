namespace DevExpress.Printing.Core.NativePdfExport
{
    using DevExpress.Pdf;
    using System;
    using System.Drawing;
    using System.Runtime.CompilerServices;

    internal static class MatrixExt
    {
        public static PdfPoint TransformPoint(this PdfTransformationMatrix matrix, PointF point) => 
            matrix.Transform((double) point.X, (double) point.Y);

        public static PdfRectangle TransformRectangle(this PdfTransformationMatrix matrix, RectangleF rect)
        {
            PdfPoint point = matrix.TransformPoint(new PointF(Math.Min(rect.Left, rect.Right), Math.Max(rect.Top, rect.Bottom)));
            PdfPoint point2 = matrix.TransformPoint(new PointF(Math.Max(rect.Left, rect.Right), Math.Min(rect.Top, rect.Bottom)));
            return new PdfRectangle(point.X, point.Y, point2.X, point2.Y);
        }
    }
}

