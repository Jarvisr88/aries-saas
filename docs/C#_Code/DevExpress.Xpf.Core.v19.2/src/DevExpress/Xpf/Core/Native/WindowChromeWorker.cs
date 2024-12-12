namespace DevExpress.Xpf.Core.Native
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.InteropServices;
    using System.Security;
    using System.Security.Permissions;
    using System.Windows;
    using System.Windows.Interop;

    internal class WindowChromeWorker : DependencyObject
    {
        private const SetWindowPosOptions swpFlags = (SetWindowPosOptions.NOOWNERZORDER | SetWindowPosOptions.DRAWFRAME | SetWindowPosOptions.NOACTIVATE | SetWindowPosOptions.NOZORDER | SetWindowPosOptions.NOMOVE | SetWindowPosOptions.NOSIZE);
        public static readonly DependencyProperty WindowChromeWorkerProperty;
        public static readonly DependencyProperty IsNCActiveProperty;
        private static readonly NonClientHitTestValues[,] hitTestBorders;
        private readonly Dictionary<WindowMessageValues, MessageHandler> messageTable;
        private readonly Window window;
        [SecurityCritical]
        private IntPtr cachedHwnd;
        [SecurityCritical]
        private HwndSource hwndSource;
        private bool isHooked;
        private WindowChrome currentChrome;
        private WindowState lastMenuState;
        private IntPtr hmenu;
        private bool isNCPainted;
        private bool windowRegionExtraUpdated;

        [SecurityCritical]
        static WindowChromeWorker();
        [SecuritySafeCritical, PermissionSet(SecurityAction.Demand, Name="FullTrust")]
        public WindowChromeWorker(Window newWindow);
        [SecurityCritical]
        private void ApplyWindowChrome();
        private static void AreEqual<T>(T expected, T actual);
        [SecuritySafeCritical]
        private void CalcMaximizedWindowSize(IntPtr wParam, IntPtr lParam);
        [SecurityCritical]
        private Size? CalcWindowSize(WINDOWPOS? wp);
        [SecurityCritical]
        private void ClearWindowRegion();
        private double CoerceCaptionHeight(double captionHeight);
        private MINMAXINFO CoerceIfSizeSpecified(MINMAXINFO tempMinMaxInfo);
        [SecuritySafeCritical]
        private void CoerceMinMaxInfo(IntPtr lParam);
        private Rect CoerceSizeGripPosition(IInputElement inputElement, Rect sizeGripPosition);
        [SecurityCritical]
        private RECT CoerceWindowRegion(WINDOWPOS? wp, RECT rcMax);
        [SecuritySafeCritical]
        private RECT CoerceWindowSize(RECT windowSize, WINDOWINFO windowInfo);
        [SecurityCritical]
        private WindowState GetHwndState();
        public static bool GetIsNCActive(Window window);
        [SecuritySafeCritical]
        private MousePosition GetMousePosWindow(IntPtr lParam, DpiScale dpi);
        [SecurityCritical]
        private RealWindowBounds GetRealWindowBounds(Rect windowPosition);
        public static WindowChromeWorker GetWindowChromeWorker(Window window);
        [SecurityCritical]
        private Rect GetWindowRect();
        [SecurityCritical]
        private IntPtr HandleCUAHDrawCaptionMessage(WindowMessageValues uMsg, IntPtr wParam, IntPtr lParam, out bool handled);
        [SecuritySafeCritical]
        private IntPtr HandleGetMinMaxInfo(WindowMessageValues uMsg, IntPtr wParam, IntPtr lParam, out bool handled);
        [SecurityCritical]
        private IntPtr HandleNCActivate(WindowMessageValues uMsg, IntPtr wParam, IntPtr lParam, out bool handled);
        [SecurityCritical]
        private IntPtr HandleNCCalcSize(WindowMessageValues uMsg, IntPtr wParam, IntPtr lParam, out bool handled);
        [SecurityCritical]
        private IntPtr HandleNCHitTest(WindowMessageValues uMsg, IntPtr wParam, IntPtr lParam, out bool handled);
        [SecurityCritical]
        private IntPtr HandleNCLButtonDown(WindowMessageValues uMsg, IntPtr wParam, IntPtr lParam, out bool handled);
        [SecurityCritical]
        private IntPtr HandleNCPaint(WindowMessageValues uMsg, IntPtr wParam, IntPtr lParam, out bool handled);
        [SecurityCritical]
        private IntPtr HandleNCRButtonUp(WindowMessageValues uMsg, IntPtr wParam, IntPtr lParam, out bool handled);
        [SecurityCritical]
        private IntPtr HandleSetTextOrIcon(WindowMessageValues uMsg, IntPtr wParam, IntPtr lParam, out bool handled);
        [SecurityCritical]
        private IntPtr HandleWindowPosChanged(WindowMessageValues uMsg, IntPtr wParam, IntPtr lParam, out bool handled);
        [SecurityCritical]
        private NonClientHitTestValues HitTestNca(Rect windowPosition, Point mousePosition, Rect sizeGripPosition = new Rect());
        [SecuritySafeCritical]
        private void InitMessageTable();
        private bool IsNeedToDelay(WINDOWPOS? windowPosition);
        [SecuritySafeCritical]
        private bool IsWindowRegionShouldBeUpdated();
        [SecurityCritical]
        private bool ModifyStyle(WindowStyles removeStyle, WindowStyles addStyle);
        [SecurityCritical]
        private static bool ModifyStyle(IntPtr cachedHwnd, WindowStyles removeStyle, WindowStyles addStyle);
        [SecuritySafeCritical]
        internal static bool ModifyStyleInternal(IntPtr handle, WindowStyles removeStyle, WindowStyles addStyle);
        private NonClientHitTestValues NCHitTestResultFromResizeGripDirection(ResizeGripDirection direction);
        [SecuritySafeCritical, PermissionSet(SecurityAction.Demand, Name="FullTrust")]
        private void OnChromeRequestRepaint(object sender, EventArgs e);
        [SecuritySafeCritical, PermissionSet(SecurityAction.Demand, Name="FullTrust")]
        private void OnWindowClosed(object sender, EventArgs e);
        [SecuritySafeCritical, PermissionSet(SecurityAction.Demand, Name="FullTrust")]
        private void OnWindowSourceInitialized(object sender, EventArgs e);
        [SecurityCritical]
        private void RestoreHrgn();
        [SecurityCritical]
        private void RestoreStandardChromeState(bool isClosing);
        public static void SetIsNCActive(Window window, bool isNCActive);
        [SecuritySafeCritical, PermissionSet(SecurityAction.Demand, Name="FullTrust")]
        internal void SetWindowChrome(WindowChrome newChrome);
        public static void SetWindowChromeWorker(Window window, WindowChromeWorker chrome);
        [SecurityCritical]
        private void SetWindowRegion(WINDOWPOS? wp);
        [SecurityCritical]
        private void SetWindowRegionMaximized(WINDOWPOS? wp);
        [SecurityCritical]
        private void SetWindowRegionNormal(WINDOWPOS? wp);
        [SecurityCritical]
        private void UnhookCustomChrome();
        [SecurityCritical]
        private void UnsubscribeWindowEvents();
        [SecurityCritical]
        private void UpdateFrameState();
        private void UpdateGlow(WINDOWPOS? windowPosition);
        [SecurityCritical]
        private void UpdateSystemMenu(WindowState? assumeState);
        [SecurityCritical]
        internal static void UpdateSystemMenu(WindowChromeWorker worker, Window window);
        [SecurityCritical]
        private IntPtr WndProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled);

        private static bool IsOSSeven { get; }
    }
}

