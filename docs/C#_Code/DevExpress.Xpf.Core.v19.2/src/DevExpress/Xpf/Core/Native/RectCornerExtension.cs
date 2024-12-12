namespace DevExpress.Xpf.Core.Native
{
    using System;
    using System.Runtime.CompilerServices;

    public static class RectCornerExtension
    {
        public static RectCorner DiagonalMirror(this RectCorner corner);
        public static RectKeyPoint HorizontalMiddle(this RectCorner corner);
        public static RectCorner HorizontalMirror(this RectCorner corner);
        public static bool IsBottom(this RectCorner corner);
        public static bool IsLeft(this RectCorner corner);
        public static bool IsRight(this RectCorner corner);
        public static bool IsSameHorizontalSide(this RectCorner corner, RectCorner otherCorner);
        public static bool IsSameVerticalSide(this RectCorner corner, RectCorner otherCorner);
        public static bool IsTop(this RectCorner corner);
        public static RectKeyPoint VerticalMiddle(this RectCorner corner);
        public static RectCorner VerticalMirror(this RectCorner corner);
    }
}

