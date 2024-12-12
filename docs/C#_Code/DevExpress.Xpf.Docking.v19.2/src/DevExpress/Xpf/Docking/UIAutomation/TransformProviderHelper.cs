namespace DevExpress.Xpf.Docking.UIAutomation
{
    using System;
    using System.Windows;

    internal static class TransformProviderHelper
    {
        public static Point PointFromScreen(FrameworkElement target, Point point) => 
            target.PointFromScreen(point);

        public static Point PointToScreen(FrameworkElement target, Point point) => 
            target.PointToScreen(point);
    }
}

