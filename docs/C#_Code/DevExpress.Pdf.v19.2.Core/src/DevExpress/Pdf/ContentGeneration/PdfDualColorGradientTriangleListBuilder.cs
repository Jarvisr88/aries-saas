namespace DevExpress.Pdf.ContentGeneration
{
    using DevExpress.Emf;
    using DevExpress.Pdf;
    using DevExpress.Pdf.Native;
    using System;
    using System.Collections.Generic;

    public class PdfDualColorGradientTriangleListBuilder : PdfTriangleListBuilder
    {
        private const double lineStep = 0.5;
        private readonly PdfColor centerColor;
        private readonly DXBlend blend;

        public PdfDualColorGradientTriangleListBuilder(DXPathGradientBrush brush, PdfPoint centerPoint) : base(centerPoint, brush.SurroundColors)
        {
            this.blend = brush.Blend;
            this.centerColor = PdfGraphicsConverter.ConvertColor(brush.CenterColor);
        }

        protected override PdfObjectList<PdfCustomFunction> CreateFunction(PdfDocumentCatalog documentCatalog) => 
            null;

        protected override IEnumerable<PdfTriangle> CreateTriangles(PdfPoint startPoint, PdfPoint endPoint, ARGBColor startColor, ARGBColor endColor)
        {
            PdfColor first = PdfGraphicsConverter.ConvertColor(startColor);
            PdfColor color2 = PdfGraphicsConverter.ConvertColor(endColor);
            List<PdfTriangle> list = new List<PdfTriangle>();
            PdfPoint centerPoint = base.CenterPoint;
            PdfVertex a = new PdfVertex(startPoint, BlendColor(first, this.centerColor, this.blend.Factors[0]));
            PdfVertex b = new PdfVertex(endPoint, BlendColor(color2, this.centerColor, this.blend.Factors[0]));
            for (int i = 0; i < (this.blend.Positions.Length - 2); i++)
            {
                PdfPoint point = GetPoint(this.blend.Positions[i + 1], startPoint, base.CenterPoint);
                PdfPoint point3 = GetPoint(this.blend.Positions[i + 1], endPoint, base.CenterPoint);
                PdfColor color4 = BlendColor(first, this.centerColor, this.blend.Factors[i + 1]);
                PdfColor color5 = BlendColor(color2, this.centerColor, this.blend.Factors[i + 1]);
                PdfVertex c = new PdfVertex(point, color4);
                PdfVertex d = new PdfVertex(point3, color5);
                list.AddRange(this.DrawPolygon(a, b, c, d));
                a = c;
                b = d;
            }
            PdfColor color = BlendColor(BlendColor(first, color2, 0.5), this.centerColor, this.blend.Factors[this.blend.Factors.Length - 1]);
            list.Add(new PdfTriangle(a, new PdfVertex(base.CenterPoint, color), b));
            return list;
        }

        private IEnumerable<PdfTriangle> DrawPolygon(PdfVertex a, PdfVertex b, PdfVertex c, PdfVertex d)
        {
            List<PdfTriangle> list = new List<PdfTriangle>();
            double num2 = Math.Ceiling((double) (PdfMathUtils.Max(PdfPoint.Distance(a.Point, c.Point), PdfPoint.Distance(b.Point, d.Point)) / 0.5));
            PdfVertex vertex = a;
            PdfVertex vertex2 = b;
            for (int i = 1; i <= ((int) num2); i++)
            {
                double position = ((double) i) / num2;
                PdfVertex vertex3 = new PdfVertex(GetPoint(position, a.Point, c.Point), BlendColor(a.Color, c.Color, position));
                PdfVertex vertex4 = new PdfVertex(GetPoint(position, b.Point, d.Point), BlendColor(b.Color, d.Color, position));
                list.Add(new PdfTriangle(vertex3, vertex, vertex2));
                list.Add(new PdfTriangle(vertex3, vertex4, vertex2));
                vertex = vertex3;
                vertex2 = vertex4;
            }
            return list;
        }

        private static PdfPoint GetPoint(double position, PdfPoint startPoint, PdfPoint endPoint) => 
            new PdfPoint(startPoint.X + ((endPoint.X - startPoint.X) * position), startPoint.Y + ((endPoint.Y - startPoint.Y) * position));

        protected override IList<PdfRange> ComponentsRange =>
            new PdfRange[] { PdfTriangleListBuilder.PdfColorRange, PdfTriangleListBuilder.PdfColorRange, PdfTriangleListBuilder.PdfColorRange };
    }
}

