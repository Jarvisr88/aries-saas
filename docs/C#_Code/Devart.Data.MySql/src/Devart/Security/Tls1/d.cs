namespace Devart.Security.Tls1
{
    using System;
    using System.Security.Cryptography;

    internal class d : DeriveBytes, IDisposable
    {
        private Devart.Security.Tls1.a a;
        private Devart.Security.Tls1.a b;
        private bool c;
        private Devart.Security.Tls1.a d;
        private bool e;

        public d(byte[] A_0, string A_1, byte[] A_2, bool A_3);
        public d(byte[] A_0, byte[] A_1, byte[] A_2, bool A_3);
        public void a();
        public override byte[] a(int A_0);
        protected void a(byte[] A_0, byte[] A_1, byte[] A_2);
        public override void b();
        protected override void Finalize();
    }
}

