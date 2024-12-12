namespace DevExpress.XtraPrinting.Export.Pdf
{
    using System;
    using System.IO;

    public class PdfDouble : PdfObject
    {
        private double value;

        public PdfDouble(double value)
        {
            this.value = value;
        }

        protected override void WriteContent(StreamWriter writer)
        {
            writer.Write(Utils.ToString(this.value));
        }

        public double Value =>
            this.value;
    }
}

