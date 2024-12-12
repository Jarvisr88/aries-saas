namespace Devart.Security.SSL3
{
    using System;
    using System.Security.Cryptography;

    internal class b : DeriveBytes, IDisposable
    {
        private byte[] a;
        private byte[] b;
        private byte[] c;
        private bool d;
        private MD5CryptoServiceProvider e;
        private SHA1CryptoServiceProvider f;
        private int g;

        public b(byte[] A_0, byte[] A_1, byte[] A_2, bool A_3);
        public void a();
        public override byte[] a(int A_0);
        protected byte[] b();
        public override void c();
        protected override void Finalize();
    }
}

