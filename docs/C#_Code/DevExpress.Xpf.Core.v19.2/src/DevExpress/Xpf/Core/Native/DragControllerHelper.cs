namespace DevExpress.Xpf.Core.Native
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Security;
    using System.Windows;
    using System.Windows.Interop;
    using System.Windows.Media;

    public static class DragControllerHelper
    {
        public static double DragOutHorizontalDistance;
        public static double DragOutVerticalDistance;
        private const uint GW_HWNDNEXT = 2;
        private const int WS_EX_TRANSPARENT = 0x20;
        private const int GWL_EXSTYLE = -20;
        private const int WS_EX_LAYERED = 0x80000;

        static DragControllerHelper();
        public static int FindDragOverIndex(IDragPanel dragPanel, FrameworkElement visualPanel, Point pointOnPanel);
        public static T FindDragPanel<T>(Window w, Point pointOnScreen) where T: FrameworkElement;
        public static Rect GetCurrentScreenWorkingArea();
        [return: MarshalAs(UnmanagedType.Bool)]
        [DllImport("user32.dll")]
        private static extern bool GetCursorPos(ref DragControllerHelper.Win32Point pt);
        [SecuritySafeCritical]
        public static Point GetMousePositionOnScreen();
        [DllImport("user32.dll")]
        private static extern IntPtr GetTopWindow(IntPtr hWnd);
        [DllImport("user32.dll", SetLastError=true)]
        private static extern IntPtr GetWindow(IntPtr hWnd, uint uCmd);
        [DllImport("user32.dll")]
        private static extern int GetWindowLong(IntPtr hwnd, int index);
        [SecuritySafeCritical]
        public static Window GetWindowUnderMouse(Point pointOnScreen);
        public static bool IsPointInsidePanel(FrameworkElement panel, Point pointOnPanel, bool useCoefs);
        public static bool IsPointOutOfPanel(FrameworkElement panel, Point pointOnPanel, bool useCoefs);
        [SecuritySafeCritical]
        public static void SetupDragWidget(Window dragWidget);
        [DllImport("user32.dll")]
        private static extern int SetWindowLong(IntPtr hwnd, int index, int newStyle);

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly DragControllerHelper.<>c <>9;
            public static Func<HwndSource, Visual> <>9__8_0;

            static <>c();
            internal Visual <GetWindowUnderMouse>b__8_0(HwndSource x);
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct Win32Point
        {
            public int X;
            public int Y;
        }
    }
}

