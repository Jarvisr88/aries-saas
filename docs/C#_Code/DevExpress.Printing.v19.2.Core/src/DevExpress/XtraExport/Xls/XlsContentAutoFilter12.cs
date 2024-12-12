namespace DevExpress.XtraExport.Xls
{
    using DevExpress.Export.Xl;
    using DevExpress.Office.Utils;
    using System;
    using System.IO;
    using System.Runtime.CompilerServices;

    internal class XlsContentAutoFilter12 : XlsContentBase
    {
        private FutureRecordHeaderRefU header;
        private XlsDxfN12List colorInfo;

        public XlsContentAutoFilter12()
        {
            FutureRecordHeaderRefU fu1 = new FutureRecordHeaderRefU();
            fu1.RecordTypeId = 0x87e;
            this.header = fu1;
            XlsDxfN12List list1 = new XlsDxfN12List();
            list1.IsEmpty = true;
            this.colorInfo = list1;
            this.IdList = -1;
        }

        public override int GetSize()
        {
            int num = 0x30;
            if ((this.FilterType == XlsAutoFilter12Type.CellColor) || (this.FilterType == XlsAutoFilter12Type.CellFont))
            {
                num += this.ColorInfo.GetSize();
            }
            else if (this.FilterType == XlsAutoFilter12Type.CellIcon)
            {
                num += 8;
            }
            return (this.header.GetSize() + num);
        }

        public override void Read(XlReader reader, int size)
        {
        }

        public override void Write(BinaryWriter writer)
        {
            this.header.RangeOfCells = true;
            this.header.Write(writer);
            writer.Write((ushort) this.ColumnId);
            writer.Write(this.HiddenButton ? 1 : 0);
            writer.Write((int) this.FilterType);
            writer.Write(this.CustomFilterType);
            writer.Write(this.CriteriaCount);
            writer.Write(this.DateGroupingsCount);
            writer.Write((this.IdList == -1) ? ((ushort) 8) : ((ushort) 0));
            writer.Write(0);
            writer.Write(this.IdList);
            writer.Write(new byte[0x10]);
            if ((this.FilterType == XlsAutoFilter12Type.CellColor) || (this.FilterType == XlsAutoFilter12Type.CellFont))
            {
                this.ColorInfo.Write(writer);
            }
            else if (this.FilterType == XlsAutoFilter12Type.CellIcon)
            {
                writer.Write((int) XlsCondFmtHelper.IconSetTypeToCode(this.IconSet));
                writer.Write(this.IconId);
            }
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

        public int ColumnId { get; set; }

        public bool HiddenButton { get; set; }

        public XlsAutoFilter12Type FilterType { get; set; }

        public int CustomFilterType { get; set; }

        public int CriteriaCount { get; set; }

        public int DateGroupingsCount { get; set; }

        public int IdList { get; set; }

        public XlsDxfN12List ColorInfo =>
            this.colorInfo;

        public XlCondFmtIconSetType IconSet { get; set; }

        public int IconId { get; set; }
    }
}

