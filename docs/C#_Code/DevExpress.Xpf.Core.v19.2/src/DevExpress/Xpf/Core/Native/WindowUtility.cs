namespace DevExpress.Xpf.Core.Native
{
    using DevExpress.Xpf.Core;
    using System;
    using System.Collections;
    using System.Drawing;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Security;
    using System.Text;
    using System.Windows;
    using System.Windows.Media;

    internal static class WindowUtility
    {
        public static void AddDependencyPropertyChangeListener(object component, DependencyProperty property, EventHandler listener);
        [DllImport("user32.dll", SetLastError=true)]
        private static extern int DestroyIcon(IntPtr hIcon);
        [DllImport("shell32.dll", CharSet=CharSet.Auto)]
        private static extern uint ExtractIconEx(string szFileName, int nIconIndex, IntPtr[] phiconLarge, IntPtr[] phiconSmall, uint nIcons);
        public static void ForWindowFromTemplate(this object templateFrameworkElement, Action<Window> action);
        public static int GET_X_LPARAM(IntPtr lParam);
        public static int GET_Y_LPARAM(IntPtr lParam);
        [SecuritySafeCritical]
        public static Window GetActiveWindow();
        public static DpiScale GetDpi(Window window);
        [SecuritySafeCritical]
        public static void GetIcons(out ImageSource[] largeIcons, out ImageSource[] smallIcons);
        private static int GetIntUnchecked(IntPtr value);
        [SecuritySafeCritical]
        public static bool GetLastOwnedWindowHwnd(IntPtr hwndOwner, IntPtr hwnd, ref IntPtr lastOwnedWindow);
        [DllImport("kernel32.dll", CharSet=CharSet.Auto)]
        public static extern int GetModuleFileName(IntPtr hModule, StringBuilder buffer, int length);
        [SecuritySafeCritical]
        public static ImageSource GetSystemIcon(SHStockIconID iconId = 2, SHGSI iconSize = 1);
        [SecuritySafeCritical]
        public static WINDOWINFO GetWindowInfo(IntPtr hWnd);
        [SecuritySafeCritical]
        public static WINDOWINFO GetWindowInfo(Window window);
        public static int High16(IntPtr value);
        public static int HIWORD(int i);
        [SecuritySafeCritical]
        private static void InitImageSources(IntPtr[] pointers, out ImageSource[] resultImages);
        public static bool IsDoubleFiniteAndNonNegative(double d);
        public static bool IsFlagSet(int value, int mask);
        public static bool IsNullOrEmpty(this IEnumerable equatable);
        public static bool IsTaskbarAutohided(IntPtr windowHandle);
        public static bool IsTaskbarAutohided(Window window);
        public static bool IsThicknessNonNegative(Thickness thickness);
        public static int Low16(IntPtr value);
        public static int LOWORD(int i);
        public static void RaiseEvent(this EventHandler eventHandler, object source);
        public static void RaiseEvent(this EventHandler eventHandler, object source, EventArgs args);
        public static void RemoveDependencyPropertyChangeListener(object component, DependencyProperty property, EventHandler listener);
        [SecurityCritical]
        public static void SafeDeleteObject(ref IntPtr gdiObject);
        public static void SetTabControlMargin(ThemedWindow window);
        public static ImageSource ToImageSource(this Icon icon);

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly WindowUtility.<>c <>9;
            public static Func<IntPtr, bool> <>9__26_0;

            static <>c();
            internal bool <InitImageSources>b__26_0(IntPtr x);
        }
    }
}

