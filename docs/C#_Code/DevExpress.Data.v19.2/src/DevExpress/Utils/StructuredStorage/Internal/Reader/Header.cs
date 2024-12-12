namespace DevExpress.Utils.StructuredStorage.Internal.Reader
{
    using DevExpress.Utils.StructuredStorage.Internal;
    using System;

    [CLSCompliant(false)]
    public class Header : AbstractHeader
    {
        public Header(InputHandler fileHandler) : base(fileHandler)
        {
            base.IoHandler.SetHeaderReference(this);
            this.ReadHeader();
        }

        private void ReadHeader()
        {
            InputHandler ioHandler = (InputHandler) base.IoHandler;
            byte[] array = new byte[2];
            ioHandler.ReadPosition(array, (long) 0x1c);
            ioHandler.InitBitConverter((array[0] == 0xfe) && (array[1] == 0xff));
            if (ioHandler.ReadUInt64(0L) != 16220472316735377360UL)
            {
                throw new InvalidOperationException("The file you are trying to open is in different format than specified by the file extension.");
            }
            base.SectorShift = ioHandler.ReadUInt16((long) 30);
            base.MiniSectorShift = ioHandler.ReadUInt16();
            base.NoSectorsInDirectoryChain4KB = ioHandler.ReadUInt32((long) 40);
            base.NoSectorsInFatChain = ioHandler.ReadUInt32();
            base.DirectoryStartSector = ioHandler.ReadUInt32();
            uint num = ioHandler.ReadUInt32((long) 0x38);
            base.MiniFatStartSector = ioHandler.ReadUInt32();
            this.MiniSectorCutoff = (base.MiniFatStartSector == -2) ? 0x1000 : num;
            base.NoSectorsInMiniFatChain = ioHandler.ReadUInt32();
            uint num2 = ioHandler.ReadUInt32();
            uint num3 = ioHandler.ReadUInt32();
            base.DiFatStartSector = ((num2 != uint.MaxValue) || (num3 != 0)) ? num2 : ((uint) (-2));
            base.NoSectorsInDiFatChain = num3;
        }

        protected override void ThrowArgumentException(string propName, object val)
        {
            string str = (val != null) ? val.ToString() : "null";
            throw new ArgumentException($"'{str}' is not a valid value for '{propName}'");
        }
    }
}

