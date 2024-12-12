namespace Devart.Common
{
    using System;

    internal abstract class a9 : s
    {
        private int a;
        private byte[] b;
        private int c;
        private int d;
        private const int e = 0x20000;
        private const int f = 0x800;
        private const int g = 0x4000;
        private Exception h;
        protected string i;
        private bool j;

        protected a9();
        public override void a(int A_0);
        protected abstract void b();
        private void b(int A_0);
        protected abstract int b(byte[] A_0, int A_1, int A_2);
        public override int c();
        protected sealed override int c(byte[] A_0, int A_1, int A_2);
        private void d();
        public void e(bool A_0);
        protected sealed override void h();
        public bool p();
    }
}

