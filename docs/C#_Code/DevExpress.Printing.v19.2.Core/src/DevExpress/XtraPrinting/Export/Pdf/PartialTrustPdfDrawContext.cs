namespace DevExpress.XtraPrinting.Export.Pdf
{
    using System;

    public class PartialTrustPdfDrawContext : PdfDrawContext
    {
        public PartialTrustPdfDrawContext(PdfStream stream, IPdfContentsOwner owner, PdfHashtable pdfHashtable) : base(stream, owner, pdfHashtable)
        {
        }

        protected override float GetCharWidth(char ch) => 
            MeasuringHelper.MeasureCharWidth(ch, base.ActualFont);
    }
}

