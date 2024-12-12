namespace Devart.Common
{
    using System;
    using System.Runtime.InteropServices;

    internal sealed class ar
    {
        public static IntPtr a(int A_0) => 
            Marshal.AllocHGlobal(A_0);

        public static string a(IntPtr A_0) => 
            Marshal.PtrToStringAnsi(A_0);

        public static IntPtr a(string A_0) => 
            Marshal.StringToHGlobalUni(A_0);

        public static IntPtr a(IntPtr A_0, int A_1) => 
            Marshal.ReadIntPtr(A_0, A_1);

        public static void a(IntPtr A_0, int A_1, IntPtr A_2)
        {
            Marshal.WriteIntPtr(A_0, A_1, A_2);
        }

        public static string b(IntPtr A_0) => 
            Marshal.PtrToStringAnsi(A_0);

        public static IntPtr b(string A_0) => 
            Marshal.StringToHGlobalAnsi(A_0);

        public static void c(IntPtr A_0)
        {
            Marshal.FreeHGlobal(A_0);
        }
    }
}

