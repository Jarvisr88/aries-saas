namespace DevExpress.Utils.Serializing.Helpers
{
    using System;
    using System.IO;
    using System.IO.Compression;

    public abstract class DeflateStreamsArchiveManagerBase
    {
        protected const int Int32Size = 4;
        protected const string Prefix = "dxsa";
        public static readonly byte[] PrefixBytes = Encoding.UTF8.GetBytes("dxsa");
        public static readonly byte[] VersionBytes = Encoding.UTF8.GetBytes("19.2.9.0");
        protected Stream baseStream;
        private bool streamAllocated;
        protected int fStreamCount;
        protected int[] offsets;

        protected DeflateStreamsArchiveManagerBase(Stream baseStream)
        {
            this.baseStream = baseStream;
            baseStream.Seek(0L, SeekOrigin.Begin);
        }

        protected void CheckStreamIndex(int streamIndex)
        {
            if ((streamIndex >= this.StreamCount) || this.streamAllocated)
            {
                ThrowInvalidOperationException();
            }
        }

        protected Stream CreateDeflateStream(CompressionMode mode)
        {
            this.streamAllocated = true;
            return new ArhiveManagerDeflateStream(this, mode);
        }

        protected Stream CreateRawStream()
        {
            this.streamAllocated = true;
            return new ArhiveManagerStream(this);
        }

        protected void StreamClosed()
        {
            this.streamAllocated = false;
        }

        protected static void ThrowInvalidOperationException()
        {
            throw new InvalidOperationException();
        }

        public int StreamCount =>
            this.fStreamCount;

        private class ArhiveManagerDeflateStream : DeflateStream
        {
            private DeflateStreamsArchiveManagerBase manager;

            public ArhiveManagerDeflateStream(DeflateStreamsArchiveManagerBase manager, CompressionMode mode) : base(manager.baseStream, mode, true)
            {
                this.manager = manager;
            }

            protected override void Dispose(bool disposing)
            {
                if (disposing)
                {
                    this.manager.StreamClosed();
                }
                base.Dispose(disposing);
            }
        }

        private class ArhiveManagerStream : Stream
        {
            private DeflateStreamsArchiveManagerBase manager;
            private Stream baseStream;

            public ArhiveManagerStream(DeflateStreamsArchiveManagerBase manager)
            {
                this.baseStream = manager.baseStream;
                this.manager = manager;
            }

            protected override void Dispose(bool disposing)
            {
                if (disposing)
                {
                    this.manager.StreamClosed();
                }
                base.Dispose(disposing);
            }

            public override void Flush()
            {
                this.baseStream.Flush();
            }

            public override int Read(byte[] buffer, int offset, int count) => 
                this.baseStream.Read(buffer, offset, count);

            public override long Seek(long offset, SeekOrigin origin) => 
                this.baseStream.Seek(offset, origin);

            public override void SetLength(long value)
            {
                this.baseStream.SetLength(value);
            }

            public override void Write(byte[] buffer, int offset, int count)
            {
                this.baseStream.Write(buffer, offset, count);
            }

            public override bool CanRead =>
                this.baseStream.CanRead;

            public override bool CanSeek =>
                this.baseStream.CanSeek;

            public override bool CanWrite =>
                this.baseStream.CanWrite;

            public override long Length =>
                this.baseStream.Length;

            public override long Position
            {
                get => 
                    this.baseStream.Position;
                set => 
                    this.baseStream.Position = value;
            }
        }
    }
}

