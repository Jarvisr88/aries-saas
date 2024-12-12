namespace DevExpress.Utils.About
{
    using System;
    using System.Text;

    internal class CRC32
    {
        private uint[] tab;
        private uint poly;
        private static CRC32 _default;

        public CRC32() : this(0x4c11db7)
        {
        }

        public CRC32(uint poly)
        {
            this.poly = poly;
        }

        public uint ComputeHash(string text) => 
            this.ComputeHash(Encoding.UTF8.GetBytes(text));

        public uint ComputeHash(byte[] data) => 
            this.ComputeHash(data, 0, data.Length);

        public virtual uint ComputeHash(byte[] data, int start, int length) => 
            this.ComputeHash<byte[]>(data, start, length);

        public virtual uint ComputeHash<T>(T data, int start, int length) where T: IList
        {
            this.Init();
            uint maxValue = uint.MaxValue;
            for (int i = 0; i < length; i++)
            {
                byte num3 = (byte) data[i + start];
                maxValue = (maxValue << 8) ^ this.tab[num3 ^ (maxValue >> 0x18)];
            }
            return ~maxValue;
        }

        private void Init()
        {
            if (this.tab == null)
            {
                this.tab = new uint[0x100];
                uint index = 0;
                while (index < 0x100)
                {
                    uint num2 = index;
                    int num3 = 0;
                    while (true)
                    {
                        if (num3 >= 8)
                        {
                            this.tab[index] = num2;
                            index++;
                            break;
                        }
                        num2 = ((num2 & 1) != 0) ? ((num2 >> 1) ^ this.poly) : (num2 >> 1);
                        num3++;
                    }
                }
            }
        }

        public static CRC32 Default
        {
            get
            {
                _default ??= new CRC32();
                return _default;
            }
        }
    }
}

