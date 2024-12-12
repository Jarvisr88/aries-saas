namespace Devart.Data.MySql
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    internal struct ad
    {
        public readonly string a;
        public readonly string b;
        public readonly string c;
        public readonly string d;
        public readonly string e;
        public readonly string f;
        public readonly int g;
        public readonly int h;
        public readonly int i;
        public readonly int j;
        public readonly int k;
        public readonly bool l;
        public int m;
        public a1 n;
        public MySqlType o;
        public ad(string A_0, string A_1, string A_2, string A_3, string A_4, a1 A_5, string A_6, int A_7, int A_8, int A_9, int A_10, int A_11);
        public ad(string A_0, string A_1, string A_2, string A_3, string A_4, a1 A_5, string A_6, int A_7, int A_8, int A_9, int A_10, int A_11, bool A_12, bool A_13, bool A_14, bool A_15);
        public bool a(int A_0);
        private static a1 a(a1 A_0);
        public bool g();
        public bool a();
        public bool e();
        public bool k();
        public bool f();
        public bool d();
        public bool c();
        public bool l();
        public bool h();
        public bool b();
        public bool m();
        public bool i();
        public Type j();
    }
}

