namespace DevExpress.XtraExport.Xls
{
    using DevExpress.Office.Utils;
    using System;
    using System.IO;

    public class XlsContentLabel : XlsContentCellBase
    {
        private XLUnicodeString stringValue = new XLUnicodeString();

        public override int GetSize() => 
            base.GetSize() + this.stringValue.Length;

        public override void Read(XlReader reader, int size)
        {
            base.Read(reader, size);
            this.stringValue = XLUnicodeString.FromStream(reader);
            int count = size - this.GetSize();
            if (count > 0)
            {
                reader.ReadBytes(count);
            }
        }

        public override void Write(BinaryWriter writer)
        {
            base.Write(writer);
            this.stringValue.Write(writer);
        }

        public string Value
        {
            get => 
                this.stringValue.Value;
            set
            {
                if (!string.IsNullOrEmpty(value) && (value.Length > 0xff))
                {
                    throw new ArgumentException("String value length exceed 255 characters");
                }
                this.stringValue.Value = value;
            }
        }
    }
}

