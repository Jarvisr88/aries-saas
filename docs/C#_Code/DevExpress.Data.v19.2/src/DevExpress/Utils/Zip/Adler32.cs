namespace DevExpress.Utils.Zip
{
    using System;
    using System.IO;

    public class Adler32
    {
        private const int adler32Base = 0xfff1;
        private const uint maxAdler = 0x7fffffff;
        private uint adler32s1 = 1;
        private uint adler32s2;

        public void Add(byte value)
        {
            this.adler32s1 += value;
            this.adler32s2 += this.adler32s1;
            if (this.adler32s2 > 0x7fffffff)
            {
                this.Normalize();
            }
        }

        public void Add(byte[] values)
        {
            this.Add(values, 0, values.Length);
        }

        public void Add(byte[] values, int offset, int count)
        {
            int num = count;
            int num2 = offset;
            while (num > 0)
            {
                this.Normalize();
                int num3 = (num < 0x15ae) ? num : 0x15ae;
                num -= num3;
                while (true)
                {
                    if (num3 < 0x10)
                    {
                        if (num3 != 0)
                        {
                            do
                            {
                                this.adler32s1 += values[num2++];
                                this.adler32s2 += this.adler32s1;
                            }
                            while (--num3 != 0);
                        }
                        break;
                    }
                    this.adler32s1 += values[num2++];
                    this.adler32s2 += this.adler32s1;
                    this.adler32s1 += values[num2++];
                    this.adler32s2 += this.adler32s1;
                    this.adler32s1 += values[num2++];
                    this.adler32s2 += this.adler32s1;
                    this.adler32s1 += values[num2++];
                    this.adler32s2 += this.adler32s1;
                    this.adler32s1 += values[num2++];
                    this.adler32s2 += this.adler32s1;
                    this.adler32s1 += values[num2++];
                    this.adler32s2 += this.adler32s1;
                    this.adler32s1 += values[num2++];
                    this.adler32s2 += this.adler32s1;
                    this.adler32s1 += values[num2++];
                    this.adler32s2 += this.adler32s1;
                    this.adler32s1 += values[num2++];
                    this.adler32s2 += this.adler32s1;
                    this.adler32s1 += values[num2++];
                    this.adler32s2 += this.adler32s1;
                    this.adler32s1 += values[num2++];
                    this.adler32s2 += this.adler32s1;
                    this.adler32s1 += values[num2++];
                    this.adler32s2 += this.adler32s1;
                    this.adler32s1 += values[num2++];
                    this.adler32s2 += this.adler32s1;
                    this.adler32s1 += values[num2++];
                    this.adler32s2 += this.adler32s1;
                    this.adler32s1 += values[num2++];
                    this.adler32s2 += this.adler32s1;
                    this.adler32s1 += values[num2++];
                    this.adler32s2 += this.adler32s1;
                    num3 -= 0x10;
                }
            }
        }

        public static long Calculate(byte[] values)
        {
            Adler32 adler = new Adler32();
            adler.Add(values);
            return (long) adler.ToUInt();
        }

        public static long Calculate(int[] values)
        {
            Adler32 adler = new Adler32();
            for (int i = 0; i < values.Length; i++)
            {
                adler.Add(BitConverter.GetBytes(values[i]));
            }
            return (long) adler.ToUInt();
        }

        internal void Initialize(uint value)
        {
            this.adler32s1 = value & 0xffff;
            this.adler32s2 = value >> 0x10;
        }

        private void Normalize()
        {
            this.adler32s2 = this.adler32s2 % 0xfff1;
            this.adler32s1 = this.adler32s1 % 0xfff1;
        }

        public long ToLong() => 
            (long) this.ToUInt();

        internal uint ToUInt()
        {
            this.Normalize();
            return ((this.adler32s2 << 0x10) | this.adler32s1);
        }

        public void Write(Stream stream)
        {
            this.Normalize();
            stream.WriteByte((byte) ((this.adler32s2 & 0xff00) >> 8));
            stream.WriteByte((byte) (this.adler32s2 & 0xff));
            stream.WriteByte((byte) ((this.adler32s1 & 0xff00) >> 8));
            stream.WriteByte((byte) (this.adler32s1 & 0xff));
        }
    }
}

