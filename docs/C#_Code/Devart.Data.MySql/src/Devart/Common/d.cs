namespace Devart.Common
{
    using System;
    using System.Security.Cryptography;

    internal sealed class d
    {
        private RNGCryptoServiceProvider a = new RNGCryptoServiceProvider();
        private byte[] b = new byte[4];

        public int a()
        {
            this.a.GetBytes(this.b);
            return (BitConverter.ToInt32(this.b, 0) & 0x7fffffff);
        }

        public int a(int A_0)
        {
            if (A_0 < 0)
            {
                throw new ArgumentOutOfRangeException("maxValue");
            }
            return this.a(0, A_0);
        }

        public void a(byte[] A_0)
        {
            if (A_0 == null)
            {
                throw new ArgumentNullException("buffer");
            }
            this.a.GetBytes(A_0);
        }

        public int a(int A_0, int A_1)
        {
            if (A_0 > A_1)
            {
                throw new ArgumentOutOfRangeException("minValue");
            }
            if (A_0 == A_1)
            {
                return A_0;
            }
            long num = A_1 - A_0;
            while (true)
            {
                this.a.GetBytes(this.b);
                uint num2 = BitConverter.ToUInt32(this.b, 0);
                long num3 = 0x100000000L;
                long num4 = num3 % num;
                if (num2 < (num3 - num4))
                {
                    return (A_0 + (((ulong) num2) % num));
                }
            }
        }

        public double b()
        {
            this.a.GetBytes(this.b);
            return (((double) BitConverter.ToUInt32(this.b, 0)) / 4294967296);
        }
    }
}

