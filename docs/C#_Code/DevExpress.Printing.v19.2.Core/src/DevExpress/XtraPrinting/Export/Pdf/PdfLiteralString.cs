namespace DevExpress.XtraPrinting.Export.Pdf
{
    using System;
    using System.IO;

    public class PdfLiteralString : PdfObject
    {
        private string value;
        private bool encryption;

        public PdfLiteralString(string value) : this(value, true)
        {
        }

        public PdfLiteralString(string value, bool encryption)
        {
            this.value = value;
            this.encryption = encryption;
        }

        protected override void WriteContent(StreamWriter writer)
        {
            writer.Flush();
            PdfStreamWriter writer2 = writer as PdfStreamWriter;
            string str2 = PdfStringUtils.EscapeString((!this.encryption || (writer2 == null)) ? this.value : writer2.EncryptString(this.value, this));
            byte[] isoBytes = PdfStringUtils.GetIsoBytes($"({str2})");
            writer.BaseStream.Write(isoBytes, 0, isoBytes.Length);
            writer.BaseStream.Flush();
        }

        public string Value =>
            this.value;
    }
}

