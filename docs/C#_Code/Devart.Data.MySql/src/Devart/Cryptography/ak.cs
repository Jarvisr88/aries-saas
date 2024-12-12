namespace Devart.Cryptography
{
    using System;
    using System.Security.Cryptography;

    internal sealed class ak : ICryptoTransform
    {
        private int a;
        private int b;
        private CipherMode c;
        private int[] d;
        private int[] e;
        private byte[] f;
        private int[] g;
        private int[] h;
        private int i;
        private int[] j;
        private int[] k;
        private int l;
        private int m;
        private int n;
        private int o;
        private PaddingMode p;
        private byte[] q;
        private Devart.Cryptography.d r;
        private static readonly int[] s;
        private static readonly int[] t;
        private static readonly int[] u;
        private static readonly byte[] v;
        private static readonly int[] w;
        private static readonly int[] x;
        private static RNGCryptoServiceProvider y;

        static ak();
        private ak();
        internal ak(byte[] A_0, CipherMode A_1, byte[] A_2, int A_3, int A_4, PaddingMode A_5, Devart.Cryptography.d A_6);
        internal static RNGCryptoServiceProvider a();
        private void a(bool A_0);
        private static int a(int A_0);
        private void a(byte[] A_0);
        public byte[] a(byte[] A_0, int A_1, int A_2);
        public int a(byte[] A_0, int A_1, int A_2, byte[] A_3, int A_4);
        private void a(int[] A_0, int[] A_1, int[] A_2, int[] A_3, int[] A_4, int[] A_5);
        private int a(byte[] A_0, int A_1, int A_2, ref byte[] A_3, int A_4, PaddingMode A_5, bool A_6);
        private static int b(int A_0);
        private void b(int[] A_0, int[] A_1, int[] A_2, int[] A_3, int[] A_4, int[] A_5);
        private int b(byte[] A_0, int A_1, int A_2, ref byte[] A_3, int A_4, PaddingMode A_5, bool A_6);
        public int c();
        private static int c(int A_0);
        public void d();
        private static int d(int A_0);
        private static int e(int A_0);
        private void f();
        public void i();

        public bool System.Security.Cryptography.ICryptoTransform.CanReuseTransform { get; }

        public bool System.Security.Cryptography.ICryptoTransform.CanTransformMultipleBlocks { get; }

        public int System.Security.Cryptography.ICryptoTransform.InputBlockSize { get; }

        public int System.Security.Cryptography.ICryptoTransform.OutputBlockSize { get; }
    }
}

