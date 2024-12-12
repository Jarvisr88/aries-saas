namespace DevExpress.XtraExport.Xls
{
    using DevExpress.Office.Utils;
    using System;
    using System.IO;

    public class XlsContentBeginOfSubstream : XlsContentBase
    {
        private XlsSubstreamType substreamType;
        private int fileHistoryFlags = XlsFileHistory.Default;
        private short biffVersion = 0x600;
        private short buildVersion = 0x3267;
        private byte lastSavedApplicationVersion;

        public override int GetSize() => 
            0x10;

        public override void Read(XlReader reader, int size)
        {
            this.biffVersion = reader.ReadNotCryptedInt16();
            if ((this.biffVersion != 0x600) && (this.biffVersion != 0x500))
            {
                base.ThrowInvalidXlsFile();
            }
            this.substreamType = (XlsSubstreamType) reader.ReadNotCryptedInt16();
            this.buildVersion = reader.ReadNotCryptedInt16();
            reader.Seek(2L, SeekOrigin.Current);
            if (size == 8)
            {
                this.fileHistoryFlags = XlsFileHistory.Default;
            }
            else
            {
                this.fileHistoryFlags = reader.ReadNotCryptedInt32();
                reader.Seek(1L, SeekOrigin.Current);
                this.lastSavedApplicationVersion = (byte) (reader.ReadNotCryptedInt16() & 15);
                reader.Seek(1L, SeekOrigin.Current);
            }
        }

        public override void Write(BinaryWriter writer)
        {
            writer.Write((short) 0x600);
            writer.Write((short) this.SubstreamType);
            writer.Write(this.BuildVersion);
            writer.Write((short) 0x7cd);
            writer.Write(this.fileHistoryFlags);
            writer.Write(0x606);
        }

        public XlsSubstreamType SubstreamType
        {
            get => 
                this.substreamType;
            set => 
                this.substreamType = value;
        }

        public int FileHistoryFlags
        {
            get => 
                this.fileHistoryFlags;
            set => 
                this.fileHistoryFlags = value;
        }

        public short BiffVersion =>
            this.biffVersion;

        public short BuildVersion
        {
            get => 
                this.buildVersion;
            set => 
                this.buildVersion = value;
        }

        public byte LastSavedApplicationVersion =>
            this.lastSavedApplicationVersion;
    }
}

