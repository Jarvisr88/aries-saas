namespace Devart.Security.SSL
{
    using Devart.Common;
    using Devart.Security;
    using System;
    using System.Net.Sockets;

    internal class e : at
    {
        private Devart.Security.SSL.k a;
        private Devart.Security.SSL.l b;
        private r c;
        private bool d;
        private bool e;
        private a4 f;

        internal e(a4 A_0);
        public override void a();
        public void a(Devart.Security.SSL.l A_0);
        public override int a(IAsyncResult A_0);
        public override void a(SocketShutdown A_0);
        public IAsyncResult a(AsyncCallback A_0, object A_1);
        public override int a(byte[] A_0, int A_1, SocketFlags A_2);
        public override int a(byte[] A_0, int A_1, int A_2, SocketFlags A_3);
        public override IAsyncResult a(byte[] A_0, int A_1, int A_2, SocketFlags A_3, AsyncCallback A_4, object A_5);
        public void b();
        public override int b(IAsyncResult A_0);
        public override int b(byte[] A_0, int A_1, int A_2, SocketFlags A_3);
        public override IAsyncResult b(byte[] A_0, int A_1, int A_2, SocketFlags A_3, AsyncCallback A_4, object A_5);
        public aa c();
        private void c(IAsyncResult A_0);
        public Devart.Security.SSL.d d();
        public void d(IAsyncResult A_0);
        public Devart.Security.SSL.b e();
        public ab f();
        public aj g();
        public ac h();
        public void i();
        public string j();
        public Devart.Security.e k();
        protected override void l();
        public Devart.Security.e m();
    }
}

