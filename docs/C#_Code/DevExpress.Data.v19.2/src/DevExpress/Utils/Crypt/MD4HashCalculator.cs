namespace DevExpress.Utils.Crypt
{
    using DevExpress.Utils.Zip;
    using System;

    [CLSCompliant(false)]
    public class MD4HashCalculator : ICheckSumCalculator<uint[]>
    {
        private const uint A = 0x67452301;
        private const uint B = 0xefcdab89;
        private const uint C = 0x98badcfe;
        private const uint D = 0x10325476;
        private const uint rootOf2 = 0x5a827999;
        private const uint rootOf3 = 0x6ed9eba1;
        private const int blockLength = 0x40;
        private const int wordsPerStep = 0x10;
        private static readonly int[] stepFCoeffs = new int[] { 3, 7, 11, 0x13 };
        private static readonly int[] stepGCoeffs = new int[] { 3, 5, 9, 13 };
        private static readonly int[] stepHIndices = new int[] { 0, 2, 1, 3 };
        private static readonly int[] stepHCoeffs = new int[] { 3, 9, 11, 15 };
        private int position;
        private int bytesRead;
        private int tailLength;
        private int index;
        private long bitsCount;
        private byte[] hashBuffer = new byte[0x40];
        private byte[] tail = new byte[0x40];

        private static uint[] Decode(byte[] input, int offset)
        {
            uint[] numArray = new uint[0x40];
            for (int i = 0; i < 0x10; i++)
            {
                numArray[i] = (uint) ((((input[offset++] & 0xff) | ((input[offset++] & 0xff) << 8)) | ((input[offset++] & 0xff) << 0x10)) | ((input[offset++] & 0xff) << 0x18));
            }
            return numArray;
        }

        private static uint F(uint x, uint y, uint z) => 
            (x & y) | (~x & z);

        private static uint G(uint x, uint y, uint z) => 
            ((x & y) | (x & z)) | (y & z);

        public uint[] GetFinalCheckSum(uint[] value)
        {
            this.bytesRead += this.tailLength;
            this.bitsCount += this.bytesRead << 3;
            if (this.tailLength > 0)
            {
                Array.Copy(this.tail, 0, this.hashBuffer, this.index, this.tailLength);
            }
            this.Reset();
            this.index = (int) ((this.bitsCount >> 3) & 0x3f);
            int num = (this.index < 0x38) ? (0x38 - this.index) : (120 - this.index);
            byte[] buffer = new byte[] { 0x80 };
            for (int i = 0; i < 8; i++)
            {
                buffer[num + i] = (byte) (this.bitsCount >> ((8 * i) & 0x3f));
            }
            return this.UpdateCheckSum(value, buffer, 0, buffer.Length);
        }

        private static uint H(uint x, uint y, uint z) => 
            (x ^ y) ^ z;

        private void Reset()
        {
            this.bytesRead = 0;
            this.position = 0;
            this.tailLength = 0;
            this.tail = new byte[0x40];
        }

        private static uint RotateLeftCircularly(uint x, int s) => 
            (x << (s & 0x1f)) | (x >> ((0x20 - s) & 0x1f));

        private static uint StepF(uint a, uint b, uint c, uint d, uint x, int s)
        {
            a = (a + F(b, c, d)) + x;
            return RotateLeftCircularly(a, s);
        }

        private static uint StepG(uint a, uint b, uint c, uint d, uint x, int s)
        {
            a = ((a + G(b, c, d)) + x) + 0x5a827999;
            return RotateLeftCircularly(a, s);
        }

        private static uint StepH(uint a, uint b, uint c, uint d, uint x, int s)
        {
            a = ((a + H(b, c, d)) + x) + 0x6ed9eba1;
            return RotateLeftCircularly(a, s);
        }

        private static void Transform(byte[] block, int offset, uint[] hash)
        {
            TransformCore(Decode(block, offset), hash);
        }

        private static void TransformCore(uint[] block, uint[] hash)
        {
            uint a = hash[0];
            uint b = hash[1];
            uint c = hash[2];
            uint d = hash[3];
            int num5 = 4;
            for (int i = 0; i < num5; i++)
            {
                a = StepF(a, b, c, d, block[i * 4], stepFCoeffs[0]);
                d = StepF(d, a, b, c, block[(i * 4) + 1], stepFCoeffs[1]);
                c = StepF(c, d, a, b, block[(i * 4) + 2], stepFCoeffs[2]);
                b = StepF(b, c, d, a, block[(i * 4) + 3], stepFCoeffs[3]);
            }
            for (int j = 0; j < num5; j++)
            {
                a = StepG(a, b, c, d, block[j], stepGCoeffs[0]);
                d = StepG(d, a, b, c, block[j + 4], stepGCoeffs[1]);
                c = StepG(c, d, a, b, block[j + 8], stepGCoeffs[2]);
                b = StepG(b, c, d, a, block[j + 12], stepGCoeffs[3]);
            }
            for (int k = 0; k < num5; k++)
            {
                int index = stepHIndices[k];
                a = StepH(a, b, c, d, block[index], stepHCoeffs[0]);
                d = StepH(d, a, b, c, block[index + 8], stepHCoeffs[1]);
                c = StepH(c, d, a, b, block[index + 4], stepHCoeffs[2]);
                b = StepH(b, c, d, a, block[index + 12], stepHCoeffs[3]);
            }
            hash[0] += a;
            hash[1] += b;
            hash[2] += c;
            hash[3] += d;
        }

        public uint[] UpdateCheckSum(uint[] value, byte[] buffer, int offset, int count)
        {
            if (count != 0)
            {
                byte[] buffer2;
                this.index = (int) ((this.bitsCount >> 3) & 0x3f);
                int length = 0x40 - this.index;
                if (this.tailLength == 0)
                {
                    buffer2 = buffer;
                }
                else
                {
                    buffer2 = new byte[this.tailLength + count];
                    Array.Copy(this.tail, 0, buffer2, 0, this.tailLength);
                    Array.Copy(buffer, 0, buffer2, this.tailLength, count);
                }
                count += this.tailLength;
                if ((this.bytesRead + count) >= length)
                {
                    if (this.bytesRead == 0)
                    {
                        Array.Copy(buffer, offset, this.hashBuffer, this.index, length);
                        Transform(this.hashBuffer, 0, value);
                    }
                    this.position = length + this.bytesRead;
                    while (true)
                    {
                        if (((this.position + 0x40) - 1) >= (this.bytesRead + count))
                        {
                            this.index = 0;
                            break;
                        }
                        Transform(buffer, (offset + this.position) - this.bytesRead, value);
                        this.position += 0x40;
                    }
                }
                if (this.position < (this.bytesRead + count))
                {
                    this.tailLength = (this.bytesRead + count) - this.position;
                    Array.Copy(buffer, (offset + this.position) - this.bytesRead, this.tail, 0, this.tailLength);
                }
                this.bytesRead = this.position;
            }
            return value;
        }

        public uint[] InitialCheckSumValue =>
            new uint[] { 0x67452301, 0xefcdab89, 0x98badcfe, 0x10325476 };
    }
}

