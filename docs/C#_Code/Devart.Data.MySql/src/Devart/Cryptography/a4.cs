namespace Devart.Cryptography
{
    using System;
    using System.Security.Cryptography;

    internal sealed class a4 : ICryptoTransform
    {
        private byte[] a;
        private int b;
        private byte[] c;
        private byte d;
        private byte e;
        private bool f;

        public a4(byte[] A_0);
        private void a();
        public byte[] a(byte[] A_0, int A_1, int A_2);
        public int a(byte[] A_0, int A_1, int A_2, byte[] A_3, int A_4);
        public void Dispose();
        protected override void e();

        public bool System.Security.Cryptography.ICryptoTransform.CanReuseTransform { get; }

        public bool System.Security.Cryptography.ICryptoTransform.CanTransformMultipleBlocks { get; }

        public int System.Security.Cryptography.ICryptoTransform.InputBlockSize { get; }

        public int System.Security.Cryptography.ICryptoTransform.OutputBlockSize { get; }
    }
}

