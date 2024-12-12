namespace DevExpress.XtraExport.Xls
{
    using DevExpress.Office.Utils;
    using System;
    using System.IO;

    public class XlsContentXFExt : XlsContentBase
    {
        private int xfIndex;
        private readonly FutureRecordHeader recordHeader = new FutureRecordHeader();
        private readonly XfProperties properties = new XfProperties();

        public override int GetSize() => 
            0x10 + this.properties.GetSize();

        public override void Read(XlReader reader, int size)
        {
            reader.ReadBytes(size);
        }

        public override void Write(BinaryWriter writer)
        {
            this.recordHeader.Write(writer);
            writer.Write((ushort) 0);
            writer.Write((ushort) this.XFIndex);
            this.properties.Write(writer);
        }

        public int XFIndex
        {
            get => 
                this.xfIndex;
            set
            {
                base.CheckValue(value, 0, 0xfd2, "XFIndex");
                this.xfIndex = value;
            }
        }

        public XfProperties Properties =>
            this.properties;

        public override FutureRecordHeaderBase RecordHeader =>
            this.recordHeader;
    }
}

