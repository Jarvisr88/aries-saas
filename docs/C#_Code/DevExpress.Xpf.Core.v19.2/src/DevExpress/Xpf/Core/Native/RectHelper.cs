namespace DevExpress.Xpf.Core.Native
{
    using DevExpress.Xpf.Core;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public static class RectHelper
    {
        public static void AlignHorizontally(ref Rect rect, Rect area, HorizontalAlignment alignment);
        public static void AlignVertically(ref Rect rect, Rect area, VerticalAlignment alignment);
        public static bool AreClose(this Rect rect, Rect another);
        public static Point BottomLeft(this Rect rect);
        public static Point BottomRight(this Rect rect);
        public static void Deflate(ref Rect rect, Thickness padding);
        public static double GetSideOffset(this Rect rect, Side side);
        public static void IncLeft(ref Rect rect, double value);
        public static void IncTop(ref Rect rect, double value);
        public static void Inflate(ref Rect rect, Thickness padding);
        public static void Inflate(ref Rect rect, Side side, double value);
        public static void Inflate(ref Rect rect, double x, double y);
        public static Point Location(this Rect rect);
        public static Rect New(System.Windows.Size size);
        public static void Offset(ref Rect rect, double x, double y);
        public static Thickness Padding(Rect outsideRect, Rect insideRect);
        public static void SetBottom(ref Rect rect, double bottom);
        public static void SetLeft(ref Rect rect, double left);
        public static void SetLocation(ref Rect rect, Point location);
        public static void SetRight(ref Rect rect, double right);
        public static void SetSize(ref Rect rect, System.Windows.Size size);
        public static void SetTop(ref Rect rect, double top);
        public static System.Windows.Size Size(this Rect rect);
        public static void SnapToDevicePixels(ref Rect rect);
        public static Point TopLeft(this Rect rect);
        public static Point TopRight(this Rect rect);
        public static void Union(this Rect rect1, Rect rect2);
    }
}

