namespace Devart.Data.MySql.Authentication
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    internal struct a
    {
        public int a;
        public int b;
        public IntPtr c;
        public a(int A_0, int A_1);
        public a(byte[] A_0, int A_1);
        public void a();
    }
}

