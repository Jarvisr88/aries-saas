namespace DevExpress.XtraExport.Xls
{
    using DevExpress.Office.Utils;
    using System;
    using System.IO;
    using System.Runtime.CompilerServices;

    public abstract class XlsContentList12Base : XlsContentBase
    {
        private FutureRecordHeader header;

        protected XlsContentList12Base()
        {
            FutureRecordHeader header1 = new FutureRecordHeader();
            header1.RecordTypeId = 0x877;
            this.header = header1;
        }

        public override int GetSize() => 
            this.header.GetSize() + 6;

        public override void Read(XlReader reader, int size)
        {
        }

        public override void Write(BinaryWriter writer)
        {
            this.header.Write(writer);
            writer.Write(this.DataType);
            writer.Write(this.TableId);
        }

        public override FutureRecordHeaderBase RecordHeader =>
            this.header;

        protected abstract short DataType { get; }

        public int TableId { get; set; }
    }
}

