namespace DevExpress.Utils.StructuredStorage.Reader
{
    using DevExpress.Utils;
    using DevExpress.Utils.StructuredStorage.Internal.Reader;
    using System;
    using System.Collections.Generic;
    using System.IO;

    [CLSCompliant(false)]
    public class VirtualStream : Stream
    {
        private readonly AbstractFat fat;
        private readonly string name;
        private readonly long length;
        private readonly List<uint> sectors;
        private uint startSector;
        private uint lastReadSectorIndex = uint.MaxValue;
        private byte[] lastReadSector;
        private long position;

        public VirtualStream(AbstractFat fat, uint startSector, long sizeOfStream, string name)
        {
            Guard.ArgumentNotNull(fat, "fat");
            this.fat = fat;
            this.startSector = startSector;
            this.length = sizeOfStream;
            this.name = name;
            if ((startSector != -2) && ((startSector != uint.MaxValue) && (this.Length != 0)))
            {
                this.sectors = fat.GetSectorChain(startSector, (ulong) Math.Ceiling((double) (((double) this.length) / ((double) fat.SectorSize))), name);
                this.CheckConsistency();
                this.lastReadSector = new byte[fat.SectorSize];
            }
        }

        private void CheckConsistency()
        {
            if (this.sectors.Count < Math.Ceiling((double) (((double) this.length) / ((double) this.fat.SectorSize))))
            {
                this.fat.ThrowChainSizeMismatchException(this.name);
            }
        }

        public Stream Clone() => 
            new VirtualStream(this.fat, this.startSector, this.length, this.name);

        public override void Flush()
        {
            throw new NotSupportedException("This method is not supported on a read-only stream.");
        }

        public override int Read(byte[] array, int offset, int count) => 
            this.Read(array, offset, count, this.position);

        public int Read(byte[] array, int offset, int count, long position)
        {
            if ((array.Length < 1) || ((count < 1) || ((position < 0L) || (offset < 0))))
            {
                return 0;
            }
            if ((offset + count) > array.Length)
            {
                return 0;
            }
            if ((position + count) > this.Length)
            {
                count = Convert.ToInt32((long) (this.Length - position));
                if (count < 1)
                {
                    return 0;
                }
            }
            this.position = position;
            int num = (int) (position / ((ulong) this.fat.SectorSize));
            int length = 0;
            int num3 = 0;
            int destinationIndex = offset;
            int sourceIndex = Convert.ToInt32((long) (position % ((ulong) this.fat.SectorSize)));
            uint num6 = this.sectors[num];
            if (num6 != this.lastReadSectorIndex)
            {
                this.fat.SeekToPositionInSector((long) num6, 0L);
                this.fat.UncheckedRead(this.lastReadSector, 0, this.fat.SectorSize);
                this.lastReadSectorIndex = num6;
            }
            int num7 = (count > (this.fat.SectorSize - sourceIndex)) ? (this.fat.SectorSize - sourceIndex) : count;
            Array.Copy(this.lastReadSector, sourceIndex, array, destinationIndex, num7);
            length = num7;
            this.position += length;
            destinationIndex += length;
            num3 += length;
            num++;
            while ((num3 + this.fat.SectorSize) < count)
            {
                this.fat.SeekToPositionInSector((long) ((ulong) this.sectors[num]), 0L);
                length = this.fat.UncheckedRead(array, destinationIndex, this.fat.SectorSize);
                this.position += length;
                destinationIndex += length;
                num3 += length;
                num++;
                if (length != this.fat.SectorSize)
                {
                    return num3;
                }
            }
            if (num3 >= count)
            {
                return num3;
            }
            this.lastReadSectorIndex = this.sectors[num];
            this.fat.SeekToPositionInSector((long) this.lastReadSectorIndex, 0L);
            this.fat.UncheckedRead(this.lastReadSector, 0, this.fat.SectorSize);
            length = count - num3;
            Array.Copy(this.lastReadSector, 0, array, destinationIndex, length);
            this.position += length;
            destinationIndex += length;
            return (num3 + length);
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
                    this.position = this.length - offset;
                    break;

                default:
                    break;
            }
            if (this.position < 0L)
            {
                this.position = 0L;
            }
            else if (this.position > this.length)
            {
                this.position = this.length;
            }
            return this.position;
        }

        public override void SetLength(long value)
        {
            throw new NotSupportedException("This method is not supported on a read-only stream.");
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            throw new NotSupportedException("This method is not supported on a read-only stream.");
        }

        public override long Position
        {
            get => 
                this.position;
            set => 
                this.position = value;
        }

        public override long Length =>
            this.length;

        public override bool CanRead =>
            true;

        public override bool CanSeek =>
            true;

        public override bool CanWrite =>
            false;
    }
}

