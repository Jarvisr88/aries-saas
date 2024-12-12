namespace DevExpress.XtraExport.Xls
{
    using DevExpress.Export.Xl;
    using DevExpress.Office.Utils;
    using System;
    using System.IO;
    using System.Runtime.CompilerServices;

    internal class XlsContentAutoFilter12DateGroup : XlsContentBase
    {
        private FutureRecordHeaderRefU header;

        public XlsContentAutoFilter12DateGroup()
        {
            FutureRecordHeaderRefU fu1 = new FutureRecordHeaderRefU();
            fu1.RecordTypeId = 0x87f;
            this.header = fu1;
        }

        public override int GetSize() => 
            this.header.GetSize() + 0x18;

        public override void Read(XlReader reader, int size)
        {
        }

        public override void Write(BinaryWriter writer)
        {
            this.header.RangeOfCells = true;
            this.header.Write(writer);
            writer.Write((ushort) this.DateGroup.Value.Year);
            writer.Write((ushort) this.DateGroup.Value.Month);
            writer.Write(this.DateGroup.Value.Day);
            writer.Write((ushort) this.DateGroup.Value.Hour);
            writer.Write((ushort) this.DateGroup.Value.Minute);
            writer.Write((ushort) this.DateGroup.Value.Second);
            writer.Write((ushort) 0);
            writer.Write(0);
            writer.Write((int) (((int) this.DateGroup.GroupingType) - 1));
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

        public XlDateGroupItem DateGroup { get; set; }
    }
}

