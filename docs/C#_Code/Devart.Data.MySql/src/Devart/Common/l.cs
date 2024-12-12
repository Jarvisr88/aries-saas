namespace Devart.Common
{
    using System;
    using System.IO;
    using System.Net;
    using System.Runtime.InteropServices;
    using System.Threading;

    internal class l : Devart.Common.g
    {
        private static int a;
        private int b;
        private int c;
        private string d;
        private string e;
        private string f;
        private string g;
        private ManualResetEvent h;
        private const int i = 0x7530;
        private const int j = 0x1b58;
        private int k;
        private Timer l;
        private object m;
        private Exception n;
        private bool o;
        private string p;
        private string q;
        private bool r;
        private Stream s;
        private int t;
        private HttpWebRequest u;
        private bool v;
        private bool w;

        public l(HttpOptions A_0, string A_1, int A_2);
        public l(string A_0, string A_1, string A_2, string A_3, int A_4, bool A_5, bool A_6);
        protected override void a();
        protected override void a(bool A_0);
        private string a(char A_0);
        private void a(Stream A_0);
        private static void a(HttpWebRequest A_0);
        private void a(object A_0);
        private void a(Stream A_0, bool A_1);
        private static void a(HttpWebRequest A_0, bool A_1);
        private Stream a(string A_0, char A_1, out HttpWebRequest A_2);
        private HttpWebRequest a(string A_0, char A_1, string A_2);
        protected override void a(byte[] A_0, int A_1, int A_2);
        private Stream a(string A_0, char A_1, bool A_2, out HttpWebRequest A_3);
        public void b(bool A_0);
        private void b(object A_0);
        protected override int b(byte[] A_0, int A_1, int A_2);
        public void c(bool A_0);
        private void d();
        public void d(bool A_0);
        private WebProxy e();
        private void f();
        private int i();
        public bool j();
        public bool k();
        public bool l();

        private static class a
        {
            public const char a = 'x';
            public const char b = 'r';
            public const char c = 'w';
            public const char d = 'l';
            public const char e = 'c';
            public const char f = 't';
        }

        private static class b
        {
            public const string a = "OK:";
            public const string b = "ER:";
            public static readonly int c;

            static b();
        }
    }
}

