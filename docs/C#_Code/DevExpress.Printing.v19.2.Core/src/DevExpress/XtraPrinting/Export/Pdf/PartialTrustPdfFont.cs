namespace DevExpress.XtraPrinting.Export.Pdf
{
    using System;
    using System.Drawing;

    public class PartialTrustPdfFont : PdfFont
    {
        public PartialTrustPdfFont(Font font, bool compressed) : base(font, compressed)
        {
        }

        protected internal override void CreateInnerFont()
        {
            if (base.innerFont == null)
            {
                base.innerFont = new PartialTrustPdfTrueTypeFont(this, base.compressed);
            }
        }

        protected override PdfCharCache CreatePdfFontCache() => 
            new PartialTrustPdfCharCache();

        protected override bool ShouldCreateTTFFile =>
            false;
    }
}

