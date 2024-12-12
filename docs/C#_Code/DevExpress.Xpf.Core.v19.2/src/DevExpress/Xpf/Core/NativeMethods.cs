namespace DevExpress.Xpf.Core
{
    using System;
    using System.ComponentModel;
    using System.Runtime.InteropServices;
    using System.Security;
    using System.Windows;

    public class NativeMethods
    {
        public const int WM_MOUSEHWHEEL = 0x20e;
        public const int WM_NCCALCSIZE = 0x83;
        public const int WM_NCPAINT = 0x85;
        public const int WM_NCUAHDRAWCAPTION = 0xae;
        public const int WM_NCUAHDRAWFRAME = 0xaf;
        public const int WM_NCACTIVATE = 0x86;
        public const int WM_NCHITTEST = 0x84;
        public const int WM_ACTIVATE = 6;
        public const int WM_PAINT = 15;
        public const int WM_RBUTTONUP = 0x205;
        public const int WM_SETICON = 0x80;
        public const int WM_SETTEXT = 12;
        public const int GWL_STYLE = -16;
        public const int WM_SIZE = 5;
        public const int WM_ENTERSIZEMOVE = 0x231;
        public const int WM_EXITSIZEMOVE = 0x232;
        public const int WM_CAPTURECHANGED = 0x215;
        public const int WM_MOVE = 3;
        public const int WM_GETICON = 0x7f;
        public const int WM_NCRBUTTONDOWN = 0xa4;
        public const int WM_NCRBUTTONUP = 0xa5;
        public const int WM_NCRBUTTONDBLCLK = 0xa6;
        public const int WM_DWMCOMPOSITIONCHANGED = 0x31e;
        public const int WM_WINDOWPOSCHANGING = 70;
        public const int WM_WINDOWPOSCHANGED = 0x47;
        public const int WS_CAPTION = 0xc00000;
        public const int DDW_FRAME = 0x400;
        public const int DDW_INVALIDATE = 1;
        public const int DDW_UPDATENOW = 0x100;
        public const int ICON_BIG = 1;
        public const int ICON_SMALL = 0;
        public const int ICON_SMALL2 = 2;
        public const int LOGPIXELSX = 0x58;
        public const int LOGPIXELSY = 90;
        public const int WM_GETTITLEBARINFOEX = 0x33f;
        public const int HTCLIENT = 1;
        public const int HTLEFT = 10;
        public const int HTRIGHT = 11;
        public const int HTTOP = 12;
        public const int HTTOPLEFT = 13;
        public const int HTTOPRIGHT = 14;
        public const int HTBOTTOM = 15;
        public const int HTBOTTOMLEFT = 0x10;
        public const int HTBOTTOMRIGHT = 0x11;

        public static bool AdjustWindowRectEx(ref RECT lpRect, int dwStyle, bool bMenu, int dwExStyle)
        {
            bool flag = IntAdjustWindowRectEx(ref lpRect, dwStyle, bMenu, dwExStyle);
            if (!flag)
            {
                throw new Win32Exception();
            }
            return flag;
        }

        [return: MarshalAs(UnmanagedType.Bool)]
        [DllImport("user32.dll")]
        internal static extern bool ClientToScreen(IntPtr hWnd, ref POINT point);
        [DllImport("gdi32")]
        public static extern IntPtr CreateRectRgn(int nLeftRect, int nTopRect, int nRightRect, int nBottomRect);
        [DllImport("gdi32.dll")]
        public static extern IntPtr CreateSolidBrush(int colorref);
        [DllImport("user32.dll", CharSet=CharSet.Unicode)]
        internal static extern IntPtr DefWindowProc(IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam);
        [return: MarshalAs(UnmanagedType.Bool)]
        [DllImport("gdi32.dll")]
        public static extern bool DeleteObject(IntPtr hObject);
        [DllImport("user32.dll")]
        public static extern int DrawMenuBar(IntPtr hwnd);
        [return: MarshalAs(UnmanagedType.Bool)]
        [DllImport("dwmapi.dll")]
        public static extern bool DwmDefWindowProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref IntPtr plResult);
        [DllImport("dwmapi.dll")]
        public static extern void DwmExtendFrameIntoClientArea(IntPtr hwnd, ref MARGIN pMarInset);
        [DllImport("dwmapi.dll")]
        private static extern int DwmGetWindowAttribute(IntPtr hwnd, int dwAttribute, out RECT pvAttribute, int cbAttribute);
        [return: MarshalAs(UnmanagedType.Bool)]
        [DllImport("dwmapi.dll", PreserveSig=false)]
        public static extern bool DwmIsCompositionEnabled();
        [DllImport("user32.dll", CharSet=CharSet.Auto, ExactSpelling=true)]
        public static extern bool EnableMenuItem(HandleRef hMenu, int UIDEnabledItem, int uEnable);
        [return: MarshalAs(UnmanagedType.Bool)]
        [DllImport("user32.dll")]
        public static extern bool FillRect(IntPtr hDC, ref RECT rect, IntPtr hbrush);
        [DllImport("user32.dll", CharSet=CharSet.Auto, ExactSpelling=true)]
        public static extern IntPtr GetActiveWindow();
        [return: MarshalAs(UnmanagedType.Bool)]
        [DllImport("user32.dll")]
        public static extern bool GetClientRect(IntPtr hwnd, out RECT lpRect);
        [DllImport("user32.dll")]
        public static extern IntPtr GetDC(IntPtr hwnd);
        [DllImport("User32.dll")]
        internal static extern IntPtr GetDCEx(IntPtr hWnd, IntPtr hrgnClip, int dwFlags);
        [DllImport("gdi32.dll")]
        public static extern int GetDeviceCaps(IntPtr hdc, int nIndex);
        [DllImport("user32.dll")]
        public static extern int GetMenuItemCount(IntPtr hmenu);
        [DllImport("user32.dll")]
        public static extern int GetMessageTime();
        [SecurityCritical, DllImport("kernel32.dll", CharSet=CharSet.Auto)]
        public static extern IntPtr GetModuleHandle(string lpModuleName);
        [DllImport("user32.dll", CharSet=CharSet.Auto, SetLastError=true)]
        public static extern bool GetMonitorInfo(HandleRef hmonitor, [In, Out] MONITORINFOEX info);
        [DllImport("user32.dll")]
        public static extern IntPtr GetSystemMenu(IntPtr hWnd, bool bRevert);
        [return: MarshalAs(UnmanagedType.Bool)]
        [DllImport("user32.dll")]
        public static extern bool GetWindowInfo(IntPtr hwnd, ref WINDOWINFO pwi);
        [DllImport("user32.dll", SetLastError=true)]
        public static extern int GetWindowLong(IntPtr hWnd, int nIndex);
        [DllImport("user32.dll", CharSet=CharSet.Auto, SetLastError=true, ExactSpelling=true)]
        public static extern bool GetWindowPlacement(HandleRef hWnd, ref WINDOWPLACEMENT placement);
        [return: MarshalAs(UnmanagedType.Bool)]
        [DllImport("user32.dll")]
        public static extern bool GetWindowRect(IntPtr hwnd, out RECT lpRect);
        [DllImport("user32.dll", CharSet=CharSet.Auto, SetLastError=true, ExactSpelling=true)]
        public static extern bool GetWindowRect(HandleRef hWnd, [In, Out] ref RECT rect);
        [SecuritySafeCritical]
        public static bool GetWindowRect_DwmGetWindowAttribute(IntPtr handle, out Rect rect)
        {
            RECT rect2;
            int num = DwmGetWindowAttribute(handle, 9, out rect2, Marshal.SizeOf(typeof(RECT)));
            rect = new Rect((double) rect2.left, (double) rect2.top, (double) rect2.Width, (double) rect2.Height);
            return (num >= 0);
        }

        [DllImport("user32.dll", EntryPoint="AdjustWindowRectEx", CharSet=CharSet.Auto, SetLastError=true, ExactSpelling=true)]
        public static extern bool IntAdjustWindowRectEx(ref RECT lpRect, int dwStyle, bool bMenu, int dwExStyle);
        [DllImport("user32.dll", EntryPoint="GetClientRect", CharSet=CharSet.Auto, SetLastError=true, ExactSpelling=true)]
        public static extern bool IntGetClientRect(HandleRef hWnd, [In, Out] ref RECT rect);
        public static int IntPtrToInt32(IntPtr intPtr) => 
            (int) intPtr.ToInt64();

        [SecurityCritical, DllImport("user32.dll", CharSet=CharSet.Auto, SetLastError=true)]
        public static extern IntPtr LoadImage(IntPtr hinst, IntPtr lpszName, int uType, int cxDesired, int cyDesired, int fuLoad);
        [DllImport("user32.dll", ExactSpelling=true)]
        public static extern IntPtr MonitorFromRect(ref RECT rect, int flags);
        [DllImport("user32.dll")]
        internal static extern IntPtr MonitorFromWindow(IntPtr handle, int flags);
        [DllImport("User32.dll", CharSet=CharSet.Auto)]
        public static extern bool RedrawWindow(IntPtr hWnd, IntPtr rectUpdate, IntPtr hRgnUpdate, int uFlags);
        [DllImport("user32.dll", CharSet=CharSet.Auto, ExactSpelling=true)]
        public static extern bool RedrawWindow(HandleRef hwnd, RECT rcUpdate, HandleRef hrgnUpdate, int flags);
        [DllImport("user32.dll", CharSet=CharSet.Auto, ExactSpelling=true)]
        public static extern bool RedrawWindow(HandleRef hwnd, IntPtr rcUpdate, HandleRef hrgnUpdate, int flags);
        [DllImport("user32.dll")]
        public static extern int ReleaseDC(IntPtr hWnd, IntPtr hDC);
        [DllImport("user32.dll")]
        public static extern int RemoveMenu(IntPtr hmenu, int npos, int wflags);
        [DllImport("user32.dll")]
        public static extern bool ScreenToClient(IntPtr hWnd, ref POINT lpPoint);
        [DllImport("user32.dll", CharSet=CharSet.Auto)]
        internal static extern IntPtr SendMessage(IntPtr hWnd, int uMsg, IntPtr wParam, ref TITLEBARINFOEX lParam);
        [DllImport("user32.dll")]
        public static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);
        [DllImport("user32.dll", CharSet=CharSet.Auto, SetLastError=true, ExactSpelling=true)]
        public static extern bool SetWindowPlacement(HandleRef hWnd, [In] ref WINDOWPLACEMENT placement);
        [return: MarshalAs(UnmanagedType.Bool)]
        [DllImport("user32.dll", SetLastError=true)]
        public static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int x, int y, int cx, int cy, SetWindowPosOptions uFlags);
        [DllImport("user32.dll")]
        public static extern int SetWindowRgn(IntPtr hWnd, IntPtr hRgn, bool bRedraw);
        public static int SignedHIWORD(int n) => 
            (short) ((n >> 0x10) & 0xffff);

        public static int SignedHIWORD(IntPtr intPtr) => 
            SignedHIWORD(IntPtrToInt32(intPtr));

        public static int SignedLOWORD(int n) => 
            (short) (n & 0xffff);

        public static int SignedLOWORD(IntPtr intPtr) => 
            SignedLOWORD(IntPtrToInt32(intPtr));

        [DllImport("user32.dll")]
        public static extern IntPtr TrackPopupMenu(IntPtr menuHandle, int uFlags, int x, int y, int nReserved, IntPtr hwnd, IntPtr par);
        [DllImport("user32.dll")]
        public static extern IntPtr WindowFromPoint(POINT Point);

        public enum GWL
        {
            EXSTYLE = -20,
            STYLE = -16
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct MARGIN
        {
            public int left;
            public int right;
            public int top;
            public int bottom;
        }

        [StructLayout(LayoutKind.Sequential, CharSet=CharSet.Auto, Pack=4)]
        public class MONITORINFOEX
        {
            internal int cbSize = Marshal.SizeOf(typeof(DevExpress.Xpf.Core.NativeMethods.MONITORINFOEX));
            public DevExpress.Xpf.Core.NativeMethods.RECT rcMonitor;
            public DevExpress.Xpf.Core.NativeMethods.RECT rcWork;
            internal int dwFlags;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst=0x20)]
            internal char[] szDevice = new char[0x20];
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct NCCALCSIZE_PARAMS
        {
            public DevExpress.Xpf.Core.NativeMethods.RECT rgrc0;
            public DevExpress.Xpf.Core.NativeMethods.RECT rgrc1;
            public DevExpress.Xpf.Core.NativeMethods.RECT rgrc2;
            public IntPtr lppos;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct POINT
        {
            public int x;
            public int y;
            public POINT(int x, int y)
            {
                this.x = x;
                this.y = y;
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct RECT
        {
            public int left;
            public int top;
            public int right;
            public int bottom;
            public RECT(int left, int top, int right, int bottom)
            {
                this.left = left;
                this.top = top;
                this.right = right;
                this.bottom = bottom;
            }

            public RECT(Rect rect)
            {
                this.left = (int) rect.Left;
                this.top = (int) rect.Top;
                this.right = (int) rect.Right;
                this.bottom = (int) rect.Bottom;
            }

            public void Offset(int dx, int dy)
            {
                this.left += dx;
                this.right += dx;
                this.top += dy;
                this.bottom += dy;
            }

            public int Height
            {
                get => 
                    this.bottom - this.top;
                set => 
                    this.bottom = this.top + value;
            }
            public int Width
            {
                get => 
                    this.right - this.left;
                set => 
                    this.right = this.left + value;
            }
        }

        [Flags]
        public enum SetWindowPosOptions
        {
            ASYNCWINDOWPOS = 0x4000,
            DEFERERASE = 0x2000,
            DRAWFRAME = 0x20,
            FRAMECHANGED = 0x20,
            HIDEWINDOW = 0x80,
            NOACTIVATE = 0x10,
            NOCOPYBITS = 0x100,
            NOMOVE = 2,
            NOOWNERZORDER = 0x200,
            NOREDRAW = 8,
            NOREPOSITION = 0x200,
            NOSENDCHANGING = 0x400,
            NOSIZE = 1,
            NOZORDER = 4,
            SHOWWINDOW = 0x40
        }

        public enum TitleBarElements
        {
            TitleBar,
            Reserved,
            MinimizeButton,
            MaximizeButton,
            HelpButton,
            CloseButton
        }

        [Flags]
        public enum TitleBarElementStates
        {
            Focusable = 0x100000,
            Invisible = 0x8000,
            Offscreen = 0x10000,
            Unavailable = 1,
            Pressed = 8
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct TITLEBARINFOEX
        {
            public const int CCHILDREN_TITLEBAR = 5;
            public int cbSize;
            public DevExpress.Xpf.Core.NativeMethods.RECT rcTitleBar;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst=6)]
            public int[] rgstate;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst=6)]
            public DevExpress.Xpf.Core.NativeMethods.RECT[] rgrect;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct WINDOWINFO
        {
            public int cbSize;
            public DevExpress.Xpf.Core.NativeMethods.RECT rcWindow;
            public DevExpress.Xpf.Core.NativeMethods.RECT rcClient;
            public int dwStyle;
            public int dwExStyle;
            public int dwWindowStatus;
            public int cxWindowBorders;
            public int cyWindowBorders;
            public short atomWindowType;
            public short wCreatorVersion;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct WINDOWPLACEMENT
        {
            public int length;
            public int flags;
            public int showCmd;
            public int ptMinPosition_x;
            public int ptMinPosition_y;
            public int ptMaxPosition_x;
            public int ptMaxPosition_y;
            public int rcNormalPosition_left;
            public int rcNormalPosition_top;
            public int rcNormalPosition_right;
            public int rcNormalPosition_bottom;
        }
    }
}

