namespace DevExpress.Xpf.Layout.Core
{
    using System;
    using System.Windows;

    public static class GeometryHelper
    {
        public static Rect CorrectBounds(Rect bounds, Rect constraints, double threshold) => 
            CorrectBounds(bounds, constraints, new Thickness(threshold));

        public static Rect CorrectBounds(Rect bounds, Rect constraints, Thickness threshold)
        {
            Rect rect = bounds;
            if ((constraints.Width * constraints.Height) != 0.0)
            {
                double left = bounds.Left;
                double top = bounds.Top;
                if ((bounds.Left + threshold.Right) > constraints.Right)
                {
                    left -= (bounds.Left + threshold.Right) - constraints.Right;
                }
                if ((bounds.Top + threshold.Bottom) > constraints.Bottom)
                {
                    top -= (bounds.Top + threshold.Bottom) - constraints.Bottom;
                }
                if ((left + threshold.Left) < constraints.Left)
                {
                    left -= (left + threshold.Left) - constraints.Left;
                }
                if ((top + threshold.Top) < constraints.Top)
                {
                    top -= (top + threshold.Top) - constraints.Top;
                }
                rect = new Rect(left, top, bounds.Width, bounds.Height);
            }
            return rect;
        }

        public static Size CorrectSize(Size size, Size desired)
        {
            Size size2 = size;
            if ((desired.Width * desired.Height) != 0.0)
            {
                size2 = new Size(Math.Max(desired.Width, size.Width), Math.Max(desired.Height, size.Height));
            }
            return size2;
        }
    }
}

