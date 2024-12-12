namespace DevExpress.XtraExport.Xls
{
    using DevExpress.Office.Utils;
    using System;
    using System.IO;
    using System.Runtime.CompilerServices;

    internal class XlsContentAutoFilter : XlsContentBase
    {
        private readonly XlsAutoFilterDataOperation first = new XlsAutoFilterDataOperation();
        private readonly XlsAutoFilterDataOperation second = new XlsAutoFilterDataOperation();

        public override int GetSize() => 
            (4 + this.First.GetSize()) + this.Second.GetSize();

        public override void Read(XlReader reader, int size)
        {
        }

        public override void Write(BinaryWriter writer)
        {
            writer.Write((ushort) this.ColumnId);
            ushort num = 0;
            if (this.Join)
            {
                num = (ushort) (num | 1);
            }
            if (this.First.IsSimple)
            {
                num = (ushort) (num | 4);
            }
            if (this.Second.IsSimple)
            {
                num = (ushort) (num | 8);
            }
            if (this.IsTopFilter)
            {
                num = (ushort) (num | 0x10);
            }
            if (this.IsTop)
            {
                num = (ushort) (num | 0x20);
            }
            if (this.IsPercent)
            {
                num = (ushort) (num | 0x40);
            }
            num = (ushort) (num | ((ushort) ((this.TopValue & 0x1ff) << 7)));
            writer.Write(num);
            this.First.Write(writer);
            this.Second.Write(writer);
            this.First.WriteStringValue(writer);
            this.Second.WriteStringValue(writer);
        }

        public int ColumnId { get; set; }

        public bool Join { get; set; }

        public bool IsTopFilter { get; set; }

        public bool IsTop { get; set; }

        public bool IsPercent { get; set; }

        public int TopValue { get; set; }

        public XlsAutoFilterDataOperation First =>
            this.first;

        public XlsAutoFilterDataOperation Second =>
            this.second;
    }
}

