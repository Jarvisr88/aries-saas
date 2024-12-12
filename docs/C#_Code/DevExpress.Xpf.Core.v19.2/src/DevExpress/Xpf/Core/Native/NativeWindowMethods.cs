namespace DevExpress.Xpf.Core.Native
{
    using DevExpress.Xpf.Core;
    using System;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Security;

    internal static class NativeWindowMethods
    {
        [return: MarshalAs(UnmanagedType.Bool)]
        [SecurityCritical, DllImport("user32.dll", EntryPoint="AdjustWindowRectEx", SetLastError=true)]
        private static extern bool _AdjustWindowRectEx(ref RECT lpRect, WindowStyles dwStyle, [MarshalAs(UnmanagedType.Bool)] bool bMenu, ExtendedWindowStyles dwExStyle);
        [SecurityCritical, DllImport("gdi32.dll", EntryPoint="CreateRectRgn", SetLastError=true)]
        private static extern IntPtr _CreateRectRgn(int nLeftRect, int nTopRect, int nRightRect, int nBottomRect);
        [SecurityCritical, DllImport("gdi32.dll", EntryPoint="CreateRectRgnIndirect", SetLastError=true)]
        private static extern IntPtr _CreateRectRgnIndirect([In] ref RECT lprc);
        [SecurityCritical, DllImport("user32.dll", EntryPoint="EnableMenuItem")]
        private static extern int _EnableMenuItem(IntPtr hMenu, SystemCommandValues uIDEnableItem, EnableMenuItemValues uEnable);
        [return: MarshalAs(UnmanagedType.Bool)]
        [SecurityCritical, DllImport("user32.dll", EntryPoint="GetMonitorInfo", SetLastError=true)]
        private static extern bool _GetMonitorInfo(IntPtr hMonitor, [In, Out] MONITORINFO lpmi);
        [return: MarshalAs(UnmanagedType.Bool)]
        [SecurityCritical, DllImport("user32.dll", EntryPoint="PostMessage", SetLastError=true)]
        private static extern bool _PostMessage(IntPtr hWnd, WindowMessageValues Msg, IntPtr wParam, IntPtr lParam);
        [return: MarshalAs(UnmanagedType.Bool)]
        [SecurityCritical, DllImport("user32.dll", EntryPoint="SetWindowPos", SetLastError=true)]
        private static extern bool _SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int x, int y, int cx, int cy, SetWindowPosOptions uFlags);
        [SecurityCritical, DllImport("user32.dll", EntryPoint="SetWindowRgn", SetLastError=true)]
        private static extern int _SetWindowRgn(IntPtr hWnd, IntPtr hRgn, [MarshalAs(UnmanagedType.Bool)] bool bRedraw);
        [SecurityCritical]
        public static RECT AdjustWindowRectEx(RECT lpRect, WindowStyles dwStyle, bool bMenu, ExtendedWindowStyles dwExStyle);
        [DllImport("gdi32.dll", EntryPoint="GdiAlphaBlend")]
        public static extern bool AlphaBlend(IntPtr hdcDest, int nXOriginDest, int nYOriginDest, int nWidthDest, int nHeightDest, IntPtr hdcSrc, int nXOriginSrc, int nYOriginSrc, int nWidthSrc, int nHeightSrc, BLENDFUNCTION blendFunction);
        [DllImport("gdi32.dll", SetLastError=true)]
        internal static extern IntPtr CreateCompatibleDC(IntPtr hdc);
        internal static IntPtr CreateCompatibleDC(HandleRef hdc);
        [DllImport("gdi32.dll", SetLastError=true)]
        internal static extern IntPtr CreateDIBSection(IntPtr hdc, ref BITMAPINFO pbmi, uint iUsage, out IntPtr ppvBits, IntPtr hSection, uint dwOffset);
        [SecurityCritical]
        public static IntPtr CreateRectRgn(int nLeftRect, int nTopRect, int nRightRect, int nBottomRect);
        [SecurityCritical]
        public static IntPtr CreateRectRgnIndirect(RECT lprc);
        [DllImport("user32.dll", CharSet=CharSet.Unicode, SetLastError=true)]
        public static extern IntPtr CreateWindowEx(int dwExStyle, IntPtr classAtom, string lpWindowName, int dwStyle, int x, int y, int nWidth, int nHeight, IntPtr hWndParent, IntPtr hMenu, IntPtr hInstance, IntPtr lpParam);
        [SecurityCritical, DllImport("user32.dll", EntryPoint="DefWindowProcW", CharSet=CharSet.Unicode)]
        public static extern IntPtr DefWindowProc(IntPtr hWnd, WindowMessageValues Msg, IntPtr wParam, IntPtr lParam);
        [SecuritySafeCritical, DllImport("user32.dll", CharSet=CharSet.Unicode)]
        internal static extern IntPtr DefWindowProc(IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam);
        [return: MarshalAs(UnmanagedType.Bool)]
        [DllImport("gdi32.dll")]
        internal static extern bool DeleteDC(IntPtr hdc);
        internal static bool DeleteDC(HandleRef hdc);
        [return: MarshalAs(UnmanagedType.Bool)]
        [SecurityCritical, DllImport("gdi32.dll")]
        public static extern bool DeleteObject(IntPtr hObject);
        [return: MarshalAs(UnmanagedType.Bool)]
        [DllImport("user32.dll", SetLastError=true)]
        public static extern bool DestroyWindow(IntPtr hwnd);
        [SecurityCritical, DllImport("dwmapi.dll", PreserveSig=false)]
        public static extern void DwmExtendFrameIntoClientArea(IntPtr hwnd, ref MARGINS pMarInset);
        [SecurityCritical]
        public static EnableMenuItemValues EnableMenuItem(IntPtr hMenu, SystemCommandValues uIDEnableItem, EnableMenuItemValues uEnable);
        [return: MarshalAs(UnmanagedType.Bool)]
        [SecuritySafeCritical, DllImport("user32.dll")]
        public static extern bool EnumThreadWindows(uint dwThreadId, NativeWindowMethods.EnumWindowsProc lpfn, IntPtr lParam);
        [return: MarshalAs(UnmanagedType.Bool)]
        [DllImport("user32.dll")]
        public static extern bool FillRect(IntPtr hDC, ref RECT rect, IntPtr hbrush);
        [DllImport("gdi32.dll")]
        public static extern bool FillRgn(IntPtr hdc, IntPtr hrgn, IntPtr hbr);
        [DllImport("user32.dll", CharSet=CharSet.Auto, ExactSpelling=true)]
        public static extern IntPtr GetActiveWindow();
        [DllImport("user32.dll")]
        public static extern bool GetClientRect(IntPtr hWnd, out RECT lpRect);
        [SecuritySafeCritical, DllImport("kernel32.dll")]
        public static extern uint GetCurrentThreadId();
        [DllImport("User32.dll", CallingConvention=CallingConvention.StdCall)]
        internal static extern IntPtr GetDC(IntPtr hWnd);
        [DllImport("user32.dll")]
        public static extern int GetDpiForWindow(IntPtr hWnd);
        [SecuritySafeCritical, DllImport("user32.dll")]
        public static extern IntPtr GetLastActivePopup(IntPtr hwnd);
        [DllImport("kernel32.dll", CharSet=CharSet.Unicode)]
        public static extern IntPtr GetModuleHandle(string moduleName);
        [SecurityCritical]
        public static MONITORINFO GetMonitorInfo(IntPtr hMonitor);
        [return: MarshalAs(UnmanagedType.Bool)]
        [DllImport("user32.dll")]
        public static extern bool GetMonitorInfo(IntPtr hMonitor, ref MONITORINFO monitorInfo);
        [SecuritySafeCritical]
        public static MONITORINFO GetMonitorInfoFromWindow(IntPtr hWnd);
        [DllImport("gdi32.dll")]
        public static extern IntPtr GetStockObject(StockObjects fnObject);
        [SecurityCritical, DllImport("user32.dll")]
        public static extern IntPtr GetSystemMenu(IntPtr hWnd, [MarshalAs(UnmanagedType.Bool)] bool bRevert);
        [DllImport("user32.dll")]
        public static extern int GetSystemMetrics(int nIndex);
        [SecuritySafeCritical, DllImport("user32.dll")]
        public static extern IntPtr GetWindow(IntPtr hwnd, int nCmd);
        [return: MarshalAs(UnmanagedType.Bool)]
        [DllImport("user32.dll")]
        public static extern bool GetWindowInfo(IntPtr hwnd, ref WINDOWINFO pwi);
        [SecurityCritical]
        public static IntPtr GetWindowLongPtr(IntPtr hwnd, GetWindowLongPtrValues nIndex);
        [SecurityCritical]
        public static WINDOWPLACEMENT GetWindowPlacement(IntPtr hwnd);
        [return: MarshalAs(UnmanagedType.Bool)]
        [SecurityCritical, DllImport("user32.dll", SetLastError=true)]
        private static extern bool GetWindowPlacement(IntPtr hwnd, WINDOWPLACEMENT lpwndpl);
        [SecurityCritical]
        public static RECT GetWindowRect(IntPtr hwnd);
        [return: MarshalAs(UnmanagedType.Bool)]
        [SecurityCritical, DllImport("user32.dll", SetLastError=true)]
        public static extern bool GetWindowRect(IntPtr hWnd, out RECT lpRect);
        [return: MarshalAs(UnmanagedType.Bool)]
        [DllImport("user32.dll")]
        public static extern bool IsIconic(IntPtr hwnd);
        [return: MarshalAs(UnmanagedType.Bool)]
        [SecurityCritical, DllImport("user32.dll")]
        public static extern bool IsWindow(IntPtr hwnd);
        [return: MarshalAs(UnmanagedType.Bool)]
        [SecurityCritical, DllImport("user32.dll")]
        public static extern bool IsWindowVisible(IntPtr hwnd);
        [return: MarshalAs(UnmanagedType.Bool)]
        [DllImport("user32.dll")]
        public static extern bool IsZoomed(IntPtr hwnd);
        [SecurityCritical, DllImport("user32.dll")]
        public static extern IntPtr MonitorFromWindow(IntPtr hwnd, uint dwFlags);
        [SecurityCritical]
        public static void PostMessage(IntPtr hWnd, WindowMessageValues Msg, IntPtr wParam, IntPtr lParam);
        [DllImport("user32.dll", CharSet=CharSet.Unicode)]
        public static extern ushort RegisterClass(ref WNDCLASS lpWndClass);
        [SecurityCritical, DllImport("user32.dll")]
        public static extern bool ReleaseCapture();
        [DllImport("User32.dll", CallingConvention=CallingConvention.StdCall)]
        internal static extern int ReleaseDC(IntPtr hWnd, IntPtr hDC);
        internal static int ReleaseDC(HandleRef hWnd, HandleRef hDC);
        [DllImport("gdi32.dll")]
        public static extern IntPtr SelectObject([In] IntPtr hdc, [In] IntPtr hgdiobj);
        [DllImport("user32.dll", CharSet=CharSet.Auto)]
        public static extern int SendMessage(IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam);
        [DllImport("user32.dll")]
        internal static extern int SetWindowCompositionAttribute(IntPtr hwnd, ref WindowCompositionAttributeData data);
        [SecuritySafeCritical]
        public static IntPtr SetWindowLong(IntPtr hWnd, int nIndex, IntPtr dwNewLong);
        [SecurityCritical]
        public static IntPtr SetWindowLongPtr(IntPtr hwnd, GetWindowLongPtrValues nIndex, IntPtr dwNewLong);
        [DllImport("user32.dll", EntryPoint="SetWindowLong", CharSet=CharSet.Auto)]
        public static extern IntPtr SetWindowLongPtr32(IntPtr hWnd, int nIndex, IntPtr dwNewLong);
        [DllImport("user32.dll", EntryPoint="SetWindowLongPtr", CharSet=CharSet.Auto)]
        public static extern IntPtr SetWindowLongPtr64(IntPtr hWnd, int nIndex, IntPtr dwNewLong);
        [SecurityCritical]
        public static bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int x, int y, int cx, int cy, SetWindowPosOptions uFlags);
        [return: MarshalAs(UnmanagedType.Bool)]
        [SecurityCritical, DllImport("user32.dll", SetLastError=true)]
        public static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int x, int y, int cx, int cy, int flags);
        [SecurityCritical]
        public static void SetWindowRgn(IntPtr hWnd, IntPtr hRgn, bool bRedraw);
        [DllImport("Shell32.dll")]
        public static extern int SHGetStockIconInfo(SHStockIconID siid, SHGSI uFlags, ref SHSTOCKICONINFO psii);
        [DllImport("user32.dll", CharSet=CharSet.Auto)]
        public static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);
        [DllImport("user32", CharSet=CharSet.Auto, SetLastError=true)]
        public static extern bool SystemParametersInfoForDpi(int nAction, int nParam, [In, Out] NONCLIENTMETRICS metrics, int nUpdate, uint dpi);
        [SecurityCritical, DllImport("user32.dll")]
        public static extern uint TrackPopupMenuEx(IntPtr hmenu, uint fuFlags, int x, int y, IntPtr hwnd, IntPtr lptpm);
        [return: MarshalAs(UnmanagedType.Bool)]
        [DllImport("user32.dll")]
        public static extern bool UnregisterClass(IntPtr classAtom, IntPtr hInstance);
        [DllImport("user32.dll", SetLastError=true, ExactSpelling=true)]
        public static extern bool UpdateLayeredWindow(IntPtr hwnd, IntPtr hdcDst, ref POINT pptDst, ref SIZE psize, IntPtr hdcSrc, ref POINT pptSrc, uint crKey, [In] ref BLENDFUNCTION pblend, uint dwFlags);

        [SecuritySafeCritical]
        public delegate bool EnumWindowsProc(IntPtr hWnd, IntPtr lParam);

        [SecuritySafeCritical]
        public delegate IntPtr WndProc(IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam);
    }
}

