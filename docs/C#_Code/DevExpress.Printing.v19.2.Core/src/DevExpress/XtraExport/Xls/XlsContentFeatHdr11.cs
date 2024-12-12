namespace DevExpress.XtraExport.Xls
{
    using System;
    using System.IO;
    using System.Runtime.CompilerServices;

    public class XlsContentFeatHdr11 : XlsContentFeatBase
    {
        private FutureRecordHeader header;

        public XlsContentFeatHdr11()
        {
            FutureRecordHeader header1 = new FutureRecordHeader();
            header1.RecordTypeId = 0x871;
            this.header = header1;
            base.FeatureType = XlsFeatureType.List;
        }

        public override int GetSize() => 
            this.header.GetSize() + 0x11;

        public override void Write(BinaryWriter writer)
        {
            this.header.Write(writer);
            writer.Write((ushort) base.FeatureType);
            writer.Write((byte) 1);
            writer.Write(-1);
            writer.Write(-1);
            writer.Write(this.NextId);
            writer.Write((ushort) 0);
        }

        public override FutureRecordHeaderBase RecordHeader =>
            this.header;

        public int NextId { get; set; }
    }
}

