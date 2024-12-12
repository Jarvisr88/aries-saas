namespace DevExpress.XtraExport.Xls
{
    using DevExpress.Office.Utils;
    using System;
    using System.IO;

    public class XLUnicodeCharactersArray
    {
        private string value = string.Empty;

        public static XLUnicodeCharactersArray FromStream(XlReader reader, int charCount)
        {
            XLUnicodeCharactersArray array = new XLUnicodeCharactersArray();
            array.Read(reader, charCount);
            return array;
        }

        protected void Read(XlReader reader, int charCount)
        {
            byte[] bytes = reader.ReadBytes(charCount * 2);
            this.value = XLStringEncoder.GetEncoding(true).GetString(bytes, 0, bytes.Length);
        }

        public virtual void Write(BinaryWriter writer)
        {
            if (!string.IsNullOrEmpty(this.value))
            {
                byte[] bytes = XLStringEncoder.GetEncoding(true).GetBytes(this.value);
                writer.Write(bytes);
            }
        }

        public string Value
        {
            get => 
                this.value;
            set
            {
                value ??= string.Empty;
                if (value.Length > 0xffff)
                {
                    throw new ArgumentException("String value too long");
                }
                this.value = value;
            }
        }

        public virtual int Length =>
            this.value.Length * 2;
    }
}

