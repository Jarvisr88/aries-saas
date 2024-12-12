namespace Devart.Common
{
    using System;
    using System.Runtime.InteropServices;
    using System.Security;

    internal static class aw
    {
        public static int a(SecureString A_0) => 
            A_0.GetHashCode();

        public static bool a(SecureString A_0, SecureString A_1) => 
            false;

        [DllImport("kernel32.dll", CharSet=CharSet.Auto)]
        private static extern int lstrcmp(IntPtr A_0, IntPtr A_1);
        [DllImport("kernel32.dll")]
        private static extern void RtlZeroMemory(IntPtr A_0, int A_1);
        [DllImport("kernel32.dll")]
        private static extern int WideCharToMultiByte(uint A_0, uint A_1, IntPtr A_2, int A_3, IntPtr A_4, int A_5, IntPtr A_6, out bool A_7);
    }
}

