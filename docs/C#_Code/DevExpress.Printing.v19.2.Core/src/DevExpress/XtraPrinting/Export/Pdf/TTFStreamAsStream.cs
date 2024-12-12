namespace DevExpress.XtraPrinting.Export.Pdf
{
    using System;
    using System.IO;

    internal class TTFStreamAsStream : TTFStream
    {
        private Stream stream;

        public TTFStreamAsStream(Stream stream)
        {
            this.stream = stream;
        }

        protected override byte _read() => 
            (byte) this.stream.ReadByte();

        protected override void _seek(int newPosition)
        {
            this.stream.Seek((long) newPosition, SeekOrigin.Begin);
        }

        protected override void _write(byte value)
        {
            this.stream.WriteByte(value);
        }

        public override int Position =>
            (int) this.stream.Position;

        public override int Length =>
            (int) this.stream.Length;
    }
}

