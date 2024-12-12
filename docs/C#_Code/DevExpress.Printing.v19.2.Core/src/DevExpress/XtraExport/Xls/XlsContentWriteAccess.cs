namespace DevExpress.XtraExport.Xls
{
    using DevExpress.Office.Utils;
    using System;
    using System.IO;

    public class XlsContentWriteAccess : XlsContentBase
    {
        private const int dataSize = 0x70;
        private const string noUserName = "  ";
        private XLUnicodeString userName;

        public XlsContentWriteAccess()
        {
            XLUnicodeString text1 = new XLUnicodeString();
            text1.Value = "  ";
            this.userName = text1;
        }

        public override int GetSize() => 
            0x70;

        public override void Read(XlReader reader, int size)
        {
            this.userName = XLUnicodeString.FromStream(reader, size);
            int num = size - this.userName.Length;
            if (num > 0)
            {
                reader.Seek((long) num, SeekOrigin.Current);
            }
        }

        public override void Write(BinaryWriter writer)
        {
            this.userName.Write(writer);
            int num = 0x70 - this.userName.Length;
            for (int i = 0; i < num; i++)
            {
                writer.Write((byte) 0x20);
            }
        }

        public string Value
        {
            get => 
                this.userName.Value;
            set
            {
                this.userName.Value = string.IsNullOrEmpty(value) ? "  " : value;
                if (this.userName.Length > 0x70)
                {
                    throw new ArgumentException("Too long string value");
                }
            }
        }
    }
}

