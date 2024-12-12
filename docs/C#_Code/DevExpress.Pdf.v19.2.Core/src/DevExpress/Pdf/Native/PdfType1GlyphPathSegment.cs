namespace DevExpress.Pdf.Native
{
    using DevExpress.Pdf;
    using System;
    using System.Collections.Generic;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct PdfType1GlyphPathSegment
    {
        private readonly IList<PdfPoint> points;
        private readonly byte code;
        public static PdfType1GlyphPathSegment CreateLineTo(double dx, double dy)
        {
            PdfPoint[] points = new PdfPoint[] { new PdfPoint(dx, dy) };
            return new PdfType1GlyphPathSegment(points, 5);
        }

        public static PdfType1GlyphPathSegment CreateMoveTo(double dx, double dy)
        {
            PdfPoint[] points = new PdfPoint[] { new PdfPoint(dx, dy) };
            return new PdfType1GlyphPathSegment(points, 0x15);
        }

        public static PdfType1GlyphPathSegment CreateCurveTo(double dx1, double dy1, double dx2, double dy2, double dx3, double dy3)
        {
            PdfPoint[] points = new PdfPoint[] { new PdfPoint(dx1, dy1), new PdfPoint(dx2, dy2), new PdfPoint(dx3, dy3) };
            return new PdfType1GlyphPathSegment(points, 8);
        }

        public static PdfType1GlyphPathSegment CreateFlexSegment(IList<PdfPoint> flexPoints)
        {
            PdfPoint point = new PdfPoint();
            foreach (PdfPoint point2 in flexPoints)
            {
                point = PdfPoint.Add(point, point2);
            }
            PdfPoint[] points = new PdfPoint[] { point };
            return new PdfType1GlyphPathSegment(points, 5);
        }

        private PdfType1GlyphPathSegment(IList<PdfPoint> points, byte code)
        {
            this.points = points;
            this.code = code;
        }

        public void Write(PdfType2CharstringBinaryWriter stream, IDictionary<string, PdfType1GlyphDescription> fontGlyphDescriptions)
        {
            foreach (PdfPoint point in this.points)
            {
                stream.WritePoint(point);
            }
            stream.WriteByte(this.code);
        }

        public PdfPoint MovePoint(PdfPoint point)
        {
            foreach (PdfPoint point2 in this.points)
            {
                point = PdfPoint.Add(point, point2);
            }
            return point;
        }
    }
}

