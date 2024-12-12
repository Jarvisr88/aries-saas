namespace DevExpress.Xpf.Core.Native
{
    using System;
    using System.Runtime.InteropServices;
    using System.Security;

    [SuppressUnmanagedCodeSecurity, SecurityCritical]
    internal static class NativeMethodsSetLastError
    {
        [DllImport("user32.dll", EntryPoint="GetWindowLong", CharSet=CharSet.Auto, SetLastError=true)]
        private static extern int _GetWindowLong(IntPtr hWnd, int nIndex);
        [DllImport("user32.dll", EntryPoint="SetWindowLong", CharSet=CharSet.Auto)]
        private static extern int _SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);
        public static long GetWindowLong(IntPtr hwnd, int nIndex);
        [DllImport("user32.dll", CharSet=CharSet.Auto, SetLastError=true)]
        public static extern IntPtr GetWindowLongPtr(IntPtr hWnd, int nIndex);
        public static IntPtr SetWindowLong(IntPtr hwnd, int nIndex, IntPtr styleFlags);
        [DllImport("user32.dll", CharSet=CharSet.Auto)]
        public static extern IntPtr SetWindowLongPtr(IntPtr hWnd, int nIndex, IntPtr dwNewLong);
    }
}

