namespace Devart.Security.Tls1
{
    using Devart.Cryptography;
    using System;
    using System.Security.Cryptography;

    internal class a : DeriveBytes, IDisposable
    {
        private at a;
        private int b;
        private byte[] c;
        private byte[] d;
        private byte[] e;
        private bool f;

        public a(HashAlgorithm A_0, byte[] A_1, string A_2);
        public a(HashAlgorithm A_0, byte[] A_1, byte[] A_2);
        public void a();
        public override byte[] a(int A_0);
        protected void a(HashAlgorithm A_0, byte[] A_1, byte[] A_2);
        protected byte[] b();
        public override void c();
        protected override void Finalize();
    }
}

