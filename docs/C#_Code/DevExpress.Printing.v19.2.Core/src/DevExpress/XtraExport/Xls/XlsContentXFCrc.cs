namespace DevExpress.XtraExport.Xls
{
    using DevExpress.Office.Utils;
    using System;
    using System.IO;
    using System.Runtime.CompilerServices;

    public class XlsContentXFCrc : XlsContentBase
    {
        private int xfCount;
        private FutureRecordHeader recordHeader = new FutureRecordHeader();

        public override int GetSize() => 
            20;

        public override void Read(XlReader reader, int size)
        {
            this.recordHeader = FutureRecordHeader.FromStream(reader);
            reader.ReadUInt16();
            this.XFCount = reader.ReadUInt16();
            this.XFCRC = reader.ReadInt32();
        }

        public override void Write(BinaryWriter writer)
        {
            this.recordHeader.Write(writer);
            writer.Write((ushort) 0);
            writer.Write((ushort) this.XFCount);
            writer.Write(this.XFCRC);
        }

        public int XFCount
        {
            get => 
                this.xfCount;
            set
            {
                base.CheckValue(value, 0, 0xfd2, "XFCount");
                this.xfCount = value;
            }
        }

        public int XFCRC { get; set; }

        public override FutureRecordHeaderBase RecordHeader =>
            this.recordHeader;
    }
}

