namespace Devart.Cryptography
{
    using System;
    using System.Collections.Generic;
    using System.Security.Cryptography;

    internal class ar : ICryptoTransform
    {
        private readonly byte[] a;
        private readonly ICryptoTransform b;
        private readonly Queue<byte> c;
        private readonly SymmetricAlgorithm d;

        public ar(SymmetricAlgorithm A_0, byte[] A_1, byte[] A_2);
        private void a();
        public byte[] a(byte[] A_0, int A_1, int A_2);
        public int a(byte[] A_0, int A_1, int A_2, byte[] A_3, int A_4);
        private void b();
        public void Dispose();

        public int System.Security.Cryptography.ICryptoTransform.InputBlockSize { get; }

        public int System.Security.Cryptography.ICryptoTransform.OutputBlockSize { get; }

        public bool System.Security.Cryptography.ICryptoTransform.CanTransformMultipleBlocks { get; }

        public bool System.Security.Cryptography.ICryptoTransform.CanReuseTransform { get; }
    }
}

