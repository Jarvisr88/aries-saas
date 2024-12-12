namespace DevExpress.Utils.Zip.Internal
{
    using System;
    using System.IO;

    public class FixedOffsetSequentialReadOnlyStream : Stream
    {
        private readonly Stream baseStream;
        private readonly long basePosition;
        private readonly long length;
        private long position;
        private bool isPackedStream;

        public FixedOffsetSequentialReadOnlyStream(Stream baseStream, long length) : this(baseStream, baseStream.Position, length, false)
        {
        }

        public FixedOffsetSequentialReadOnlyStream(Stream baseStream, long basePosition, long length, bool isPackedStream)
        {
            this.baseStream = baseStream;
            this.basePosition = basePosition;
            this.length = length;
            this.isPackedStream = isPackedStream;
        }

        public override void Flush()
        {
            throw new NotSupportedException();
        }

        protected void IncrementPosition(long offset)
        {
            this.position += offset;
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            this.ValidateBaseStreamPosition();
            if ((this.length >= 0L) && (!this.isPackedStream && ((this.position + count) > this.length)))
            {
                count = (int) (this.length - this.position);
            }
            int num = this.ReadFromBaseStream(buffer, offset, count);
            this.position += num;
            return num;
        }

        protected virtual int ReadFromBaseStream(byte[] buffer, int offset, int count) => 
            this.BaseStream.Read(buffer, offset, count);

        public override long Seek(long offset, SeekOrigin origin)
        {
            if (!this.CanSeek)
            {
                throw new NotSupportedException();
            }
            if ((origin < SeekOrigin.Begin) || (origin > SeekOrigin.End))
            {
                throw new ArgumentException();
            }
            if (origin == SeekOrigin.Begin)
            {
                this.position = offset;
            }
            else if (origin == SeekOrigin.Current)
            {
                this.position += offset;
            }
            else if (origin == SeekOrigin.End)
            {
                this.position = this.Length - offset;
            }
            if (this.position < 0L)
            {
                this.position = 0L;
            }
            else if (this.position > this.Length)
            {
                this.position = this.Length;
            }
            this.ValidateBaseStreamPosition();
            return this.position;
        }

        public override void SetLength(long value)
        {
            throw new NotSupportedException();
        }

        protected internal virtual void ValidateBaseStreamPosition()
        {
            if (this.BaseStream.Position != (this.basePosition + this.position))
            {
                this.BaseStream.Seek((this.basePosition + this.position) - this.BaseStream.Position, SeekOrigin.Current);
            }
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            throw new NotSupportedException();
        }

        public Stream BaseStream =>
            this.baseStream;

        public override bool CanRead =>
            this.BaseStream.CanRead;

        public override bool CanSeek =>
            this.BaseStream.CanSeek;

        public override bool CanWrite =>
            false;

        public override long Length
        {
            get
            {
                if (this.length < 0L)
                {
                    throw new NotSupportedException();
                }
                return this.length;
            }
        }

        public override long Position
        {
            get => 
                this.position;
            set
            {
                throw new NotSupportedException();
            }
        }
    }
}

