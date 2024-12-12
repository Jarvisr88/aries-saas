namespace DevExpress.Pdf.ContentGeneration
{
    using DevExpress.Emf;
    using DevExpress.Pdf;
    using DevExpress.Pdf.Native;
    using System;
    using System.Collections.Generic;

    public class PdfMulticolorGradientTriangleListBuilder : PdfTriangleListBuilder
    {
        private const double gradientStartColorValue = 0.0;
        private const double gradientEndColorValue = 0.5;
        private readonly double[] positions;
        private readonly PdfColor[] colors;
        private readonly DXPointF focusScales;
        private readonly PdfColor centerColor;

        public PdfMulticolorGradientTriangleListBuilder(PdfPoint centerPoint, double[] positions, PdfColor[] colors, DXPointF focusScales, PdfColor centerColor) : base(centerPoint, colorArray1)
        {
            ARGBColor[] colorArray1 = new ARGBColor[] { ARGBColor.FromArgb(0, 0, 0) };
            this.positions = positions;
            this.colors = colors;
            this.focusScales = focusScales;
            this.centerColor = centerColor;
        }

        protected override PdfObjectList<PdfCustomFunction> CreateFunction(PdfDocumentCatalog documentCatalog)
        {
            double num = 0.5;
            double[] bounds = new double[this.colors.Length - 1];
            PdfRange[] encode = new PdfRange[this.colors.Length];
            PdfObjectList<PdfCustomFunction> functions = new PdfObjectList<PdfCustomFunction>(documentCatalog.Objects);
            PdfRange[] range = new PdfRange[] { PdfTriangleListBuilder.PdfColorRange, PdfTriangleListBuilder.PdfColorRange, PdfTriangleListBuilder.PdfColorRange };
            PdfRange[] domain = new PdfRange[] { PdfTriangleListBuilder.PdfColorRange };
            for (int i = 0; i < (this.colors.Length - 1); i++)
            {
                if (i < (this.colors.Length - 2))
                {
                    bounds[i] = this.positions[i + 1] * num;
                }
                encode[i] = new PdfRange(0.0, 1.0);
                functions.Add(new PdfExponentialInterpolationFunction(this.colors[i].Components, this.colors[i + 1].Components, 1.0, domain, range));
            }
            encode[this.colors.Length - 1] = new PdfRange(0.0, 1.0);
            bounds[this.colors.Length - 2] = 0.5;
            functions.Add(new PdfExponentialInterpolationFunction(this.centerColor.Components, this.centerColor.Components, 1.0, domain, range));
            PdfStitchingFunction item = new PdfStitchingFunction(bounds, encode, functions, domain, range);
            PdfObjectList<PdfCustomFunction> list1 = new PdfObjectList<PdfCustomFunction>(documentCatalog.Objects);
            list1.Add(item);
            return list1;
        }

        protected override IEnumerable<PdfTriangle> CreateTriangles(PdfPoint startPoint, PdfPoint endPoint, ARGBColor startColor, ARGBColor endColor)
        {
            PdfColor color = new PdfColor(new double[1]);
            double[] components = new double[] { 0.5 };
            PdfColor color2 = new PdfColor(components);
            PdfVertex vertex = new PdfVertex(new PdfPoint(startPoint.X + ((base.CenterPoint.X - startPoint.X) * (1f - this.focusScales.X)), startPoint.Y + ((base.CenterPoint.Y - startPoint.Y) * (1f - this.focusScales.Y))), color2);
            PdfVertex vertex2 = new PdfVertex(new PdfPoint(endPoint.X + ((base.CenterPoint.X - endPoint.X) * (1f - this.focusScales.X)), endPoint.Y + ((base.CenterPoint.Y - endPoint.Y) * (1f - this.focusScales.Y))), color2);
            PdfTriangle[] triangleArray1 = new PdfTriangle[3];
            triangleArray1[0] = new PdfTriangle(new PdfVertex(startPoint, color), new PdfVertex(endPoint, color), vertex);
            triangleArray1[1] = new PdfTriangle(vertex2, new PdfVertex(endPoint, color), vertex);
            double[] numArray2 = new double[] { 1.0 };
            triangleArray1[2] = new PdfTriangle(vertex2, new PdfVertex(base.CenterPoint, new PdfColor(numArray2)), vertex);
            return triangleArray1;
        }

        protected override IList<PdfRange> ComponentsRange =>
            new PdfRange[] { PdfTriangleListBuilder.PdfColorRange };
    }
}

