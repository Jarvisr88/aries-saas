namespace DevExpress.XtraPrinting.Export.Pdf
{
    using System;
    using System.IO;

    public class PdfTrailer
    {
        private PdfDictionary attributes = new PdfDictionary();
        private PdfXRef xRef;

        public PdfTrailer(PdfXRef xRef)
        {
            this.xRef = xRef;
        }

        private long GetXRefByteOffset() => 
            (this.xRef != null) ? this.xRef.ByteOffset : 0L;

        public void Write(StreamWriter writer)
        {
            writer.WriteLine("trailer");
            this.attributes.WriteToStream(writer);
            writer.WriteLine();
            writer.WriteLine("startxref");
            writer.WriteLine(Convert.ToString(this.GetXRefByteOffset()));
            writer.Write("%%EOF");
        }

        public PdfDictionary Attributes =>
            this.attributes;

        public PdfXRef XRef =>
            this.xRef;
    }
}

