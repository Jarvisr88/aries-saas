namespace Devart.Security.SSL
{
    using Devart.Common;
    using Devart.Security;
    using System;
    using System.Collections;
    using System.Runtime.CompilerServices;
    using System.Threading;

    internal class k : IDisposable
    {
        private const int a = 0x4000;
        private Devart.Security.SSL.e b;
        private a4 c;
        private Devart.Common.s d;
        private Devart.Security.SSL.h e;
        private z f;
        private ArrayList g;
        private ArrayList h;
        private Devart.Security.SSL.q i;
        private Devart.Security.SSL.q j;
        private Devart.Security.SSL.r k;
        private bool l;
        private bool m;
        private bool n;
        private byte[] o;
        private Devart.Common.e p;
        private object q;
        private Exception r;
        private ManualResetEvent s;
        private bool t;
        private AutoResetEvent u;
        private Devart.Security.SSL.k.a v;
        private Devart.Security.SSL.k.b w;

        public k(Devart.Security.SSL.e A_0, a4 A_1, Devart.Security.SSL.l A_2);
        private int a();
        private static bool a(Devart.Common.s A_0);
        protected void a(byte[] A_0);
        private void a(Exception A_0);
        private void a(IAsyncResult A_0);
        protected byte[] a(ref byte[] A_0, int A_1);
        public Devart.Security.SSL.r a(AsyncCallback A_0, object A_1);
        protected int a(IAsyncResult A_0, ArrayList A_1);
        private int a(byte[] A_0, int A_1, int A_2);
        protected Devart.Security.SSL.r a(byte[] A_0, int A_1, int A_2, Devart.Security.SSL.r A_3, aq A_4);
        public Devart.Security.SSL.r a(byte[] A_0, int A_1, int A_2, AsyncCallback A_3, object A_4);
        protected byte[] a(byte[] A_0, int A_1, int A_2, byte[] A_3, int A_4, int A_5);
        private bool b();
        protected void b(Exception A_0);
        public Devart.Security.SSL.r b(IAsyncResult A_0);
        private int b(byte[] A_0, int A_1, int A_2);
        public Devart.Security.SSL.r b(byte[] A_0, int A_1, int A_2, AsyncCallback A_3, object A_4);
        public ac c();
        protected void c(IAsyncResult A_0);
        public void d();
        public Devart.Security.SSL.q d(IAsyncResult A_0);
        protected void e();
        public Devart.Security.SSL.q e(IAsyncResult A_0);
        public Devart.Security.e f();
        protected void f(IAsyncResult A_0);
        public void g();
        public Devart.Security.SSL.e h();
        public void i();
        public int j();
        public void k();

        private delegate int a(byte[] A_0, int A_1, int A_2);

        private delegate int b(byte[] A_0, int A_1, int A_2);
    }
}

