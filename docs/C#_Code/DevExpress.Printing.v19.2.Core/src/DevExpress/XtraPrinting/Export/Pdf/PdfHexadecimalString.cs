namespace DevExpress.XtraPrinting.Export.Pdf
{
    using System;
    using System.IO;

    public class PdfHexadecimalString : PdfObject
    {
        private byte[] value;

        public PdfHexadecimalString(byte[] value)
        {
            this.value = value;
        }

        protected void SetValue(byte[] value)
        {
            this.value = value;
        }

        protected override void WriteContent(StreamWriter writer)
        {
            writer.Flush();
            string str = PdfStringUtils.ArrayToHexadecimalString(this.value);
            byte[] isoBytes = PdfStringUtils.GetIsoBytes($"<{str}>");
            writer.BaseStream.Write(isoBytes, 0, isoBytes.Length);
            writer.BaseStream.Flush();
        }
    }
}

