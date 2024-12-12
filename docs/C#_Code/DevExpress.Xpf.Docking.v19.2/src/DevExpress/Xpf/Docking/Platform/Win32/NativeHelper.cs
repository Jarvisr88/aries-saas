namespace DevExpress.Xpf.Docking.Platform.Win32
{
    using DevExpress.Xpf.Core;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Security;
    using System.Windows;
    using System.Windows.Interop;

    internal static class NativeHelper
    {
        private const uint GW_HWNDNEXT = 2;
        internal const int SW_PARENTCLOSING = 1;
        internal const int SW_PARENTOPENING = 3;
        internal const int GWL_STYLE = -16;
        internal const int GWL_EXSTYLE = -20;
        internal const uint MF_BYCOMMAND = 0;
        internal const uint MF_GRAYED = 1;
        internal const uint MF_ENABLED = 0;
        public const int MOUSEEVENTF_LEFTDOWN = 2;
        public const int MOUSEEVENTF_LEFTUP = 4;

        [SecurityCritical, DllImport("gdi32.dll", EntryPoint="CreateRectRgn", SetLastError=true)]
        private static extern IntPtr _CreateRectRgn(int nLeftRect, int nTopRect, int nRightRect, int nBottomRect);
        [SecurityCritical, DllImport("gdi32.dll", EntryPoint="CreateRectRgnIndirect", SetLastError=true)]
        private static extern IntPtr _CreateRectRgnIndirect([In] ref DevExpress.Xpf.Core.NativeMethods.RECT lprc);
        [SecurityCritical, DllImport("gdi32.dll", EntryPoint="CreateRoundRectRgn", SetLastError=true)]
        private static extern IntPtr _CreateRoundRectRgn(int nLeftRect, int nTopRect, int nRightRect, int nBottomRect, int nWidthEllipse, int nHeightEllipse);
        [return: MarshalAs(UnmanagedType.Bool)]
        [SecurityCritical, DllImport("user32.dll", EntryPoint="GetWindowRect", SetLastError=true)]
        private static extern bool _GetWindowRect(IntPtr hWnd, out DevExpress.Xpf.Core.NativeMethods.RECT lpRect);
        [SecurityCritical, DllImport("user32.dll", EntryPoint="SetWindowRgn", SetLastError=true)]
        private static extern int _SetWindowRgn(IntPtr hWnd, IntPtr hRgn, [MarshalAs(UnmanagedType.Bool)] bool bRedraw);
        [SecurityCritical]
        public static IntPtr CreateRectRgn(int nLeftRect, int nTopRect, int nRightRect, int nBottomRect)
        {
            IntPtr ptr = _CreateRectRgn(nLeftRect, nTopRect, nRightRect, nBottomRect);
            if (IntPtr.Zero == ptr)
            {
                throw new Win32Exception();
            }
            return ptr;
        }

        [SecurityCritical]
        public static IntPtr CreateRectRgnIndirect(DevExpress.Xpf.Core.NativeMethods.RECT lprc)
        {
            IntPtr ptr = _CreateRectRgnIndirect(ref lprc);
            if (IntPtr.Zero == ptr)
            {
                throw new Win32Exception();
            }
            return ptr;
        }

        [SecurityCritical]
        public static IntPtr CreateRoundRectRgn(int nLeftRect, int nTopRect, int nRightRect, int nBottomRect, int nWidthEllipse, int nHeightEllipse)
        {
            IntPtr ptr = _CreateRoundRectRgn(nLeftRect, nTopRect, nRightRect, nBottomRect, nWidthEllipse, nHeightEllipse);
            if (IntPtr.Zero == ptr)
            {
                throw new Win32Exception();
            }
            return ptr;
        }

        [SecurityCritical, DllImport("user32.dll", EntryPoint="DefWindowProcW", CharSet=CharSet.Unicode)]
        public static extern IntPtr DefWindowProc(IntPtr hWnd, WM Msg, IntPtr wParam, IntPtr lParam);
        [return: MarshalAs(UnmanagedType.Bool)]
        [SecurityCritical, DllImport("gdi32.dll")]
        public static extern bool DeleteObject(IntPtr hObject);
        [DllImport("user32.dll")]
        private static extern bool EnableMenuItem(IntPtr hMenu, uint uIDEnableItem, uint uEnable);
        [SecuritySafeCritical]
        internal static bool EnableMenuItemSafe(IntPtr hMenu, uint uIDEnableItem, uint uEnable) => 
            EnableMenuItem(hMenu, uIDEnableItem, uEnable);

        public static int GET_X_LPARAM(IntPtr lParam) => 
            LOWORD(lParam.ToInt32());

        public static int GET_Y_LPARAM(IntPtr lParam) => 
            HIWORD(lParam.ToInt32());

        [return: MarshalAs(UnmanagedType.Bool)]
        [DllImport("user32.dll")]
        private static extern bool GetCursorPos(ref Win32Point pt);
        [SecurityCritical, DllImport("Shcore.dll", SetLastError=true)]
        public static extern int GetDpiForMonitor(IntPtr hmonitor, MONITOR_DPI_TYPE dpiType, ref int dpiX, ref int dpiY);
        [SecurityCritical]
        public static MONITORINFO GetMonitorInfo(IntPtr hMonitor)
        {
            MONITORINFO lpmi = new MONITORINFO();
            if (!GetMonitorInfo(hMonitor, lpmi))
            {
                throw new Win32Exception();
            }
            return lpmi;
        }

        [DllImport("user32")]
        internal static extern bool GetMonitorInfo(IntPtr hMonitor, MONITORINFO lpmi);
        [SecuritySafeCritical]
        internal static Point GetMousePositionSafe()
        {
            Win32Point pt = new Win32Point();
            GetCursorPos(ref pt);
            return new Point((double) pt.X, (double) pt.Y);
        }

        [SecuritySafeCritical]
        private static IntPtr GetNextWindowSafe(IntPtr hWnd, uint wCmd) => 
            GetWindow(hWnd, wCmd);

        [SecurityCritical, DllImport("Shcore.dll", SetLastError=true)]
        public static extern int GetProcessDpiAwareness(IntPtr hprocess, out PROCESS_DPI_AWARENESS value);
        [DllImport("user32.dll")]
        private static extern IntPtr GetSystemMenu(IntPtr hWnd, bool bRevert);
        [SecuritySafeCritical]
        internal static IntPtr GetSystemMenuSafe(IntPtr hWnd, bool bRevert) => 
            GetSystemMenu(hWnd, bRevert);

        [DllImport("User32")]
        private static extern IntPtr GetTopWindow(IntPtr hWnd);
        [SecuritySafeCritical]
        private static IntPtr GetTopWindowSafe(IntPtr hWnd) => 
            GetTopWindow(hWnd);

        [DllImport("User32")]
        private static extern IntPtr GetWindow(IntPtr hWnd, uint wCmd);
        [DllImport("user32.dll", SetLastError=true)]
        private static extern int GetWindowLong(IntPtr hWnd, int nIndex);
        [SecuritySafeCritical]
        internal static int GetWindowLongSafe(IntPtr hWnd, int nIndex) => 
            GetWindowLong(hWnd, nIndex);

        [SecurityCritical]
        public static DevExpress.Xpf.Docking.Platform.Win32.WINDOWPLACEMENT GetWindowPlacement(IntPtr hwnd)
        {
            DevExpress.Xpf.Docking.Platform.Win32.WINDOWPLACEMENT lpwndpl = new DevExpress.Xpf.Docking.Platform.Win32.WINDOWPLACEMENT();
            if (!GetWindowPlacement(hwnd, lpwndpl))
            {
                throw new Win32Exception();
            }
            return lpwndpl;
        }

        [return: MarshalAs(UnmanagedType.Bool)]
        [SecurityCritical, DllImport("user32.dll", SetLastError=true)]
        private static extern bool GetWindowPlacement(IntPtr hwnd, DevExpress.Xpf.Docking.Platform.Win32.WINDOWPLACEMENT lpwndpl);
        [SecurityCritical]
        public static DevExpress.Xpf.Core.NativeMethods.RECT GetWindowRect(IntPtr hwnd)
        {
            DevExpress.Xpf.Core.NativeMethods.RECT rect;
            if (!_GetWindowRect(hwnd, out rect))
            {
                throw new Win32Exception();
            }
            return rect;
        }

        public static int HIWORD(int i) => 
            (short) (i >> 0x10);

        public static bool IsFlagSet(int value, int mask) => 
            (value & mask) != 0;

        [return: MarshalAs(UnmanagedType.Bool)]
        [SecurityCritical, DllImport("user32.dll")]
        public static extern bool IsWindowVisible(IntPtr hwnd);
        [DllImport("user32.dll")]
        private static extern bool IsZoomed(IntPtr hWnd);
        [SecuritySafeCritical]
        private static void LeftMouseClick(int xpos, int ypos)
        {
            SetCursorPos(xpos, ypos);
            mouse_event(2, xpos, ypos, 0, 0);
            mouse_event(4, xpos, ypos, 0, 0);
        }

        public static int LOWORD(int i) => 
            (short) (i & 0xffff);

        [DllImport("User32")]
        internal static extern IntPtr MonitorFromWindow(IntPtr handle, int flags);
        [DllImport("user32.dll")]
        public static extern void mouse_event(int dwFlags, int dx, int dy, int cButtons, int dwExtraInfo);
        [SecuritySafeCritical]
        public static void MouseDoubleClick(int x, int y)
        {
            LeftMouseClick(x, y);
            LeftMouseClick(x, y);
        }

        [DllImport("user32.dll")]
        private static extern int PostMessage(IntPtr hWnd, int Msg, IntPtr wParam, IntPtr lParam);
        [SecurityCritical]
        public static void SafeDeleteObject(ref IntPtr gdiObject)
        {
            IntPtr hObject = gdiObject;
            gdiObject = IntPtr.Zero;
            if (IntPtr.Zero != hObject)
            {
                DeleteObject(hObject);
            }
        }

        [DllImport("user32.dll")]
        internal static extern int SendMessage(IntPtr hWnd, int Msg, IntPtr wParam, IntPtr lParam);
        [SecuritySafeCritical]
        internal static int SendMessageSafe(IntPtr hWnd, int Msg, IntPtr wParam, IntPtr lParam) => 
            SendMessage(hWnd, Msg, wParam, lParam);

        [DllImport("user32.dll")]
        private static extern bool SetCursorPos(int x, int y);
        [DllImport("user32.dll", CharSet=CharSet.Auto, ExactSpelling=true)]
        private static extern bool SetForegroundWindow(HandleRef hWnd);
        [SecuritySafeCritical]
        internal static bool SetForegroundWindowSafe(HandleRef hWnd) => 
            SetForegroundWindow(hWnd);

        [DllImport("user32.dll")]
        private static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);
        [SecuritySafeCritical]
        internal static int SetWindowLongSafe(IntPtr hWnd, int nIndex, int dwNewLong) => 
            SetWindowLong(hWnd, nIndex, dwNewLong);

        [return: MarshalAs(UnmanagedType.Bool)]
        [DllImport("user32.dll")]
        private static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, SetWindowPosFlags uFlags);
        [SecuritySafeCritical]
        internal static bool SetWindowPosSafe(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, int uFlags) => 
            SetWindowPos(hWnd, hWndInsertAfter, X, Y, cx, cy, (SetWindowPosFlags) uFlags);

        [SecurityCritical]
        public static void SetWindowRgn(IntPtr hWnd, IntPtr hRgn, bool bRedraw)
        {
            if (_SetWindowRgn(hWnd, hRgn, bRedraw) == 0)
            {
                throw new Win32Exception();
            }
        }

        [IteratorStateMachine(typeof(<SortWindowsTopToBottom>d__0)), SecuritySafeCritical]
        public static IEnumerable<Window> SortWindowsTopToBottom(IEnumerable<Window> unsorted)
        {
            <SortWindowsTopToBottom>d__0 d__1 = new <SortWindowsTopToBottom>d__0(-2);
            d__1.<>3__unsorted = unsorted;
            return d__1;
        }

        [SecuritySafeCritical]
        internal static void WmGetMinMaxInfo(IntPtr hwnd, IntPtr lParam, bool overlapTaskbar)
        {
            MINMAXINFO structure = (MINMAXINFO) Marshal.PtrToStructure(lParam, typeof(MINMAXINFO));
            int flags = 2;
            IntPtr hMonitor = MonitorFromWindow(hwnd, flags);
            if (hMonitor != IntPtr.Zero)
            {
                MONITORINFO lpmi = new MONITORINFO();
                GetMonitorInfo(hMonitor, lpmi);
                DevExpress.Xpf.Core.NativeMethods.RECT rcWork = lpmi.rcWork;
                DevExpress.Xpf.Core.NativeMethods.RECT rcMonitor = lpmi.rcMonitor;
                structure.ptMaxPosition.x = Math.Abs((int) (rcWork.left - rcMonitor.left));
                structure.ptMaxPosition.y = Math.Abs((int) (rcWork.top - rcMonitor.top));
                DevExpress.Xpf.Core.NativeMethods.RECT rect3 = overlapTaskbar ? rcMonitor : rcWork;
                structure.ptMaxSize.x = Math.Abs((int) (rect3.right - rect3.left));
                structure.ptMaxSize.y = Math.Abs((int) (rect3.bottom - rect3.top));
                if (IsZoomed(hwnd))
                {
                    structure.ptMaxTrackSize.x = structure.ptMaxSize.x;
                    structure.ptMaxTrackSize.y = structure.ptMaxSize.y;
                }
            }
            Marshal.StructureToPtr<MINMAXINFO>(structure, lParam, true);
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly NativeHelper.<>c <>9 = new NativeHelper.<>c();
            public static Func<Window, bool> <>9__0_0;
            public static Func<Window, IntPtr> <>9__0_1;

            internal bool <SortWindowsTopToBottom>b__0_0(Window x) => 
                PresentationSource.FromVisual(x) != null;

            internal IntPtr <SortWindowsTopToBottom>b__0_1(Window win) => 
                ((HwndSource) PresentationSource.FromVisual(win)).Handle;
        }

        [CompilerGenerated]
        private sealed class <SortWindowsTopToBottom>d__0 : IEnumerable<Window>, IEnumerable, IEnumerator<Window>, IDisposable, IEnumerator
        {
            private int <>1__state;
            private Window <>2__current;
            private int <>l__initialThreadId;
            private IEnumerable<Window> unsorted;
            public IEnumerable<Window> <>3__unsorted;
            private Dictionary<IntPtr, Window> <byHandle>5__1;
            private IntPtr <hWnd>5__2;

            [DebuggerHidden]
            public <SortWindowsTopToBottom>d__0(int <>1__state)
            {
                this.<>1__state = <>1__state;
                this.<>l__initialThreadId = Environment.CurrentManagedThreadId;
            }

            private bool MoveNext()
            {
                int num = this.<>1__state;
                if (num == 0)
                {
                    this.<>1__state = -1;
                    Func<Window, bool> predicate = NativeHelper.<>c.<>9__0_0;
                    if (NativeHelper.<>c.<>9__0_0 == null)
                    {
                        Func<Window, bool> local1 = NativeHelper.<>c.<>9__0_0;
                        predicate = NativeHelper.<>c.<>9__0_0 = new Func<Window, bool>(this.<SortWindowsTopToBottom>b__0_0);
                    }
                    Func<Window, IntPtr> keySelector = NativeHelper.<>c.<>9__0_1;
                    if (NativeHelper.<>c.<>9__0_1 == null)
                    {
                        Func<Window, IntPtr> local2 = NativeHelper.<>c.<>9__0_1;
                        keySelector = NativeHelper.<>c.<>9__0_1 = new Func<Window, IntPtr>(this.<SortWindowsTopToBottom>b__0_1);
                    }
                    this.<byHandle>5__1 = this.unsorted.Where<Window>(predicate).ToDictionary<Window, IntPtr>(keySelector);
                    this.<hWnd>5__2 = NativeHelper.GetTopWindowSafe(IntPtr.Zero);
                }
                else
                {
                    if (num != 1)
                    {
                        return false;
                    }
                    this.<>1__state = -1;
                    goto TR_0006;
                }
            TR_0004:
                if (this.<hWnd>5__2 == IntPtr.Zero)
                {
                    return false;
                }
                if (this.<byHandle>5__1.ContainsKey(this.<hWnd>5__2))
                {
                    this.<>2__current = this.<byHandle>5__1[this.<hWnd>5__2];
                    this.<>1__state = 1;
                    return true;
                }
            TR_0006:
                while (true)
                {
                    this.<hWnd>5__2 = NativeHelper.GetNextWindowSafe(this.<hWnd>5__2, 2);
                    break;
                }
                goto TR_0004;
            }

            [DebuggerHidden]
            IEnumerator<Window> IEnumerable<Window>.GetEnumerator()
            {
                NativeHelper.<SortWindowsTopToBottom>d__0 d__;
                if ((this.<>1__state != -2) || (this.<>l__initialThreadId != Environment.CurrentManagedThreadId))
                {
                    d__ = new NativeHelper.<SortWindowsTopToBottom>d__0(0);
                }
                else
                {
                    this.<>1__state = 0;
                    d__ = this;
                }
                d__.unsorted = this.<>3__unsorted;
                return d__;
            }

            [DebuggerHidden]
            IEnumerator IEnumerable.GetEnumerator() => 
                this.System.Collections.Generic.IEnumerable<System.Windows.Window>.GetEnumerator();

            [DebuggerHidden]
            void IEnumerator.Reset()
            {
                throw new NotSupportedException();
            }

            [DebuggerHidden]
            void IDisposable.Dispose()
            {
            }

            Window IEnumerator<Window>.Current =>
                this.<>2__current;

            object IEnumerator.Current =>
                this.<>2__current;
        }

        public enum MONITOR_DPI_TYPE
        {
            MDT_Effective_DPI = 0,
            MDT_Angular_DPI = 1,
            MDT_Raw_DPI = 2,
            MDT_Default = 0
        }

        public enum PROCESS_DPI_AWARENESS
        {
            Process_DPI_Unaware,
            Process_System_DPI_Aware,
            Process_Per_Monitor_DPI_Aware
        }

        [Flags]
        internal enum SetWindowPosFlags : uint
        {
            SynchronousWindowPosition = 0x4000,
            DeferErase = 0x2000,
            DrawFrame = 0x20,
            FrameChanged = 0x20,
            HideWindow = 0x80,
            DoNotActivate = 0x10,
            DoNotCopyBits = 0x100,
            IgnoreMove = 2,
            DoNotChangeOwnerZOrder = 0x200,
            DoNotRedraw = 8,
            DoNotReposition = 0x200,
            DoNotSendChangingEvent = 0x400,
            IgnoreResize = 1,
            IgnoreZOrder = 4,
            ShowWindow = 0x40
        }

        [StructLayout(LayoutKind.Sequential)]
        internal struct Win32Point
        {
            public int X;
            public int Y;
        }
    }
}

