namespace DevExpress.XtraPrinting.Export.Pdf
{
    using System;

    public class PdfXRefEntryFree : PdfXRefEntryBase
    {
        protected override string TypeString =>
            "f";

        protected override long ByteOffset =>
            0L;

        protected override int Generation =>
            0xffff;
    }
}

