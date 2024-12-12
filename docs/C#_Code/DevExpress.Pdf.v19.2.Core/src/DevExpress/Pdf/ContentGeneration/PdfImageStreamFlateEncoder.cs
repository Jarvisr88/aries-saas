namespace DevExpress.Pdf.ContentGeneration
{
    using DevExpress.Pdf.Native;
    using DevExpress.Utils.Zip;
    using System;
    using System.Runtime.CompilerServices;

    public class PdfImageStreamFlateEncoder : IDisposable
    {
        private const byte pngUpPrediction = 2;
        private PdfFlateEncoder encoder = new PdfFlateEncoder();
        private Adler32 adler = new Adler32();
        private byte[] rowBuffer;
        private byte[] lastImageRow;
        private int offset;

        public PdfImageStreamFlateEncoder(int imageRowLength)
        {
            this.rowBuffer = new byte[imageRowLength];
            this.lastImageRow = new byte[imageRowLength];
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Add(byte value)
        {
            this.rowBuffer[this.offset] = (byte) (value - this.lastImageRow[this.offset]);
            int offset = this.offset;
            this.offset = offset + 1;
            this.lastImageRow[offset] = value;
        }

        public void Dispose()
        {
            this.encoder.Dispose();
        }

        public void EndRow()
        {
            this.encoder.DeflateStream.WriteByte(2);
            this.adler.Add((byte) 2);
            this.encoder.DeflateStream.Write(this.rowBuffer, 0, this.rowBuffer.Length);
            this.adler.Add(this.rowBuffer);
            this.offset = 0;
        }

        public byte[] GetEncodedData()
        {
            this.encoder.Close();
            this.adler.Write(this.encoder.Stream);
            return this.encoder.GetData();
        }
    }
}

