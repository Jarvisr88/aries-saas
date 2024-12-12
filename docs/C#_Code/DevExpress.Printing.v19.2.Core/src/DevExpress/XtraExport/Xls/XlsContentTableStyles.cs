namespace DevExpress.XtraExport.Xls
{
    using DevExpress.Office.Utils;
    using System;
    using System.IO;

    public class XlsContentTableStyles : XlsContentBase
    {
        private const int fixedPartSize = 20;
        private const int predefinedTableStylesCount = 0x90;
        private FutureRecordHeader header;
        private XLUnicodeCharactersArray defaultTableStyleName;
        private XLUnicodeCharactersArray defaultPivotTableStyleName;

        public XlsContentTableStyles()
        {
            FutureRecordHeader header1 = new FutureRecordHeader();
            header1.RecordTypeId = 0x88e;
            this.header = header1;
            this.defaultTableStyleName = new XLUnicodeCharactersArray();
            this.defaultPivotTableStyleName = new XLUnicodeCharactersArray();
        }

        public override int GetSize() => 
            (20 + this.defaultTableStyleName.Length) + this.defaultPivotTableStyleName.Length;

        public override void Read(XlReader reader, int size)
        {
        }

        public override void Write(BinaryWriter writer)
        {
            this.header.Write(writer);
            writer.Write(0x90);
            writer.Write((short) this.DefaultTableStyleName.Length);
            writer.Write((short) this.DefaultPivotTableStyleName.Length);
            if (!string.IsNullOrEmpty(this.DefaultTableStyleName))
            {
                this.defaultTableStyleName.Write(writer);
            }
            if (!string.IsNullOrEmpty(this.DefaultPivotTableStyleName))
            {
                this.defaultPivotTableStyleName.Write(writer);
            }
        }

        public override FutureRecordHeaderBase RecordHeader =>
            this.header;

        public string DefaultTableStyleName
        {
            get => 
                this.defaultTableStyleName.Value;
            set => 
                this.defaultTableStyleName.Value = value;
        }

        public string DefaultPivotTableStyleName
        {
            get => 
                this.defaultPivotTableStyleName.Value;
            set => 
                this.defaultPivotTableStyleName.Value = value;
        }
    }
}

