namespace DevExpress.Pdf.Native
{
    using System;
    using System.IO;
    using System.IO.Compression;

    public class PdfFlateDataSource : IPdfFlateDataSource, IDisposable
    {
        private readonly Stream deflateStream;

        public PdfFlateDataSource(byte[] data)
        {
            Stream stream = new MemoryStream(data) {
                Position = 2L
            };
            this.deflateStream = new DeflateStream(stream, CompressionMode.Decompress);
        }

        public void Dispose()
        {
            this.deflateStream.Dispose();
        }

        public void FillBuffer(byte[] buffer)
        {
            this.deflateStream.Read(buffer, 0, buffer.Length);
        }
    }
}

