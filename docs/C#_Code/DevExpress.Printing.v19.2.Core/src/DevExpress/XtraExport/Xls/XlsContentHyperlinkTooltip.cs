namespace DevExpress.XtraExport.Xls
{
    using DevExpress.Office.Utils;
    using System;
    using System.IO;

    public class XlsContentHyperlinkTooltip : XlsContentBase
    {
        private FutureRecordHeaderRefNoFlags header;
        private NullTerminatedUnicodeString tooltip;

        public XlsContentHyperlinkTooltip()
        {
            FutureRecordHeaderRefNoFlags flags1 = new FutureRecordHeaderRefNoFlags();
            flags1.RecordTypeId = 0x800;
            this.header = flags1;
            this.tooltip = new NullTerminatedUnicodeString();
        }

        public override int GetSize() => 
            this.header.GetSize() + this.tooltip.Length;

        public override void Read(XlReader reader, int size)
        {
            this.header = FutureRecordHeaderRefNoFlags.FromStream(reader);
            this.tooltip = NullTerminatedUnicodeString.FromStream(reader);
        }

        public override void Write(BinaryWriter writer)
        {
            this.header.Write(writer);
            this.tooltip.Write(writer);
        }

        public XlsRef8 Range
        {
            get => 
                this.header.Range;
            set => 
                this.header.Range = value;
        }

        public string Tooltip
        {
            get => 
                this.tooltip.Value;
            set => 
                this.tooltip.Value = value;
        }

        public override FutureRecordHeaderBase RecordHeader =>
            this.header;
    }
}

