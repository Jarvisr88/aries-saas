namespace DevExpress.Xpf.Core.Native
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public static class PointExtensions
    {
        public static double Distance(this Point point, Point otherPoint);
        private static FrameworkElement GetRootVisual(DependencyObject obj);
        public static Point ToLocal(this Point pt, object from);
        public static Point ToLocalSafe(this Point pt, object from);
        public static Point ToRootVisual(this Point pt, object from);
        public static Point ToRootVisualSafe(this Point pt, object from);
    }
}

