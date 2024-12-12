namespace DevExpress.XtraPrinting.Export.Pdf
{
    using System;
    using System.Collections.Generic;
    using System.IO;

    public class PdfTextUnicode : PdfObject
    {
        private string value;

        public PdfTextUnicode(string value)
        {
            this.value = value;
        }

        private byte[] GetBytes(char ch)
        {
            byte[] bytes = BitConverter.GetBytes(ch);
            if (BitConverter.IsLittleEndian)
            {
                Array.Reverse(bytes);
            }
            List<byte> list = new List<byte>(4);
            list.AddRange(bytes);
            return list.ToArray();
        }

        protected override void WriteContent(StreamWriter writer)
        {
            MemoryStream stream = new MemoryStream();
            stream.WriteByte(0xfe);
            stream.WriteByte(0xff);
            foreach (char ch in this.value)
            {
                byte[] bytes = this.GetBytes(ch);
                stream.Write(bytes, 0, bytes.Length);
            }
            PdfStreamWriter writer2 = writer as PdfStreamWriter;
            stream = new MemoryStream(PdfStringUtils.GetIsoBytes(PdfStringUtils.EscapeString(PdfStringUtils.GetIsoString(((writer2 == null) ? stream : writer2.EncryptStream(stream, this)).ToArray()))));
            writer.Write("(");
            writer.Flush();
            stream.WriteTo(writer.BaseStream);
            writer.Write(")");
        }

        public string Value =>
            this.value;
    }
}

