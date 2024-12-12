namespace DevExpress.Xpf.Layout.Core.Platform
{
    using System;
    using System.Windows.Interop;

    internal static class SystemInformation
    {
        public static int DoubleClickTime =>
            BrowserInteropHelper.IsBrowserHosted ? 500 : WinAPI.DoubleClickTime;

        public static int DoubleClickWidth =>
            BrowserInteropHelper.IsBrowserHosted ? 4 : WinAPI.DoubleClickWidth;

        public static int DoubleClickHeight =>
            BrowserInteropHelper.IsBrowserHosted ? 4 : WinAPI.DoubleClickHeight;
    }
}

