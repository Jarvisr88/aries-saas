namespace DevExpress.XtraExport.Xls
{
    using DevExpress.Office.Utils;
    using System;
    using System.IO;

    public class XLUnicodeString : XLUnicodeStringBase, ISupportPartialReading
    {
        private const int headerSize = 3;
        private int charCount;

        public static XLUnicodeString FromBinary(byte[] data, ref long position)
        {
            XLUnicodeString str = new XLUnicodeString();
            str.Read(data, ref position);
            return str;
        }

        public static XLUnicodeString FromStream(BinaryDataReaderBase reader)
        {
            XLUnicodeString str = new XLUnicodeString();
            str.Read(reader);
            return str;
        }

        public static XLUnicodeString FromStream(BinaryReader reader)
        {
            XLUnicodeString str = new XLUnicodeString();
            str.Read(reader);
            return str;
        }

        public static XLUnicodeString FromStream(XlReader reader, int size)
        {
            XLUnicodeString str = new XLUnicodeString();
            str.Read(reader, size);
            return str;
        }

        protected override int GetHeaderSize()
        {
            int num = 3;
            if (base.Value.Length > 0)
            {
                num += base.HasHighBytes ? 2 : 1;
            }
            return num;
        }

        protected void Read(byte[] data, ref long position)
        {
            int num = this.ReadCharCount(data, ref position);
            byte num2 = base.ReadFlags(data, ref position);
            if (num <= 0)
            {
                base.Value = string.Empty;
            }
            else
            {
                int count = base.HasHighBytes ? (num * 2) : num;
                byte[] destinationArray = new byte[count];
                Array.Copy(data, position, destinationArray, 0L, (long) count);
                position += count;
                base.Value = XLStringEncoder.GetEncoding(base.HasHighBytes).GetString(destinationArray, 0, count);
            }
        }

        public void ReadData(XlReader reader)
        {
            if (this.Complete)
            {
                base.Value = null;
                this.charCount = 0;
            }
            if ((this.charCount == 0) && (base.Value.Length == 0))
            {
                this.charCount = this.ReadCharCount(reader);
                byte flags = base.ReadFlags(reader);
                this.ReadExtraHeader(reader, flags);
            }
            if (reader.Position == 0)
            {
                base.ReadFlags(reader);
            }
            long num = reader.StreamLength - reader.Position;
            int count = this.charCount - base.Value.Length;
            if (base.HasHighBytes)
            {
                count *= 2;
            }
            if (count > num)
            {
                count = (int) num;
            }
            byte[] bytes = reader.ReadBytes(count);
            string str = XLStringEncoder.GetEncoding(base.HasHighBytes).GetString(bytes, 0, count);
            base.Value = base.Value + str;
        }

        public override int Length =>
            base.HasHighBytes ? ((base.Value.Length * 2) + 3) : (base.Value.Length + 3);

        public bool Complete =>
            base.Value.Length == this.charCount;
    }
}

