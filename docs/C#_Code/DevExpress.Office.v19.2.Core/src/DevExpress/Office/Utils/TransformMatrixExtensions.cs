namespace DevExpress.Office.Utils
{
    using System;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Runtime.CompilerServices;

    public static class TransformMatrixExtensions
    {
        public static Matrix CreateFlipTransformUnsafe(RectangleF bounds, bool flipH, bool flipV)
        {
            if (!flipH && !flipV)
            {
                return null;
            }
            float dx = flipH ? ((2f * bounds.X) + bounds.Width) : 0f;
            return new Matrix(flipH ? ((float) (-1)) : ((float) 1), 0f, 0f, flipV ? ((float) (-1)) : ((float) 1), dx, flipV ? ((2f * bounds.Y) + bounds.Height) : 0f);
        }

        public static Matrix CreateFlipTransformUnsafe(RectangleF bounds, bool flipH, bool flipV, float angle)
        {
            angle = angle % 360f;
            if ((angle == 0f) && (!flipH && !flipV))
            {
                return null;
            }
            Matrix matrix = new Matrix();
            if (angle != 0f)
            {
                matrix.RotateAt(angle, RectangleUtils.CenterPoint(bounds));
            }
            if (flipH | flipV)
            {
                RectangleF ef = RectangleUtils.BoundingRectangle(bounds, angle);
                float dx = flipH ? ((2f * ef.X) + ef.Width) : 0f;
                float dy = flipV ? ((2f * ef.Y) + ef.Height) : 0f;
                Matrix matrix2 = new Matrix(flipH ? ((float) (-1)) : ((float) 1), 0f, 0f, flipV ? ((float) (-1)) : ((float) 1), dx, dy);
                matrix.Multiply(matrix2);
                matrix2.Dispose();
            }
            return matrix;
        }

        public static Matrix CreateTransformUnsafe(float angle, Rectangle bounds)
        {
            if ((angle % 360f) == 0f)
            {
                return null;
            }
            Matrix matrix = new Matrix();
            matrix.RotateAt(angle, (PointF) RectangleUtils.CenterPoint(bounds));
            return matrix;
        }

        public static Matrix CreateTransformUnsafe(float angle, RectangleF bounds)
        {
            if ((angle % 360f) == 0f)
            {
                return null;
            }
            Matrix matrix = new Matrix();
            matrix.RotateAt(angle, RectangleUtils.CenterPoint(bounds));
            return matrix;
        }

        public static void InvertSafe(this Matrix matrix)
        {
            if (matrix.IsInvertible)
            {
                matrix.Invert();
            }
        }

        public static void RotateAtExt(this Matrix matrix, float angle, PointF point)
        {
            matrix.RotateAtExt(angle, point, MatrixOrder.Prepend);
        }

        public static void RotateAtExt(this Matrix matrix, float angle, PointF point, MatrixOrder order)
        {
            if (angle != 0f)
            {
                matrix.RotateAt(angle, point, order);
            }
        }

        public static Point TransformPoint(this Matrix matrix, Point point)
        {
            Point[] pts = new Point[] { point };
            matrix.TransformPoints(pts);
            return pts[0];
        }

        public static PointF TransformPoint(this Matrix matrix, PointF point)
        {
            PointF[] pts = new PointF[] { point };
            matrix.TransformPoints(pts);
            return pts[0];
        }
    }
}

