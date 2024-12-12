namespace DevExpress.Pdf.Native
{
    using System;
    using System.IO;

    public class JBIG2StreamHelper : PdfBigEndianStreamReader
    {
        public JBIG2StreamHelper(byte[] data) : base(new MemoryStream(data))
        {
        }

        public JBIG2StreamHelper(Stream stream) : base(stream)
        {
        }

        internal int[] ReadAdaptiveTemplate(int length)
        {
            int[] numArray = new int[length];
            for (int i = 0; i < length; i++)
            {
                numArray[i] = (sbyte) base.ReadByte();
            }
            return numArray;
        }

        public JBIG2StreamHelper ReadData(long dataLength)
        {
            Stream stream = base.Stream;
            if ((dataLength < 0L) || (dataLength > 0xf4240L))
            {
                dataLength = stream.Length - stream.Position;
            }
            byte[] buffer = new byte[dataLength];
            stream.Read(buffer, 0, (int) dataLength);
            return new JBIG2StreamHelper(buffer);
        }
    }
}

