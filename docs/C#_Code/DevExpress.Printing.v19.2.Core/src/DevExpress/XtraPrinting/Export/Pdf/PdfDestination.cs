namespace DevExpress.XtraPrinting.Export.Pdf
{
    using System;
    using System.IO;

    public class PdfDestination : PdfObject
    {
        private const string destType = "XYZ";
        private PdfArray array = new PdfArray();

        public PdfDestination(PdfPage page, float top)
        {
            this.array.Add(page.InnerObject);
            this.array.Add("XYZ");
            this.array.Add(new PdfNull());
            this.array.Add(new PdfDouble((double) top));
            this.array.Add(new PdfNull());
        }

        protected override void WriteContent(StreamWriter writer)
        {
            this.array.WriteToStream(writer);
        }
    }
}

