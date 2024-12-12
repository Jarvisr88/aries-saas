namespace DevExpress.XtraExport.Xls
{
    using DevExpress.Office.Utils;
    using DevExpress.Utils;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Runtime.CompilerServices;

    public class CompositeMemoryStream : Stream
    {
        private readonly List<SubstreamInfo> substreams;
        private long position;
        private bool leaveOpen;

        public CompositeMemoryStream() : this(null, false)
        {
        }

        public CompositeMemoryStream(bool leaveOpen) : this(null, leaveOpen)
        {
        }

        public CompositeMemoryStream(Stream stream) : this(stream, false)
        {
        }

        public CompositeMemoryStream(Stream stream, bool leaveOpen)
        {
            this.substreams = new List<SubstreamInfo>();
            this.leaveOpen = leaveOpen;
            stream ??= new ChunkedMemoryStream();
            this.Attach(stream);
        }

        public void Attach(Stream stream)
        {
            Guard.ArgumentNotNull(stream, "stream");
            if ((stream.CanRead != this.CanRead) || ((stream.CanWrite != this.CanWrite) || (stream.CanSeek != this.CanSeek)))
            {
                throw new ArgumentException("Not compatible stream");
            }
            SubstreamInfo item = new SubstreamInfo(stream, this.Length);
            this.substreams.Add(item);
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            if (disposing)
            {
                if (!this.leaveOpen)
                {
                    foreach (SubstreamInfo info in this.substreams)
                    {
                        info.BaseStream.Dispose();
                    }
                }
                this.substreams.Clear();
            }
        }

        private SubstreamInfo FindSubstream(long position)
        {
            int num = Algorithms.BinarySearchReverseOrder<SubstreamInfo>(this.substreams, new SubstreamComparable(position));
            return ((num >= 0) ? this.substreams[num] : null);
        }

        public override void Flush()
        {
            foreach (SubstreamInfo info in this.substreams)
            {
                info.BaseStream.Flush();
            }
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            int num4;
            int num = 0;
            for (int i = count; i > 0; i -= num4)
            {
                SubstreamInfo info = this.FindSubstream(this.position);
                if (info == null)
                {
                    return num;
                }
                info.InnerPosition = this.position - info.StartPosition;
                long num3 = info.Length - info.InnerPosition;
                num4 = (int) Math.Min((long) i, num3);
                info.BaseStream.Read(buffer, offset, num4);
                this.position += num4;
                offset += num4;
                num += num4;
            }
            return num;
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
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
            long length = this.Length;
            if (value != length)
            {
                if (value < length)
                {
                    int num2 = Algorithms.BinarySearchReverseOrder<SubstreamInfo>(this.substreams, new SubstreamComparable(this.position));
                    for (int i = this.substreams.Count - 1; i > num2; i--)
                    {
                        if (!this.leaveOpen)
                        {
                            this.substreams[i].BaseStream.Dispose();
                        }
                        this.substreams.RemoveAt(i);
                    }
                }
                SubstreamInfo info = this.substreams[this.substreams.Count - 1];
                info.BaseStream.SetLength(value - info.StartPosition);
            }
        }

        public byte[] ToArray()
        {
            byte[] buffer = new byte[this.Length];
            long position = this.Position;
            this.Position = 0L;
            this.Read(buffer, 0, buffer.Length);
            this.Position = position;
            return buffer;
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            int num3;
            for (int i = count; i > 0; i -= num3)
            {
                SubstreamInfo info = this.FindSubstream(this.position);
                if (info == null)
                {
                    info = this.substreams[this.substreams.Count - 1];
                    info.InnerPosition = this.position - info.StartPosition;
                    info.BaseStream.Write(buffer, offset, i);
                    this.position += i;
                    return;
                }
                info.InnerPosition = this.position - info.StartPosition;
                long num2 = info.Length - info.InnerPosition;
                num3 = (int) Math.Min((long) i, num2);
                info.BaseStream.Write(buffer, offset, num3);
                this.position += num3;
                offset += num3;
            }
        }

        public override bool CanRead =>
            true;

        public override bool CanSeek =>
            true;

        public override bool CanWrite =>
            true;

        public override long Length
        {
            get
            {
                long num = 0L;
                int count = this.substreams.Count;
                if (count > 0)
                {
                    SubstreamInfo info = this.substreams[count - 1];
                    num = info.StartPosition + info.Length;
                }
                return num;
            }
        }

        public override long Position
        {
            get => 
                this.position;
            set
            {
                if (value < 0L)
                {
                    value = 0L;
                }
                this.position = value;
            }
        }

        private class SubstreamComparable : IComparable<CompositeMemoryStream.SubstreamInfo>
        {
            private long position;

            public SubstreamComparable(long position)
            {
                this.position = position;
            }

            public int CompareTo(CompositeMemoryStream.SubstreamInfo other) => 
                (this.position >= other.StartPosition) ? ((this.position < (other.StartPosition + other.Length)) ? 0 : -1) : 1;
        }

        private class SubstreamInfo
        {
            public SubstreamInfo(Stream stream, long startPosition)
            {
                this.StartPosition = startPosition;
                this.BaseStream = stream;
            }

            public Stream BaseStream { get; private set; }

            public long StartPosition { get; private set; }

            public long InnerPosition
            {
                get => 
                    this.BaseStream.Position;
                set => 
                    this.BaseStream.Position = value;
            }

            public long Length =>
                this.BaseStream.Length;
        }
    }
}

