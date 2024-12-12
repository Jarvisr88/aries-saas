namespace Devart.Security.SSL3
{
    using Devart.Security;
    using System;
    using System.Security.Cryptography;

    internal sealed class c : KeyedHashAlgorithm
    {
        private HashAlgorithm a;
        private bool b;
        private bool c;
        private int d;

        public c(j A_0);
        public c(j A_0, byte[] A_1);
        protected override void a(bool A_0);
        protected override void Finalize();
        public override int get_HashSize();
        protected override void HashCore(byte[] rgb, int ib, int cb);
        protected override byte[] HashFinal();
        public override void Initialize();
    }
}

