namespace DevExpress.Xpf.Docking.Platform.Shell
{
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Docking.Platform.Win32;
    using System;
    using System.Collections.Generic;
    using System.Runtime.InteropServices;
    using System.Security;
    using System.Security.Permissions;
    using System.Windows;
    using System.Windows.Interop;

    internal class WindowChromeWorker : DependencyObject
    {
        public static readonly DependencyProperty WindowChromeWorkerProperty = DependencyProperty.RegisterAttached("WindowChromeWorker", typeof(WindowChromeWorker), typeof(WindowChromeWorker), new PropertyMetadata(null, new PropertyChangedCallback(WindowChromeWorker._OnChromeWorkerChanged)));
        private const SWP _swpFlags = (SWP.NOOWNERZORDER | SWP.DRAWFRAME | SWP.NOACTIVATE | SWP.NOZORDER | SWP.NOMOVE | SWP.NOSIZE);
        private readonly List<KeyValuePair<WM, MessageHandler>> _messageTable;
        private Window _window;
        [SecurityCritical]
        private IntPtr _hwnd;
        [SecurityCritical]
        private HwndSource _hwndSource;
        private bool _isHooked;
        private WindowChrome _chrome;
        private WindowState _lastRoundingState;
        private WINDOWPOS _previousWP;

        [SecuritySafeCritical, PermissionSet(SecurityAction.Demand, Name="FullTrust")]
        public WindowChromeWorker()
        {
            List<KeyValuePair<WM, MessageHandler>> list1 = new List<KeyValuePair<WM, MessageHandler>>();
            list1.Add(new KeyValuePair<WM, MessageHandler>(WM.WM_NCACTIVATE, new MessageHandler(this.OnNCActivate)));
            list1.Add(new KeyValuePair<WM, MessageHandler>(WM.WM_NCCALCSIZE, new MessageHandler(this.OnNCCalcSize)));
            list1.Add(new KeyValuePair<WM, MessageHandler>(WM.WM_WINDOWPOSCHANGED, new MessageHandler(this.OnWindowPosChanged)));
            this._messageTable = list1;
        }

        [SecurityCritical]
        private bool _ModifyStyle(WS removeStyle, WS addStyle)
        {
            WS windowLongSafe = (WS) NativeHelper.GetWindowLongSafe(this._hwnd, -16);
            WS ws2 = (windowLongSafe & ~removeStyle) | addStyle;
            if (windowLongSafe == ws2)
            {
                return false;
            }
            NativeHelper.SetWindowLongSafe(this._hwnd, -16, (int) ws2);
            return true;
        }

        [SecuritySafeCritical, PermissionSet(SecurityAction.Demand, Name="FullTrust")]
        private static void _OnChromeWorkerChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((WindowChromeWorker) e.NewValue).SetWindow((Window) d);
        }

        [SecurityCritical]
        private IntPtr _WndProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            IntPtr ptr;
            WM uMsg = (WM) msg;
            using (List<KeyValuePair<WM, MessageHandler>>.Enumerator enumerator = this._messageTable.GetEnumerator())
            {
                while (true)
                {
                    if (enumerator.MoveNext())
                    {
                        KeyValuePair<WM, MessageHandler> current = enumerator.Current;
                        if (((WM) current.Key) != uMsg)
                        {
                            continue;
                        }
                        ptr = current.Value(uMsg, wParam, lParam, out handled);
                    }
                    else
                    {
                        return IntPtr.Zero;
                    }
                    break;
                }
            }
            return ptr;
        }

        [SecurityCritical]
        private void ApplyWindowChrome()
        {
            if ((this._hwnd != IntPtr.Zero) && !this._hwndSource.IsDisposed)
            {
                if (this._chrome == null)
                {
                    this.RestoreStandardChromeState(false);
                }
                else
                {
                    if (!this._isHooked)
                    {
                        this._hwndSource.AddHook(new HwndSourceHook(this._WndProc));
                        this._isHooked = true;
                    }
                    this.UpdateFrameState();
                    NativeHelper.SetWindowPosSafe(this._hwnd, IntPtr.Zero, 0, 0, 0, 0, 0x237);
                }
            }
        }

        [SecurityCritical]
        private void ClearRoundingRegion()
        {
            NativeHelper.SetWindowRgn(this._hwnd, IntPtr.Zero, NativeHelper.IsWindowVisible(this._hwnd));
        }

        [SecurityCritical]
        private static IntPtr CreateRectRgn(Rect region) => 
            NativeHelper.CreateRectRgn((int) Math.Floor(region.Left), (int) Math.Floor(region.Top), (int) Math.Ceiling(region.Right), (int) Math.Ceiling(region.Bottom));

        [SecurityCritical]
        private WindowState GetHwndState()
        {
            SW showCmd = NativeHelper.GetWindowPlacement(this._hwnd).showCmd;
            return ((showCmd == SW.SHOWMINIMIZED) ? WindowState.Minimized : ((showCmd == SW.SHOWMAXIMIZED) ? WindowState.Maximized : WindowState.Normal));
        }

        public static WindowChromeWorker GetWindowChromeWorker(Window window) => 
            (WindowChromeWorker) window.GetValue(WindowChromeWorkerProperty);

        [SecurityCritical]
        private Rect GetWindowRect()
        {
            DevExpress.Xpf.Core.NativeMethods.RECT windowRect = NativeHelper.GetWindowRect(this._hwnd);
            return new Rect((double) windowRect.left, (double) windowRect.top, (double) windowRect.Width, (double) windowRect.Height);
        }

        [SecurityCritical]
        private IntPtr OnNCActivate(WM uMsg, IntPtr wParam, IntPtr lParam, out bool handled)
        {
            IntPtr ptr = NativeHelper.DefWindowProc(this._hwnd, WM.WM_NCACTIVATE, wParam, new IntPtr(-1));
            handled = true;
            return ptr;
        }

        [SecurityCritical]
        private IntPtr OnNCCalcSize(WM uMsg, IntPtr wParam, IntPtr lParam, out bool handled)
        {
            handled = true;
            return IntPtr.Zero;
        }

        [SecurityCritical]
        private IntPtr OnWindowPosChanged(WM uMsg, IntPtr wParam, IntPtr lParam, out bool handled)
        {
            WINDOWPOS windowpos = (WINDOWPOS) Marshal.PtrToStructure(lParam, typeof(WINDOWPOS));
            if (!windowpos.Equals(this._previousWP) && !NativeHelper.IsFlagSet(windowpos.flags, 1))
            {
                this.SetRoundingRegion(new WINDOWPOS?(windowpos));
            }
            this._previousWP = windowpos;
            handled = false;
            return IntPtr.Zero;
        }

        [SecuritySafeCritical, PermissionSet(SecurityAction.Demand, Name="FullTrust")]
        private void OnWindowSourceInitialized(object sender, EventArgs e)
        {
            this._hwnd = new WindowInteropHelper(this._window).Handle;
            this._hwndSource = HwndSource.FromHwnd(this._hwnd);
            if (this._window.SizeToContent == SizeToContent.WidthAndHeight)
            {
                this._window.InvalidateMeasure();
            }
            if (this._chrome != null)
            {
                this.ApplyWindowChrome();
            }
        }

        [SecurityCritical]
        private void RestoreHrgn()
        {
            this.ClearRoundingRegion();
            NativeHelper.SetWindowPosSafe(this._hwnd, IntPtr.Zero, 0, 0, 0, 0, 0x237);
        }

        [SecurityCritical]
        private void RestoreStandardChromeState(bool isClosing)
        {
            base.VerifyAccess();
            this.UnhookCustomChrome();
            if (!isClosing && !this._hwndSource.IsDisposed)
            {
                this.RestoreHrgn();
                this._window.InvalidateMeasure();
            }
        }

        [SecurityCritical]
        private void SetRoundingRegion(WINDOWPOS? wp)
        {
            DevExpress.Xpf.Docking.Platform.Win32.WINDOWPLACEMENT windowPlacement = WindowPlacementHelper.GetWindowPlacement(this._window);
            if (windowPlacement.showCmd != SW.SHOWMINIMIZED)
            {
                if (windowPlacement.showCmd != SW.SHOWMAXIMIZED)
                {
                    Size size;
                    if ((wp != null) && !NativeHelper.IsFlagSet(wp.Value.flags, 1))
                    {
                        size = new Size((double) wp.Value.cx, (double) wp.Value.cy);
                    }
                    else
                    {
                        if ((wp != null) && (this._lastRoundingState == this._window.WindowState))
                        {
                            return;
                        }
                        size = this.GetWindowRect().Size;
                    }
                    this._lastRoundingState = this._window.WindowState;
                    IntPtr zero = IntPtr.Zero;
                    try
                    {
                        zero = CreateRectRgn(new Rect(size));
                        NativeHelper.SetWindowRgn(this._hwnd, zero, NativeHelper.IsWindowVisible(this._hwnd));
                        zero = IntPtr.Zero;
                    }
                    finally
                    {
                        NativeHelper.SafeDeleteObject(ref zero);
                    }
                }
                else
                {
                    int x;
                    int y;
                    if (wp != null)
                    {
                        x = wp.Value.x;
                        y = wp.Value.y;
                    }
                    else
                    {
                        Rect windowRect = this.GetWindowRect();
                        x = (int) windowRect.Left;
                        y = (int) windowRect.Top;
                    }
                    MONITORINFO monitorInfo = NativeHelper.GetMonitorInfo(NativeHelper.MonitorFromWindow(this._hwnd, 2));
                    DevExpress.Xpf.Core.NativeMethods.RECT lprc = WindowChrome.GetOverlapTaskbar(this._window) ? monitorInfo.rcMonitor : monitorInfo.rcWork;
                    lprc.Offset(-x, -y);
                    IntPtr zero = IntPtr.Zero;
                    try
                    {
                        zero = NativeHelper.CreateRectRgnIndirect(lprc);
                        NativeHelper.SetWindowRgn(this._hwnd, zero, NativeHelper.IsWindowVisible(this._hwnd));
                        zero = IntPtr.Zero;
                    }
                    finally
                    {
                        NativeHelper.SafeDeleteObject(ref zero);
                    }
                }
            }
        }

        [SecurityCritical]
        private void SetWindow(Window window)
        {
            this.UnsubscribeWindowEvents();
            this._window = window;
            this._hwnd = new WindowInteropHelper(this._window).Handle;
            this._window.Closed += new EventHandler(this.UnsetWindow);
            if (!(IntPtr.Zero != this._hwnd))
            {
                this._window.SourceInitialized += new EventHandler(this.OnWindowSourceInitialized);
            }
            else
            {
                this._hwndSource = HwndSource.FromHwnd(this._hwnd);
                this._window.ApplyTemplate();
                if (this._chrome != null)
                {
                    this.ApplyWindowChrome();
                }
            }
        }

        [SecuritySafeCritical, PermissionSet(SecurityAction.Demand, Name="FullTrust")]
        public void SetWindowChrome(WindowChrome windowChrome)
        {
            base.VerifyAccess();
            if (!ReferenceEquals(windowChrome, this._chrome))
            {
                this._chrome = windowChrome;
                this.ApplyWindowChrome();
            }
        }

        public static void SetWindowChromeWorker(Window window, WindowChromeWorker chrome)
        {
            window.SetValue(WindowChromeWorkerProperty, chrome);
        }

        [SecurityCritical]
        private void UnhookCustomChrome()
        {
            if (this._isHooked)
            {
                this._hwndSource.RemoveHook(new HwndSourceHook(this._WndProc));
                this._isHooked = false;
            }
        }

        [SecuritySafeCritical, PermissionSet(SecurityAction.Demand, Name="FullTrust")]
        private void UnsetWindow(object sender, EventArgs e)
        {
            this.UnsubscribeWindowEvents();
            this.RestoreStandardChromeState(true);
        }

        [SecurityCritical]
        private void UnsubscribeWindowEvents()
        {
            if (this._window != null)
            {
                this._window.SourceInitialized -= new EventHandler(this.OnWindowSourceInitialized);
            }
        }

        [SecurityCritical]
        private void UpdateFrameState()
        {
            if ((IntPtr.Zero != this._hwnd) && !this._hwndSource.IsDisposed)
            {
                WINDOWPOS? wp = null;
                this.SetRoundingRegion(wp);
                NativeHelper.SetWindowPosSafe(this._hwnd, IntPtr.Zero, 0, 0, 0, 0, 0x237);
            }
        }
    }
}

