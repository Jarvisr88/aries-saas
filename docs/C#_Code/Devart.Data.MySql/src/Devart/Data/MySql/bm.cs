namespace Devart.Data.MySql
{
    using System;
    using System.Runtime.InteropServices;
    using System.Text;

    [StructLayout(LayoutKind.Sequential)]
    internal struct bm
    {
        public int a;
        public string b;
        public string c;
        public Encoding d;
        internal bm(int A_0, string A_1, string A_2, string A_3);
    }
}

