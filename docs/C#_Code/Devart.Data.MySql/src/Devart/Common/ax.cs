namespace Devart.Common
{
    using System;
    using System.Security.Cryptography;
    using System.Text;

    internal sealed class ax
    {
        public static byte[] a(int A_0)
        {
            byte[] data = new byte[A_0];
            new RNGCryptoServiceProvider().GetBytes(data);
            return data;
        }

        public static string a(byte[] A_0) => 
            Encoding.ASCII.GetString(A_0, 0, A_0.Length);

        public static byte[] a(uint[] A_0)
        {
            int length = A_0.Length;
            byte[] buffer = new byte[length * 4];
            int index = 0;
            for (int i = 0; i < length; i++)
            {
                uint num4 = A_0[i];
                buffer[index + 3] = (byte) num4;
                buffer[index + 2] = (byte) (num4 >> 8);
                buffer[index + 1] = (byte) (num4 >> 0x10);
                buffer[index] = (byte) (num4 >> 0x18);
                index += 4;
            }
            return buffer;
        }

        public static int a(byte[] A_0, byte[] A_1) => 
            (A_0.Length == A_1.Length) ? a(A_0, 0, A_1, 0, A_0.Length) : (A_1.Length - A_0.Length);

        public static byte[] a(byte[] A_0, int A_1)
        {
            byte[] dst = new byte[A_1];
            Buffer.BlockCopy(A_0, 0, dst, 0, Math.Min(A_0.Length, dst.Length));
            return dst;
        }

        public static void a(uint A_0, byte[] A_1, int A_2)
        {
            A_1[A_2] = (byte) ((A_0 >> 0x18) & 0xff);
            A_1[A_2 + 1] = (byte) ((A_0 >> 0x10) & 0xff);
            A_1[A_2 + 2] = (byte) ((A_0 >> 8) & 0xff);
            A_1[A_2 + 3] = (byte) (A_0 & 0xff);
        }

        public static int a(byte[] A_0, int A_1, int A_2, byte A_3)
        {
            int num = A_1 + A_2;
            for (int i = A_1; i < num; i++)
            {
                if (A_0[i] != A_3)
                {
                    return (A_3 - A_0[i]);
                }
            }
            return 0;
        }

        public static int a(byte[] A_0, int A_1, byte[] A_2, int A_3, int A_4)
        {
            for (int i = 0; i < A_4; i++)
            {
                if (A_0[A_1 + i] != A_2[A_3 + i])
                {
                    return (A_2[A_3 + i] - A_0[A_1 + i]);
                }
            }
            return 0;
        }

        public static unsafe void a(byte[] A_0, int A_1, int A_2, byte[] A_3, int A_4)
        {
            while (A_2 > 0)
            {
                byte* numPtr1 = &(A_3[A_4++]);
                numPtr1[0] = (byte) (numPtr1[0] ^ A_0[A_1++]);
                A_2--;
            }
        }

        public static ulong b(byte[] A_0) => 
            (ulong) ((((((((A_0[0] << 0x18) | (A_0[1] << 0x10)) | (A_0[2] << 8)) | A_0[3]) | (A_0[4] << 0x18)) | (A_0[5] << 0x10)) | (A_0[6] << 8)) | A_0[7]);

        public static uint b(byte[] A_0, int A_1) => 
            (uint) ((((A_0[A_1] << 0x18) | (A_0[A_1 + 1] << 0x10)) | (A_0[A_1 + 2] << 8)) | A_0[A_1 + 3]);

        public static void b(uint A_0, byte[] A_1, int A_2)
        {
            A_1[A_2] = (byte) (A_0 & 0xff);
            A_1[A_2 + 1] = (byte) ((A_0 >> 8) & 0xff);
            A_1[A_2 + 2] = (byte) ((A_0 >> 0x10) & 0xff);
            A_1[A_2 + 3] = (byte) ((A_0 >> 0x18) & 0xff);
        }

        public static uint c(byte[] A_0, int A_1) => 
            (uint) (((A_0[A_1] | (A_0[A_1 + 1] << 8)) | (A_0[A_1 + 2] << 0x10)) | (A_0[A_1 + 3] << 0x18));
    }
}

