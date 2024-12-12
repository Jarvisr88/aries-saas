namespace DevExpress.XtraPrinting.Export.Pdf
{
    using System;
    using System.IO;

    public class PdfNumber : PdfObject
    {
        private int value;

        public PdfNumber(int value)
        {
            this.value = value;
        }

        protected override void WriteContent(StreamWriter writer)
        {
            writer.Write(this.value);
        }

        public int Value =>
            this.value;
    }
}

