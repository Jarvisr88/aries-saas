namespace DevExpress.Pdf.Native
{
    using System;

    public abstract class PdfSampledDataConverter
    {
        private readonly int bitsPerSample;
        private readonly int samplesCount;

        protected PdfSampledDataConverter(int bitsPerSample, int samplesCount)
        {
            this.bitsPerSample = bitsPerSample;
            this.samplesCount = samplesCount;
        }

        protected abstract long[] Convert(byte[] data);
        public static long[] Convert(int bitsPerSample, int samplesCount, byte[] data) => 
            Create(bitsPerSample, samplesCount).Convert(data);

        protected abstract byte[] ConvertBack(long[] data);
        public static byte[] ConvertBack(int bitsPerSample, int samplesCount, long[] data) => 
            Create(bitsPerSample, samplesCount).ConvertBack(data);

        private static PdfSampledDataConverter Create(int bitsPerSample, int samplesCount)
        {
            if (bitsPerSample > 8)
            {
                if ((bitsPerSample == 0x10) || ((bitsPerSample == 0x18) || (bitsPerSample == 0x20)))
                {
                    return new PdfSampledDataHighBitsCountConverter(bitsPerSample, samplesCount);
                }
            }
            else
            {
                switch (bitsPerSample)
                {
                    case 1:
                    case 2:
                    case 4:
                        return new PdfSampledDataLowBitsCountConverter(bitsPerSample, samplesCount);

                    case 3:
                        break;

                    default:
                        if (bitsPerSample != 8)
                        {
                            break;
                        }
                        return new PdfSampledDataSingleByteConverter(bitsPerSample, samplesCount);
                }
            }
            PdfDocumentStructureReader.ThrowIncorrectDataException();
            return null;
        }

        protected int BitsPerSample =>
            this.bitsPerSample;

        protected int SamplesCount =>
            this.samplesCount;
    }
}

