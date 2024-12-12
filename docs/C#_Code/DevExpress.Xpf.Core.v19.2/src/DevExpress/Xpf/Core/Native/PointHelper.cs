namespace DevExpress.Xpf.Core.Native
{
    using System;
    using System.Windows;

    public static class PointHelper
    {
        public static Point Abs(Point p);
        public static Point Add(Point p1, Point p2);
        public static Point Average(Point p1, Point p2);
        public static bool IsEmpty(Point p);
        public static Point Max(Point p1, Point p2);
        public static Point Min(Point p1, Point p2);
        public static Point Multiply(Point p, double by);
        public static Point Multiply(Point p1, Point p2);
        public static void Offset(ref Point p, double x, double y);
        public static Point Sign(Point p);
        public static Point Subtract(Point p1, Point p2);

        public static Point Empty { get; }
    }
}

