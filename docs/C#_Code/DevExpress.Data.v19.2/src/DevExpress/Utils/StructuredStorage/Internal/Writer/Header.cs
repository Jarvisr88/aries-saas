namespace DevExpress.Utils.StructuredStorage.Internal.Writer
{
    using DevExpress.Utils;
    using DevExpress.Utils.StructuredStorage.Internal;
    using System;
    using System.Collections.Generic;
    using System.IO;

    [CLSCompliant(false)]
    public class Header : AbstractHeader
    {
        private readonly List<byte> diFatSectors;
        private readonly StructuredStorageContext context;
        private int diFatSectorCount;

        public Header(StructuredStorageContext context) : base(new OutputHandler(new ChunkedMemoryStream()))
        {
            this.diFatSectors = new List<byte>();
            Guard.ArgumentNotNull(context, "context");
            base.IoHandler.SetHeaderReference(this);
            base.IoHandler.InitBitConverter(true);
            this.context = context;
            this.Init();
        }

        private void Init()
        {
            base.MiniSectorShift = 6;
            base.SectorShift = 9;
            base.NoSectorsInDirectoryChain4KB = 0;
            base.MiniSectorCutoff = 0x1000;
        }

        internal void Write()
        {
            OutputHandler ioHandler = (OutputHandler) base.IoHandler;
            ioHandler.Write(BitConverter.GetBytes((ulong) 16220472316735377360UL));
            ioHandler.Write(new byte[0x10]);
            ioHandler.WriteUInt16(0x3e);
            ioHandler.WriteUInt16(3);
            ioHandler.WriteUInt16(0xfffe);
            ioHandler.WriteUInt16(base.SectorShift);
            ioHandler.WriteUInt16(base.MiniSectorShift);
            ioHandler.WriteUInt16(0);
            ioHandler.WriteUInt32(0);
            ioHandler.WriteUInt32(base.NoSectorsInDirectoryChain4KB);
            ioHandler.WriteUInt32(base.NoSectorsInFatChain);
            ioHandler.WriteUInt32(base.DirectoryStartSector);
            ioHandler.WriteUInt32(0);
            ioHandler.WriteUInt32(base.MiniSectorCutoff);
            ioHandler.WriteUInt32(base.MiniFatStartSector);
            ioHandler.WriteUInt32(base.NoSectorsInMiniFatChain);
            ioHandler.WriteUInt32(base.DiFatStartSector);
            ioHandler.WriteUInt32(base.NoSectorsInDiFatChain);
            ioHandler.Write(this.diFatSectors.ToArray());
            if (base.SectorSize == 0x1000)
            {
                ioHandler.Write(new byte[0xe00]);
            }
        }

        internal void WriteNextDiFatSector(uint sector)
        {
            if (this.diFatSectorCount >= 0x6d)
            {
                this.context.Fat.ThrowInconsistencyException();
            }
            this.diFatSectors.AddRange(this.context.InternalBitConverter.GetBytes(sector));
            this.diFatSectorCount++;
        }

        internal void writeToStream(Stream stream)
        {
            ((OutputHandler) base.IoHandler).WriteToStream(stream);
        }
    }
}

