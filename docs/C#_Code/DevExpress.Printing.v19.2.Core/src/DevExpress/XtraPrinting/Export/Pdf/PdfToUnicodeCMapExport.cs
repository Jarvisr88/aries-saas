namespace DevExpress.XtraPrinting.Export.Pdf
{
    using System;

    internal class PdfToUnicodeCMapExport : PdfToUnicodeCMapBase
    {
        private readonly string fontName;
        private readonly PdfCharCache charCache;

        public PdfToUnicodeCMapExport(PdfCharCache charCache, string fontName, bool compressed) : base(compressed)
        {
            this.charCache = charCache;
            this.fontName = fontName;
        }

        protected override string CreateCMapName() => 
            PdfFontUtils.Subname + "+" + this.fontName;

        public override void FillUp()
        {
            base.FillUpCharMap();
        }

        protected override PdfCharCache GetCharCache() => 
            this.charCache;

        public byte[] GetData()
        {
            this.FillUp();
            base.Stream.Data.Position = 0L;
            byte[] buffer = new byte[base.Stream.Data.Length];
            base.Stream.Data.Read(buffer, 0, (int) base.Stream.Data.Length);
            return buffer;
        }
    }
}

