namespace Devart.Security.SSL3
{
    using Devart.Security;
    using System;
    using System.Security.Cryptography;

    internal sealed class d : KeyedHashAlgorithm
    {
        private HashAlgorithm a;
        private bool b;
        private int c;

        public d(j A_0);
        public d(j A_0, byte[] A_1);
        public d(j A_0, HashAlgorithm A_1, byte[] A_2);
        protected override void a(bool A_0);
        protected override void Finalize();
        public override int get_HashSize();
        protected override void HashCore(byte[] rgb, int ib, int cb);
        protected override byte[] HashFinal();
        public override void Initialize();
    }
}

