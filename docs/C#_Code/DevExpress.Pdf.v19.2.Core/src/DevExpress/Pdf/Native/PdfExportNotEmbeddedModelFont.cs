namespace DevExpress.Pdf.Native
{
    using DevExpress.Pdf;
    using DevExpress.Pdf.ContentGeneration;
    using DevExpress.Pdf.ContentGeneration.Extensions;
    using DevExpress.Text.Fonts;
    using System;
    using System.Runtime.CompilerServices;

    public class PdfExportNotEmbeddedModelFont : PdfExportModelFont
    {
        public PdfExportNotEmbeddedModelFont(IPdfExportPlatformFont platformFont) : base(false)
        {
            string fontName;
            PdfFontNameTableDirectoryEntry name;
            PdfFontFile fontFile = platformFont.FontFile;
            if (fontFile != null)
            {
                name = fontFile.Name;
            }
            else
            {
                PdfFontFile local1 = fontFile;
                name = null;
            }
            PdfFontNameTableDirectoryEntry entry = name;
            string postScriptName = entry?.PostScriptName;
            string familyName = entry?.FamilyName;
            if (!string.IsNullOrEmpty(postScriptName))
            {
                fontName = postScriptName;
            }
            else if (string.IsNullOrEmpty(familyName))
            {
                fontName = platformFont.Descriptor.GetFontName();
            }
            else
            {
                fontName = familyName;
                string subFamilyName = entry?.SubFamilyName;
                if (!string.IsNullOrEmpty(subFamilyName) && !subFamilyName.Equals("regular", StringComparison.InvariantCultureIgnoreCase))
                {
                    fontName = fontName + "," + subFamilyName;
                }
            }
            this.<SimpleFont>k__BackingField = new PdfDeferredTrueTypeFont(fontName, PdfSimpleFontEncoding.CreateWinAnsiEncoding(platformFont.Descriptor.FamilyName), new PdfFontDescriptor(new PdfExportFontDescriptorBuilder(platformFont)));
        }

        public static PdfExportNotEmbeddedModelFont Create(IPdfExportPlatformFont platformFont) => 
            platformFont.Descriptor.IsSymbolFont() ? new PdfExportNotEmbeddedModelFont(platformFont) : new PdfExportNotEmbeddedCustomEncodingModelFont(platformFont);

        public override DXGlyph CreateGlyph(int index, char unicode, float width, float advance) => 
            new DXGlyph((ushort) this.MapCharacter(unicode, width), advance, DXGlyphOffset.Empty);

        public override float GetGlyphWidth(int mappedIndex) => 
            (mappedIndex >= this.SimpleFont.Widths.Length) ? 0f : ((float) this.SimpleFont.Widths[mappedIndex]);

        protected virtual short MapCharacter(char unicode, float width)
        {
            if (unicode < this.SimpleFont.Widths.Length)
            {
                this.SimpleFont.Widths[unicode] = width;
            }
            return (short) unicode;
        }

        public override PdfFont Font =>
            this.SimpleFont;

        protected PdfTrueTypeFont SimpleFont { get; }
    }
}

