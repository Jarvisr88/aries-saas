namespace Devart.Cryptography
{
    using System;
    using System.Security.Cryptography;

    internal sealed class at : KeyedHashAlgorithm
    {
        private HashAlgorithm a;
        private byte[] b;
        private bool c;
        private bool d;
        private byte[] e;

        public at(HashAlgorithm A_0);
        public at(HashAlgorithm A_0, byte[] A_1);
        protected override void a(bool A_0);
        protected override void Finalize();
        public override int get_HashSize();
        protected override void HashCore(byte[] rgb, int ib, int cb);
        protected override byte[] HashFinal();
        public override void Initialize();
    }
}

