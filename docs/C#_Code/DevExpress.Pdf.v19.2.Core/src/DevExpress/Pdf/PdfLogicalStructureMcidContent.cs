namespace DevExpress.Pdf
{
    using DevExpress.Pdf.Native;
    using System;

    public class PdfLogicalStructureMcidContent : PdfLogicalStructureItem
    {
        private readonly int mcid;

        internal PdfLogicalStructureMcidContent(int mcid) : base(-1)
        {
            this.mcid = mcid;
        }

        protected override PdfWriterDictionary CreateDictionary(PdfObjectCollection collection) => 
            null;

        protected internal override object Write(PdfObjectCollection collection) => 
            this.mcid;

        public int Mcid =>
            this.mcid;
    }
}

