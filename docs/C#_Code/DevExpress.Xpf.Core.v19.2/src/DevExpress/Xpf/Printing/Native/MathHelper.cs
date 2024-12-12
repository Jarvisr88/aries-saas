namespace DevExpress.Xpf.Printing.Native
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Windows;

    internal static class MathHelper
    {
        public const double Epsilon = 1E-05;

        public static bool AreEqual(double x, double y, double delta = 1E-05) => 
            Math.Abs((double) (x - y)) < delta;

        public static bool AreEqual(Point p1, Point p2, double delta = 1E-05) => 
            AreEqual(p1.X, p2.X, delta) && AreEqual(p1.Y, p2.Y, delta);

        public static Tuple<bool, bool> AreInsideSegment(Point p1, Point p2, Tuple<Point, Point> segmentPoints) => 
            new Tuple<bool, bool>(PointInsideSegment(p1, segmentPoints.Item1, segmentPoints.Item2), PointInsideSegment(p2, segmentPoints.Item1, segmentPoints.Item2));

        public static Tuple<Point, Point> GetBoundPointsToTransform(Point startPoint, Point endPoint)
        {
            double num = -(endPoint.X - startPoint.X) / (endPoint.Y - startPoint.Y);
            double num2 = (-num * endPoint.X) + endPoint.Y;
            double y = (num * startPoint.X) + num2;
            return new Tuple<Point, Point>(new Point((startPoint.Y - num2) / num, startPoint.Y), new Point(startPoint.X, y));
        }

        public static double GetDistance(Point p1, Point p2) => 
            Math.Sqrt(Math.Pow(p2.X - p1.X, 2.0) + Math.Pow(p2.Y - p1.Y, 2.0));

        public static Point GetNormalIntersectionPoint(Point p1, Point p2, Point linePoint)
        {
            if (p2.Y == p1.Y)
            {
                return new Point(linePoint.X, p1.Y);
            }
            if (p2.X == p1.X)
            {
                return new Point(p1.X, linePoint.Y);
            }
            double num = (p2.Y - p1.Y) / (p2.X - p1.X);
            double num2 = (-num * p1.X) + p1.Y;
            double x = ((linePoint.Y + ((1.0 / num) * linePoint.X)) - num2) / (num + (1.0 / num));
            return new Point(x, (num * x) + num2);
        }

        public static Tuple<Point, Point> GetVectorPoints(Point startPoint, Point endPoint, Rect bounds)
        {
            Point bottomLeft = bounds.BottomLeft;
            Point topRight = bounds.TopRight;
            if ((endPoint.X > startPoint.X) && (endPoint.Y >= startPoint.Y))
            {
                bottomLeft = bounds.TopLeft;
                topRight = bounds.BottomRight;
            }
            else if ((endPoint.X <= startPoint.X) && (endPoint.Y > startPoint.Y))
            {
                bottomLeft = bounds.TopRight;
                topRight = bounds.BottomLeft;
            }
            else if ((endPoint.X < startPoint.X) && (endPoint.Y <= startPoint.Y))
            {
                bottomLeft = bounds.BottomRight;
                topRight = bounds.TopLeft;
            }
            return new Tuple<Point, Point>(GetNormalIntersectionPoint(startPoint, endPoint, bottomLeft), GetNormalIntersectionPoint(startPoint, endPoint, topRight));
        }

        public static bool IsValid(this Rect rect) => 
            (rect != Rect.Empty) && ((rect.Width != 0.0) && !(rect.Height == 0.0));

        private static bool PointInsideSegment(Point p, Point start, Point end) => 
            (GetDistance(p, start) + GetDistance(p, end)) <= GetDistance(start, end);
    }
}

