namespace Devart.Cryptography
{
    using System;
    using System.Security.Cryptography;

    internal abstract class @as : ICryptoTransform
    {
        protected ad a;

        public @as(ad A_0);
        public virtual byte[] a(byte[] A_0, int A_1, int A_2);
        public virtual int a(byte[] A_0, int A_1, int A_2, byte[] A_3, int A_4);
        protected void b(byte[] A_0, int A_1, int A_2, byte[] A_3, int A_4);
        public void Dispose();

        public bool System.Security.Cryptography.ICryptoTransform.CanReuseTransform { get; }

        public bool System.Security.Cryptography.ICryptoTransform.CanTransformMultipleBlocks { get; }

        public int System.Security.Cryptography.ICryptoTransform.InputBlockSize { get; }

        public int System.Security.Cryptography.ICryptoTransform.OutputBlockSize { get; }
    }
}

