namespace DevExpress.Pdf.Native
{
    using DevExpress.Utils.Zip;
    using System;
    using System.IO;
    using System.IO.Compression;

    public class PdfFlateEncoder : PdfDisposableObject
    {
        private readonly MemoryStream stream = new MemoryStream();
        private readonly System.IO.Compression.DeflateStream deflateStream;

        public PdfFlateEncoder()
        {
            this.stream.WriteByte(0x58);
            this.stream.WriteByte(0x85);
            this.deflateStream = new System.IO.Compression.DeflateStream(this.stream, CompressionMode.Compress, true);
        }

        public void Close()
        {
            this.deflateStream.Dispose();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.deflateStream.Dispose();
                this.stream.Dispose();
            }
        }

        public static byte[] Encode(byte[] data)
        {
            using (PdfFlateEncoder encoder = new PdfFlateEncoder())
            {
                encoder.DeflateStream.Write(data, 0, data.Length);
                encoder.Close();
                Adler32 adler = new Adler32();
                adler.Add(data);
                adler.Write(encoder.Stream);
                return encoder.GetData();
            }
        }

        public byte[] GetData() => 
            this.stream.ToArray();

        public MemoryStream Stream =>
            this.stream;

        public System.IO.Compression.DeflateStream DeflateStream =>
            this.deflateStream;
    }
}

