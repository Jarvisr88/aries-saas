namespace DevExpress.Pdf.Native
{
    using System;
    using System.IO;

    public abstract class PdfDataStream : Stream
    {
        private readonly long length;
        private long position;

        protected PdfDataStream(long length)
        {
            this.length = length;
        }

        public override void Flush()
        {
        }

        public byte[] Read(int length)
        {
            byte[] buffer = new byte[length];
            if (this.Read(buffer, 0, length) != length)
            {
                PdfDocumentStructureReader.ThrowIncorrectDataException();
            }
            return buffer;
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            this.position = (origin == SeekOrigin.Current) ? (this.position + offset) : ((origin == SeekOrigin.End) ? (this.length - offset) : offset);
            return this.position;
        }

        public override void SetLength(long value)
        {
        }

        public abstract void Synchronize();
        public override void Write(byte[] buffer, int offset, int count)
        {
        }

        public override bool CanSeek =>
            true;

        public override bool CanRead =>
            (this.position >= 0L) && (this.position < this.length);

        public override bool CanWrite =>
            false;

        public override long Length =>
            this.length;

        public override long Position
        {
            get => 
                this.position;
            set => 
                this.position = value;
        }

        public abstract int CurrentByte { get; }
    }
}

