namespace DevExpress.Utils.StructuredStorage.Internal.Reader
{
    using DevExpress.Utils;
    using System;
    using System.Collections.Generic;

    [CLSCompliant(false)]
    public class MiniFat : AbstractFat
    {
        private List<uint> sectorsUsedByMiniFat;
        private List<uint> sectorsUsedByMiniStream;
        private readonly Fat fat;
        private readonly uint miniStreamStart;
        private readonly ulong sizeOfMiniStream;

        internal MiniFat(Fat fat, Header header, InputHandler fileHandler, uint miniStreamStart, ulong sizeOfMiniStream) : base(header, fileHandler)
        {
            this.sectorsUsedByMiniFat = new List<uint>();
            this.sectorsUsedByMiniStream = new List<uint>();
            Guard.ArgumentNotNull(fat, "fat");
            this.fat = fat;
            this.miniStreamStart = miniStreamStart;
            this.sizeOfMiniStream = sizeOfMiniStream;
            this.Init();
        }

        private void CheckConsistency()
        {
            if (this.sizeOfMiniStream != 0)
            {
                if (this.sectorsUsedByMiniFat.Count != base.Header.NoSectorsInMiniFatChain)
                {
                    base.ThrowChainSizeMismatchException("MiniFat");
                }
                int num = (int) Math.Ceiling((double) (((double) this.sizeOfMiniStream) / ((double) base.Header.SectorSize)));
                if ((num > 1) && (this.sectorsUsedByMiniStream.Count < num))
                {
                    base.ThrowChainSizeMismatchException("MiniStream");
                }
            }
        }

        protected override uint GetNextSectorInChain(uint currentSector)
        {
            if (this.sectorsUsedByMiniFat.Count == 0)
            {
                return 0xfffffffe;
            }
            uint num = this.sectorsUsedByMiniFat[(int) (((ulong) currentSector) / ((long) base.AddressesPerSector))];
            base.FileHandler.SeekToPositionInSector((long) num, 4 * (currentSector % base.AddressesPerSector));
            return base.FileHandler.ReadUInt32();
        }

        private void Init()
        {
            this.ReadSectorsUsedByMiniFAT();
            this.ReadSectorsUsedByMiniStream();
            this.CheckConsistency();
        }

        private void ReadSectorsUsedByMiniFAT()
        {
            if ((base.Header.MiniFatStartSector != -2) && (base.Header.NoSectorsInMiniFatChain != 0))
            {
                this.sectorsUsedByMiniFat = this.fat.GetSectorChain(base.Header.MiniFatStartSector, (ulong) base.Header.NoSectorsInMiniFatChain, "MiniFat");
            }
        }

        private void ReadSectorsUsedByMiniStream()
        {
            if ((this.miniStreamStart != -2) && (this.sizeOfMiniStream != 0))
            {
                try
                {
                    this.sectorsUsedByMiniStream = this.fat.GetSectorChain(this.miniStreamStart, (ulong) Math.Ceiling((double) (((double) this.sizeOfMiniStream) / ((double) base.Header.SectorSize))), "MiniStream");
                }
                catch
                {
                    this.sectorsUsedByMiniStream = this.fat.GetSectorChain(this.miniStreamStart, ((ulong) 1L) + ((ulong) Math.Ceiling((double) (((double) this.sizeOfMiniStream) / ((double) base.Header.SectorSize)))), "MiniStream");
                }
            }
        }

        public override long SeekToPositionInSector(long sector, long position)
        {
            int num = (int) ((sector * base.Header.MiniSectorSize) / ((ulong) this.fat.SectorSize));
            int num2 = (int) ((sector * base.Header.MiniSectorSize) % ((ulong) this.fat.SectorSize));
            if (position < 0L)
            {
                throw new ArgumentOutOfRangeException("position");
            }
            return base.FileHandler.SeekToPositionInSector((long) ((ulong) this.sectorsUsedByMiniStream[num]), num2 + position);
        }

        public override ushort SectorSize =>
            base.Header.MiniSectorSize;
    }
}

