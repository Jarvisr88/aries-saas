namespace DevExpress.Pdf.Native
{
    using System;

    public class PdfSampledDataHighBitsCountConverter : PdfSampledDataConverter
    {
        private readonly int multiplier;

        public PdfSampledDataHighBitsCountConverter(int bitsPerSample, int samplesCount) : base(bitsPerSample, samplesCount)
        {
            this.multiplier = bitsPerSample / 8;
        }

        protected override long[] Convert(byte[] data)
        {
            int samplesCount = base.SamplesCount;
            if (data.Length < (samplesCount * this.multiplier))
            {
                PdfDocumentStructureReader.ThrowIncorrectDataException();
            }
            long[] numArray = new long[samplesCount];
            int index = 0;
            int num3 = 0;
            while (index < samplesCount)
            {
                long num4 = data[num3++];
                int num5 = 1;
                while (true)
                {
                    if (num5 >= this.multiplier)
                    {
                        numArray[index] = num4;
                        index++;
                        break;
                    }
                    num4 = (num4 << 8) + data[num3++];
                    num5++;
                }
            }
            return numArray;
        }

        protected override byte[] ConvertBack(long[] data)
        {
            int samplesCount = base.SamplesCount;
            int num2 = this.multiplier - 1;
            byte[] buffer = new byte[samplesCount * this.multiplier];
            byte[] buffer2 = new byte[this.multiplier];
            int index = 0;
            int num4 = 0;
            while (index < samplesCount)
            {
                long num5 = data[index];
                int num6 = 0;
                while (true)
                {
                    if (num6 >= this.multiplier)
                    {
                        int num7 = num2;
                        while (true)
                        {
                            if (num7 < 0)
                            {
                                index++;
                                break;
                            }
                            buffer[num4++] = buffer2[num7];
                            num7--;
                        }
                        break;
                    }
                    buffer2[num6] = (byte) (num5 & 0xffL);
                    num5 = num5 >> 8;
                    num6++;
                }
            }
            return buffer;
        }
    }
}

