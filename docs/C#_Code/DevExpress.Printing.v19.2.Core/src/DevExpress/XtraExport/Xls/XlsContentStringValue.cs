namespace DevExpress.XtraExport.Xls
{
    using DevExpress.Office.Utils;
    using System;
    using System.IO;

    public class XlsContentStringValue : XlsContentBase
    {
        private XLUnicodeString internalString = new XLUnicodeString();

        public override int GetSize() => 
            !string.IsNullOrEmpty(this.internalString.Value) ? this.internalString.Length : 0;

        public override void Read(XlReader reader, int size)
        {
            if (size > 0)
            {
                this.internalString = XLUnicodeString.FromStream(reader);
            }
            else
            {
                this.internalString.Value = string.Empty;
            }
        }

        public override void Write(BinaryWriter writer)
        {
            if (!string.IsNullOrEmpty(this.internalString.Value))
            {
                this.internalString.Write(writer);
            }
        }

        public string Value
        {
            get => 
                this.internalString.Value;
            set => 
                this.internalString.Value = value;
        }
    }
}

