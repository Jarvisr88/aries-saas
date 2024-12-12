namespace DevExpress.Pdf.ContentGeneration.TiffParsing
{
    using System;
    using System.IO;

    public class TiffLittleEndianReader : ITiffReader
    {
        private BinaryReader reader;

        public TiffLittleEndianReader(Stream stream)
        {
            this.reader = new BinaryReader(stream);
        }

        public byte ReadByte() => 
            this.reader.ReadByte();

        public byte[] ReadBytes(int count) => 
            this.reader.ReadBytes(count);

        public short ReadInt16() => 
            this.reader.ReadInt16();

        public int ReadInt32() => 
            this.reader.ReadInt32();

        public long Position
        {
            get => 
                this.reader.BaseStream.Position;
            set => 
                this.reader.BaseStream.Position = value;
        }
    }
}

