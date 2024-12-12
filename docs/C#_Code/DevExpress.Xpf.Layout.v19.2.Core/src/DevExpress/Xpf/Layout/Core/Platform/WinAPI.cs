namespace DevExpress.Xpf.Layout.Core.Platform
{
    using System;
    using System.Runtime.InteropServices;
    using System.Security;

    internal static class WinAPI
    {
        [DllImport("user32.dll", CharSet=CharSet.Auto, ExactSpelling=true)]
        private static extern int GetDoubleClickTime();
        [DllImport("user32.dll", CharSet=CharSet.Auto, ExactSpelling=true)]
        private static extern int GetSystemMetrics(int nIndex);
        [SecuritySafeCritical]
        private static int WA_GetDoubleClickTime() => 
            GetDoubleClickTime();

        [SecuritySafeCritical]
        private static int WA_GetSystemMetrics(int nIndex) => 
            GetSystemMetrics(0x24);

        public static int DoubleClickTime =>
            WA_GetDoubleClickTime();

        public static int DoubleClickWidth =>
            WA_GetSystemMetrics(0x24);

        public static int DoubleClickHeight =>
            WA_GetSystemMetrics(0x25);
    }
}

