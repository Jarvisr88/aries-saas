namespace DevExpress.XtraPrinting.Export.Pdf
{
    using System;

    internal class PdfToUnicodeCMap : PdfToUnicodeCMapBase
    {
        private PdfFontBase ownerFont;

        public PdfToUnicodeCMap(PdfFontBase ownerFont, bool compressed) : base(compressed)
        {
            this.ownerFont = ownerFont;
        }

        protected override string CreateCMapName() => 
            PdfFontUtils.Subname + "+" + this.OwnerFont.Name;

        protected override PdfCharCache GetCharCache() => 
            this.OwnerFont.Owner.CharCache;

        public PdfFontBase OwnerFont =>
            this.ownerFont;
    }
}

