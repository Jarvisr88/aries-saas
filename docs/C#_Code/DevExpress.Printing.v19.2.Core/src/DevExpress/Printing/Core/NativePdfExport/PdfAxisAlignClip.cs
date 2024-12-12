namespace DevExpress.Printing.Core.NativePdfExport
{
    using DevExpress.Pdf.ContentGeneration;
    using System;
    using System.Drawing;
    using System.Drawing.Drawing2D;

    public class PdfAxisAlignClip : PdfGraphicsClip
    {
        private readonly RectangleF clipRectangle;

        public PdfAxisAlignClip(RectangleF clipRectangle)
        {
            this.clipRectangle = clipRectangle;
        }

        public override void Apply(PdfGraphicsCommandConstructor constructor)
        {
            constructor.IntersectClipWithoutWorldTransform(this.clipRectangle);
        }

        public override PdfGraphicsClip ApplyTransform(Matrix matrix)
        {
            PointF[] pts = RectangleToPointArray(this.clipRectangle);
            matrix.TransformPoints(pts);
            return new PdfAxisAlignClip(CalculateBoundingRectangle(pts));
        }

        public override RectangleF GetBounds(Matrix boundsTransform)
        {
            if (boundsTransform.IsIdentity)
            {
                return this.clipRectangle;
            }
            PointF[] pts = RectangleToPointArray(this.clipRectangle);
            using (Matrix matrix = boundsTransform.Clone())
            {
                matrix.Invert();
                matrix.TransformPoints(pts);
            }
            return CalculateBoundingRectangle(pts);
        }

        public override PdfGraphicsClip Intersect(PdfGraphicsClip clip)
        {
            PdfAxisAlignClip clip2 = clip as PdfAxisAlignClip;
            return ((clip2 == null) ? ((PdfGraphicsClip) new PdfCompositeClip(this, clip)) : ((PdfGraphicsClip) new PdfAxisAlignClip(RectangleF.Intersect(this.clipRectangle, clip2.clipRectangle))));
        }
    }
}

