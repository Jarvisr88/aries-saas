namespace DevExpress.XtraExport.Xls
{
    using System;
    using System.IO;

    public class XlsChunkWriterInternalStream : Stream
    {
        private const int defaultCapacity = 0x10000;
        private const int copyBufferSize = 0x4000;
        private int capacity;
        private byte[] internalBuffer;
        private byte[] copyBuffer;
        private int length;
        private int position;
        private bool disposed;

        public XlsChunkWriterInternalStream()
        {
            this.capacity = 0x10000;
            this.internalBuffer = new byte[this.capacity];
            this.copyBuffer = new byte[0x4000];
        }

        public XlsChunkWriterInternalStream(int capacity)
        {
            if (capacity < 0)
            {
                capacity = 0x10000;
            }
            this.capacity = capacity;
            this.internalBuffer = new byte[this.capacity];
            this.copyBuffer = new byte[0x4000];
        }

        private void CheckArguments(byte[] buffer, int offset, int count)
        {
            if (buffer == null)
            {
                throw new ArgumentNullException("buffer");
            }
            if ((offset + count) > buffer.Length)
            {
                throw new ArgumentException("offset + count exceed buffer length");
            }
            if (offset < 0)
            {
                throw new ArgumentOutOfRangeException("offset less than 0");
            }
            if (count < 0)
            {
                throw new ArgumentOutOfRangeException("count less than 0");
            }
        }

        private void CheckDisposed()
        {
            if (this.disposed)
            {
                throw new ObjectDisposedException(this.ToString());
            }
        }

        protected override void Dispose(bool disposing)
        {
            this.disposed = true;
            this.internalBuffer = null;
            this.copyBuffer = null;
        }

        public override void Flush()
        {
            this.CheckDisposed();
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            this.CheckDisposed();
            this.CheckArguments(buffer, offset, count);
            if (count == 0)
            {
                return 0;
            }
            if (this.position >= this.length)
            {
                return 0;
            }
            int num = this.length - this.position;
            int length = count;
            if (length > num)
            {
                length = num;
            }
            Array.Copy(this.internalBuffer, this.position, buffer, offset, length);
            return length;
        }

        public void RemoveBytes(int count)
        {
            if (count < 0)
            {
                throw new ArgumentOutOfRangeException("count less than 0");
            }
            if (count != 0)
            {
                if (count >= this.length)
                {
                    this.length = 0;
                    this.position = 0;
                }
                else
                {
                    int length = this.length - count;
                    int destinationIndex = 0;
                    while (length > 0)
                    {
                        if (length <= 0x4000)
                        {
                            Array.Copy(this.internalBuffer, destinationIndex + count, this.copyBuffer, 0, length);
                            Array.Copy(this.copyBuffer, 0, this.internalBuffer, destinationIndex, length);
                            length = 0;
                            continue;
                        }
                        Array.Copy(this.internalBuffer, destinationIndex + count, this.copyBuffer, 0, 0x4000);
                        Array.Copy(this.copyBuffer, 0, this.internalBuffer, destinationIndex, 0x4000);
                        destinationIndex += 0x4000;
                        length -= 0x4000;
                    }
                    this.length -= count;
                    this.position -= count;
                    if (this.position < 0)
                    {
                        this.position = 0;
                    }
                }
            }
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            this.CheckDisposed();
            switch (origin)
            {
                case SeekOrigin.Begin:
                    this.Position = offset;
                    break;

                case SeekOrigin.Current:
                    this.Position += offset;
                    break;

                case SeekOrigin.End:
                    this.Position = this.Length + offset;
                    break;

                default:
                    break;
            }
            return this.Position;
        }

        public override void SetLength(long value)
        {
            this.CheckDisposed();
            int num = (int) value;
            if (num < 0)
            {
                num = 0;
            }
            if (this.length != num)
            {
                if (value > this.capacity)
                {
                    this.Capacity = ((num / 0x10000) + 1) * 0x10000;
                }
                this.length = num;
            }
        }

        public byte[] ToArray() => 
            this.ToArray(this.length);

        public byte[] ToArray(int size)
        {
            if (size < 0)
            {
                throw new ArgumentOutOfRangeException("size less than 0");
            }
            byte[] destinationArray = new byte[size];
            if (size > 0)
            {
                Array.Copy(this.internalBuffer, destinationArray, Math.Min(size, this.length));
            }
            return destinationArray;
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            this.CheckDisposed();
            this.CheckArguments(buffer, offset, count);
            if ((this.position + count) > this.length)
            {
                this.SetLength((long) (this.position + count));
            }
            if (count != 0)
            {
                Array.Copy(buffer, offset, this.internalBuffer, this.position, count);
                this.position += count;
            }
        }

        public long Capacity
        {
            get
            {
                this.CheckDisposed();
                return (long) this.capacity;
            }
            set
            {
                this.CheckDisposed();
                int length = (int) value;
                if (length < 0)
                {
                    length = 0x10000;
                }
                if (length < this.length)
                {
                    length = this.length;
                }
                if (this.capacity != length)
                {
                    this.capacity = length;
                    byte[] internalBuffer = this.internalBuffer;
                    this.internalBuffer = new byte[this.capacity];
                    Array.Copy(internalBuffer, 0, this.internalBuffer, 0, this.length);
                }
            }
        }

        public override bool CanRead
        {
            get
            {
                this.CheckDisposed();
                return true;
            }
        }

        public override bool CanSeek
        {
            get
            {
                this.CheckDisposed();
                return true;
            }
        }

        public override bool CanWrite
        {
            get
            {
                this.CheckDisposed();
                return true;
            }
        }

        public override long Length
        {
            get
            {
                this.CheckDisposed();
                return (long) this.length;
            }
        }

        public override long Position
        {
            get
            {
                this.CheckDisposed();
                return (long) this.position;
            }
            set
            {
                this.CheckDisposed();
                this.position = (int) value;
                if (this.position < 0)
                {
                    this.position = 0;
                }
                if (this.position > this.length)
                {
                    long num = Math.Min(this.position, this.capacity) - this.length;
                    for (int i = 0; i < num; i++)
                    {
                        this.internalBuffer[i + this.length] = 0;
                    }
                }
            }
        }
    }
}

