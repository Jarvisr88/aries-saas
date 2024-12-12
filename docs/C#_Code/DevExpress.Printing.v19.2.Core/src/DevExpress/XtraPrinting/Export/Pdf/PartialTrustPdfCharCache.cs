namespace DevExpress.XtraPrinting.Export.Pdf
{
    using System;

    public class PartialTrustPdfCharCache : PdfCharCache
    {
        protected override bool ShouldExpandCompositeGlyphs =>
            false;
    }
}

