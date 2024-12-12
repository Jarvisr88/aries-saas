namespace DevExpress.XtraExport.Xls
{
    using DevExpress.Office.Utils;
    using System;
    using System.IO;

    internal class XlsContentAutoFilter12Criteria : XlsContentBase
    {
        private FutureRecordHeaderRefU header;
        private readonly XlsAutoFilterDataOperation criteria;

        public XlsContentAutoFilter12Criteria()
        {
            FutureRecordHeaderRefU fu1 = new FutureRecordHeaderRefU();
            fu1.RecordTypeId = 0x87f;
            this.header = fu1;
            this.criteria = new XlsAutoFilter12DataOperation();
        }

        public override int GetSize() => 
            this.header.GetSize() + this.criteria.GetSize();

        public override void Read(XlReader reader, int size)
        {
        }

        public override void Write(BinaryWriter writer)
        {
            this.header.RangeOfCells = true;
            this.header.Write(writer);
            this.criteria.Write(writer);
            this.criteria.WriteStringValue(writer);
        }

        public override FutureRecordHeaderBase RecordHeader =>
            this.header;

        public XlsRef8 BoundRange
        {
            get => 
                this.header.Range;
            set
            {
                if (value != null)
                {
                    this.header.Range = value;
                }
                else
                {
                    this.header.Range = new XlsRef8();
                }
            }
        }

        public XlsAutoFilterDataOperation Criteria =>
            this.criteria;
    }
}

