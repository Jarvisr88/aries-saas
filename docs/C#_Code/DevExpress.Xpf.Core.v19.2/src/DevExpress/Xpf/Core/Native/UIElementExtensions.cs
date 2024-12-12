namespace DevExpress.Xpf.Core.Native
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Input;

    public static class UIElementExtensions
    {
        public static Rect InvisibleBounds;

        static UIElementExtensions();
        public static void AddMouseUpHandler(this UIElement element, MouseButtonEventHandler handler);
        public static bool Focus(this UIElement element);
        public static Size GetDesiredSize(this UIElement element);
        public static bool GetIsKeyboardFocusWithin(this UIElement element);
        public static FrameworkElement GetRootVisual(this UIElement element);
        public static double GetRoundedSize(double size);
        public static Size GetRoundedSize(Size size);
        public static bool GetVisible(this UIElement element);
        public static bool HasDefaultRenderTransform(this UIElement element);
        public static void InvalidateParentsOfModifiedChildren(this UIElement element);
        public static Point MapPoint(this UIElement element, Point p, UIElement destination);
        public static Point MapPointFromScreen(this UIElement element, Point p);
        public static Rect MapRect(this UIElement element, Rect rect, UIElement destination);
        public static Rect MapRectFromScreen(this UIElement element, Rect rect);
        public static void RemoveMouseUpHandler(this UIElement element, MouseButtonEventHandler handler);
        public static void SetVisible(this UIElement element, bool visible);
    }
}

