namespace DevExpress.XtraPrinting.Export.Pdf
{
    using System;
    using System.Drawing;
    using System.Drawing.Drawing2D;

    internal static class LineCapHelper
    {
        private static LineCapDrawInfo DrawCapPath(PointF start, PointF end, float delta, GraphicsPath path, float scaledWidth)
        {
            TransformLineCap(ref start, end, ref path, delta);
            return new LineCapDrawInfo(false, start, path, scaledWidth);
        }

        private static LineCapDrawInfo DrawCustomCap(PointF start, PointF end, float width, CustomLineCap customCap)
        {
            if (!(customCap is AdjustableArrowCap))
            {
                return null;
            }
            AdjustableArrowCap cap = (AdjustableArrowCap) customCap;
            float scaledWidth = width * cap.WidthScale;
            float x = scaledWidth * cap.Height;
            float y = (scaledWidth * cap.Width) / 2f;
            float delta = (cap.Height / cap.Width) * scaledWidth;
            PointF[] points = new PointF[] { new PointF(x, y), new PointF(0f, 0f), new PointF(x, -y) };
            GraphicsPath path = new GraphicsPath();
            if (cap.Filled)
            {
                path.AddPolygon(points);
                return FillCapPath(start, end, delta, path);
            }
            path.AddLines(points);
            return DrawCapPath(start, end, delta, path, scaledWidth);
        }

        private static LineCapDrawInfo FillCapPath(PointF start, PointF end, float delta, GraphicsPath path)
        {
            TransformLineCap(ref start, end, ref path, delta);
            return new LineCapDrawInfo(true, start, path, 0f);
        }

        private static PointF[] GetArrowAnchor(float width, ref float delta)
        {
            float x = width * 1.7f;
            delta = 1.6f * width;
            return new PointF[] { new PointF(0f, 0f), new PointF(x, width), new PointF(x, -width) };
        }

        private static PointF[] GetDiamondAnchor(float width) => 
            new PointF[] { new PointF(-width, 0f), new PointF(0f, width), new PointF(width, 0f), new PointF(0f, -width) };

        private static GraphicsPath GetRoundAnchor(float width)
        {
            GraphicsPath path = new GraphicsPath();
            path.AddEllipse(-width, -width, 2f * width, 2f * width);
            return path;
        }

        private static PointF[] GetSquareAnchor(float width)
        {
            float x = -width * 0.7f;
            float y = -x;
            return new PointF[] { new PointF(x, x), new PointF(x, y), new PointF(y, y), new PointF(y, x) };
        }

        private static PointF[] GetTriangle(float width)
        {
            float y = width / 2f;
            float x = 0.1f * width;
            return new PointF[] { new PointF(x, y), new PointF(x, -y), new PointF(0f, -y), new PointF(-y, 0f), new PointF(0f, y) };
        }

        public static LineCapDrawInfo MakeCapInfo(PointF start, PointF end, float width, LineCap cap, CustomLineCap customCap)
        {
            float delta = 0f;
            GraphicsPath roundAnchor = null;
            if (cap == LineCap.Triangle)
            {
                roundAnchor = ToPolyPath(GetTriangle(width));
            }
            else
            {
                switch (cap)
                {
                    case LineCap.SquareAnchor:
                        roundAnchor = ToPolyPath(GetSquareAnchor(width));
                        break;

                    case LineCap.RoundAnchor:
                        roundAnchor = GetRoundAnchor(width);
                        break;

                    case LineCap.DiamondAnchor:
                        roundAnchor = ToPolyPath(GetDiamondAnchor(width));
                        break;

                    case LineCap.ArrowAnchor:
                        roundAnchor = ToPolyPath(GetArrowAnchor(width, ref delta));
                        break;

                    default:
                        if (cap != LineCap.Custom)
                        {
                            break;
                        }
                        return DrawCustomCap(start, end, width, customCap);
                }
            }
            return ((roundAnchor == null) ? null : FillCapPath(start, end, delta, roundAnchor));
        }

        private static GraphicsPath ToPolyPath(PointF[] points)
        {
            GraphicsPath path = new GraphicsPath();
            path.AddPolygon(points);
            return path;
        }

        private static void TransformLineCap(ref PointF start, PointF end, ref GraphicsPath path, float delta)
        {
            bool flag = path != null;
            bool flag2 = Math.Abs(delta) > 0.001;
            if (flag || flag2)
            {
                float num = end.X - start.X;
                float num2 = end.Y - start.Y;
                float num3 = (float) Math.Sqrt((double) ((num * num) + (num2 * num2)));
                using (Matrix matrix = new Matrix(num / num3, num2 / num3, -num2 / num3, num / num3, 0f, 0f))
                {
                    if (flag)
                    {
                        using (Matrix matrix2 = new Matrix())
                        {
                            matrix2.Translate(start.X, start.Y);
                            matrix2.Multiply(matrix);
                            PointF[] pathPoints = path.PathPoints;
                            matrix2.TransformPoints(pathPoints);
                            path = new GraphicsPath(pathPoints, path.PathTypes, path.FillMode);
                        }
                    }
                    if (flag2)
                    {
                        using (Matrix matrix3 = new Matrix())
                        {
                            matrix3.Multiply(matrix);
                            matrix3.Translate(delta, 0f);
                            matrix.Invert();
                            matrix3.Multiply(matrix);
                            PointF[] pts = new PointF[] { start };
                            matrix3.TransformPoints(pts);
                            start = pts[0];
                        }
                    }
                }
            }
        }

        public class LineCapDrawInfo
        {
            public bool fill;
            public PointF start;
            public GraphicsPath path;
            public float width;

            public LineCapDrawInfo(bool fill, PointF start, GraphicsPath path, float width)
            {
                this.fill = fill;
                this.start = start;
                this.path = path;
                this.width = width;
            }
        }
    }
}

