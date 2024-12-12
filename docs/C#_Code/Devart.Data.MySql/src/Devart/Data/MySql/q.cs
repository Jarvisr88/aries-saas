namespace Devart.Data.MySql
{
    using System;
    using System.Collections;
    using System.IO;
    using System.Net.Sockets;
    using System.Runtime.InteropServices;
    using System.Timers;

    internal class q : bl
    {
        private int a;
        private string b;
        private int c;
        private Socket d;
        private Socket e;
        private readonly Devart.Data.MySql.q.f f;
        private DateTime g;
        private bool h;
        private int i;
        private int j;
        private bool k;
        private readonly byte[] l;
        private bool m;
        private readonly Timer n;
        private Devart.Data.MySql.q.d o;
        private bool p;
        private int q;
        public int r;
        public static int s;
        private const int t = 0xfffff0;
        private const byte u = 0x42;
        private const byte v = 0x43;
        private const int w = 3;
        internal const int x = -2;
        private const int y = 0x2000;
        private const int z = 0x3e8;
        internal const int aa = 0x2dc6c0;
        private const int ab = 0x5b8d80;
        private const int ac = 0xfe00;
        private const int ad = 2;
        private const int ae = 1;
        private const int af = 300;
        private const int ag = 0x19000;
        internal const int ah = 0x22b8;
        internal const int ai = 0xc38;
        internal const string aj = "localhost";

        public q();
        public q(MySqlHttpOptions A_0);
        private bool a();
        public void a(bool A_0);
        private int a(int A_0);
        public static void a(MemoryStream A_0);
        private static byte[] a(byte[] A_0);
        private static void a(Socket A_0);
        private static bool a(string A_0);
        private void a(object A_0, ElapsedEventArgs A_1);
        private void a(byte[] A_0, int A_1, int A_2);
        private int a(Devart.Data.MySql.q.e A_0, byte[] A_1, int A_2, int A_3);
        public int a(byte[] A_0, int A_1, int A_2, MemoryStream A_3);
        private int a(byte[] A_0, int A_1, int A_2, out Devart.Data.MySql.q.e A_3, MemoryStream A_4);
        private void b();
        private static byte[] b(byte[] A_0);
        public void b(int A_0);
        private void b(Socket A_0);
        private static bool b(string A_0);
        public int b(byte[] A_0, int A_1, int A_2);
        private void b(Devart.Data.MySql.q.e A_0, byte[] A_1, int A_2, int A_3);
        private void c();
        public void c(int A_0);
        public void c(string A_0);
        private void d();
        public void d(int A_0);
        public void d(string A_0);
        private void e();
        public void e(int A_0);
        public void e(string A_0);
        private void f();
        public void f(int A_0);
        public void f(string A_0);
        protected override void g();
        public void g(int A_0);
        public void g(string A_0);
        public int h();
        public void h(int A_0);
        public bool i();
        public int j();
        public int k();
        public string l();
        public string m();
        public void n();
        private void o();
        public string p();
        public int q();
        public Stream r();
        public string s();
        public int t();
        public string u();
        public int v();

        private enum a
        {
            public const Devart.Data.MySql.q.a a = Devart.Data.MySql.q.a.a;,
            public const Devart.Data.MySql.q.a b = Devart.Data.MySql.q.a.b;,
            public const Devart.Data.MySql.q.a c = Devart.Data.MySql.q.a.c;,
            public const Devart.Data.MySql.q.a d = Devart.Data.MySql.q.a.d;,
            public const Devart.Data.MySql.q.a e = Devart.Data.MySql.q.a.e;
        }

        private class b : Exception
        {
            public b();
            public b(string A_0);
        }

        private class c
        {
            private static string a(q.c.d A_0);
            private static q.c.c a(MemoryStream A_0);
            private static q.c.c a(Socket A_0);
            private static byte[] a(MemoryStream A_0, int A_1);
            private static int a(Socket A_0, Devart.Data.MySql.q.c.b A_1);
            private static int a(Socket A_0, q.c.c A_1);
            public static int a(Socket A_0, q.f A_1);
            private static byte[] a(Socket A_0, int A_1);
            public static int a(Socket A_0, q.f A_1, int A_2);
            private static int a(Socket A_0, q.f A_1, q.c.d A_2, int A_3);
            public static void b(MemoryStream A_0);
            public static void b(Socket A_0);

            private class a
            {
                private ArrayList a;
                public int b;
                public int c;
                public int d;
                public string e;

                public a();
                public ArrayList a();
                public void a(ArrayList A_0);
            }

            private class b
            {
                public Devart.Data.MySql.q.c.d a;
                public string b;
                public int c;
                public int d;
                public ArrayList e;

                public b(Devart.Data.MySql.q.c.d A_0, string A_1, int A_2, int A_3);
                public void a(string A_0, string A_1);
            }

            private class c
            {
                public string a;
                public string b;

                public c();
                public c(string A_0, string A_1);
            }

            private enum d
            {
                public const Devart.Data.MySql.q.c.d a = Devart.Data.MySql.q.c.d.a;,
                public const Devart.Data.MySql.q.c.d b = Devart.Data.MySql.q.c.d.b;,
                public const Devart.Data.MySql.q.c.d c = Devart.Data.MySql.q.c.d.c;,
                public const Devart.Data.MySql.q.c.d d = Devart.Data.MySql.q.c.d.d;,
                public const Devart.Data.MySql.q.c.d e = Devart.Data.MySql.q.c.d.e;,
                public const Devart.Data.MySql.q.c.d f = Devart.Data.MySql.q.c.d.f;,
                public const Devart.Data.MySql.q.c.d g = Devart.Data.MySql.q.c.d.g;,
                public const Devart.Data.MySql.q.c.d h = Devart.Data.MySql.q.c.d.h;
            }
        }

        private class d : Stream, IDisposable
        {
            private readonly q a;
            public MemoryStream b;

            internal d(q A_0);
            public override void a();
            public override long a(long A_0, SeekOrigin A_1);
            public override int a(byte[] A_0, int A_1, int A_2);
            public override void b(long A_0);
            public override void b(byte[] A_0, int A_1, int A_2);
            public override void d();
            private void f();
            protected override void Finalize();

            [__DynamicallyInvokable]
            public override bool System.IO.Stream.CanRead { get; }

            [__DynamicallyInvokable]
            public override bool System.IO.Stream.CanSeek { get; }

            [__DynamicallyInvokable]
            public override bool System.IO.Stream.CanWrite { get; }

            [__DynamicallyInvokable]
            public override long System.IO.Stream.Length { get; }

            [__DynamicallyInvokable]
            public override long System.IO.Stream.Position { get; set; }
        }

        private enum e : byte
        {
            public const Devart.Data.MySql.q.e a = Devart.Data.MySql.q.e.a;,
            public const Devart.Data.MySql.q.e b = Devart.Data.MySql.q.e.b;,
            public const Devart.Data.MySql.q.e c = Devart.Data.MySql.q.e.c;,
            public const Devart.Data.MySql.q.e d = Devart.Data.MySql.q.e.d;,
            public const Devart.Data.MySql.q.e e = Devart.Data.MySql.q.e.e;,
            public const Devart.Data.MySql.q.e f = Devart.Data.MySql.q.e.f;,
            public const Devart.Data.MySql.q.e g = Devart.Data.MySql.q.e.g;,
            public const Devart.Data.MySql.q.e h = Devart.Data.MySql.q.e.h;
        }

        private class f
        {
            public string a;
            public int b;
            public string c;
            public int d;
            public string e;
            public string f;
            public string g;
            public Devart.Data.MySql.q.a h;

            public f();
            public string a();
        }

        private class g : q.b
        {
            public g();
            public g(string A_0);
        }
    }
}

