namespace Devart.Cryptography
{
    using System;
    using System.Security.Cryptography;

    internal sealed class r : t
    {
        public const int a = 8;
        public const int b = 8;
        private static KeySizes[] c;
        private static KeySizes[] d;
        private uint[] e;
        private byte[] f;
        private static readonly uint[,] g;
        private static uint[,] h;

        static r();
        public r();
        public override void a();
        public static bool a(byte[] A_0);
        private static void a(uint[] A_0);
        public override ICryptoTransform a(byte[] A_0, byte[] A_1);
        private static void a(byte[] A_0, uint[] A_1);
        public static uint[] a(byte[] A_0, int A_1);
        private static void a(uint[] A_0, int A_1, uint[] A_2);
        public static void a(byte[] A_0, int A_1, byte[] A_2, int A_3, uint[] A_4);
        public void a(byte[] A_0, int A_1, int A_2, byte[] A_3, int A_4);
        public override void b();
        public static bool b(byte[] A_0);
        private static void b(uint[] A_0);
        public override ICryptoTransform b(byte[] A_0, byte[] A_1);
        public void b(byte[] A_0, int A_1);
        private static void b(uint[] A_0, int A_1, uint[] A_2);
        public static void b(byte[] A_0, int A_1, byte[] A_2, int A_3, uint[] A_4);
        public void b(byte[] A_0, int A_1, int A_2, byte[] A_3, int A_4);
        private static bool c(byte[] A_0);
        public void c(byte[] A_0, int A_1);
        public void d(byte[] A_0);
        public void e(byte[] A_0);
    }
}

