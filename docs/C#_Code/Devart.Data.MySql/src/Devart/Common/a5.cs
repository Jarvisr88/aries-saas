namespace Devart.Common
{
    using Devart.Security;
    using Devart.Security.Ssh;
    using System;
    using System.Net;
    using System.Runtime.InteropServices;

    internal class a5
    {
        private string a;
        private int b;
        private string c;
        private string d;
        private w e;
        private Devart.Security.Ssh.g[] f;
        internal static int g;
        private Devart.Security.p h;
        private int i;
        private const string j = "Unknown server's host key.\r\nYou have no guarantee that server is the computer you think it is.\r\nThe servers {0} key fingerprint is:\r\n{1} {2}.";
        private const string k = "The server host key does not match the client one.This means that either\r\nthe server administrator has changed the host key, or you have actually connected\r\nto another computer pretending to be the server.\r\nThe server {0} key fingerprint is:\r\n{1} {2}.";
        private const string l = "Host key verification failed.\r\nNo {0} host key is known for {1} and you have requested strict checking.";
        private const string m = "Host key verification failed.\r\n{0} host key for {1} hase changed and you have requested strict checking.";
        private ak n;
        private int o;
        private Devart.Common.a5.a p;
        private Devart.Security.Ssh.p q;

        static a5();
        public a5(ak A_0);
        public string a();
        public void a(Devart.Security.p A_0);
        private bool a(aj A_0);
        public void a(int A_0);
        private IPAddress a(string A_0);
        public a1 a(string A_0, int A_1);
        private bool a(string A_0, int A_1, out string A_2, out string A_3);
        public Devart.Security.p b();
        private static bool b(string A_0);
        public string c();
        public void c(string A_0);
        public a5 d();
        public void d(string A_0);
        public string e();
        public void e(string A_0);
        public int f();
        public void f(string A_0);
        public int g();
        public int h();
        public void i();

        internal class a : ae
        {
            private a7 a;

            public a();
            public a(a7 A_0);
            public void a();
            public void a(byte[] A_0);
            public void a(j A_0, ad A_1);
            public void a(bool A_0, byte[] A_1);
            public void a(byte A_0, byte[] A_1);
            public void a(Exception A_0, string A_1);
            public r a(string A_0, int A_1, string A_2, int A_3);
            public void a(string A_0, string A_1, string[] A_2, string[] A_3);
        }
    }
}

