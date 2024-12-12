namespace DevExpress.XtraExport.Xls
{
    using System;
    using System.IO;

    public class XlsContentFeatHdr : XlsContentFeatBase
    {
        private FutureRecordHeader header;
        private byte[] data;

        public XlsContentFeatHdr()
        {
            FutureRecordHeader header1 = new FutureRecordHeader();
            header1.RecordTypeId = 0x867;
            this.header = header1;
        }

        public override int GetSize()
        {
            int num = this.header.GetSize() + 7;
            if (this.Data != null)
            {
                num += this.Data.Length;
            }
            return num;
        }

        public override void Write(BinaryWriter writer)
        {
            this.header.Write(writer);
            writer.Write((ushort) base.FeatureType);
            writer.Write((byte) 1);
            if (this.Data == null)
            {
                writer.Write(0);
            }
            else
            {
                writer.Write(-1);
                writer.Write(this.Data);
            }
        }

        public override FutureRecordHeaderBase RecordHeader =>
            this.header;

        public byte[] Data
        {
            get => 
                this.data;
            set => 
                this.data = value;
        }
    }
}

