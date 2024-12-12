namespace DevExpress.XtraExport.Xls
{
    using System;
    using System.IO;

    public class XlsChunkFrtHeader : XlsChunk
    {
        private readonly FutureRecordHeader header;

        public XlsChunkFrtHeader(short recordType) : base(recordType)
        {
            FutureRecordHeader header1 = new FutureRecordHeader();
            header1.RecordTypeId = recordType;
            this.header = header1;
        }

        public override int GetMaxDataSize() => 
            0x2020 - this.header.GetSize();

        protected override short GetSize() => 
            (short) (base.GetSize() + this.header.GetSize());

        protected override void WriteCore(BinaryWriter writer)
        {
            this.header.Write(writer);
            base.WriteCore(writer);
        }
    }
}

