namespace DevExpress.XtraPrinting.Native
{
    using DevExpress.XtraPrinting;
    using System;
    using System.Drawing;
    using System.Runtime.InteropServices;

    public class RectHelper
    {
        public static RectangleF AdjustBorderRect(RectangleF rect, BorderSide sides, float borderWidth, BrickBorderStyle borderStyle);
        public static Rectangle AlignRectangle(Rectangle rect, Rectangle baseRect, ContentAlignment alignment);
        public static RectangleF AlignRectangleF(RectangleF rect, RectangleF baseRect, ContentAlignment alignment);
        public static PointF BottomLeft(RectangleF rect);
        public static PointF BottomRight(RectangleF rect);
        public static Size Ceiling(SizeF value);
        public static Rectangle CeilingVertical(RectangleF value);
        public static Point CenterOf(Rectangle rect);
        public static PointF CenterOf(RectangleF rect);
        public static RectangleF DeflateRect(RectangleF rect, MarginsF m);
        public static Rectangle DeflateRect(Rectangle rect, int left = 0, int top = 0, int right = 0, int bottom = 0);
        public static RectangleF DeflateRect(RectangleF rect, float left = 0f, float top = 0f, float right = 0f, float bottom = 0f);
        private static bool FirstLessOrEqualSecond(float first, float second, float epsilon);
        private static bool FirstLessSecond(float first, float second, float epsilon);
        public static RectangleF InflateRect(RectangleF rect, MarginsF m);
        public static RectangleF InflateRect(RectangleF rect, float width, BorderSide borders);
        public static Rectangle InflateRect(Rectangle rect, int left = 0, int top = 0, int right = 0, int bottom = 0);
        public static RectangleF InflateRect(RectangleF rect, float left = 0f, float top = 0f, float right = 0f, float bottom = 0f);
        public static RectangleF InflateRect(RectangleF rect, float left, float top, float right, float bottom, BorderSide borders);
        public static Rectangle InflateRectFToInteger(RectangleF rect);
        public static bool IsEqual(Rectangle[] arr1, Rectangle[] arr2);
        public static Rectangle OffsetRect(Rectangle rect, Point pos);
        public static Rectangle OffsetRect(Rectangle rect, int x, int y);
        public static RectangleF OffsetRectF(RectangleF rect, PointF pos);
        public static RectangleF OffsetRectF(RectangleF rect, float x, float y);
        public static bool RectangleFContains(RectangleF rect, PointF pt, int digits);
        public static bool RectangleFContains(RectangleF rect1, RectangleF rect2, float epsilon);
        public static bool RectangleFEquals(RectangleF rect1, RectangleF rect2, double epsilon);
        public static RectangleF RectangleFFromPoints(PointF pt1, PointF pt2);
        public static bool RectangleFIntersects(RectangleF rect1, RectangleF rect2, float epsilon);
        public static bool RectangleFIsEmpty(RectangleF rect, double epsilon);
        public static bool RectangleFIsEmptySize(RectangleF rect, double epsilon);
        public static Rectangle RectangleFromPoints(Point pt1, Point pt2);
        public static RectangleF SnapRectangle(RectangleF rect, float dpi, float snapDpi);
        public static RectangleF SnapRectangleHorizontal(RectangleF rect, float dpi, float snapDpi);
        public static PointF TopLeft(RectangleF rect);
        public static PointF TopRight(RectangleF rect);
        public static Rectangle ValidateRect(Rectangle r);
        public static RectangleF ValidateRectF(RectangleF r);

        public static Rectangle[] EmptyArray { get; }
    }
}

