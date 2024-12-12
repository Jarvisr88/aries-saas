namespace DevExpress.XtraPrinting.Native
{
    using DevExpress.XtraPrinting.Caching;
    using System;
    using System.IO;

    public class ExportInfoWriteStream : Stream
    {
        private DocumentStorage storage;
        private byte[] buf = new byte[0x100000];
        private int bufPosition;
        private int position;

        public ExportInfoWriteStream(DocumentStorage storage)
        {
            this.storage = storage;
        }

        public override void Flush()
        {
            if (this.bufPosition > 0)
            {
                int position = this.position;
                this.position = position + 1;
                this.storage.StoreAndFlateIfNeed(DocumentStorageLocation.ExportInfo, position, new MemoryStream(this.buf, 0, this.bufPosition));
                this.bufPosition = 0;
            }
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            throw new NotImplementedException();
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            throw new NotImplementedException();
        }

        public override void SetLength(long value)
        {
            throw new NotImplementedException();
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            while ((this.buf.Length - this.bufPosition) < count)
            {
                int length = this.buf.Length - this.bufPosition;
                Array.Copy(buffer, offset, this.buf, this.bufPosition, length);
                offset += length;
                count -= length;
                this.bufPosition += length;
                this.Flush();
            }
            Array.Copy(buffer, offset, this.buf, this.bufPosition, count);
            this.bufPosition += count;
        }

        public override bool CanRead =>
            false;

        public override bool CanSeek =>
            false;

        public override bool CanWrite =>
            true;

        public override long Length
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public override long Position
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }
    }
}

