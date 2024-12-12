namespace Devart.Common
{
    using System;

    internal class aq
    {
        private static readonly int[] a = new int[] { 
            -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1,
            -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1,
            -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, 0x3e, -1, -1, -1, 0x3f,
            0x34, 0x35, 0x36, 0x37, 0x38, 0x39, 0x3a, 0x3b, 60, 0x3d, -1, -1, -1, -1, -1, -1,
            -1, 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14,
            15, 0x10, 0x11, 0x12, 0x13, 20, 0x15, 0x16, 0x17, 0x18, 0x19, -1, -1, -1, -1, -1,
            -1, 0x1a, 0x1b, 0x1c, 0x1d, 30, 0x1f, 0x20, 0x21, 0x22, 0x23, 0x24, 0x25, 0x26, 0x27, 40,
            0x29, 0x2a, 0x2b, 0x2c, 0x2d, 0x2e, 0x2f, 0x30, 0x31, 50, 0x33, -1, -1, -1, -1, -1
        };
        private static readonly byte[] b = new byte[] { 
            0x41, 0x42, 0x43, 0x44, 0x45, 70, 0x47, 0x48, 0x49, 0x4a, 0x4b, 0x4c, 0x4d, 0x4e, 0x4f, 80,
            0x51, 0x52, 0x53, 0x54, 0x55, 0x56, 0x57, 0x58, 0x59, 90, 0x61, 0x62, 0x63, 100, 0x65, 0x66,
            0x67, 0x68, 0x69, 0x6a, 0x6b, 0x6c, 0x6d, 110, 0x6f, 0x70, 0x71, 0x72, 0x73, 0x74, 0x75, 0x76,
            0x77, 120, 0x79, 0x7a, 0x30, 0x31, 50, 0x33, 0x34, 0x35, 0x36, 0x37, 0x38, 0x39, 0x2b, 0x2f,
            0x3d
        };
        private const int c = 0x40;

        public static byte[] a(byte[] A_0) => 
            b(A_0, 0, A_0.Length);

        public static byte[] a(byte[] A_0, int A_1, int A_2)
        {
            int num;
            int num2;
            int num3;
            int num4;
            int num7 = A_2 % 3;
            int num8 = A_1 + (A_2 - num7);
            byte[] buffer = new byte[((A_2 / 3) * 4) + ((num7 != 0) ? 4 : 0)];
            int index = A_1;
            int num6 = 0;
            while (index < num8)
            {
                num = (A_0[index] & 0xfc) >> 2;
                num2 = ((A_0[index] & 3) << 4) | ((A_0[index + 1] & 240) >> 4);
                num3 = ((A_0[index + 1] & 15) << 2) | ((A_0[index + 2] & 0xc0) >> 6);
                num4 = A_0[index + 2] & 0x3f;
                buffer[num6] = b[num];
                buffer[num6 + 1] = b[num2];
                buffer[num6 + 2] = b[num3];
                buffer[num6 + 3] = b[num4];
                index += 3;
                num6 += 4;
            }
            if (num7 != 0)
            {
                num = (A_0[index] & 0xfc) >> 2;
                num2 = (A_0[index] & 3) << 4;
                num3 = 0x40;
                num4 = 0x40;
                if (num7 == 2)
                {
                    num2 |= (A_0[index + 1] & 240) >> 4;
                    num3 = (A_0[index + 1] & 15) << 2;
                }
                buffer[num6++] = b[num];
                buffer[num6++] = b[num2];
                buffer[num6++] = b[num3];
                buffer[num6++] = b[num4];
            }
            return buffer;
        }

        public static byte[] b(byte[] A_0) => 
            a(A_0, 0, A_0.Length);

        public static byte[] b(byte[] A_0, int A_1, int A_2)
        {
            int num4 = 0;
            int num5 = A_1 + A_2;
            byte[] src = new byte[(A_2 * 3) / 4];
            int num3 = 0;
            int count = 0;
            for (int i = A_1; i < num5; i++)
            {
                int num2 = a[A_0[i]];
                if (num2 < 0)
                {
                    if (A_0[i] == b[0x40])
                    {
                        break;
                    }
                }
                else
                {
                    num3 = (num3 << 6) | num2;
                    num4 += 6;
                    if (num4 >= 8)
                    {
                        src[count++] = (byte) ((num3 >> ((num4 - 8) & 0x1f)) & 0xff);
                    }
                }
            }
            if (src.Length > count)
            {
                byte[] dst = new byte[count];
                Buffer.BlockCopy(src, 0, dst, 0, count);
                src = dst;
            }
            return src;
        }
    }
}

