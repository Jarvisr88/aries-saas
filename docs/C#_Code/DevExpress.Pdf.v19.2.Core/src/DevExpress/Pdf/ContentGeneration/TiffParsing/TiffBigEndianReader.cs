namespace DevExpress.Pdf.ContentGeneration.TiffParsing
{
    using DevExpress.Pdf.Native;
    using System;
    using System.IO;

    public class TiffBigEndianReader : ITiffReader
    {
        private PdfBigEndianStreamReader reader;

        public TiffBigEndianReader(Stream stream)
        {
            this.reader = new PdfBigEndianStreamReader(stream);
        }

        public byte ReadByte() => 
            this.reader.ReadByte();

        public byte[] ReadBytes(int count) => 
            this.reader.ReadBytes(count);

        public short ReadInt16() => 
            (short) this.reader.ReadInt16();

        public int ReadInt32() => 
            this.reader.ReadInt32();

        public long Position
        {
            get => 
                this.reader.Position;
            set => 
                this.reader.Position = value;
        }
    }
}

