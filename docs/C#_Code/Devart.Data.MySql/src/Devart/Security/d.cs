namespace Devart.Security
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    internal struct d
    {
        public int a;
        public int b;
        public int c;
        public int d;
        public int e;
        public int f;
        [MarshalAs(UnmanagedType.LPWStr)]
        public string g;
        public int h;
        [MarshalAs(UnmanagedType.LPWStr)]
        public string i;
        public string b();
        public void b(string A_0);
        public string a();
        public void a(string A_0);
    }
}

