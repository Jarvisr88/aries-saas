namespace Devart.Cryptography.PKI
{
    using Devart.Cryptography.Ed;
    using System;
    using System.Runtime.InteropServices;

    internal class i : m, Devart.Cryptography.PKI.c, l
    {
        private g a;
        private byte[] b;
        private Devart.Cryptography.Ed.b c;

        public i(byte[] A_0, bool A_1 = true);
        public i(byte[] A_0, byte[] A_1, bool A_2 = true);
        public override h a();
        public byte[] a(byte[] A_0);
        public void a(byte[] A_0, byte[] A_1);
        private byte[] a(byte[] A_0, bool A_1);
        private bool a(byte[] A_0, byte[] A_1, bool A_2);
        public override p b();
        public byte[] c();
    }
}

