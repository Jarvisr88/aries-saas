namespace DevExpress.XtraExport.Xls
{
    using DevExpress.Office.Utils;
    using System;
    using System.IO;

    public class ShortXLUnicodeString : XLUnicodeStringBase
    {
        private const int headerSize = 2;

        public static ShortXLUnicodeString FromStream(BinaryDataReaderBase reader)
        {
            ShortXLUnicodeString str = new ShortXLUnicodeString();
            str.Read(reader);
            return str;
        }

        public static ShortXLUnicodeString FromStream(BinaryReader reader)
        {
            ShortXLUnicodeString str = new ShortXLUnicodeString();
            str.Read(reader);
            return str;
        }

        protected override int GetHeaderSize()
        {
            int num = 2;
            if (base.Value.Length > 0)
            {
                num += base.HasHighBytes ? 2 : 1;
            }
            return num;
        }

        protected override int ReadCharCount(BinaryDataReaderBase reader) => 
            reader.ReadByte();

        protected override int ReadCharCount(BinaryReader reader) => 
            reader.ReadByte();

        protected override int ReadCharCount(byte[] data, ref long position)
        {
            position += 1L;
            return data[(int) ((IntPtr) (position - 1L))];
        }

        protected override void ValidateStringValue(string text)
        {
            if (text.Length > 0xff)
            {
                throw new ArgumentException("String value too long");
            }
        }

        protected override void WriteCharCount(BinaryWriter writer)
        {
            writer.Write((byte) base.Value.Length);
        }

        public override int Length =>
            base.HasHighBytes ? ((base.Value.Length * 2) + 2) : (base.Value.Length + 2);
    }
}

