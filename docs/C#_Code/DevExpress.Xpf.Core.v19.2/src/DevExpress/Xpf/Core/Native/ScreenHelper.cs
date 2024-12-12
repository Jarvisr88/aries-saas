namespace DevExpress.Xpf.Core.Native
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Runtime.InteropServices;
    using System.Security;
    using System.Windows;
    using System.Windows.Forms;
    using System.Windows.Media;

    public class ScreenHelper : DependencyObject
    {
        private static readonly double standartDpi;
        private static readonly double dpiThicknessCorrection;
        private static readonly Func<Visual, object> getDpiScaleHandler;
        private static readonly Func<object, double> getDpiFromDpiScaleHandler;
        private static readonly Func<object, double> getDpiPerInchFromDpiScaleHandler;
        private static double? scaleXCore;
        private static double? dpiXCore;
        public static readonly DependencyProperty ScreenPaddingProperty;
        public static readonly DependencyProperty ScreenMarginProperty;

        static ScreenHelper();
        [SecuritySafeCritical]
        private static double? CalcPrimaryDpiX();
        [SecuritySafeCritical]
        private static double? CalcPrimaryScaleX();
        private static double CalcScaleX(Visual visual);
        public static bool ContainsPointsOnScreens(params Point[] points);
        public static Point GetClientLocation(Point globalLocation, FrameworkElement owner);
        [SecuritySafeCritical]
        public static Point GetDpi(Point monitorPoint);
        [DllImport("Shcore.dll")]
        private static extern IntPtr GetDpiForMonitor([In] IntPtr hmonitor, [In] DpiType dpiType, out uint dpiX, out uint dpiY);
        public static double GetDpiX(Visual visual);
        private static double GetMaxValue(double firstValue, double secondValue);
        private static double GetMinValue(double firstValue, double secondValue);
        private static void GetMoveToRect(Point point, List<Rect> rectScreens, ref double minX, ref double minY);
        public static Rect GetNearestScreenRect(Point point, bool checkContains = false);
        public static Point GetScaledPoint(Point point);
        public static Point GetScaledPoint(Point point, Visual visual);
        public static Point GetScaledPoint(Point point, Point dpi);
        public static Rect GetScaledRect(Rect rect);
        public static Rect GetScaledRect(Rect rect, Visual visual);
        public static Rect GetScaledRect(Rect rect, Point monitorPoint);
        public static Size GetScaledSize(Size size);
        public static Size GetScaledSize(Size size, Visual visual);
        public static Size GetScaledSize(Size size, Point dpi);
        public static double GetScaledValue(double value);
        public static double GetScaleX(Visual visual);
        private static Screen GetScreen(FrameworkElement element);
        private static Screen GetScreen(Point p);
        public static Point GetScreenLocation(Point localLocation, FrameworkElement owner);
        public static double GetScreenMargin(DependencyObject obj);
        public static double GetScreenPadding(DependencyObject obj);
        public static Point GetScreenPoint(FrameworkElement edit, Point offset = new Point());
        public static Rect GetScreenRect(IntPtr handle);
        private static Rect GetScreenRect(Screen screen);
        public static Rect GetScreenRect(FrameworkElement edit);
        public static Rect GetScreenRect(Point point);
        public static List<Rect> GetScreenRects();
        public static Rect GetScreenRectsUnion();
        private static Rect GetScreenWorkArea(Screen screen);
        public static Rect GetScreenWorkArea(FrameworkElement element);
        public static bool IsAttachedToPresentationSource(FrameworkElement owner);
        public static bool IsOnPrimaryScreen(Point point);
        [DllImport("User32.dll")]
        private static extern IntPtr MonitorFromPoint([In] Point pt, [In] uint dwFlags);
        public static void OnScreenMarginChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e);
        public static void OnScreenPaddingChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e);
        protected static Point PointToScreenCore(Point point, FrameworkElement baseElement);
        public static void SetScreenMargin(DependencyObject obj, double value);
        public static void SetScreenPadding(DependencyObject obj, double value);
        public static Point UpdateContainerLocation(Rect containerRect);

        public static double CurrentDpi { get; }

        public static double DpiThicknessCorrection { get; }

        public static double ScaleX { get; internal set; }

        public static double DpiX { get; internal set; }
    }
}

