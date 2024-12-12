namespace DevExpress.Xpf.Core.HandleDecorator.Helpers
{
    using System;
    using System.Windows;
    using System.Windows.Interop;

    public class NativeHelper
    {
        public const int WM_DISPLAYCHANGE = 0x7e;
        public const int WS_EX_NOACTIVATE = 0x8000000;
        public const int WS_EX_LAYERED = 0x80000;
        public const int WS_EX_TOOLWINDOW = 0x80;
        public const int WS_EX_TRANSPARENT = 0x20;
        public const int WS_VISIBLE = 0x10000000;
        public const int WS_MINIMIZE = 0x20000000;
        public const int WS_CHILDWINDOW = 0x40000000;
        public const int WS_CLIPCHILDREN = 0x2000000;
        public const int WS_DISABLED = 0x8000000;
        public const int WM_DESTROY = 2;
        public const int GWL_WNDPROC = -4;
        public const int GWL_HWNDPARENT = -8;
        public const int GWL_EXSTYLE = -20;
        public const int WA_ACTIVE = 1;
        public const int WA_CLICKACTIVE = 2;

        internal static IntPtr GetHandle(Window window) => 
            new WindowInteropHelper(window).Handle;

        internal static int GetLowWord(int value) => 
            (short) (value & 0xffff);

        internal static bool IsHandleCreated(Window window) => 
            GetHandle(window) != IntPtr.Zero;
    }
}

