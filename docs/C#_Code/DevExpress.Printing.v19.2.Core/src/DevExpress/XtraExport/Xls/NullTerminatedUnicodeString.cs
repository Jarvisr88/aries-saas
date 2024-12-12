namespace DevExpress.XtraExport.Xls
{
    using DevExpress.Office.Utils;
    using System;
    using System.IO;
    using System.Text;

    public class NullTerminatedUnicodeString
    {
        private string value = string.Empty;

        public static NullTerminatedUnicodeString FromStream(XlReader reader)
        {
            NullTerminatedUnicodeString str = new NullTerminatedUnicodeString();
            str.Read(reader);
            return str;
        }

        protected void Read(XlReader reader)
        {
            StringBuilder builder = new StringBuilder();
            for (ushort i = reader.ReadUInt16(); i != 0; i = reader.ReadUInt16())
            {
                builder.Append(Convert.ToChar(i));
            }
            this.value = builder.ToString();
        }

        public virtual void Write(BinaryWriter writer)
        {
            if (!string.IsNullOrEmpty(this.value))
            {
                byte[] bytes = XLStringEncoder.GetEncoding(true).GetBytes(this.value);
                writer.Write(bytes);
            }
            writer.Write((ushort) 0);
        }

        public string Value
        {
            get => 
                this.value;
            set
            {
                value ??= string.Empty;
                this.value = value;
            }
        }

        public virtual int Length =>
            (this.value.Length * 2) + 2;
    }
}

