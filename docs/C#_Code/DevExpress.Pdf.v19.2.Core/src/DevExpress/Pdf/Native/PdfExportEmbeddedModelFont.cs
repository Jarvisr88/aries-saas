namespace DevExpress.Pdf.Native
{
    using DevExpress.Pdf;
    using DevExpress.Pdf.ContentGeneration;
    using DevExpress.Pdf.ContentGeneration.Extensions;
    using DevExpress.Text.Fonts;
    using System;
    using System.Collections.Generic;

    public class PdfExportEmbeddedModelFont : PdfExportModelFont
    {
        private readonly PdfDeferredCIDType2Font font;
        private readonly PdfFontFile fontFile;
        private readonly IPdfDocumentContext context;

        public PdfExportEmbeddedModelFont(IPdfDocumentContext context, IPdfExportPlatformFont platformFont) : base(true)
        {
            this.context = context;
            this.fontFile = platformFont.FontFile;
            this.font = new PdfDeferredCIDType2Font($"{"DEVEXP"}+" + platformFont.Descriptor.GetFontName(), new PdfCompositeFontDescriptor(new PdfExportFontDescriptorBuilder(platformFont)));
        }

        public override DXGlyph CreateGlyph(int index, char unicode, float width, float advance) => 
            new DXGlyph((ushort) index, advance, DXGlyphOffset.Empty);

        public override float GetGlyphWidth(int mappedIndex) => 
            this.fontFile.GetCharacterWidth(mappedIndex);

        protected override void UpdateFontFile(PdfExportModelFont.Subset subset)
        {
            PdfFontFileSubset subset2 = this.fontFile.CreateSubset(subset.Indices);
            if (subset2.Type == PdfFontFileSubsetType.TrueType)
            {
                this.font.FontFileData = subset2.Data;
            }
            else if (subset2.Type == PdfFontFileSubsetType.CFF)
            {
                this.font.OpenTypeFontFileData = subset2.Data;
            }
            this.font.Widths = new SortedDictionary<int, double>(subset.Widths);
            if (this.context != null)
            {
                this.context.NotifyFontChanged(this.font);
            }
        }

        public override PdfFont Font =>
            this.font;
    }
}

