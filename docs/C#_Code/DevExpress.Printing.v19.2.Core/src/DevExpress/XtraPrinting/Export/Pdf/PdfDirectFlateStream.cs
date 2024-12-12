namespace DevExpress.XtraPrinting.Export.Pdf
{
    using DevExpress.Utils.Zip;
    using System;
    using System.IO;
    using System.IO.Compression;

    public class PdfDirectFlateStream : PdfStream
    {
        protected override Stream CreateStream() => 
            new ZLibStream();

        protected override void FillAttributes(MemoryStream actualData)
        {
            base.FillAttributes(actualData);
            base.Attributes.Add("Filter", "FlateDecode");
        }

        protected override void WriteContent(StreamWriter writer)
        {
            DeflateStream data = (DeflateStream) base.Data;
            MemoryStream baseStream = (MemoryStream) data.BaseStream;
            base.Writer.Close();
            data.Close();
            try
            {
                this.WriteStream(writer, (baseStream.Length > 6L) ? baseStream : new MemoryStream());
            }
            finally
            {
                baseStream.Close();
            }
        }

        private class ZLibStream : DeflateStream
        {
            private Adler32 adler;

            public ZLibStream() : base(new MemoryStream(), CompressionMode.Compress, true)
            {
                base.BaseStream.WriteByte(0x58);
                base.BaseStream.WriteByte(0x85);
                this.adler = new Adler32();
            }

            protected override void Dispose(bool disposing)
            {
                Stream baseStream = base.BaseStream;
                base.Dispose(disposing);
                if (baseStream != null)
                {
                    this.adler.Write(baseStream);
                }
            }

            public override void Write(byte[] array, int offset, int count)
            {
                base.Write(array, offset, count);
                this.adler.Add(array, offset, count);
            }
        }
    }
}

