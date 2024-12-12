namespace DevExpress.XtraPrinting.Export.Pdf
{
    using System;
    using System.IO;

    public class PdfBoolean : PdfObject
    {
        private bool value;

        public PdfBoolean(bool value)
        {
            this.value = value;
        }

        protected override void WriteContent(StreamWriter writer)
        {
            writer.Write(this.value ? "true" : "false");
        }

        public bool Value =>
            this.value;
    }
}

