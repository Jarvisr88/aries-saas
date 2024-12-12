namespace DevExpress.Xpf.Core
{
    using System;

    internal static class Crc8
    {
        public static byte Calc(byte[] data)
        {
            byte num = 0xff;
            int index = 0;
            while (index < data.Length)
            {
                num = (byte) (num ^ data[index]);
                int num3 = 0;
                while (true)
                {
                    if (num3 >= 8)
                    {
                        index++;
                        break;
                    }
                    num = ((num & 0x80) != 0) ? ((byte) ((num << 1) ^ 0x31)) : ((byte) (num << 1));
                    num3++;
                }
            }
            return num;
        }
    }
}

