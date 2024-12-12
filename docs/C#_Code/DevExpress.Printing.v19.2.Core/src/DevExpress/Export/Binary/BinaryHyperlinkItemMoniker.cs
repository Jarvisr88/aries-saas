namespace DevExpress.Export.Binary
{
    using DevExpress.Office.Utils;
    using DevExpress.XtraExport.Xls;
    using System;
    using System.IO;

    public class BinaryHyperlinkItemMoniker : BinaryHyperlinkMonikerBase
    {
        private string delimiter;
        private string item;

        public BinaryHyperlinkItemMoniker() : base(BinaryHyperlinkMonikerFactory.CLSID_ItemMoniker)
        {
            this.delimiter = string.Empty;
            this.item = string.Empty;
            this.Delimiter = string.Empty;
            this.Item = string.Empty;
        }

        public static BinaryHyperlinkItemMoniker FromStream(XlReader reader)
        {
            BinaryHyperlinkItemMoniker moniker = new BinaryHyperlinkItemMoniker();
            moniker.Read(reader);
            return moniker;
        }

        private int GetComplexStringBytesCount(string value)
        {
            int num = value.Length + 1;
            if (XLStringEncoder.StringHasHighBytes(value))
            {
                num += value.Length * 2;
            }
            return num;
        }

        private int GetComplexStringSize(string value) => 
            this.GetComplexStringBytesCount(value) + 4;

        private int GetNullTerminatingPosition(byte[] buf)
        {
            for (int i = 0; i < buf.Length; i++)
            {
                if (buf[i] == 0)
                {
                    return i;
                }
            }
            return -1;
        }

        public override int GetSize() => 
            (base.GetSize() + this.GetComplexStringSize(this.Delimiter)) + this.GetComplexStringSize(this.Item);

        protected void Read(XlReader reader)
        {
            this.Delimiter = this.ReadComplexString(reader);
            this.Item = this.ReadComplexString(reader);
        }

        private string ReadComplexString(XlReader reader)
        {
            int count = reader.ReadInt32();
            byte[] buf = reader.ReadBytes(count);
            int nullTerminatingPosition = this.GetNullTerminatingPosition(buf);
            return ((nullTerminatingPosition != -1) ? ((nullTerminatingPosition >= (count - 1)) ? XLStringEncoder.GetEncoding(false).GetString(buf, 0, nullTerminatingPosition) : XLStringEncoder.GetEncoding(true).GetString(buf, nullTerminatingPosition + 1, (count - nullTerminatingPosition) - 1)) : XLStringEncoder.GetEncoding(false).GetString(buf, 0, count));
        }

        public override void Write(BinaryWriter writer)
        {
            base.Write(writer);
            this.WriteComplexString(writer, this.Delimiter);
            this.WriteComplexString(writer, this.Item);
        }

        private void WriteComplexString(BinaryWriter writer, string value)
        {
            int complexStringBytesCount = this.GetComplexStringBytesCount(value);
            writer.Write(complexStringBytesCount);
            writer.Write(XLStringEncoder.GetBytes(value, false));
            writer.Write((byte) 0);
            if (XLStringEncoder.StringHasHighBytes(value))
            {
                writer.Write(XLStringEncoder.GetBytes(value, true));
            }
        }

        public string Delimiter
        {
            get => 
                this.delimiter;
            set
            {
                value ??= string.Empty;
                this.delimiter = value;
            }
        }

        public string Item
        {
            get => 
                this.item;
            set
            {
                value ??= string.Empty;
                this.item = value;
            }
        }
    }
}

