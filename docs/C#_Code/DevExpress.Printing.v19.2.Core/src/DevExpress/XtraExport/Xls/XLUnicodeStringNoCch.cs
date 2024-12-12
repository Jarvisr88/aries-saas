namespace DevExpress.XtraExport.Xls
{
    using DevExpress.Office.Utils;
    using System;
    using System.IO;

    public class XLUnicodeStringNoCch
    {
        private bool hasHighBytes;
        private string value = string.Empty;

        public static XLUnicodeStringNoCch FromStream(XlReader reader, int charCount)
        {
            XLUnicodeStringNoCch cch = new XLUnicodeStringNoCch();
            cch.Read(reader, charCount);
            return cch;
        }

        public static XLUnicodeStringNoCch FromStream(BinaryReader reader, int charCount)
        {
            XLUnicodeStringNoCch cch = new XLUnicodeStringNoCch();
            cch.Read(reader, charCount);
            return cch;
        }

        protected void Read(XlReader reader, int charCount)
        {
            this.hasHighBytes = Convert.ToBoolean(reader.ReadByte());
            if (charCount > 0)
            {
                int count = this.hasHighBytes ? (charCount * 2) : charCount;
                byte[] bytes = reader.ReadBytes(count);
                this.value = XLStringEncoder.GetEncoding(this.HasHighBytes).GetString(bytes, 0, count);
            }
        }

        protected void Read(BinaryReader reader, int charCount)
        {
            this.hasHighBytes = Convert.ToBoolean(reader.ReadByte());
            if (charCount > 0)
            {
                int count = this.hasHighBytes ? (charCount * 2) : charCount;
                byte[] bytes = reader.ReadBytes(count);
                this.value = XLStringEncoder.GetEncoding(this.HasHighBytes).GetString(bytes, 0, count);
            }
        }

        public void Write(BinaryWriter writer)
        {
            writer.Write(this.HasHighBytes ? ((byte) 1) : ((byte) 0));
            if (this.value.Length > 0)
            {
                writer.Write(XLStringEncoder.GetBytes(this.value, this.HasHighBytes));
            }
        }

        public bool HasHighBytes =>
            this.hasHighBytes;

        public string Value
        {
            get => 
                this.value;
            set
            {
                value ??= string.Empty;
                if (value.Length > 0x7fff)
                {
                    throw new ArgumentException("String value too long");
                }
                this.value = value;
                this.hasHighBytes = XLStringEncoder.StringHasHighBytes(this.value);
            }
        }

        public int Length =>
            (this.HasHighBytes ? (this.value.Length * 2) : this.value.Length) + 1;
    }
}

