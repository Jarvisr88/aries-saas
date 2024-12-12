namespace Devart.Common
{
    using System;
    using System.Runtime.InteropServices;

    internal class k
    {
        public static int a = 0;
        public static int b = 0;
        public static int c = 2;
        public static int d = 0x103;
        public static int e = 2;
        public static int f = 2;

        [DllImport("Mpr.dll")]
        public static extern int WNetCloseEnum(IntPtr A_0);
        [DllImport("Mpr.dll")]
        public static extern int WNetEnumResource(IntPtr A_0, ref int A_1, IntPtr A_2, ref int A_3);
        [DllImport("Mpr.dll")]
        public static extern int WNetOpenEnum(int A_0, int A_1, int A_2, IntPtr A_3, out IntPtr A_4);

        [StructLayout(LayoutKind.Sequential)]
        public struct a
        {
            private int a;
            private int b;
            public int c;
            public int d;
            private IntPtr e;
            public string f;
            private IntPtr g;
            private IntPtr h;
        }
    }
}

