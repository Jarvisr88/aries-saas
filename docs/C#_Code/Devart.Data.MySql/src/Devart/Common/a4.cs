namespace Devart.Common
{
    using Devart.Security;
    using Devart.Security.SSL;
    using System;
    using System.Runtime.InteropServices;

    internal class a4 : a9
    {
        private m a;
        private l b;
        private l c;
        private bool d;
        private Devart.Security.e[] e;
        private Devart.Security.e f;
        private s g;
        private const int h = 0x20000;

        public a4(s A_0);
        protected override void a();
        private void a(bool A_0);
        protected override bool a(Exception A_0);
        private ac a(string A_0);
        public void a(string A_0, string A_1);
        private void a(Devart.Security.SSL.e A_0, ah A_1, i A_2);
        protected override void a(byte[] A_0, int A_1, int A_2);
        private static void a(string A_0, out p A_1, out string A_2);
        private void a(Devart.Security.SSL.e A_0, Devart.Security.e A_1, aj A_2, Devart.Security.SSL.f A_3);
        protected override void b();
        public void b(bool A_0);
        private ac b(string A_0);
        protected override int b(byte[] A_0, int A_1, int A_2);
        private Devart.Security.e c(string A_0);
        public bool d();
        private static bool d(string A_0);
        public s e();
        public void e(string A_0);
        public void f();
        public void f(string A_0);
        public override bool g();
        public void g(string A_0);
    }
}

