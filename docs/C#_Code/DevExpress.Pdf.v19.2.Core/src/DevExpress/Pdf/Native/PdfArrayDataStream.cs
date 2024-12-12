namespace DevExpress.Pdf.Native
{
    using System;

    public class PdfArrayDataStream : PdfDataStream
    {
        private readonly byte[] data;

        public PdfArrayDataStream(byte[] data) : this(data, data.Length)
        {
        }

        public PdfArrayDataStream(byte[] data, int length) : base((long) length)
        {
            this.data = data;
        }

        public override int Read(byte[] buffer, int offset, int length)
        {
            long position = this.Position;
            length = Math.Min(length, this.data.Length - ((int) position));
            Array.Copy(this.data, (int) position, buffer, offset, length);
            return length;
        }

        public override void Synchronize()
        {
        }

        public override int CurrentByte =>
            this.CanRead ? this.data[(int) ((IntPtr) this.Position)] : -1;
    }
}

