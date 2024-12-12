namespace DevExpress.XtraPrinting.Export.Pdf
{
    using DevExpress.Utils;
    using System;
    using System.IO;

    public class PdfStream : PdfObject
    {
        private PdfDictionary attributes;
        private Stream data;
        private StreamWriter writer;
        private bool useLength1;

        public PdfStream() : this(false)
        {
        }

        public PdfStream(bool useLength1) : base(PdfObjectType.Indirect)
        {
            this.attributes = new PdfDictionary();
            this.useLength1 = useLength1;
            this.data = this.CreateStream();
            this.writer = new StreamWriter(this.data, DXEncoding.GetEncoding(0x4e4));
            this.writer.NewLine = "\n";
        }

        public void Close()
        {
            if (this.data != null)
            {
                this.writer.Dispose();
                this.writer = null;
                this.data.Dispose();
                this.data = null;
            }
        }

        protected virtual Stream CreateStream() => 
            new MemoryStream();

        protected virtual void FillAttributes(MemoryStream actualData)
        {
            this.attributes.Add("Length", (int) actualData.Length);
            if (this.useLength1)
            {
                this.Attributes.Add("Length1", (int) this.Data.Length);
            }
        }

        public void SetByte(byte byte_)
        {
            this.data.WriteByte(byte_);
        }

        public void SetBytes(byte[] bytes)
        {
            this.SetBytes(bytes, 0);
        }

        public void SetBytes(byte[] bytes, int startIndex)
        {
            this.SetBytes(bytes, startIndex, bytes.Length - startIndex);
        }

        public void SetBytes(byte[] bytes, int startIndex, int length)
        {
            this.Writer.Flush();
            this.data.Write(bytes, startIndex, length);
        }

        public void SetString(string string_)
        {
            this.Writer.Write(string_);
        }

        public void SetStringLine(string string_)
        {
            this.Writer.WriteLine(string_);
        }

        protected override void WriteContent(StreamWriter writer)
        {
            this.Writer.Flush();
            this.WriteStream(writer, (MemoryStream) this.data);
        }

        protected void WriteStream(StreamWriter writer, MemoryStream actualData)
        {
            PdfStreamWriter writer2 = writer as PdfStreamWriter;
            actualData = (writer2 == null) ? actualData : writer2.EncryptStream(actualData, this);
            this.FillAttributes(actualData);
            this.attributes.WriteToStream(writer);
            writer.WriteLine();
            writer.WriteLine("stream");
            writer.Flush();
            actualData.WriteTo(writer.BaseStream);
            writer.WriteLine();
            writer.Write("endstream");
        }

        protected StreamWriter Writer =>
            this.writer;

        public PdfDictionary Attributes =>
            this.attributes;

        public Stream Data
        {
            get
            {
                this.Writer.Flush();
                return this.data;
            }
        }
    }
}

