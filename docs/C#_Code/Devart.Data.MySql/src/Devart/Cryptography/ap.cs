namespace Devart.Cryptography
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    internal struct ap
    {
        [MarshalAs(UnmanagedType.LPWStr)]
        private string a;
        [MarshalAs(UnmanagedType.LPWStr)]
        public string b;
        public int c;
        public int d;
        public int e;
        public IntPtr f;
        public int g;
        public string a();
        public void b(string A_0);
        public string b();
        public void a(string A_0);
    }
}

