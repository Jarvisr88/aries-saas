namespace DevExpress.Office.Drawing
{
    using System;
    using System.Drawing;
    using System.Drawing.Drawing2D;

    public static class SnapToDevicePixelsHelper
    {
        public static RectangleF GetCorrectedBounds(Graphics graphics, Size sizeInPixel, RectangleF bounds)
        {
            PointF[] pts = new PointF[] { new PointF(bounds.Left, bounds.Top), new PointF(bounds.Right, bounds.Bottom) };
            graphics.TransformPoints(CoordinateSpace.Device, CoordinateSpace.World, pts);
            int num = (int) (pts[1].X - pts[0].X);
            int num2 = (int) (pts[1].Y - pts[0].Y);
            if ((Math.Abs((int) (sizeInPixel.Width - num)) > 5) || (Math.Abs((int) (sizeInPixel.Height - num2)) > 5))
            {
                return bounds;
            }
            pts[0].X = (int) (pts[0].X + 0.5);
            pts[0].Y = (int) (pts[0].Y + 0.5);
            pts[1].X = pts[0].X + sizeInPixel.Width;
            pts[1].Y = pts[0].Y + sizeInPixel.Height;
            graphics.TransformPoints(CoordinateSpace.World, CoordinateSpace.Device, pts);
            return new RectangleF(pts[0].X, pts[0].Y, pts[1].X - pts[0].X, pts[1].Y - pts[0].Y);
        }
    }
}

