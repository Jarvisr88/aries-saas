namespace DevExpress.Utils.StructuredStorage.Internal
{
    using DevExpress.Utils;
    using System;

    [CLSCompliant(false)]
    public abstract class AbstractHeader
    {
        public const ulong MAGIC_NUMBER = 16220472316735377360UL;
        private AbstractIOHandler ioHandler;
        private ushort sectorShift;
        private ushort sectorSize;
        private ushort miniSectorShift;
        private ushort miniSectorSize;
        private uint noSectorsInDirectoryChain4KB;
        private uint noSectorsInFatChain;
        private uint directoryStartSector;
        private uint miniSectorCutoff;
        private uint miniFatStartSector;
        private uint noSectorsInMiniFatChain;
        private uint diFatStartSector;
        private uint noSectorsInDiFatChain;

        protected AbstractHeader(AbstractIOHandler ioHandler)
        {
            Guard.ArgumentNotNull(ioHandler, "ioHandler");
            this.ioHandler = ioHandler;
        }

        protected virtual void ThrowArgumentException(string propName, object val)
        {
            string str = (val == string.Empty) ? "String.Empty" : ((val == null) ? "null" : val.ToString());
            throw new ArgumentException($"'{str}' is not a valid value for '{propName}'");
        }

        public void ThrowInvalidHeaderValueException(string name)
        {
            throw new Exception("The value for '" + name + "' in the header is invalid.");
        }

        public void ThrowUnsupportedSizeException(string name)
        {
            throw new Exception("The size of " + name + " is not supported.");
        }

        public AbstractIOHandler IoHandler =>
            this.ioHandler;

        public ushort SectorShift
        {
            get => 
                this.sectorShift;
            set
            {
                if ((value != 9) && (value != 12))
                {
                    this.ThrowUnsupportedSizeException("SectorShift");
                }
                this.sectorShift = value;
                this.sectorSize = (ushort) (1 << (this.sectorShift & 0x1f));
            }
        }

        public ushort SectorSize =>
            this.sectorSize;

        public ushort MiniSectorShift
        {
            get => 
                this.miniSectorShift;
            set
            {
                if (value != 6)
                {
                    this.ThrowUnsupportedSizeException("MiniSectorShift");
                }
                this.miniSectorShift = value;
                this.miniSectorSize = (ushort) (1 << (this.miniSectorShift & 0x1f));
            }
        }

        public ushort MiniSectorSize =>
            this.miniSectorSize;

        public uint NoSectorsInDirectoryChain4KB
        {
            get => 
                this.noSectorsInDirectoryChain4KB;
            set
            {
                if ((this.SectorSize == 0x200) && (value != 0))
                {
                    this.ThrowArgumentException("NoSectorsInDirectoryChain4KB", value);
                }
                this.noSectorsInDirectoryChain4KB = value;
            }
        }

        public uint NoSectorsInFatChain
        {
            get => 
                this.noSectorsInFatChain;
            set
            {
                if (value > (this.ioHandler.IOStreamSize / ((ulong) this.SectorSize)))
                {
                    this.ThrowInvalidHeaderValueException("NoSectorsInFatChain");
                }
                this.noSectorsInFatChain = value;
            }
        }

        public uint DirectoryStartSector
        {
            get => 
                this.directoryStartSector;
            set
            {
                if ((value > (this.ioHandler.IOStreamSize / ((ulong) this.SectorSize))) && (value != -2))
                {
                    this.ThrowInvalidHeaderValueException("DirectoryStartSector");
                }
                this.directoryStartSector = value;
            }
        }

        public uint MiniSectorCutoff
        {
            get => 
                this.miniSectorCutoff;
            set
            {
                if (value != 0x1000)
                {
                    this.ThrowUnsupportedSizeException("MiniSectorCutoff");
                }
                this.miniSectorCutoff = value;
            }
        }

        public uint MiniFatStartSector
        {
            get => 
                this.miniFatStartSector;
            set
            {
                if ((value > (this.ioHandler.IOStreamSize / ((ulong) this.SectorSize))) && (value != -2))
                {
                    this.ThrowInvalidHeaderValueException("MiniFatStartSector");
                }
                this.miniFatStartSector = value;
            }
        }

        public uint NoSectorsInMiniFatChain
        {
            get => 
                this.noSectorsInMiniFatChain;
            set
            {
                if (value > (this.ioHandler.IOStreamSize / ((ulong) this.SectorSize)))
                {
                    this.ThrowInvalidHeaderValueException("NoSectorsInMiniFatChain");
                }
                this.noSectorsInMiniFatChain = value;
            }
        }

        public uint DiFatStartSector
        {
            get => 
                this.diFatStartSector;
            set
            {
                if ((value > (this.ioHandler.IOStreamSize / ((ulong) this.SectorSize))) && (value != -2))
                {
                    this.ThrowInvalidHeaderValueException("DiFatStartSector");
                }
                this.diFatStartSector = value;
            }
        }

        public uint NoSectorsInDiFatChain
        {
            get => 
                this.noSectorsInDiFatChain;
            set
            {
                if (value > (this.ioHandler.IOStreamSize / ((ulong) this.SectorSize)))
                {
                    this.ThrowInvalidHeaderValueException("NoSectorsInDiFatChain");
                }
                this.noSectorsInDiFatChain = value;
            }
        }
    }
}

