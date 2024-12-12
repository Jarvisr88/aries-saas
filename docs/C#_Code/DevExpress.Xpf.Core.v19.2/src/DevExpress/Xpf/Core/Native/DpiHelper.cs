namespace DevExpress.Xpf.Core.Native
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Media;

    internal static class DpiHelper
    {
        [ThreadStatic]
        private static Matrix transformToDevice;
        [ThreadStatic]
        private static Matrix transformToDip;

        public static Point DevicePixelsToLogical(Point devicePoint, double dpiScaleX, double dpiScaleY);
        public static Rect DeviceRectToLogical(Rect deviceRectangle, double dpiScaleX, double dpiScaleY);
        public static Size DeviceSizeToLogical(Size deviceSize, double dpiScaleX, double dpiScaleY);
        public static double GetScaledValue(this double value, double dpi);
        public static Point LogicalPixelsToDevice(Point logicalPoint, double dpiScaleX, double dpiScaleY);
        public static Rect LogicalRectToDevice(Rect logicalRectangle, double dpiScaleX, double dpiScaleY);
        public static Size LogicalSizeToDevice(Size logicalSize, double dpiScaleX, double dpiScaleY);
        public static Thickness LogicalThicknessToDevice(Thickness logicalThickness, double dpiScaleX, double dpiScaleY);
    }
}

