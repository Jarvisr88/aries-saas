namespace DevExpress.Office.Utils
{
    using DevExpress.Office;
    using DevExpress.Utils;
    using System;
    using System.Collections.Generic;
    using System.IO;

    public class ChunkedMemoryStream : Stream, ISupportsCopyFrom<ChunkedMemoryStream>
    {
        public const int DefaultMaxBufferSize = 0x10000;
        private int maxBufferSize = 0x10000;
        private long totalLength;
        private long position;
        private readonly List<byte[]> buffers = new List<byte[]>();

        public void CopyFrom(ChunkedMemoryStream value)
        {
            int count = value.buffers.Count;
            for (int i = 0; i < count; i++)
            {
                this.buffers.Add(value.buffers[i]);
            }
            this.totalLength = value.totalLength;
        }

        private void EnsureBuffers(long size)
        {
            if (size > (this.buffers.Count * this.maxBufferSize))
            {
                this.SetLength(size);
            }
        }

        public bool Equals(ChunkedMemoryStream stream)
        {
            if (stream == null)
            {
                return false;
            }
            IList<byte[]> buffers = this.GetBuffers();
            IList<byte[]> list2 = stream.GetBuffers();
            if (buffers.Count != list2.Count)
            {
                return false;
            }
            int num = 0;
            while (num < buffers.Count)
            {
                if (buffers[num].Length != list2[num].Length)
                {
                    return false;
                }
                int index = 0;
                while (true)
                {
                    if (index >= buffers[num].Length)
                    {
                        num++;
                        break;
                    }
                    if (buffers[num][index] != list2[num][index])
                    {
                        return false;
                    }
                    index++;
                }
            }
            return true;
        }

        public override bool Equals(object obj)
        {
            ChunkedMemoryStream stream = obj as ChunkedMemoryStream;
            return this.Equals(stream);
        }

        public override void Flush()
        {
        }

        public IList<byte[]> GetBuffers() => 
            this.buffers;

        public override int GetHashCode() => 
            base.GetHashCode();

        public override int Read(byte[] buffer, int offset, int count)
        {
            count = Math.Min(count, (int) (this.Length - this.position));
            if (count <= 0)
            {
                return 0;
            }
            int num = count;
            int num2 = (int) (this.position / ((long) this.maxBufferSize));
            byte[] sourceArray = this.buffers[num2];
            int sourceIndex = (int) (this.position % ((long) this.maxBufferSize));
            int length = this.MaxBufferSize - sourceIndex;
            if (count < length)
            {
                Array.Copy(sourceArray, sourceIndex, buffer, offset, count);
                this.position += count;
                return count;
            }
            Array.Copy(sourceArray, sourceIndex, buffer, offset, length);
            offset += length;
            count -= length;
            this.position += length;
            num2++;
            while (count >= this.maxBufferSize)
            {
                sourceArray = this.buffers[num2];
                Array.Copy(sourceArray, 0, buffer, offset, this.maxBufferSize);
                offset += this.maxBufferSize;
                count -= this.maxBufferSize;
                this.position += this.maxBufferSize;
                num2++;
            }
            if (count > 0)
            {
                Array.Copy(this.buffers[num2], 0, buffer, offset, count);
                this.position += count;
            }
            return num;
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            switch (origin)
            {
                case SeekOrigin.Begin:
                    this.position = offset;
                    break;

                case SeekOrigin.Current:
                    this.position += offset;
                    break;

                case SeekOrigin.End:
                    this.position = this.totalLength + offset;
                    break;

                default:
                    break;
            }
            if (this.position < 0L)
            {
                ThrowArgumentException("offset", offset);
            }
            return this.position;
        }

        public override void SetLength(long value)
        {
            Guard.ArgumentNonNegative((float) value, "value");
            int index = (int) (value / ((long) this.maxBufferSize));
            if ((value % ((long) this.maxBufferSize)) != 0)
            {
                index++;
            }
            int count = this.buffers.Count;
            if (index < count)
            {
                this.buffers.RemoveRange(index, count - index);
            }
            else
            {
                int num3 = index - count;
                for (int i = 0; i < num3; i++)
                {
                    this.buffers.Add(new byte[this.maxBufferSize]);
                }
            }
            this.totalLength = value;
            if (this.position > value)
            {
                this.position = value;
            }
        }

        private static void ThrowArgumentException(string propName, object val)
        {
            string str = (val != null) ? val.ToString() : "null";
            throw new ArgumentException($"'{str}' is not a valid value for '{propName}'");
        }

        public byte[] ToArray()
        {
            byte[] destinationArray = new byte[this.totalLength];
            int destinationIndex = 0;
            int num2 = (int) (this.totalLength / ((long) this.maxBufferSize));
            for (int i = 0; i < num2; i++)
            {
                Array.Copy(this.buffers[i], 0, destinationArray, destinationIndex, this.maxBufferSize);
                destinationIndex += this.maxBufferSize;
            }
            long num3 = this.totalLength % ((long) this.maxBufferSize);
            if (num3 > 0L)
            {
                Array.Copy(this.buffers[num2], 0, destinationArray, destinationIndex, (int) num3);
            }
            return destinationArray;
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            if (count > 0)
            {
                this.EnsureBuffers(this.position + count);
                this.totalLength = Math.Max(this.position + count, this.totalLength);
                int num = (int) (this.position / ((long) this.maxBufferSize));
                int destinationIndex = (int) (this.position % ((long) this.maxBufferSize));
                byte[] destinationArray = this.buffers[num];
                int length = this.maxBufferSize - destinationIndex;
                if (count <= length)
                {
                    Array.Copy(buffer, offset, destinationArray, destinationIndex, count);
                    this.position += count;
                }
                else
                {
                    Array.Copy(buffer, offset, destinationArray, destinationIndex, length);
                    offset += length;
                    count -= length;
                    this.position += length;
                    num++;
                    while (count >= this.maxBufferSize)
                    {
                        destinationArray = this.buffers[num];
                        Array.Copy(buffer, offset, destinationArray, 0, this.maxBufferSize);
                        offset += this.maxBufferSize;
                        count -= this.maxBufferSize;
                        this.position += this.maxBufferSize;
                        num++;
                    }
                    if (count > 0)
                    {
                        destinationArray = this.buffers[num];
                        Array.Copy(buffer, offset, destinationArray, 0, count);
                        this.position += count;
                    }
                }
            }
        }

        public override bool CanRead =>
            true;

        public override bool CanSeek =>
            true;

        public override bool CanWrite =>
            true;

        public override long Length =>
            this.totalLength;

        public override long Position
        {
            get => 
                this.position;
            set => 
                this.position = value;
        }

        public int MaxBufferSize
        {
            get => 
                this.maxBufferSize;
            protected internal set => 
                this.maxBufferSize = value;
        }

        protected internal List<byte[]> Buffers =>
            this.buffers;
    }
}

