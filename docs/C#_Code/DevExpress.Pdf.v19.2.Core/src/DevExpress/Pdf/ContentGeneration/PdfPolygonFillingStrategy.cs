namespace DevExpress.Pdf.ContentGeneration
{
    using DevExpress.Pdf;
    using DevExpress.Pdf.Native;
    using System;
    using System.Drawing;
    using System.Runtime.CompilerServices;

    public class PdfPolygonFillingStrategy : PdfShapeFillingStrategy
    {
        private readonly PdfPoint[] points;
        private readonly PdfGraphicsPath path;
        private readonly bool nonZero;

        public PdfPolygonFillingStrategy(PointF[] points, PdfPoint[] transformedPoints, bool nonZero)
        {
            Converter<PointF, PdfPoint> converter = <>c.<>9__5_0;
            if (<>c.<>9__5_0 == null)
            {
                Converter<PointF, PdfPoint> local1 = <>c.<>9__5_0;
                converter = <>c.<>9__5_0 = point => new PdfPoint((double) point.X, (double) point.Y);
            }
            this.points = Array.ConvertAll<PointF, PdfPoint>(points, converter);
            this.path = new PdfGraphicsPath(transformedPoints[0]);
            int length = transformedPoints.Length;
            for (int i = 1; i < length; i++)
            {
                this.path.AppendLineSegment(transformedPoints[i]);
            }
            this.path.Closed = true;
            this.nonZero = nonZero;
        }

        public override void Clip(PdfCommandConstructor constructor)
        {
            constructor.IntersectClip(this.path, this.nonZero);
        }

        public override PdfPoint[] ShapePoints =>
            this.points;

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly PdfPolygonFillingStrategy.<>c <>9 = new PdfPolygonFillingStrategy.<>c();
            public static Converter<PointF, PdfPoint> <>9__5_0;

            internal PdfPoint <.ctor>b__5_0(PointF point) => 
                new PdfPoint((double) point.X, (double) point.Y);
        }
    }
}

