namespace DevExpress.Office.Utils
{
    using DevExpress.Utils;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Runtime.CompilerServices;

    public class XlsCommandStream : Stream
    {
        private const short beginOfSubstreamTypeCode = 0x809;
        private XlReader reader;
        private IXlsContentBuilder contentBuilder;
        private short[] typeCodes;
        private List<BlockItem> blocks;
        private long position;

        public XlsCommandStream(XlReader reader, int firstBlockSize)
        {
            this.blocks = new List<BlockItem>();
            Guard.ArgumentNotNull(reader, "reader");
            this.reader = reader;
            this.typeCodes = new short[0];
            this.AddBlock(reader.Position, firstBlockSize);
        }

        public XlsCommandStream(XlReader reader, short[] typeCodes, int firstBlockSize)
        {
            this.blocks = new List<BlockItem>();
            Guard.ArgumentNotNull(reader, "reader");
            Guard.ArgumentNotNull(typeCodes, "typeCodes");
            Guard.ArgumentPositive(typeCodes.Length, "typeCodes length");
            this.reader = reader;
            this.typeCodes = typeCodes;
            this.AddBlock(reader.Position, firstBlockSize);
        }

        public XlsCommandStream(IXlsContentBuilder contentBuilder, XlReader reader, short[] typeCodes, int firstBlockSize) : this(reader, typeCodes, firstBlockSize)
        {
            Guard.ArgumentNotNull(contentBuilder, "contentBuilder");
            this.contentBuilder = contentBuilder;
        }

        private void AddBlock(long basePosition, int size)
        {
            long startPosition = 0L;
            int count = this.blocks.Count;
            if (count > 0)
            {
                BlockItem item = this.blocks[count - 1];
                startPosition = item.StartPosition + item.Size;
            }
            this.blocks.Add(new BlockItem(startPosition, basePosition, size));
        }

        protected override void Dispose(bool disposing)
        {
            this.reader = null;
            base.Dispose(disposing);
        }

        private BlockInfo FindBlock(long position)
        {
            int num = Algorithms.BinarySearchReverseOrder<BlockItem>(this.blocks, new BlockComparable(position));
            if (num < 0)
            {
                return null;
            }
            BlockItem item = this.blocks[num];
            return new BlockInfo(item, (int) (position - item.StartPosition));
        }

        public override void Flush()
        {
        }

        private int GetFutureRecordHeaderSize(short typeCode)
        {
            if (typeCode > 0x866)
            {
                if ((typeCode == 0x875) || ((typeCode == 0x87f) || (typeCode == 0x89f)))
                {
                    return 12;
                }
            }
            else
            {
                if (typeCode == 0x812)
                {
                    return 4;
                }
                if (typeCode == 0x866)
                {
                    return 14;
                }
            }
            return 0;
        }

        public short GetNextTypeCode()
        {
            if ((this.reader.StreamLength - this.reader.Position) < 2L)
            {
                return 0;
            }
            short num = this.reader.ReadNotCryptedInt16();
            this.reader.Seek((long) (-2), SeekOrigin.Current);
            return num;
        }

        public bool IsAppropriateTypeCode(short typeCode)
        {
            for (int i = 0; i < this.typeCodes.Length; i++)
            {
                if (this.typeCodes[i] == typeCode)
                {
                    return true;
                }
            }
            return false;
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            int num4;
            if (count <= 0)
            {
                return 0;
            }
            int num = 0;
            for (int i = count; i > 0; i -= num4)
            {
                BlockInfo info = this.FindBlock(this.position);
                if (info == null)
                {
                    if (!this.TryReadNextBlock())
                    {
                        return num;
                    }
                    info = new BlockInfo(this.blocks[this.blocks.Count - 1], 0);
                }
                num4 = Math.Min(i, info.Size - info.InnerPosition);
                Array.Copy(this.reader.ReadBytes(num4), 0, buffer, offset, num4);
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
            throw new NotSupportedException();
        }

        public void SkipTillEndOfBlock()
        {
            BlockInfo info = this.FindBlock(this.position);
            if (info != null)
            {
                int num = info.Size - info.InnerPosition;
                if (num > 0)
                {
                    this.Seek((long) num, SeekOrigin.Current);
                }
            }
        }

        private bool TryReadNextBlock()
        {
            if ((this.reader.StreamLength - this.reader.Position) < 2L)
            {
                return false;
            }
            short typeCode = this.reader.ReadNotCryptedInt16();
            if (!this.IsAppropriateTypeCode(typeCode))
            {
                if ((typeCode != 0x809) || (this.contentBuilder == null))
                {
                    this.reader.Seek((long) (-2), SeekOrigin.Current);
                    return false;
                }
                this.reader.Seek((long) (-2), SeekOrigin.Current);
                this.contentBuilder.ReadSubstream();
                return this.TryReadNextBlock();
            }
            int size = this.reader.ReadNotCryptedUInt16();
            if (size > 0x2020)
            {
                throw new Exception($"Record data size greater than {0x2020}");
            }
            int futureRecordHeaderSize = this.GetFutureRecordHeaderSize(typeCode);
            if (futureRecordHeaderSize > 0)
            {
                this.reader.ReadBytes(futureRecordHeaderSize);
                size -= futureRecordHeaderSize;
            }
            this.AddBlock(this.reader.Position, size);
            return true;
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            throw new NotSupportedException();
        }

        public override bool CanRead =>
            true;

        public override bool CanSeek =>
            true;

        public override bool CanWrite =>
            false;

        public override long Length
        {
            get
            {
                long num = 0L;
                for (int i = 0; i < this.blocks.Count; i++)
                {
                    num += this.blocks[i].Size;
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
                if (this.position != value)
                {
                    long length = this.Length;
                    if (value <= 0L)
                    {
                        this.position = 0L;
                        this.reader.Position = this.blocks[0].BasePosition;
                    }
                    else if (value < length)
                    {
                        BlockInfo info = this.FindBlock(value);
                        this.position = value;
                        this.reader.Position = info.BasePosition + info.InnerPosition;
                    }
                    else
                    {
                        BlockItem item = this.blocks[this.blocks.Count - 1];
                        this.position = length;
                        this.reader.Position = item.BasePosition + item.Size;
                        while (this.TryReadNextBlock())
                        {
                            item = this.blocks[this.blocks.Count - 1];
                            length += item.Size;
                            if (value < length)
                            {
                                BlockInfo info2 = this.FindBlock(value);
                                this.position = value;
                                this.reader.Position = info2.BasePosition + info2.InnerPosition;
                                return;
                            }
                            this.reader.Seek((long) item.Size, SeekOrigin.Current);
                        }
                        this.position = length;
                    }
                }
            }
        }

        private class BlockComparable : IComparable<XlsCommandStream.BlockItem>
        {
            private long position;

            public BlockComparable(long position)
            {
                this.position = position;
            }

            public int CompareTo(XlsCommandStream.BlockItem other) => 
                (this.position >= other.StartPosition) ? ((this.position < (other.StartPosition + other.Size)) ? 0 : -1) : 1;
        }

        private class BlockInfo : XlsCommandStream.BlockItem
        {
            public BlockInfo(XlsCommandStream.BlockItem item, int position) : base(item.StartPosition, item.BasePosition, item.Size)
            {
                this.InnerPosition = position;
            }

            public int InnerPosition { get; private set; }
        }

        private class BlockItem
        {
            public BlockItem(long startPosition, long basePosition, int size)
            {
                this.StartPosition = startPosition;
                this.BasePosition = basePosition;
                this.Size = size;
            }

            public long StartPosition { get; private set; }

            public long BasePosition { get; private set; }

            public int Size { get; private set; }
        }
    }
}

