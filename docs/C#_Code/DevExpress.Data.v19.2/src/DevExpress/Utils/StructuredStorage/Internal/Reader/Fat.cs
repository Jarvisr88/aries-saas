namespace DevExpress.Utils.StructuredStorage.Internal.Reader
{
    using System;
    using System.Collections.Generic;

    [CLSCompliant(false)]
    public class Fat : AbstractFat
    {
        private readonly List<uint> sectorsUsedByFat;
        private readonly List<uint> sectorsUsedByDiFat;

        public Fat(Header header, InputHandler fileHandler) : base(header, fileHandler)
        {
            this.sectorsUsedByFat = new List<uint>();
            this.sectorsUsedByDiFat = new List<uint>();
            this.Init();
        }

        private void CheckConsistency()
        {
            if ((this.sectorsUsedByDiFat.Count != base.Header.NoSectorsInDiFatChain) || (this.sectorsUsedByFat.Count != base.Header.NoSectorsInFatChain))
            {
                base.ThrowChainSizeMismatchException("Fat/DiFat");
            }
        }

        protected override uint GetNextSectorInChain(uint currentSector)
        {
            uint num = this.sectorsUsedByFat[(int) (((ulong) currentSector) / ((long) base.AddressesPerSector))];
            base.FileHandler.SeekToPositionInSector((long) num, (long) (((ulong) 4L) * (((ulong) currentSector) % ((long) base.AddressesPerSector))));
            return base.FileHandler.ReadUInt32();
        }

        private void Init()
        {
            this.ReadFirst109SectorsUsedByFAT();
            this.ReadSectorsUsedByFatFromDiFat();
            this.CheckConsistency();
        }

        private void ReadFirst109SectorsUsedByFAT()
        {
            base.FileHandler.SeekToPositionInSector(-1L, (long) 0x4c);
            int num2 = 0;
            while (true)
            {
                if (num2 < 0x6d)
                {
                    uint item = base.FileHandler.ReadUInt32();
                    if (item != uint.MaxValue)
                    {
                        this.sectorsUsedByFat.Add(item);
                        num2++;
                        continue;
                    }
                }
                return;
            }
        }

        private void ReadSectorsUsedByFatFromDiFat()
        {
            if ((base.Header.DiFatStartSector != -2) && (base.Header.NoSectorsInDiFatChain != 0))
            {
                base.FileHandler.SeekToSector((long) base.Header.DiFatStartSector);
                bool flag = false;
                this.sectorsUsedByDiFat.Add(base.Header.DiFatStartSector);
                while (true)
                {
                    int num2 = 0;
                    while (true)
                    {
                        if (num2 < (base.AddressesPerSector - 1))
                        {
                            uint item = base.FileHandler.ReadUInt32();
                            if ((item != uint.MaxValue) && (item != -2))
                            {
                                this.sectorsUsedByFat.Add(item);
                                num2++;
                                continue;
                            }
                            flag = true;
                        }
                        if (flag)
                        {
                            break;
                        }
                        else
                        {
                            uint item = base.FileHandler.ReadUInt32();
                            if ((item == uint.MaxValue) || (item == -2))
                            {
                                break;
                            }
                            else
                            {
                                this.sectorsUsedByDiFat.Add(item);
                                base.FileHandler.SeekToSector((long) item);
                                if (this.sectorsUsedByDiFat.Count > base.Header.NoSectorsInDiFatChain)
                                {
                                    base.ThrowChainSizeMismatchException("DiFat");
                                }
                            }
                        }
                        break;
                    }
                }
            }
        }

        public override long SeekToPositionInSector(long sector, long position) => 
            base.FileHandler.SeekToPositionInSector(sector, position);

        public override ushort SectorSize =>
            base.Header.SectorSize;
    }
}

