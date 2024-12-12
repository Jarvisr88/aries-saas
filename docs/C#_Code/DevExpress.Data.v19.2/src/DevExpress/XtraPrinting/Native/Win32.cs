namespace DevExpress.XtraPrinting.Native
{
    using System;
    using System.Runtime.InteropServices;
    using System.Security;
    using System.Windows.Forms;

    public static class Win32
    {
        public static int EM_CHARFROMPOS = 0xd7;
        public static int EM_REPLACESEL = 0xc2;
        public const int GWL_EXSTYLE = -20;
        public const int WS_EX_TOOLWINDOW = 0x80;
        public const int WS_EX_CLIENTEDGE = 0x200;
        public const int WS_EX_STATICEDGE = 0x20000;
        public const int WS_BORDER = 0x800000;
        public const int WM_DESTROY = 2;
        public const int WM_SETFOCUS = 7;
        public const int WM_KILLFOCUS = 8;
        public const int WM_CONTEXTMENU = 0x7b;
        public const int WM_HSCROLL = 0x114;
        public const int WM_VSCROLL = 0x115;
        public const int WM_MOUSEACTIVATE = 0x21;
        public const int WM_MOUSEFIRST = 0x200;
        public const int WM_MOUSEMOVE = 0x200;
        public const int WM_LBUTTONDOWN = 0x201;
        public const int WM_LBUTTONUP = 0x202;
        public const int WM_LBUTTONDBLCLK = 0x203;
        public const int WM_RBUTTONDOWN = 0x204;
        public const int WM_RBUTTONUP = 0x205;
        public const int WM_RBUTTONDBLCLK = 0x206;
        public const int WM_MBUTTONDOWN = 0x207;
        public const int WM_MBUTTONUP = 520;
        public const int WM_MBUTTONDBLCLK = 0x209;
        public const int WM_XBUTTONDOWN = 0x20b;
        public const int WM_XBUTTONUP = 0x20c;
        public const int WM_XBUTTONDBLCLK = 0x20d;
        public const int WM_MOUSEWHEEL = 0x20a;
        public const int WM_MOUSELAST = 0x20a;
        public const int WM_CAPTURECHANGED = 0x215;
        public const int WM_USER = 0x400;
        public const int EM_FORMATRANGE = 0x439;
        public const int EM_SETZOOM = 0x4e1;
        public const int MM_ISOTROPIC = 7;
        public const int SC_SIZE = 0xf000;
        public const int WM_SYSCOMMAND = 0x112;
        public const int ROP_DSTINVERT = 0x550009;

        [SecuritySafeCritical]
        public static int BitBlt(IntPtr hDC, int x, int y, int nWidth, int nHeight, IntPtr hSrcDC, int xSrc, int ySrc, int dwRop) => 
            BitBltCore(hDC, x, y, nWidth, nHeight, hSrcDC, xSrc, ySrc, dwRop);

        [DllImport("gdi32.dll", EntryPoint="BitBlt")]
        private static extern int BitBltCore(IntPtr hDC, int x, int y, int nWidth, int nHeight, IntPtr hSrcDC, int xSrc, int ySrc, int dwRop);
        [SecuritySafeCritical]
        public static IntPtr CreateDC(string lpszDriver, string lpszDevice, IntPtr lpszOutput, IntPtr lpInitData) => 
            CreateDCCore(lpszDriver, lpszDevice, lpszOutput, lpInitData);

        [DllImport("gdi32.dll", EntryPoint="CreateDC")]
        private static extern IntPtr CreateDCCore(string lpszDriver, string lpszDevice, IntPtr lpszOutput, IntPtr lpInitData);
        [SecuritySafeCritical]
        public static bool DeleteDC(IntPtr hdc) => 
            DeleteDCCore(hdc);

        [DllImport("gdi32.dll", EntryPoint="DeleteDC")]
        private static extern bool DeleteDCCore(IntPtr hdc);
        [SecuritySafeCritical]
        public static IntPtr GetActiveWindow() => 
            GetActiveWindowCore();

        [DllImport("user32.dll", EntryPoint="GetActiveWindow")]
        private static extern IntPtr GetActiveWindowCore();
        [SecuritySafeCritical]
        public static short GetAsyncKeyState(int keyCode) => 
            GetAsyncKeyStateCore(keyCode);

        [DllImport("user32.dll", EntryPoint="GetAsyncKeyState")]
        private static extern short GetAsyncKeyStateCore(int keyCode);
        [SecuritySafeCritical]
        public static IntPtr GetFocus() => 
            GetFocusCore();

        [DllImport("user32.dll", EntryPoint="GetFocus", CharSet=CharSet.Auto, ExactSpelling=true)]
        private static extern IntPtr GetFocusCore();
        public static MouseButtons GetMouseButtons()
        {
            MouseButtons none = MouseButtons.None;
            if (GetAsyncKeyState(1) < 0)
            {
                none |= MouseButtons.Left;
            }
            if (GetAsyncKeyState(2) < 0)
            {
                none |= MouseButtons.Right;
            }
            if (GetAsyncKeyState(4) < 0)
            {
                none |= MouseButtons.Middle;
            }
            if (GetAsyncKeyState(5) < 0)
            {
                none |= MouseButtons.XButton1;
            }
            if (GetAsyncKeyState(6) < 0)
            {
                none |= MouseButtons.XButton2;
            }
            return none;
        }

        [SecuritySafeCritical]
        public static int GetWindowLong(IntPtr hWnd, int nIndex) => 
            GetWindowLongCore(hWnd, nIndex);

        [DllImport("user32.dll", EntryPoint="GetWindowLong")]
        private static extern int GetWindowLongCore(IntPtr hWnd, int nIndex);
        [SecuritySafeCritical]
        public static IntPtr GlobalAlloc(int uFlags, IntPtr dwBytes) => 
            GlobalAllocCore(uFlags, dwBytes);

        [DllImport("kernel32.dll", EntryPoint="GlobalAlloc", CharSet=CharSet.Auto, SetLastError=true, ExactSpelling=true)]
        private static extern IntPtr GlobalAllocCore(int uFlags, IntPtr dwBytes);
        [SecuritySafeCritical]
        public static IntPtr GlobalFree(HandleRef handle) => 
            GlobalFreeCore(handle);

        [DllImport("kernel32.dll", EntryPoint="GlobalFree", CharSet=CharSet.Auto, SetLastError=true, ExactSpelling=true)]
        private static extern IntPtr GlobalFreeCore(HandleRef handle);
        [SecuritySafeCritical]
        public static IntPtr GlobalLock(HandleRef handle) => 
            GlobalLockCore(handle);

        [DllImport("kernel32.dll", EntryPoint="GlobalLock", CharSet=CharSet.Auto, SetLastError=true, ExactSpelling=true)]
        private static extern IntPtr GlobalLockCore(HandleRef handle);
        [SecuritySafeCritical]
        public static bool GlobalUnlock(HandleRef handle) => 
            GlobalUnlockCore(handle);

        [DllImport("kernel32.dll", EntryPoint="GlobalUnlock", CharSet=CharSet.Auto, SetLastError=true, ExactSpelling=true)]
        private static extern bool GlobalUnlockCore(HandleRef handle);
        private static int HiWord(int n) => 
            (n >> 0x10) & 0xffff;

        public static int HiWord(IntPtr n) => 
            HiWord((int) ((long) n));

        private static int LoWord(int n) => 
            n & 0xffff;

        public static int LoWord(IntPtr n) => 
            LoWord((int) ((long) n));

        public static IntPtr MakeLParam(int low, int high) => 
            (IntPtr) ((high << 0x10) | (low & 0xffff));

        [SecuritySafeCritical]
        public static bool MoveWindow(IntPtr hWnd, int x, int y, int width, int height, bool needRepaint) => 
            MoveWindowCore(hWnd, x, y, width, height, needRepaint);

        [SuppressUnmanagedCodeSecurity, DllImport("user32.dll", EntryPoint="MoveWindow")]
        private static extern bool MoveWindowCore(IntPtr hWnd, int x, int y, int width, int height, bool needRepaint);
        [SecuritySafeCritical]
        public static int SendMessage(IntPtr hWnd, int msg, int wParam, IntPtr lParam) => 
            SendMessageCore(hWnd, msg, wParam, lParam);

        [SecuritySafeCritical]
        public static IntPtr SendMessage(HandleRef hWnd, int msg, int wParam, IntPtr lParam) => 
            SendMessageCore(hWnd, msg, wParam, lParam);

        [SecuritySafeCritical]
        public static IntPtr SendMessage(HandleRef hWnd, int msg, int wParam, string s) => 
            SendMessageCore(hWnd, msg, wParam, s);

        [DllImport("user32.dll", EntryPoint="SendMessage")]
        private static extern int SendMessageCore(IntPtr hWnd, int msg, int wParam, IntPtr lParam);
        [DllImport("user32", EntryPoint="SendMessage")]
        private static extern IntPtr SendMessageCore(HandleRef hWnd, int msg, int wParam, IntPtr lParam);
        [DllImport("user32", EntryPoint="SendMessage")]
        private static extern IntPtr SendMessageCore(HandleRef hWnd, int msg, int wParam, string s);
        [SecuritySafeCritical]
        public static IntPtr SetActiveWindow(IntPtr hWnd) => 
            SetActiveWindowCore(hWnd);

        [DllImport("user32.dll", EntryPoint="SetActiveWindow")]
        private static extern IntPtr SetActiveWindowCore(IntPtr hWnd);
        [SecuritySafeCritical]
        public static IntPtr SetFocus(HandleRef hWnd) => 
            SetFocusCore(hWnd);

        [DllImport("user32.dll", EntryPoint="SetFocus", CharSet=CharSet.Auto, ExactSpelling=true)]
        private static extern IntPtr SetFocusCore(HandleRef hWnd);
        [SecuritySafeCritical]
        public static int SetMapMode(HandleRef hDC, int nMapMode) => 
            SetMapModeCore(hDC, nMapMode);

        [DllImport("gdi32.dll", EntryPoint="SetMapMode")]
        private static extern int SetMapModeCore(HandleRef hDC, int nMapMode);
        [SecuritySafeCritical]
        public static bool SetViewportExtEx(HandleRef hDC, int x, int y, SIZE size) => 
            SetViewportExtExCore(hDC, x, y, size);

        [DllImport("gdi32.dll", EntryPoint="SetViewportExtEx")]
        private static extern bool SetViewportExtExCore(HandleRef hDC, int x, int y, SIZE size);
        [SecuritySafeCritical]
        public static bool SetViewportOrgEx(IntPtr hDC, int x, int y, [In, Out] SIZE size) => 
            SetViewportOrgExCore(hDC, x, y, size);

        [DllImport("gdi32.dll", EntryPoint="SetViewportOrgEx")]
        private static extern bool SetViewportOrgExCore(IntPtr hDC, int x, int y, [In, Out] SIZE size);
        [SecuritySafeCritical]
        public static bool SetWindowExtEx(HandleRef hDC, int x, int y, SIZE size) => 
            SetWindowExtExCore(hDC, x, y, size);

        [DllImport("gdi32.dll", EntryPoint="SetWindowExtEx")]
        private static extern bool SetWindowExtExCore(HandleRef hDC, int x, int y, [In, Out] SIZE size);
        [SecuritySafeCritical]
        public static IntPtr SetWindowLong(IntPtr hWnd, int nIndex, IntPtr dwNewLong) => 
            SetWindowLongCore(hWnd, nIndex, dwNewLong);

        [DllImport("user32.dll", EntryPoint="SetWindowLong")]
        private static extern IntPtr SetWindowLongCore(IntPtr hWnd, int nIndex, IntPtr dwNewLong);
        [SecuritySafeCritical]
        public static bool ShowScrollBar(IntPtr hWnd, int wBar, bool bShow) => 
            ShowScrollBarCore(hWnd, wBar, bShow);

        [DllImport("user32.dll", EntryPoint="ShowScrollBar")]
        private static extern bool ShowScrollBarCore(IntPtr hWnd, int wBar, bool bShow);
        [SecuritySafeCritical]
        public static void WaitMessage()
        {
            WaitMessageCore();
        }

        [DllImport("user32.dll", EntryPoint="WaitMessage")]
        private static extern void WaitMessageCore();

        [StructLayout(LayoutKind.Sequential)]
        internal struct CharFormat2
        {
            public int cbSize;
            public int dwMask;
            public int dwEffects;
            public int yHeight;
            public int yOffset;
            public int crTextColor;
            public byte bCharSet;
            public byte bPitchAndFamily;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst=0x20)]
            public string szFaceName;
            public short wWeight;
            public short sSpacing;
            public int crBackColor;
            public int lcid;
            public int dwReserved;
            public short sStyle;
            public short wKerning;
            public byte bUnderlineType;
            public byte bAnimation;
            public byte bRevAuthor;
            public byte bReserved1;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct PARAFORMAT2
        {
            public int cbSize;
            public int dwMask;
            public short wNumbering;
            public short wReserved;
            public int dxStartIndent;
            public int dxRightIndent;
            public int dxOffset;
            public short wAlignment;
            public short cTabCount;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst=0x20)]
            public int[] rgxTabs;
            public int dySpaceBefore;
            public int dySpaceAfter;
            public int dyLineSpacing;
            public short sStyle;
            public byte bLineSpacingRule;
            public byte bOutlineLevel;
            public short wShadingWeight;
            public short wShadingStyle;
            public short wNumberingStart;
            public short wNumberingStyle;
            public short wNumberingTab;
            public short wBorderSpace;
            public short wBorderWidth;
            public short wBorders;
        }

        [StructLayout(LayoutKind.Sequential)]
        public class SIZE
        {
            public int cx;
            public int cy;
            public SIZE()
            {
            }

            public SIZE(int cx, int cy)
            {
                this.cx = cx;
                this.cy = cy;
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct STRUCT_CHARRANGE
        {
            public int cpMin;
            public int cpMax;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct STRUCT_FORMATRANGE
        {
            public IntPtr hdc;
            public IntPtr hdcTarget;
            public Win32.STRUCT_RECT rc;
            public Win32.STRUCT_RECT rcPage;
            public Win32.STRUCT_CHARRANGE chrg;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct STRUCT_RECT
        {
            public int left;
            public int top;
            public int right;
            public int bottom;
        }
    }
}

