namespace DevExpress.Pdf.Native
{
    using System;
    using System.IO;

    public class PdfBigEndianStreamReader
    {
        private readonly System.IO.Stream stream;

        public PdfBigEndianStreamReader(System.IO.Stream stream)
        {
            this.stream = stream;
        }

        public PdfBitReader CreateBitReader() => 
            new PdfBitReader(this.Stream);

        public byte ReadByte() => 
            (byte) this.stream.ReadByte();

        public byte[] ReadBytes() => 
            this.ReadBytes((int) (this.stream.Length - this.stream.Position));

        public byte[] ReadBytes(int count)
        {
            byte[] buffer = new byte[count];
            this.stream.Read(buffer, 0, count);
            return buffer;
        }

        public int ReadInt(int count)
        {
            int num = this.ReadByte();
            if (count == 1)
            {
                return num;
            }
            num = (0x100 * num) + this.ReadByte();
            return ((count != 2) ? ((0x100 * ((0x100 * num) + this.ReadByte())) + this.ReadByte()) : num);
        }

        public int ReadInt16() => 
            (0x100 * this.ReadByte()) + this.ReadByte();

        public int ReadInt32() => 
            this.ReadInt(4);

        public void Skip(int count)
        {
            this.stream.Position += count;
        }

        public long Length =>
            this.stream.Length;

        public long Position
        {
            get => 
                this.stream.Position;
            set => 
                this.stream.Position = value;
        }

        public bool Finish =>
            this.Position == this.Length;

        protected System.IO.Stream Stream =>
            this.stream;
    }
}

