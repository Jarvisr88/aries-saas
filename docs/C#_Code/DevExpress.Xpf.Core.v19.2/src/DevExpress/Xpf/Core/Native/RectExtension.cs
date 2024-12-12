namespace DevExpress.Xpf.Core.Native
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public static class RectExtension
    {
        public static MathLine BottomLine(this Rect rect);
        public static Point BottomMiddle(this Rect rect);
        public static Point Center(this Rect rect);
        public static double CenterX(this Rect rect);
        public static double CenterY(this Rect rect);
        public static Point Corner(this Rect rect, RectCorner corner);
        public static bool IsInRange(double x, double min, double max);
        public static bool IsInside(this Rect rect, Point point);
        public static bool IsXInRange(this Rect rect, Point point);
        public static bool IsYInRange(this Rect rect, Point point);
        public static Point KeyPoint(this Rect rect, RectKeyPoint keyPoint);
        public static MathLine LeftLine(this Rect rect);
        public static Point LeftMiddle(this Rect rect);
        public static RectCorner NearestCorner(this Rect rect, Point point);
        public static MathLine RightLine(this Rect rect);
        public static Point RightMiddle(this Rect rect);
        public static MathLine TopLine(this Rect rect);
        public static Point TopMiddle(this Rect rect);
    }
}

