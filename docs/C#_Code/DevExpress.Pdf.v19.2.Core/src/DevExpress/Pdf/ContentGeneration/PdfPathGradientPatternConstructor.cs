namespace DevExpress.Pdf.ContentGeneration
{
    using DevExpress.Emf;
    using DevExpress.Pdf;
    using DevExpress.Pdf.Native;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class PdfPathGradientPatternConstructor
    {
        private readonly DXPathGradientBrush brush;
        private readonly PdfTransformationMatrix actualBrushTransform;

        public PdfPathGradientPatternConstructor(DXPathGradientBrush brush, PdfRectangle bBox, PdfTransformationMatrix actualBrushTransform)
        {
            this.brush = brush;
            this.actualBrushTransform = actualBrushTransform;
        }

        public PdfPattern CreatePattern(PdfDocumentCatalog documentCatalog)
        {
            PdfPoint local2;
            DXGraphicsPathData path = this.brush.Path;
            Converter<DXPointF, PdfPoint> converter = <>c.<>9__3_0;
            if (<>c.<>9__3_0 == null)
            {
                Converter<DXPointF, PdfPoint> local1 = <>c.<>9__3_0;
                converter = <>c.<>9__3_0 = p => new PdfPoint((double) p.X, (double) p.Y);
            }
            IList<PdfPoint> points = Array.ConvertAll<DXPointF, PdfPoint>(path.Points, converter);
            PdfRectangle brushBbox = (points.Count == 0) ? new PdfRectangle(0.0, 0.0, 0.0, 0.0) : PdfRectangle.CreateBoundingBox(PdfBrushContainer.GetTransformationMatrix(this.brush).Transform(points));
            PdfTriangleListBuilder builder = PdfTriangleListBuilder.Create(this.brush, brushBbox);
            int count = points.Count;
            if (count > 0)
            {
                local2 = points[0];
            }
            else
            {
                local2 = new PdfPoint();
            }
            PdfPoint startPoint = local2;
            int index = 1;
            while (index < count)
            {
                DXPathPointTypes types = path.PathTypes[index];
                switch (types)
                {
                    case DXPathPointTypes.StartSubPathPoint:
                    case DXPathPointTypes.LineEndPoint:
                        builder.AppendLine(startPoint, points[index]);
                        break;

                    case DXPathPointTypes.BezierControlPoint:
                        builder.AppendBezier(startPoint, points[index++], points[index++], points[index]);
                        break;

                    default:
                        break;
                }
                startPoint = points[index++];
            }
            if (count > 0)
            {
                builder.ClosePath(points[0], this.brush.SurroundColors[0]);
            }
            return PdfBrushPatternConstructor.CreatePattern(this.brush.WrapMode, this.actualBrushTransform, brushBbox, builder.GetShading(documentCatalog), null, documentCatalog, new PdfTransformationMatrix());
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly PdfPathGradientPatternConstructor.<>c <>9 = new PdfPathGradientPatternConstructor.<>c();
            public static Converter<DXPointF, PdfPoint> <>9__3_0;

            internal PdfPoint <CreatePattern>b__3_0(DXPointF p) => 
                new PdfPoint((double) p.X, (double) p.Y);
        }
    }
}

