namespace DevExpress.Pdf.ContentGeneration
{
    using DevExpress.Pdf;
    using DevExpress.Pdf.Native;
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Runtime.CompilerServices;

    public class PdfPathFillingStrategy : PdfShapeFillingStrategy
    {
        private readonly PdfPoint[] points;
        private readonly IList<PdfGraphicsPath> transformedPaths;
        private readonly bool nonZero;

        public PdfPathFillingStrategy(PointF[] points, IList<PdfGraphicsPath> transformedPaths, bool nonZero)
        {
            Converter<PointF, PdfPoint> converter = <>c.<>9__5_0;
            if (<>c.<>9__5_0 == null)
            {
                Converter<PointF, PdfPoint> local1 = <>c.<>9__5_0;
                converter = <>c.<>9__5_0 = point => new PdfPoint((double) point.X, (double) point.Y);
            }
            this.points = Array.ConvertAll<PointF, PdfPoint>(points, converter);
            this.transformedPaths = transformedPaths;
            this.nonZero = nonZero;
        }

        public override void Clip(PdfCommandConstructor constructor)
        {
            constructor.IntersectClip(this.transformedPaths, this.nonZero);
        }

        public override PdfPoint[] ShapePoints =>
            this.points;

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly PdfPathFillingStrategy.<>c <>9 = new PdfPathFillingStrategy.<>c();
            public static Converter<PointF, PdfPoint> <>9__5_0;

            internal PdfPoint <.ctor>b__5_0(PointF point) => 
                new PdfPoint((double) point.X, (double) point.Y);
        }
    }
}

