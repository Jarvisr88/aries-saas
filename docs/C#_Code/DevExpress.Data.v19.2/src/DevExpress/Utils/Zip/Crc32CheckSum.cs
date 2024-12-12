namespace DevExpress.Utils.Zip
{
    using DevExpress.Utils;
    using System;
    using System.IO;

    [CLSCompliant(false)]
    public static class Crc32CheckSum
    {
        private static uint[] table = CreateTable();

        public static uint Calculate(Stream stream)
        {
            long position = stream.Position;
            byte[] buffer = new byte[0x32000];
            int count = 0;
            uint maxValue = uint.MaxValue;
            while (true)
            {
                count = stream.Read(buffer, 0, 0x32000);
                maxValue = Update(maxValue, buffer, 0, count);
                if (count != 0x32000)
                {
                    stream.Position = position;
                    return ~maxValue;
                }
            }
        }

        public static uint Calculate(string str)
        {
            byte[] bytes = DXEncoding.ASCII.GetBytes(str);
            return (Update(uint.MaxValue, bytes, 0, bytes.Length) ^ uint.MaxValue);
        }

        public static uint Calculate(uint checkSum, byte ch) => 
            (checkSum >> 8) ^ table[((int) (checkSum ^ ch)) & 0xff];

        private static uint[] CreateTable()
        {
            uint[] numArray = new uint[0x100];
            uint index = 0;
            while (index < 0x100)
            {
                uint num2 = index;
                int num3 = 0;
                while (true)
                {
                    if (num3 >= 8)
                    {
                        numArray[index] = num2;
                        index++;
                        break;
                    }
                    num2 = ((num2 & 1) == 0) ? (num2 >> 1) : (0xedb88320 ^ (num2 >> 1));
                    num3++;
                }
            }
            return numArray;
        }

        public static uint Update(uint checkSum, byte[] buffer, int offset, int count)
        {
            int index = offset;
            int num2 = offset + count;
            while (index < num2)
            {
                checkSum = (checkSum >> 8) ^ table[((int) (checkSum ^ buffer[index])) & 0xff];
                index++;
            }
            return checkSum;
        }

        public static uint[] Table =>
            table;
    }
}

