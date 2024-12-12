namespace DevExpress.Printing.Core.NativePdfExport
{
    using DevExpress.Pdf.ContentGeneration;
    using System;
    using System.Drawing;
    using System.Drawing.Drawing2D;

    public class PdfGraphicsPathClip : PdfGraphicsClip
    {
        private readonly PointF[] points;
        private readonly byte[] types;
        private readonly bool isWindingFillMode;

        public PdfGraphicsPathClip(PointF[] points, byte[] types, bool isWindingFillMode)
        {
            this.points = points;
            this.types = types;
            this.isWindingFillMode = isWindingFillMode;
        }

        public override void Apply(PdfGraphicsCommandConstructor constructor)
        {
            constructor.IntersectClipWithoutWorldTransform(this.points, this.types, this.isWindingFillMode);
        }

        public override PdfGraphicsClip ApplyTransform(Matrix matrix)
        {
            PointF[] pts = (PointF[]) this.points.Clone();
            matrix.TransformPoints(pts);
            return new PdfGraphicsPathClip(pts, this.types, this.isWindingFillMode);
        }

        public override RectangleF GetBounds(Matrix boundsTransform)
        {
            if (boundsTransform.IsIdentity)
            {
                return CalculateBoundingRectangle(this.points);
            }
            PointF[] pts = (PointF[]) this.points.Clone();
            using (Matrix matrix = boundsTransform.Clone())
            {
                matrix.Invert();
                matrix.TransformPoints(pts);
            }
            return CalculateBoundingRectangle(pts);
        }

        public override PdfGraphicsClip Intersect(PdfGraphicsClip clip) => 
            new PdfCompositeClip(this, clip);
    }
}

