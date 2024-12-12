namespace DevExpress.Utils.StructuredStorage.Internal.Reader
{
    using DevExpress.Utils;
    using System;
    using System.Collections.Generic;

    [CLSCompliant(false)]
    public abstract class AbstractFat
    {
        private readonly DevExpress.Utils.StructuredStorage.Internal.Reader.Header header;
        private readonly InputHandler fileHandler;
        private readonly int addressesPerSector;
        private readonly ulong maxSectorsInFile;

        protected AbstractFat(DevExpress.Utils.StructuredStorage.Internal.Reader.Header header, InputHandler fileHandler)
        {
            Guard.ArgumentNotNull(header, "header");
            Guard.ArgumentNotNull(fileHandler, "fileHandler");
            this.header = header;
            this.fileHandler = fileHandler;
            this.addressesPerSector = header.SectorSize / 4;
            this.maxSectorsInFile = ((fileHandler.IOStreamSize + this.SectorSize) - ((ulong) 1L)) / ((ulong) this.SectorSize);
        }

        protected abstract uint GetNextSectorInChain(uint currentSector);
        public List<uint> GetSectorChain(uint startSector, ulong maxCount, string name) => 
            this.GetSectorChain(startSector, maxCount, name, false);

        public List<uint> GetSectorChain(uint startSector, ulong maxCount, string name, bool immediateCycleCheck)
        {
            List<uint> list = new List<uint> {
                startSector
            };
            do
            {
                uint nextSectorInChain = this.GetNextSectorInChain(list[list.Count - 1]);
                if ((nextSectorInChain == -4) || ((nextSectorInChain == -3) || (nextSectorInChain == uint.MaxValue)))
                {
                    this.InvalidSectorInChainException();
                }
                if (nextSectorInChain == -2)
                {
                    break;
                }
                if (immediateCycleCheck && list.Contains(nextSectorInChain))
                {
                    this.ThrowCycleDetectedException(name);
                }
                list.Add(nextSectorInChain);
            }
            while (list.Count <= this.maxSectorsInFile);
            return list;
        }

        internal void InvalidSectorInChainException()
        {
            throw new Exception("Chain could not be build due to an invalid sector id.");
        }

        public abstract long SeekToPositionInSector(long sector, long position);
        internal void ThrowChainSizeMismatchException(string name)
        {
            throw new Exception("The number of sectors used by " + name + " does not match the specified size.");
        }

        internal void ThrowCycleDetectedException(string chain)
        {
            throw new Exception(chain + " contains a cycle.");
        }

        internal int UncheckedRead(byte[] array, int offset, int count) => 
            this.fileHandler.UncheckedRead(array, offset, count);

        public DevExpress.Utils.StructuredStorage.Internal.Reader.Header Header =>
            this.header;

        public int AddressesPerSector =>
            this.addressesPerSector;

        public InputHandler FileHandler =>
            this.fileHandler;

        public abstract ushort SectorSize { get; }
    }
}

