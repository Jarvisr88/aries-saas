namespace Devart.Common
{
    using System;

    internal static class a0
    {
        public static byte[] a(bool A_0) => 
            BitConverter.GetBytes(A_0);

        public static byte[] a(char A_0)
        {
            byte[] bytes = BitConverter.GetBytes(A_0);
            if (!BitConverter.IsLittleEndian)
            {
                b(bytes);
            }
            return bytes;
        }

        public static byte[] a(double A_0)
        {
            byte[] bytes = BitConverter.GetBytes(A_0);
            if (!BitConverter.IsLittleEndian)
            {
                b(bytes);
            }
            return bytes;
        }

        public static byte[] a(short A_0)
        {
            byte[] bytes = BitConverter.GetBytes(A_0);
            if (!BitConverter.IsLittleEndian)
            {
                b(bytes);
            }
            return bytes;
        }

        public static byte[] a(int A_0)
        {
            byte[] bytes = BitConverter.GetBytes(A_0);
            if (!BitConverter.IsLittleEndian)
            {
                b(bytes);
            }
            return bytes;
        }

        public static byte[] a(long A_0)
        {
            byte[] bytes = BitConverter.GetBytes(A_0);
            if (!BitConverter.IsLittleEndian)
            {
                b(bytes);
            }
            return bytes;
        }

        public static byte[] a(float A_0)
        {
            byte[] bytes = BitConverter.GetBytes(A_0);
            if (!BitConverter.IsLittleEndian)
            {
                b(bytes);
            }
            return bytes;
        }

        public static byte[] a(ushort A_0)
        {
            byte[] bytes = BitConverter.GetBytes(A_0);
            if (!BitConverter.IsLittleEndian)
            {
                b(bytes);
            }
            return bytes;
        }

        public static byte[] a(uint A_0)
        {
            byte[] bytes = BitConverter.GetBytes(A_0);
            if (!BitConverter.IsLittleEndian)
            {
                b(bytes);
            }
            return bytes;
        }

        public static byte[] a(ulong A_0)
        {
            byte[] bytes = BitConverter.GetBytes(A_0);
            if (!BitConverter.IsLittleEndian)
            {
                b(bytes);
            }
            return bytes;
        }

        public static string a(byte[] A_0) => 
            BitConverter.ToString(A_0);

        public static ulong a(byte[] A_0, int A_1)
        {
            if (!BitConverter.IsLittleEndian)
            {
                byte[] buffer = new byte[8];
                A_0 = a(A_0, ref buffer, A_1, 8);
                A_1 = 0;
            }
            return BitConverter.ToUInt64(A_0, A_1);
        }

        public static string a(byte[] A_0, int A_1, int A_2) => 
            BitConverter.ToString(A_0, A_1, A_2);

        private static byte[] a(byte[] A_0, ref byte[] A_1, int A_2, int A_3)
        {
            int num = (A_2 + A_3) - 1;
            for (int i = 0; i < A_3; i++)
            {
                A_1[i] = A_0[num--];
            }
            return A_1;
        }

        private static void b(byte[] A_0)
        {
            for (int i = 0; i < (A_0.Length / 2); i++)
            {
                byte num2 = A_0[i];
                A_0[i] = A_0[(A_0.Length - i) - 1];
                A_0[(A_0.Length - i) - 1] = num2;
            }
        }

        public static uint b(byte[] A_0, int A_1)
        {
            if (!BitConverter.IsLittleEndian)
            {
                byte[] buffer = new byte[4];
                A_0 = a(A_0, ref buffer, A_1, 4);
                A_1 = 0;
            }
            return BitConverter.ToUInt32(A_0, A_1);
        }

        public static ushort c(byte[] A_0, int A_1)
        {
            if (!BitConverter.IsLittleEndian)
            {
                byte[] buffer = new byte[2];
                A_0 = a(A_0, ref buffer, A_1, 2);
                A_1 = 0;
            }
            return BitConverter.ToUInt16(A_0, A_1);
        }

        public static string d(byte[] A_0, int A_1) => 
            BitConverter.ToString(A_0, A_1);

        public static float e(byte[] A_0, int A_1)
        {
            if (!BitConverter.IsLittleEndian)
            {
                byte[] buffer = new byte[4];
                A_0 = a(A_0, ref buffer, A_1, 4);
                A_1 = 0;
            }
            return BitConverter.ToSingle(A_0, A_1);
        }

        public static long f(byte[] A_0, int A_1)
        {
            if (!BitConverter.IsLittleEndian)
            {
                byte[] buffer = new byte[8];
                A_0 = a(A_0, ref buffer, A_1, 8);
                A_1 = 0;
            }
            return BitConverter.ToInt64(A_0, A_1);
        }

        public static int g(byte[] A_0, int A_1)
        {
            if (!BitConverter.IsLittleEndian)
            {
                byte[] buffer = new byte[4];
                A_0 = a(A_0, ref buffer, A_1, 4);
                A_1 = 0;
            }
            return BitConverter.ToInt32(A_0, A_1);
        }

        public static short h(byte[] A_0, int A_1)
        {
            if (!BitConverter.IsLittleEndian)
            {
                byte[] buffer = new byte[2];
                A_0 = a(A_0, ref buffer, A_1, 2);
                A_1 = 0;
            }
            return BitConverter.ToInt16(A_0, A_1);
        }

        public static double i(byte[] A_0, int A_1)
        {
            if (!BitConverter.IsLittleEndian)
            {
                byte[] buffer = new byte[8];
                A_0 = a(A_0, ref buffer, A_1, 8);
                A_1 = 0;
            }
            return BitConverter.ToDouble(A_0, A_1);
        }

        public static char j(byte[] A_0, int A_1)
        {
            if (!BitConverter.IsLittleEndian)
            {
                byte[] buffer = new byte[2];
                A_0 = a(A_0, ref buffer, A_1, 2);
                A_1 = 0;
            }
            return BitConverter.ToChar(A_0, A_1);
        }

        public static bool k(byte[] A_0, int A_1) => 
            BitConverter.ToBoolean(A_0, A_1);
    }
}

