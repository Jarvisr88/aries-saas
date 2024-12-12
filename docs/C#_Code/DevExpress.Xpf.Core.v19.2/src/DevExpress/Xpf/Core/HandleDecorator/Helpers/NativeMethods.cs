namespace DevExpress.Xpf.Core.HandleDecorator.Helpers
{
    using System;
    using System.Drawing;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Security;
    using System.Windows.Forms;

    [SecuritySafeCritical]
    public class NativeMethods
    {
        public const int GW_HWNDPREV = 3;
        public const int GW_OWNER = 4;

        public static IntPtr CreateCompatibleDC(IntPtr hDC) => 
            UnsafeNativeMethods.CreateCompatibleDC(hDC);

        public static IntPtr CreateCompatibleDC(HandleRef hDC) => 
            UnsafeNativeMethods.CreateCompatibleDC(hDC);

        public static IntPtr CreateWindowEx(int dwExStyle, IntPtr classAtom, string lpWindowName, int dwStyle, int x, int y, int nWidth, int nHeight, IntPtr hWndParent, IntPtr hMenu, IntPtr hInstance, IntPtr lpParam) => 
            UnsafeNativeMethods.CreateWindowEx(dwExStyle, classAtom, lpWindowName, dwStyle, x, y, nWidth, nHeight, hWndParent, hMenu, hInstance, lpParam);

        public static IntPtr DefWindowProc(IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam) => 
            UnsafeNativeMethods.DefWindowProc(hWnd, msg, wParam, lParam);

        public static bool DeleteObject(IntPtr hObject) => 
            UnsafeNativeMethods.DeleteObject(hObject);

        public static bool DeleteObject(HandleRef hObject) => 
            UnsafeNativeMethods.DeleteObject(hObject);

        public static bool DestroyWindow(IntPtr hWnd) => 
            UnsafeNativeMethods.DestroyWindow(hWnd);

        public static int GetClassInfo(IntPtr hInstance, string lpClassName, ref WNDCLASS lpWndClass) => 
            UnsafeNativeMethods.GetClassInfo(hInstance, lpClassName, ref lpWndClass);

        public static HDC GetDC(HWND handle) => 
            UnsafeNativeMethods.GetDC(handle);

        internal static long GetIntFromPtr(IntPtr ptr) => 
            (IntPtr.Size != 4) ? ptr.ToInt64() : ((long) ptr.ToInt32());

        public static IntPtr GetWindow(IntPtr hWnd, int wCmd) => 
            UnsafeNativeMethods.GetWindow(hWnd, (uint) wCmd);

        public static int GetWindowLong(IntPtr hwnd, int index) => 
            UnsafeNativeMethods.GetWindowLong(hwnd, index);

        public static bool GetWindowPlacement(IntPtr hWnd, out WINDOWPLACEMENT lpwndpl) => 
            UnsafeNativeMethods.GetWindowPlacement(hWnd, out lpwndpl);

        public static bool GetWindowRect(IntPtr hWnd, ref RECT lpRect) => 
            UnsafeNativeMethods.GetWindowRect(hWnd, ref lpRect);

        public static bool IsIconic(IntPtr hWnd) => 
            UnsafeNativeMethods.IsZoomed(hWnd);

        public static bool IsWindowVisible(IntPtr hWnd) => 
            UnsafeNativeMethods.IsWindowVisible(hWnd);

        public static bool IsZoomed(IntPtr hWnd) => 
            UnsafeNativeMethods.IsZoomed(hWnd);

        public static int RegisterClass(ref WNDCLASS lpWndClass) => 
            UnsafeNativeMethods.RegisterClass(ref lpWndClass);

        public static IntPtr SelectObject(IntPtr hdc, IntPtr obj) => 
            UnsafeNativeMethods.SelectObject(hdc, obj);

        public static IntPtr SelectObject(HandleRef hdc, HandleRef obj) => 
            UnsafeNativeMethods.SelectObject(hdc, obj);

        public static IntPtr SetWindowLong(IntPtr hWnd, int nIndex, IntPtr dwNewLong) => 
            UnsafeNativeMethods.SetWindowLong(hWnd, nIndex, dwNewLong);

        public static bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int x, int y, int cx, int cy, int uFlags) => 
            UnsafeNativeMethods.SetWindowPos(hWnd, hWndInsertAfter, x, y, cx, cy, uFlags);

        public static int SetWindowRgn(IntPtr hWnd, IntPtr hRgn, bool redraw) => 
            UnsafeNativeMethods.SetWindowRgn(hWnd, hRgn, redraw);

        public static bool UnregisterClass(IntPtr classAtom, IntPtr hInstance) => 
            UnsafeNativeMethods.UnregisterClass(classAtom, hInstance);

        public static bool UpdateLayeredWindow(IntPtr hwnd, IntPtr hdcDst, ref POINT pptDst, ref SIZE pSizeDst, IntPtr hdcSrc, ref POINT pptSrc, int crKey, ref BLENDFUNCTION pBlend, int dwFlags) => 
            UnsafeNativeMethods.UpdateLayeredWindow(hwnd, hdcDst, ref pptDst, ref pSizeDst, hdcSrc, ref pptSrc, crKey, ref pBlend, dwFlags);

        [StructLayout(LayoutKind.Sequential)]
        public struct BLENDFUNCTION
        {
            public byte BlendOp;
            public byte BlendFlags;
            public byte SourceConstantAlpha;
            public byte AlphaFormat;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct HDC
        {
            private IntPtr _Handle;
            public static readonly DevExpress.Xpf.Core.HandleDecorator.Helpers.NativeMethods.HDC Empty;
            public HDC(IntPtr aValue)
            {
                this._Handle = aValue;
            }

            public HDC(int aValue)
            {
                this._Handle = new IntPtr(aValue);
            }

            public override bool Equals(object aObj) => 
                (aObj != null) ? (!(aObj is DevExpress.Xpf.Core.HandleDecorator.Helpers.NativeMethods.HDC) ? ((aObj is IntPtr) && this.Equals((IntPtr) aObj)) : this.Equals((DevExpress.Xpf.Core.HandleDecorator.Helpers.NativeMethods.HDC) aObj)) : false;

            public bool Equals(DevExpress.Xpf.Core.HandleDecorator.Helpers.NativeMethods.HDC aHDC) => 
                this._Handle.Equals(aHDC._Handle);

            public bool Equals(IntPtr aIntPtr) => 
                this._Handle.Equals(aIntPtr);

            public override int GetHashCode() => 
                this._Handle.GetHashCode();

            public override string ToString() => 
                "{Handle=0x" + DevExpress.Xpf.Core.HandleDecorator.Helpers.NativeMethods.GetIntFromPtr(this._Handle).ToString("X8") + "}";

            public unsafe IntPtr SelectObject(IntPtr aGDIObj) => 
                DevExpress.Xpf.Core.HandleDecorator.Helpers.NativeMethods.SelectObject(*((IntPtr*) this), aGDIObj);

            public DevExpress.Xpf.Core.HandleDecorator.Helpers.NativeMethods.HDC CreateCompatible() => 
                DevExpress.Xpf.Core.HandleDecorator.Helpers.NativeMethods.CreateCompatibleDC(this._Handle);

            public IntPtr Handle =>
                this._Handle;
            public bool IsEmpty =>
                this._Handle == IntPtr.Zero;
            public static bool operator ==(DevExpress.Xpf.Core.HandleDecorator.Helpers.NativeMethods.HDC aHdc1, DevExpress.Xpf.Core.HandleDecorator.Helpers.NativeMethods.HDC aHdc2) => 
                (aHdc1 != 0) ? aHdc1.Equals(aHdc2) : (aHdc2 == 0);

            public static bool operator ==(IntPtr aIntPtr, DevExpress.Xpf.Core.HandleDecorator.Helpers.NativeMethods.HDC aHdc) => 
                (aIntPtr != IntPtr.Zero) ? aHdc.Equals(aIntPtr) : (aHdc == 0);

            public static bool operator ==(DevExpress.Xpf.Core.HandleDecorator.Helpers.NativeMethods.HDC aHdc, IntPtr aIntPtr) => 
                (aHdc != 0) ? aHdc.Equals(aIntPtr) : (aIntPtr == IntPtr.Zero);

            public static bool operator !=(DevExpress.Xpf.Core.HandleDecorator.Helpers.NativeMethods.HDC aHdc1, DevExpress.Xpf.Core.HandleDecorator.Helpers.NativeMethods.HDC aHdc2) => 
                !(aHdc1 == aHdc2);

            public static bool operator !=(IntPtr aIntPtr, DevExpress.Xpf.Core.HandleDecorator.Helpers.NativeMethods.HDC aHdc) => 
                aIntPtr != aHdc;

            public static bool operator !=(DevExpress.Xpf.Core.HandleDecorator.Helpers.NativeMethods.HDC aHdc, IntPtr aIntPtr) => 
                aHdc != aIntPtr;

            public static implicit operator IntPtr(DevExpress.Xpf.Core.HandleDecorator.Helpers.NativeMethods.HDC aHdc) => 
                aHdc.Handle;

            public static implicit operator DevExpress.Xpf.Core.HandleDecorator.Helpers.NativeMethods.HDC(IntPtr aIntPtr) => 
                new DevExpress.Xpf.Core.HandleDecorator.Helpers.NativeMethods.HDC(aIntPtr);

            static HDC()
            {
                Empty = new DevExpress.Xpf.Core.HandleDecorator.Helpers.NativeMethods.HDC(0);
            }
        }

        public class HT
        {
            public const int HTERROR = -2;
            public const int HTTRANSPARENT = -1;
            public const int HTNOWHERE = 0;
            public const int HTCLIENT = 1;
            public const int HTCAPTION = 2;
            public const int HTSYSMENU = 3;
            public const int HTGROWBOX = 4;
            public const int HTSIZE = 4;
            public const int HTMENU = 5;
            public const int HTHSCROLL = 6;
            public const int HTVSCROLL = 7;
            public const int HTMINBUTTON = 8;
            public const int HTMAXBUTTON = 9;
            public const int HTLEFT = 10;
            public const int HTRIGHT = 11;
            public const int HTTOP = 12;
            public const int HTTOPLEFT = 13;
            public const int HTTOPRIGHT = 14;
            public const int HTBOTTOM = 15;
            public const int HTBOTTOMLEFT = 0x10;
            public const int HTBOTTOMRIGHT = 0x11;
            public const int HTBORDER = 0x12;
            public const int HTREDUCE = 8;
            public const int HTZOOM = 9;
            public const int HTSIZEFIRST = 10;
            public const int HTSIZELAST = 0x11;
            public const int HTOBJECT = 0x13;
            public const int HTCLOSE = 20;
            public const int HTHELP = 0x15;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct HWND : IWin32Window
        {
            private IntPtr _Handle;
            public static readonly DevExpress.Xpf.Core.HandleDecorator.Helpers.NativeMethods.HWND Empty;
            public HWND(IntPtr aValue)
            {
                this._Handle = aValue;
            }

            public override bool Equals(object obj) => 
                (obj != null) ? (!(obj is DevExpress.Xpf.Core.HandleDecorator.Helpers.NativeMethods.HWND) ? ((obj is IntPtr) && this.Equals((IntPtr) obj)) : this.Equals((DevExpress.Xpf.Core.HandleDecorator.Helpers.NativeMethods.HWND) obj)) : false;

            public bool Equals(IntPtr ptr) => 
                DevExpress.Xpf.Core.HandleDecorator.Helpers.NativeMethods.GetIntFromPtr(this._Handle).Equals(DevExpress.Xpf.Core.HandleDecorator.Helpers.NativeMethods.GetIntFromPtr(ptr));

            public bool Equals(DevExpress.Xpf.Core.HandleDecorator.Helpers.NativeMethods.HWND hwnd) => 
                this.Equals(hwnd._Handle);

            public bool Equals(IWin32Window window) => 
                this.Equals(window.Handle);

            public override int GetHashCode() => 
                this._Handle.GetHashCode();

            public override string ToString() => 
                "{Handle=0x" + DevExpress.Xpf.Core.HandleDecorator.Helpers.NativeMethods.GetIntFromPtr(this._Handle).ToString("X8") + "}";

            public bool IsEmpty =>
                this._Handle == IntPtr.Zero;
            public bool IsVisible =>
                DevExpress.Xpf.Core.HandleDecorator.Helpers.NativeMethods.IsWindowVisible(this._Handle);
            public IntPtr Handle =>
                this._Handle;
            public static bool operator ==(DevExpress.Xpf.Core.HandleDecorator.Helpers.NativeMethods.HWND aHwnd1, DevExpress.Xpf.Core.HandleDecorator.Helpers.NativeMethods.HWND aHwnd2) => 
                (aHwnd1 != 0) ? aHwnd1.Equals(aHwnd2) : (aHwnd2 == 0);

            public static bool operator ==(IntPtr aIntPtr, DevExpress.Xpf.Core.HandleDecorator.Helpers.NativeMethods.HWND aHwnd) => 
                (aIntPtr != IntPtr.Zero) ? aHwnd.Equals(aIntPtr) : (aHwnd == 0);

            public static bool operator ==(DevExpress.Xpf.Core.HandleDecorator.Helpers.NativeMethods.HWND aHwnd, IntPtr aIntPtr) => 
                (aHwnd != 0) ? aHwnd.Equals(aIntPtr) : (aIntPtr == IntPtr.Zero);

            public static bool operator !=(DevExpress.Xpf.Core.HandleDecorator.Helpers.NativeMethods.HWND aHwnd1, DevExpress.Xpf.Core.HandleDecorator.Helpers.NativeMethods.HWND aHwnd2) => 
                !(aHwnd1 == aHwnd2);

            public static bool operator !=(IntPtr aIntPtr, DevExpress.Xpf.Core.HandleDecorator.Helpers.NativeMethods.HWND aHwnd) => 
                aIntPtr != aHwnd;

            public static bool operator !=(DevExpress.Xpf.Core.HandleDecorator.Helpers.NativeMethods.HWND aHwnd, IntPtr aIntPtr) => 
                aHwnd != aIntPtr;

            public static implicit operator IntPtr(DevExpress.Xpf.Core.HandleDecorator.Helpers.NativeMethods.HWND aHwnd) => 
                aHwnd.Handle;

            public static implicit operator DevExpress.Xpf.Core.HandleDecorator.Helpers.NativeMethods.HWND(IntPtr aIntPtr) => 
                new DevExpress.Xpf.Core.HandleDecorator.Helpers.NativeMethods.HWND(aIntPtr);

            static HWND()
            {
                Empty = new DevExpress.Xpf.Core.HandleDecorator.Helpers.NativeMethods.HWND(IntPtr.Zero);
            }
        }

        [Serializable, StructLayout(LayoutKind.Sequential)]
        public struct POINT
        {
            public int X;
            public int Y;
            public POINT(int x, int y)
            {
                this.X = x;
                this.Y = y;
            }

            public POINT(Point pt)
            {
                this.X = pt.X;
                this.Y = pt.Y;
            }

            public Point ToPoint() => 
                new Point(this.X, this.Y);
        }

        [Serializable, StructLayout(LayoutKind.Sequential)]
        public struct RECT
        {
            public int Left;
            public int Top;
            public int Right;
            public int Bottom;
            public RECT(int l, int t, int r, int b)
            {
                this.Left = l;
                this.Top = t;
                this.Right = r;
                this.Bottom = b;
            }

            public RECT(Rectangle r)
            {
                this.Left = r.Left;
                this.Top = r.Top;
                this.Right = r.Right;
                this.Bottom = r.Bottom;
            }

            public Rectangle ToRectangle() => 
                Rectangle.FromLTRB(this.Left, this.Top, this.Right, this.Bottom);

            public void Inflate(int width, int height)
            {
                this.Left -= width;
                this.Top -= height;
                this.Right += width;
                this.Bottom += height;
            }

            public override string ToString() => 
                $"x:{this.Left},y:{this.Top},width:{this.Right - this.Left},height:{this.Bottom - this.Top}";
        }

        public enum ShowWindowCommands
        {
            Hide = 0,
            Normal = 1,
            ShowMinimized = 2,
            Maximize = 3,
            ShowMaximized = 3,
            ShowNoActivate = 4,
            Show = 5,
            Minimize = 6,
            ShowMinNoActive = 7,
            ShowNA = 8,
            Restore = 9,
            ShowDefault = 10,
            ForceMinimize = 11
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct SIZE
        {
            public int Width;
            public int Height;
            public SIZE(int w, int h)
            {
                this.Width = w;
                this.Height = h;
            }

            public SIZE(Size size)
            {
                this.Width = size.Width;
                this.Height = size.Height;
            }

            public Size ToSize() => 
                new Size(this.Width, this.Height);
        }

        public class SWP
        {
            public const int SWP_NOSIZE = 1;
            public const int SWP_NOMOVE = 2;
            public const int SWP_NOZORDER = 4;
            public const int SWP_NOREDRAW = 8;
            public const int SWP_NOACTIVATE = 0x10;
            public const int SWP_FRAMECHANGED = 0x20;
            public const int SWP_DRAWFRAME = 0x20;
            public const int SWP_SHOWWINDOW = 0x40;
            public const int SWP_HIDEWINDOW = 0x80;
            public const int SWP_NOCOPYBITS = 0x100;
            public const int SWP_NOOWNERZORDER = 0x200;
            public const int SWP_NOREPOSITION = 0x200;
            public const int SWP_NOSENDCHANGING = 0x400;
        }

        private static class UnsafeNativeMethods
        {
            [DllImport("gdi32.dll")]
            internal static extern IntPtr CreateCompatibleDC(IntPtr hDC);
            [DllImport("gdi32.dll")]
            internal static extern IntPtr CreateCompatibleDC(HandleRef hDC);
            [DllImport("user32.dll", CharSet=CharSet.Unicode, SetLastError=true)]
            internal static extern IntPtr CreateWindowEx(int dwExStyle, IntPtr classAtom, [MarshalAs(UnmanagedType.LPTStr)] string lpWindowName, int dwStyle, int x, int y, int nWidth, int nHeight, IntPtr hWndParent, IntPtr hMenu, IntPtr hInstance, IntPtr lpParam);
            [DllImport("user32.dll", CharSet=CharSet.Unicode)]
            internal static extern IntPtr DefWindowProc(IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam);
            [DllImport("gdi32.dll")]
            internal static extern bool DeleteObject(IntPtr hObject);
            [DllImport("gdi32.dll")]
            internal static extern bool DeleteObject(HandleRef hObject);
            [return: MarshalAs(UnmanagedType.Bool)]
            [DllImport("user32.dll")]
            internal static extern bool DestroyWindow(IntPtr hwnd);
            [DllImport("user32.dll", CharSet=CharSet.Unicode, SetLastError=true)]
            internal static extern int GetClassInfo(IntPtr hInstance, [MarshalAs(UnmanagedType.LPTStr)] string lpClassName, ref DevExpress.Xpf.Core.HandleDecorator.Helpers.NativeMethods.WNDCLASS lpWndClass);
            [DllImport("User32.dll")]
            internal static extern DevExpress.Xpf.Core.HandleDecorator.Helpers.NativeMethods.HDC GetDC(DevExpress.Xpf.Core.HandleDecorator.Helpers.NativeMethods.HWND handle);
            [DllImport("User32.dll")]
            internal static extern IntPtr GetWindow(IntPtr hWnd, uint wCmd);
            [DllImport("user32.dll")]
            internal static extern int GetWindowLong(IntPtr hwnd, int index);
            [return: MarshalAs(UnmanagedType.Bool)]
            [DllImport("user32.dll", SetLastError=true)]
            internal static extern bool GetWindowPlacement(IntPtr hWnd, out DevExpress.Xpf.Core.HandleDecorator.Helpers.NativeMethods.WINDOWPLACEMENT lpwndpl);
            [DllImport("USER32.dll")]
            internal static extern bool GetWindowRect(IntPtr hWnd, ref DevExpress.Xpf.Core.HandleDecorator.Helpers.NativeMethods.RECT lpRect);
            [DllImport("USER32.dll")]
            internal static extern bool IsIconic(IntPtr hwnd);
            [DllImport("USER32.dll")]
            internal static extern bool IsWindowVisible(IntPtr hWnd);
            [DllImport("USER32.dll")]
            internal static extern bool IsZoomed(IntPtr hwnd);
            [DllImport("user32.dll", CharSet=CharSet.Unicode, SetLastError=true)]
            internal static extern int RegisterClass(ref DevExpress.Xpf.Core.HandleDecorator.Helpers.NativeMethods.WNDCLASS lpWndClass);
            [DllImport("gdi32.dll", CharSet=CharSet.Auto, SetLastError=true, ExactSpelling=true)]
            internal static extern IntPtr SelectObject(IntPtr hdc, IntPtr obj);
            [DllImport("gdi32.dll", CharSet=CharSet.Auto, SetLastError=true, ExactSpelling=true)]
            internal static extern IntPtr SelectObject(HandleRef hdc, HandleRef obj);
            [DllImport("USER32.dll")]
            internal static extern int SetWindowLong(IntPtr hwnd, int flags, long val);
            [DllImport("user32.dll")]
            internal static extern IntPtr SetWindowLong(IntPtr hWnd, int nIndex, IntPtr dwNewLong);
            [DllImport("USER32.dll")]
            internal static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, int uFlags);
            [DllImport("USER32.dll")]
            internal static extern int SetWindowRgn(IntPtr hWnd, IntPtr hRgn, bool redraw);
            [return: MarshalAs(UnmanagedType.Bool)]
            [DllImport("user32.dll")]
            internal static extern bool UnregisterClass(IntPtr classAtom, IntPtr hInstance);
            [DllImport("user32.dll", CharSet=CharSet.Auto, SetLastError=true, ExactSpelling=true)]
            internal static extern bool UpdateLayeredWindow(IntPtr hwnd, IntPtr hdcDst, ref DevExpress.Xpf.Core.HandleDecorator.Helpers.NativeMethods.POINT pptDst, ref DevExpress.Xpf.Core.HandleDecorator.Helpers.NativeMethods.SIZE pSizeDst, IntPtr hdcSrc, ref DevExpress.Xpf.Core.HandleDecorator.Helpers.NativeMethods.POINT pptSrc, int crKey, ref DevExpress.Xpf.Core.HandleDecorator.Helpers.NativeMethods.BLENDFUNCTION pBlend, int dwFlags);
        }

        [Serializable, StructLayout(LayoutKind.Sequential)]
        public struct WINDOWPLACEMENT
        {
            public int Length;
            public int Flags;
            public DevExpress.Xpf.Core.HandleDecorator.Helpers.NativeMethods.ShowWindowCommands ShowCmd;
            public DevExpress.Xpf.Core.HandleDecorator.Helpers.NativeMethods.POINT MinPosition;
            public DevExpress.Xpf.Core.HandleDecorator.Helpers.NativeMethods.POINT MaxPosition;
            public DevExpress.Xpf.Core.HandleDecorator.Helpers.NativeMethods.RECT NormalPosition;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct WINDOWPOS
        {
            public IntPtr hWnd;
            public IntPtr hHndInsertAfter;
            public int x;
            public int y;
            public int cx;
            public int cy;
            public int flags;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct WNDCLASS
        {
            public int style;
            public IntPtr lpfnWndProc;
            public int cbClsExtra;
            public int cbWndExtra;
            public IntPtr hInstance;
            public IntPtr hIcon;
            public IntPtr hCursor;
            public IntPtr hbrBackground;
            [MarshalAs(UnmanagedType.LPWStr)]
            public string lpszMenuName;
            [MarshalAs(UnmanagedType.LPWStr)]
            public string lpszClassName;
        }

        public delegate IntPtr WndProc(IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam);
    }
}

