namespace DevExpress.Pdf.Native
{
    using System;
    using System.IO;

    public class PdfUnsignedDocumentStream : Stream
    {
        private readonly PdfDocumentStream stream;
        private readonly long skipOffset;
        private readonly int skipLength;
        private readonly long skipEnd;
        private readonly long initialPosition;

        public PdfUnsignedDocumentStream(PdfDocumentStream stream, long skipOffset, int skipLength)
        {
            this.stream = stream;
            this.skipOffset = skipOffset;
            this.skipLength = skipLength;
            this.skipEnd = skipOffset + this.skipEnd;
            this.initialPosition = stream.Position;
            this.Position = 0L;
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            if (disposing)
            {
                this.stream.Position = this.initialPosition;
            }
        }

        public override void Flush()
        {
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            bool flag = false;
            long position = this.Position;
            if (position < this.skipOffset)
            {
                int num3 = (int) ((position + count) - this.skipOffset);
                if (num3 > 0)
                {
                    count -= num3;
                    flag = true;
                }
            }
            int length = Math.Min(count, (int) (this.Length - position));
            if (length <= 0)
            {
                return 0;
            }
            Array.Copy(this.stream.ReadBytes(length), 0, buffer, offset, length);
            if (flag)
            {
                this.Position = this.Position;
            }
            return length;
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            long num = (origin == SeekOrigin.Current) ? (this.Position + offset) : ((origin == SeekOrigin.End) ? (this.Length - offset) : offset);
            this.Position = num;
            return num;
        }

        public override void SetLength(long value)
        {
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
        }

        public override bool CanSeek =>
            true;

        public override bool CanRead =>
            true;

        public override bool CanWrite =>
            false;

        public override long Length =>
            this.stream.Length - this.skipLength;

        public override long Position
        {
            get
            {
                long position = this.stream.Position;
                return ((position <= this.skipOffset) ? position : (position - this.skipLength));
            }
            set => 
                this.stream.Position = (value >= this.skipOffset) ? (value + this.skipLength) : value;
        }
    }
}

