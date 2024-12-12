namespace DevExpress.Xpf.Layout.Core
{
    using System;
    using System.Windows;

    public static class DockHelper
    {
        private static double factor = 0.3;

        public static Rect GetBottomDock(Rect r, double preferredHeight) => 
            new Rect(r.Left, r.Bottom - GetHeight(r, preferredHeight), r.Width, GetHeight(r, preferredHeight));

        public static Rect GetDockRect(Rect rect, DockType type) => 
            GetDockRect(rect, new Size(0.0, 0.0), type);

        public static Rect GetDockRect(Rect rect, Size preferredSize, DockType type)
        {
            Rect empty = Rect.Empty;
            switch (type)
            {
                case DockType.Left:
                    empty = GetLeftDock(rect, preferredSize.Width);
                    break;

                case DockType.Right:
                    empty = GetRightDock(rect, preferredSize.Width);
                    break;

                case DockType.Top:
                    empty = GetTopDock(rect, preferredSize.Height);
                    break;

                case DockType.Bottom:
                    empty = GetBottomDock(rect, preferredSize.Height);
                    break;

                case DockType.Fill:
                    empty = rect;
                    break;

                default:
                    break;
            }
            return empty;
        }

        private static double GetHeight(Rect r, double preferredHeight) => 
            MathHelper.IsZero(preferredHeight) ? (r.Height * factor) : preferredHeight;

        public static Rect GetLeftDock(Rect r, double preferredWidth) => 
            new Rect(r.Left, r.Top, GetWidth(r, preferredWidth), r.Height);

        public static Rect GetRightDock(Rect r, double preferredWidth) => 
            new Rect(r.Right - GetWidth(r, preferredWidth), r.Top, GetWidth(r, preferredWidth), r.Height);

        public static Rect GetTopDock(Rect r, double preferredHeight) => 
            new Rect(r.Left, r.Top, r.Width, GetHeight(r, preferredHeight));

        private static double GetWidth(Rect r, double preferredWidth) => 
            MathHelper.IsZero(preferredWidth) ? (r.Width * factor) : preferredWidth;
    }
}

