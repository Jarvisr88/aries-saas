namespace DevExpress.Pdf.Native
{
    using System;

    public class PdfSampledDataLowBitsCountConverter : PdfSampledDataConverter
    {
        private readonly int divisor;
        private readonly int dataLength;
        private readonly int lastElementSize;

        public PdfSampledDataLowBitsCountConverter(int bitsPerSample, int samplesCount) : base(bitsPerSample, samplesCount)
        {
            this.divisor = 8 / bitsPerSample;
            this.dataLength = samplesCount / this.divisor;
            this.lastElementSize = samplesCount % this.divisor;
        }

        protected override long[] Convert(byte[] data)
        {
            if (data.Length < (this.dataLength + ((this.lastElementSize == 0) ? 0 : 1)))
            {
                PdfDocumentStructureReader.ThrowIncorrectDataException();
            }
            int bitsPerSample = base.BitsPerSample;
            long[] numArray = new long[base.SamplesCount];
            byte num2 = (byte) ((0xff >> (bitsPerSample & 0x1f)) ^ 0xff);
            int index = 0;
            int num4 = 0;
            while (index < this.dataLength)
            {
                byte num5 = data[index];
                byte num6 = num2;
                int num7 = 0;
                int num8 = 8 - bitsPerSample;
                while (true)
                {
                    if (num7 >= this.divisor)
                    {
                        index++;
                        break;
                    }
                    numArray[num4++] = (num5 & num6) >> (num8 & 0x1f);
                    num7++;
                    num8 -= bitsPerSample;
                    num6 = (byte) (num6 >> (bitsPerSample & 0x1f));
                }
            }
            if (this.lastElementSize > 0)
            {
                byte num9 = data[index];
                byte num10 = num2;
                int num11 = 0;
                for (int i = 8 - bitsPerSample; num11 < this.lastElementSize; i -= bitsPerSample)
                {
                    numArray[num4++] = (num9 & num2) >> (i & 0x1f);
                    num11++;
                }
            }
            return numArray;
        }

        protected override byte[] ConvertBack(long[] data)
        {
            int bitsPerSample = base.BitsPerSample;
            int samplesCount = base.SamplesCount;
            byte[] buffer = new byte[this.dataLength + ((this.lastElementSize == 0) ? 0 : 1)];
            byte num3 = 0;
            int num4 = 0;
            int index = 0;
            int num6 = 0;
            while (index < samplesCount)
            {
                num3 = (byte) ((num3 << (bitsPerSample & 0x1f)) + data[index]);
                if (++num4 == this.divisor)
                {
                    buffer[num6++] = num3;
                    num3 = 0;
                    num4 = 0;
                }
                index++;
            }
            if (this.lastElementSize > 0)
            {
                while (true)
                {
                    if (num4 >= this.divisor)
                    {
                        buffer[this.dataLength] = num3;
                        break;
                    }
                    num3 = (byte) (num3 << (bitsPerSample & 0x1f));
                    num4++;
                }
            }
            return buffer;
        }
    }
}

