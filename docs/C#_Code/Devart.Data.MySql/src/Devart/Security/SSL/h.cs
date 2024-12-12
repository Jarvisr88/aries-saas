namespace Devart.Security.SSL
{
    using Devart.Common;
    using Devart.Security;
    using System;
    using System.Security.Cryptography;

    internal class h : IDisposable
    {
        private const int a = 0x4000;
        private ICryptoTransform b;
        private ICryptoTransform c;
        private KeyedHashAlgorithm d;
        private KeyedHashAlgorithm e;
        private ulong f;
        private ulong g;
        private ae h;
        private Devart.Security.SSL.g i;
        private Devart.Security.SSL.k j;
        private bool k;

        public h(Devart.Security.SSL.k A_0, Devart.Security.SSL.g A_1);
        public Devart.Security.SSL.e a();
        public an a(byte[] A_0);
        public byte[] a(af A_0);
        protected void a(ar A_0);
        internal void a(Devart.Security.SSL.g A_0);
        private int a(int A_0);
        protected byte[] a(ulong A_0);
        public void a(ICryptoTransform A_0, KeyedHashAlgorithm A_1);
        public an a(byte[] A_0, int A_1, int A_2);
        protected byte[] a(byte[] A_0, int A_1, int A_2, av A_3);
        public Devart.Security.e b();
        protected void b(ar A_0);
        public void b(ICryptoTransform A_0, KeyedHashAlgorithm A_1);
        public byte[] b(byte[] A_0, int A_1, int A_2, av A_3);
        public bool c();
        protected byte[] c(byte[] A_0, int A_1, int A_2, av A_3);
        public void d();
        protected int e();
        public ac f();
        internal Devart.Security.SSL.g g();
    }
}

