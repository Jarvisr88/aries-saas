namespace DevExpress.Pdf.Native
{
    using System;

    public class PdfSampledDataSingleByteConverter : PdfSampledDataConverter
    {
        public PdfSampledDataSingleByteConverter(int bitsPerSample, int samplesCount) : base(bitsPerSample, samplesCount)
        {
        }

        protected override long[] Convert(byte[] data)
        {
            int samplesCount = base.SamplesCount;
            if (data.Length < samplesCount)
            {
                PdfDocumentStructureReader.ThrowIncorrectDataException();
            }
            long[] numArray = new long[samplesCount];
            for (int i = 0; i < samplesCount; i++)
            {
                numArray[i] = data[i];
            }
            return numArray;
        }

        protected override byte[] ConvertBack(long[] data)
        {
            int samplesCount = base.SamplesCount;
            byte[] buffer = new byte[samplesCount];
            for (int i = 0; i < samplesCount; i++)
            {
                buffer[i] = (byte) data[i];
            }
            return buffer;
        }
    }
}

