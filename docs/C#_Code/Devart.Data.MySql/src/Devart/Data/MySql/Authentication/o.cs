namespace Devart.Data.MySql.Authentication
{
    using Devart.Data.MySql;
    using System;
    using System.Runtime.InteropServices;
    using System.Text;

    internal class o
    {
        public const string a = "mysql_old_password";
        public const string b = "mysql_native_password";
        public const string c = "mysql_clear_password";
        public const string d = "authentication_windows_client";
        public const string e = "auth_gssapi_client";
        public const string f = "sha256_password";
        public const string g = "caching_sha2_password";
        public const string h = "client_ed25519";
        public const string i = "dialog";
        public const int j = 8;
        public const int k = 20;
        public const int l = 20;

        private static double a(ref Devart.Data.MySql.Authentication.o.a A_0);
        public static Devart.Data.MySql.Authentication.j a(bj A_0, Devart.Data.MySql.Authentication.h A_1);
        private static void a(long[] A_0, byte[] A_1);
        private static void a(byte[] A_0, byte[] A_1, int A_2);
        private static void a(ref Devart.Data.MySql.Authentication.o.a A_0, long A_1, long A_2);
        public static void a(byte[] A_0, string A_1, Encoding A_2);
        public static byte[] a(string A_0, byte[] A_1, Encoding A_2);
        public static void a(byte[] A_0, int A_1, byte[] A_2, byte[] A_3, int A_4);
        public static void b(byte[] A_0, byte[] A_1, int A_2);
        public static byte[] b(string A_0, byte[] A_1, Encoding A_2);
        public static byte[] c(string A_0, byte[] A_1, Encoding A_2);
        public static byte[] d(string A_0, byte[] A_1, Encoding A_2);

        [StructLayout(LayoutKind.Sequential)]
        private struct a
        {
            public long a;
            public long b;
            public long c;
            public double d;
        }
    }
}

