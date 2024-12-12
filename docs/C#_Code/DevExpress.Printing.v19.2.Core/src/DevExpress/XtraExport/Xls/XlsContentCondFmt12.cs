namespace DevExpress.XtraExport.Xls
{
    using DevExpress.Office.Utils;
    using System;
    using System.IO;

    public class XlsContentCondFmt12 : XlsContentCondFmt
    {
        private FutureRecordHeaderRefU header;

        public XlsContentCondFmt12()
        {
            FutureRecordHeaderRefU fu1 = new FutureRecordHeaderRefU();
            fu1.RecordTypeId = 0x879;
            this.header = fu1;
        }

        protected override void CheckRecordCount(int value)
        {
            base.CheckValue(value, 1, 0xffff, "RecordCount");
        }

        public override int GetSize() => 
            base.GetSize() + this.header.GetSize();

        public override void Read(XlReader reader, int size)
        {
            this.header = FutureRecordHeaderRefU.FromStream(reader);
            base.Read(reader, size);
        }

        public override void Write(BinaryWriter writer)
        {
            this.header.RangeOfCells = true;
            this.header.Range = base.BoundRange;
            this.header.Write(writer);
            base.Write(writer);
        }

        public override FutureRecordHeaderBase RecordHeader =>
            this.header;
    }
}

