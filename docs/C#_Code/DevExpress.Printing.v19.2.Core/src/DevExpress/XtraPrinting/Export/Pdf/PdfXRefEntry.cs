namespace DevExpress.XtraPrinting.Export.Pdf
{
    using System;

    public class PdfXRefEntry : PdfXRefEntryBase
    {
        private PdfIndirectReference indirectReference;

        public PdfXRefEntry(PdfIndirectReference indirectReference)
        {
            this.indirectReference = indirectReference;
        }

        protected override string TypeString =>
            "n";

        protected override long ByteOffset =>
            this.indirectReference.ByteOffset;

        protected override int Generation =>
            0;
    }
}

