namespace DevExpress.Printing.Core.NativePdfExport
{
    using DevExpress.Pdf.ContentGeneration;
    using System;
    using System.Drawing;
    using System.Drawing.Drawing2D;

    public abstract class PdfGraphicsClip
    {
        protected PdfGraphicsClip()
        {
        }

        public abstract void Apply(PdfGraphicsCommandConstructor constructor);
        public abstract PdfGraphicsClip ApplyTransform(Matrix matrix);
        protected static RectangleF CalculateBoundingRectangle(PointF[] points)
        {
            float maxValue = float.MaxValue;
            float num2 = float.MaxValue;
            float minValue = float.MinValue;
            float num4 = float.MinValue;
            foreach (PointF tf in points)
            {
                maxValue = Math.Min(maxValue, tf.X);
                num2 = Math.Min(num2, tf.Y);
                minValue = Math.Max(minValue, tf.X);
                num4 = Math.Max(num4, tf.Y);
            }
            return RectangleF.FromLTRB(maxValue, num2, minValue, num4);
        }

        public static PdfGraphicsClip Create(GraphicsPath path, Matrix transformationMatrix)
        {
            PointF[] pathPoints = path.PathPoints;
            if (!transformationMatrix.IsIdentity)
            {
                transformationMatrix.TransformPoints(pathPoints);
            }
            return new PdfGraphicsPathClip(pathPoints, path.PathTypes, path.FillMode == FillMode.Winding);
        }

        public static PdfGraphicsClip Create(RectangleF clipRectangle, Matrix transformationMatrix)
        {
            PointF[] pts = RectangleToPointArray(clipRectangle);
            bool isIdentity = transformationMatrix.IsIdentity;
            if (!isIdentity)
            {
                transformationMatrix.TransformPoints(pts);
            }
            if (isIdentity || IsNotRotated(transformationMatrix))
            {
                return new PdfAxisAlignClip(CalculateBoundingRectangle(pts));
            }
            return new PdfGraphicsPathClip(pts, new byte[] { 0, 1, 1, 0x81 }, true);
        }

        public abstract RectangleF GetBounds(Matrix boundsTransform);
        public abstract PdfGraphicsClip Intersect(PdfGraphicsClip clip);
        public static bool IsNotRotated(Matrix matrix)
        {
            float[] elements = matrix.Elements;
            return ((!IsZeroComponent((double) elements[0]) || !IsZeroComponent((double) elements[3])) ? (IsZeroComponent((double) elements[1]) && IsZeroComponent((double) elements[2])) : true);
        }

        private static bool IsZeroComponent(double component) => 
            Math.Abs(component) < 1E-06;

        protected static PointF[] RectangleToPointArray(RectangleF rect) => 
            new PointF[] { new PointF(rect.Left, rect.Top), new PointF(rect.Right, rect.Top), new PointF(rect.Right, rect.Bottom), new PointF(rect.Left, rect.Bottom) };
    }
}

