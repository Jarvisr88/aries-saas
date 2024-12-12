namespace DevExpress.XtraPrinting.Native
{
    using DevExpress.XtraPrinting.Caching;
    using System;
    using System.IO;

    public class ExportInfoReadStream : Stream
    {
        private DocumentStorage storage;
        private byte[] buf;
        private int position;
        private int itemsCount;

        public ExportInfoReadStream(DocumentStorage storage)
        {
            this.storage = storage;
            this.buf = new byte[0];
            this.itemsCount = storage.ContinuousInfoCount;
        }

        public override void Flush()
        {
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            int position;
            int num = 0;
            while (true)
            {
                if ((this.buf.Length >= count) || (this.position >= this.itemsCount))
                {
                    count = Math.Min(count, this.buf.Length);
                    Array.Copy(this.buf, 0, buffer, offset, count);
                    num += count;
                    byte[] destinationArray = new byte[this.buf.Length - count];
                    Array.Copy(this.buf, count, destinationArray, 0, destinationArray.Length);
                    this.buf = destinationArray;
                    return num;
                }
                Array.Copy(this.buf, 0, buffer, offset, this.buf.Length);
                count -= this.buf.Length;
                offset += this.buf.Length;
                num += this.buf.Length;
                position = this.position;
                this.position = position + 1;
                using (Stream stream = this.storage.RestoreAndDeflateIfNeed(DocumentStorageLocation.ExportInfo, position))
                {
                    if ((stream == null) || (stream.Length == 0))
                    {
                        this.buf = new byte[0];
                        position = num;
                        break;
                    }
                    this.buf = new byte[stream.Length];
                    stream.Read(this.buf, 0, this.buf.Length);
                }
            }
            return position;
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
            throw new NotImplementedException();
        }

        public override bool CanRead =>
            true;

        public override bool CanSeek =>
            false;

        public override bool CanWrite =>
            false;

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

