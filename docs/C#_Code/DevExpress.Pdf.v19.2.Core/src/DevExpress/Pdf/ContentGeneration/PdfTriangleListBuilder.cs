namespace DevExpress.Pdf.ContentGeneration
{
    using DevExpress.Emf;
    using DevExpress.Pdf;
    using DevExpress.Pdf.Native;
    using System;
    using System.Collections.Generic;

    public abstract class PdfTriangleListBuilder
    {
        private static readonly PdfRange pdfColorRange = new PdfRange(0.0, 1.0);
        private static readonly PdfRange xRange = new PdfRange(-32768.0, 32767.0);
        private static readonly PdfRange yRange = new PdfRange(-32768.0, 32767.0);
        private const int bitsPerCoordinate = 0x10;
        private const int bitsPerComponent = 8;
        private const int bitsPerFlag = 8;
        private const int bezierTrianglesCount = 30;
        private readonly List<PdfTriangle> triangles = new List<PdfTriangle>();
        private readonly PdfPoint centerPoint;
        private readonly ARGBColor[] surroundColors;
        private int currentColorIndex;
        private PdfPoint subpathEndPoint;

        protected PdfTriangleListBuilder(PdfPoint centerPoint, ARGBColor[] surroundColors)
        {
            this.centerPoint = centerPoint;
            this.surroundColors = surroundColors;
        }

        private void AddTriangle(PdfPoint startPoint, PdfPoint endPoint, ARGBColor startColor, ARGBColor endColor)
        {
            this.subpathEndPoint = endPoint;
            this.triangles.AddRange(this.CreateTriangles(startPoint, endPoint, startColor, endColor));
        }

        public void AppendBezier(PdfPoint startPoint, PdfPoint controlPoint1, PdfPoint controlPoint2, PdfPoint EndPoint)
        {
            PdfPoint point = startPoint;
            for (double i = 0.0; i <= 1.0; i += 0.033333333333333333)
            {
                PdfPoint endPoint = new PdfPoint(CalculateBezierCurveCoordinate(startPoint.X, controlPoint1.X, controlPoint2.X, EndPoint.X, i), CalculateBezierCurveCoordinate(startPoint.Y, controlPoint1.Y, controlPoint2.Y, EndPoint.Y, i));
                this.AddTriangle(point, endPoint, this.surroundColors[this.currentColorIndex], this.GetNextSurroundColor());
                point = endPoint;
            }
        }

        public void AppendLine(PdfPoint startPoint, PdfPoint endPoint)
        {
            this.AddTriangle(startPoint, endPoint, this.surroundColors[this.currentColorIndex], this.GetNextSurroundColor());
        }

        protected static PdfColor BlendColor(PdfColor first, PdfColor second, double factor)
        {
            double[] components = first.Components;
            double[] numArray2 = second.Components;
            int length = components.Length;
            double[] numArray3 = new double[length];
            for (int i = 0; i < length; i++)
            {
                numArray3[i] = (components[i] * (1.0 - factor)) + (numArray2[i] * factor);
            }
            return new PdfColor(numArray3);
        }

        private static double CalculateBezierCurveCoordinate(double startCoordinate, double controlCoordinate1, double controlCoordinate2, double endCoordinate, double t) => 
            (((Math.Pow(1.0 - t, 3.0) * startCoordinate) + (((3.0 * t) * Math.Pow(1.0 - t, 2.0)) * controlCoordinate1)) + (((3.0 * Math.Pow(t, 2.0)) * (1.0 - t)) * controlCoordinate2)) + (Math.Pow(t, 3.0) * endCoordinate);

        public void ClosePath(PdfPoint endPoint, ARGBColor endColor)
        {
            this.AddTriangle(this.subpathEndPoint, endPoint, this.surroundColors[this.currentColorIndex], endColor);
        }

        public static PdfTriangleListBuilder Create(DXPathGradientBrush brush, PdfRectangle brushBbox)
        {
            DXColorBlend interpolationColors = brush.InterpolationColors;
            DXPointF? centerPoint = brush.CenterPoint;
            PdfPoint point = (centerPoint != null) ? new PdfPoint((double) centerPoint.Value.X, (double) centerPoint.Value.Y) : new PdfPoint((brushBbox.Width / 2.0) + brushBbox.Left, (brushBbox.Height / 2.0) + brushBbox.Top);
            if (interpolationColors != null)
            {
                ARGBColor[] colorArray = interpolationColors.Colors;
                int num = colorArray.Length;
                PdfColor[] colorArray2 = new PdfColor[num];
                for (int j = 0; j < num; j++)
                {
                    colorArray2[j] = PdfGraphicsConverter.ConvertColor(colorArray[j]);
                }
                return new PdfMulticolorGradientTriangleListBuilder(point, interpolationColors.Positions, colorArray2, brush.FocusScales, PdfGraphicsConverter.ConvertColor(colorArray[num - 1]));
            }
            if ((brush.SurroundColors.Length != 1) || ((brush.FocusScales.X == 0f) && (brush.FocusScales.Y == 0f)))
            {
                return new PdfDualColorGradientTriangleListBuilder(brush, point);
            }
            PdfColor first = PdfGraphicsConverter.ConvertColor(brush.SurroundColors[0]);
            PdfColor second = PdfGraphicsConverter.ConvertColor(brush.CenterColor);
            DXBlend blend = brush.Blend;
            double[] factors = blend.Factors;
            int length = factors.Length;
            PdfColor[] colors = new PdfColor[length];
            for (int i = 0; i < length; i++)
            {
                colors[i] = BlendColor(first, second, factors[i]);
            }
            return new PdfMulticolorGradientTriangleListBuilder(point, blend.Positions, colors, brush.FocusScales, second);
        }

        protected abstract PdfObjectList<PdfCustomFunction> CreateFunction(PdfDocumentCatalog documentCatalog);
        protected abstract IEnumerable<PdfTriangle> CreateTriangles(PdfPoint startPoint, PdfPoint endPoint, ARGBColor startColor, ARGBColor endColor);
        private ARGBColor GetNextSurroundColor()
        {
            if (this.currentColorIndex < (this.surroundColors.Length - 1))
            {
                this.currentColorIndex++;
            }
            return this.surroundColors[this.currentColorIndex];
        }

        public PdfShading GetShading(PdfDocumentCatalog documentCatalog)
        {
            IList<PdfRange> componentsRange = this.ComponentsRange;
            PdfDecodeRange[] decodeC = new PdfDecodeRange[componentsRange.Count];
            for (int i = 0; i < componentsRange.Count; i++)
            {
                decodeC[i] = new PdfDecodeRange(componentsRange[i].Min, componentsRange[i].Max, 8);
            }
            return new PdfFreeFormGouraudShadedTriangleMesh(this.CreateFunction(documentCatalog), 8, 0x10, 8, new PdfDecodeRange(xRange.Min, xRange.Max, 0x10), new PdfDecodeRange(yRange.Min, yRange.Max, 0x10), decodeC, this.triangles);
        }

        protected static PdfRange PdfColorRange =>
            pdfColorRange;

        protected abstract IList<PdfRange> ComponentsRange { get; }

        protected PdfPoint CenterPoint =>
            this.centerPoint;
    }
}

