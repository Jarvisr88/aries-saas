namespace DevExpress.Xpf.Layout.Core
{
    using System;
    using System.Windows;

    public static class PlacementHelper
    {
        public static Rect Arrange(Size size, Rect targetRect, Alignment alignment) => 
            (alignment != Alignment.Fill) ? new Rect(new Point(GetLeft(size.Width, targetRect, alignment), GetTop(size.Height, targetRect, alignment)), size) : targetRect;

        public static Rect Arrange(Size size, Rect targetRect, Alignment alignment, Point offset) => 
            (alignment != Alignment.Fill) ? new Rect(new Point(GetLeft(size.Width, targetRect, alignment) + offset.X, GetTop(size.Height, targetRect, alignment) + offset.Y), size) : targetRect;

        private static double GetLeft(double width, Rect targetRect, Alignment alignment)
        {
            double left = targetRect.Left;
            switch (alignment)
            {
                case Alignment.TopCenter:
                case Alignment.MiddleCenter:
                case Alignment.BottomCenter:
                    left = MathHelper.CenterRange(targetRect.Left, targetRect.Width, width);
                    break;

                case Alignment.TopRight:
                case Alignment.MiddleRight:
                case Alignment.BottomRight:
                    left = targetRect.Right - width;
                    break;

                default:
                    break;
            }
            return left;
        }

        private static double GetTop(double height, Rect targetRect, Alignment alignment)
        {
            double top = targetRect.Top;
            switch (alignment)
            {
                case Alignment.MiddleCenter:
                case Alignment.MiddleLeft:
                case Alignment.MiddleRight:
                    top = MathHelper.CenterRange(targetRect.Top, targetRect.Height, height);
                    break;

                case Alignment.BottomCenter:
                case Alignment.BottomLeft:
                case Alignment.BottomRight:
                    top = targetRect.Bottom - height;
                    break;

                default:
                    break;
            }
            return top;
        }
    }
}

