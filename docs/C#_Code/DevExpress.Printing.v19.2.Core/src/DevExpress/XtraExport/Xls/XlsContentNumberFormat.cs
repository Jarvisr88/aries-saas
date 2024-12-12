namespace DevExpress.XtraExport.Xls
{
    using DevExpress.Office.Utils;
    using System;
    using System.IO;

    public class XlsContentNumberFormat : XlsContentBase
    {
        private int formatId;
        private XLUnicodeString formatCode = new XLUnicodeString();

        public override int GetSize() => 
            this.formatCode.Length + 2;

        public override void Read(XlReader reader, int size)
        {
            this.formatId = reader.ReadUInt16();
            this.formatCode = XLUnicodeString.FromStream(reader);
        }

        public override void Write(BinaryWriter writer)
        {
            writer.Write((ushort) this.formatId);
            this.formatCode.Write(writer);
        }

        public int FormatId
        {
            get => 
                this.formatId;
            set
            {
                base.CheckValue(value, 0, 0xffff, "FormatId");
                this.formatId = value;
            }
        }

        public string FormatCode
        {
            get => 
                this.formatCode.Value;
            set => 
                this.formatCode.Value = value;
        }
    }
}

