namespace Devart.Common
{
    using Devart.Cryptography;
    using System;
    using System.Text;

    internal class n
    {
        private static Devart.Cryptography.ad a;

        private static Devart.Cryptography.ad a()
        {
            Devart.Cryptography.ad a;
            lock (typeof(Devart.Common.n))
            {
                if (Devart.Common.n.a != null)
                {
                    a = Devart.Common.n.a;
                }
                else
                {
                    Devart.Common.n.a = new Devart.Cryptography.ad();
                    Devart.Common.n.a.a(b());
                    a = Devart.Common.n.a;
                }
            }
            return a;
        }

        public static byte[] a(byte[] A_0)
        {
            byte[] buffer = new byte[A_0.Length];
            a().a(A_0, 0, A_0.Length, buffer, 0, new byte[8]);
            int count = (buffer[1] << 8) | buffer[0];
            byte[] dst = new byte[count];
            Buffer.BlockCopy(buffer, 2, dst, 0, count);
            return dst;
        }

        public static byte[] a(byte[] A_0, byte[] A_1, byte[] A_2)
        {
            a().a(A_0, 0, A_0.Length, A_1, 0, new byte[8]);
            int count = (A_1[1] << 8) | A_1[0];
            byte[] dst = new byte[count];
            Buffer.BlockCopy(A_1, 2, dst, 0, count);
            return dst;
        }

        private static byte[] b() => 
            new byte[] { 0xf9, 0x88, 0x48, 1, 0x6c, 0x76, 0xd9, 0x8a, 8, 0xa4, 0x4b, 0xc0, 6, 0x31, 0x1d, 0xae };

        public static string b(byte[] A_0)
        {
            byte[] buffer = new byte[A_0.Length];
            a().a(A_0, 0, A_0.Length, buffer, 0, new byte[8]);
            int count = (buffer[1] << 8) | buffer[0];
            byte[] buffer1 = new byte[count];
            return c().GetString(buffer, 2, count);
        }

        private static Encoding c() => 
            Encoding.Default;
    }
}

