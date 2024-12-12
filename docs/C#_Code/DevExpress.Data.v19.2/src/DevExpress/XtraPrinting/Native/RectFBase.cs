namespace DevExpress.XtraPrinting.Native
{
    using System;
    using System.Drawing;

    public class RectFBase
    {
        public static RectangleF Center(RectangleF rect, RectangleF baseRect)
        {
            rect.Offset(baseRect.Left - rect.Left, baseRect.Top - rect.Top);
            float x = (baseRect.Width - rect.Width) / 2f;
            rect.Offset(x, (baseRect.Height - rect.Height) / 2f);
            return rect;
        }

        public static bool ContainsByX(RectangleF rect1, RectangleF rect2) => 
            (rect1.X <= rect2.X) && (rect2.Right <= rect1.Right);

        public static bool ContainsByY(RectangleF rect1, RectangleF rect2) => 
            (rect1.Y <= rect2.Y) && (rect2.Bottom <= rect1.Bottom);

        public static RectangleF FromPoints(PointF pt1, PointF pt2) => 
            RectangleF.FromLTRB(Math.Min(pt1.X, pt2.X), Math.Min(pt1.Y, pt2.Y), Math.Max(pt1.X, pt2.X), Math.Max(pt1.Y, pt2.Y));

        public static bool IntersectAbove(RectangleF baseRect, RectangleF rect) => 
            (baseRect.Top > rect.Top) && (baseRect.Top < rect.Bottom);

        public static bool IntersectBelow(RectangleF baseRect, RectangleF rect) => 
            (baseRect.Bottom > rect.Top) && (baseRect.Bottom < rect.Bottom);

        public static bool IntersectByX(RectangleF rect1, RectangleF rect2) => 
            (rect1.X < rect2.Right) && (rect2.X < rect1.Right);

        public static bool IntersectByY(RectangleF rect1, RectangleF rect2) => 
            (rect1.Y < rect2.Bottom) && (rect2.Y < rect1.Bottom);

        public static RectangleF Offset(RectangleF val, float dx, float dy)
        {
            val.Offset(dx, dy);
            return val;
        }

        public static Rectangle Round(RectangleF value) => 
            Rectangle.FromLTRB((int) Math.Round((double) value.Left), (int) Math.Round((double) value.Top), (int) Math.Round((double) value.Right), (int) Math.Round((double) value.Bottom));
    }
}

