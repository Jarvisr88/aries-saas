namespace DevExpress.XtraPrinting.Export.Pdf
{
    using System;
    using System.IO;

    public class PdfHeader
    {
        private PdfDocument document;

        public PdfHeader(PdfDocument document)
        {
            this.document = document;
        }

        public void Write(StreamWriter writer)
        {
            string str = "%PDF-1.4";
            if ((this.document.Encryption != null) && this.document.Encryption.IsEncryptionOn)
            {
                str = "%PDF-1.6";
            }
            writer.WriteLine(str);
            if (this.document.PdfACompatible)
            {
                writer.Write("%");
                writer.Flush();
                writer.BaseStream.WriteByte(0xca);
                writer.BaseStream.WriteByte(0xe0);
                writer.BaseStream.WriteByte(0xfb);
                writer.BaseStream.WriteByte(0xac);
                writer.WriteLine("");
            }
        }
    }
}

