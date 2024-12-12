namespace Devart.Data.MySql.Authentication
{
    using Devart.Data.MySql;
    using System;
    using System.Runtime.InteropServices;

    internal class g
    {
        private const int a = 0;
        private const int b = 0x90312;
        private const int c = 0x1013;
        private const int d = 0x1014;
        private const int e = 2;
        private const int f = 0;
        private const int g = 0x10;
        private const int h = 1;
        private const int i = 0x3000;
        private const int j = 0;
        private const int k = 0;
        private const int l = 0;
        private const int m = 1;
        private const int n = 2;
        private const int o = 0x100;
        private Devart.Data.MySql.Authentication.l p;
        private Devart.Data.MySql.Authentication.l q;
        private bj r;
        private string s;
        private byte[] t;
        private int u;
        private bool v;
        private const CharSet w = CharSet.Auto;

        public g(string A_0, bj A_1, int A_2, bool A_3);
        private byte[] a();
        private void a(byte[] A_0);
        private bool a(out byte[] A_0, byte[] A_1);
        [DllImport("secur32", CharSet=CharSet.Auto)]
        private static extern int AcquireCredentialsHandle(string A_0, string A_1, int A_2, IntPtr A_3, IntPtr A_4, int A_5, IntPtr A_6, ref Devart.Data.MySql.Authentication.l A_7, out Devart.Data.MySql.Authentication.l A_8);
        public void b();
        [DllImport("secur32", CharSet=CharSet.Auto, SetLastError=true)]
        private static extern int CompleteAuthToken(ref Devart.Data.MySql.Authentication.l A_0, ref Devart.Data.MySql.Authentication.f A_1);
        [DllImport("secur32.Dll", CharSet=CharSet.Auto)]
        public static extern int DeleteSecurityContext(ref Devart.Data.MySql.Authentication.l A_0);
        [DllImport("secur32.Dll", CharSet=CharSet.Auto)]
        public static extern int FreeCredentialsHandle(ref Devart.Data.MySql.Authentication.l A_0);
        [DllImport("secur32", CharSet=CharSet.Auto, SetLastError=true)]
        private static extern int InitializeSecurityContext(ref Devart.Data.MySql.Authentication.l A_0, IntPtr A_1, string A_2, int A_3, int A_4, int A_5, IntPtr A_6, int A_7, out Devart.Data.MySql.Authentication.l A_8, out Devart.Data.MySql.Authentication.f A_9, out uint A_10, out Devart.Data.MySql.Authentication.l A_11);
        [DllImport("secur32", CharSet=CharSet.Auto, SetLastError=true)]
        private static extern int InitializeSecurityContext(ref Devart.Data.MySql.Authentication.l A_0, ref Devart.Data.MySql.Authentication.l A_1, string A_2, int A_3, int A_4, int A_5, ref Devart.Data.MySql.Authentication.f A_6, int A_7, out Devart.Data.MySql.Authentication.l A_8, out Devart.Data.MySql.Authentication.f A_9, out uint A_10, out Devart.Data.MySql.Authentication.l A_11);
    }
}

