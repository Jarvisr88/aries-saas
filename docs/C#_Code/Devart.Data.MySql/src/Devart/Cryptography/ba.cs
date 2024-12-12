namespace Devart.Cryptography
{
    using System;
    using System.Security.Cryptography;

    internal sealed class ba : av
    {
        private const int a = 8;
        private const int b = 0x18;
        private byte[] c;
        private uint[] d;
        private uint[] e;
        private uint[] f;
        private bool g;
        private static KeySizes[] h;
        private static KeySizes[] i;

        static ba();
        public ba();
        public bool a();
        public static bool a(byte[] A_0);
        public void a(bool A_0);
        public override ICryptoTransform a(byte[] A_0, byte[] A_1);
        private static bool a(byte[] A_0, int A_1, int A_2, int A_3);
        public void a(byte[] A_0, int A_1, int A_2, byte[] A_3, int A_4);
        public override ICryptoTransform b();
        private static bool b(byte[] A_0);
        public override ICryptoTransform b(byte[] A_0, byte[] A_1);
        public void b(byte[] A_0, int A_1, int A_2, byte[] A_3, int A_4);
        public override ICryptoTransform c();
        public override void f();
        public override void g();

        public override byte[] System.Security.Cryptography.SymmetricAlgorithm.Key { get; set; }

        public override byte[] System.Security.Cryptography.SymmetricAlgorithm.IV { get; set; }
    }
}

