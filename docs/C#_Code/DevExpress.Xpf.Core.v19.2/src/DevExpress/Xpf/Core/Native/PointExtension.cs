namespace DevExpress.Xpf.Core.Native
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Windows;

    public static class PointExtension
    {
        public static Rect GetNearestRect(this Point point, IEnumerable<Rect> rects, bool checkContains = false);
        public static Rect GetNearestRect(this Point point, IEnumerable<Rect> rects, Func<Point, Rect, double> measure, bool checkContains = false);
        public static Point MiddlePoint(this Point point, Point otherPoint);
        public static Point MirrorPoint(this Point point, MathLine line);
        public static Point RadialEdgePoint(this Point startPoint, Point endPoint, double radius);

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly PointExtension.<>c <>9;
            public static Func<Point, Rect, double> <>9__3_0;

            static <>c();
            internal double <GetNearestRect>b__3_0(Point p, Rect r);
        }
    }
}

