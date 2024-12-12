namespace DevExpress.Pdf.ContentGeneration
{
    using DevExpress.Pdf;
    using DevExpress.Pdf.ContentGeneration.Extensions;
    using DevExpress.Pdf.Native;
    using DevExpress.Text.Fonts;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Text;

    public abstract class PdfExportFont : DXShaper
    {
        private readonly Lazy<PdfFont> appearanceFont;
        private readonly DXFontDescriptor descriptor;

        protected PdfExportFont(IPdfExportPlatformFont platformFont, PdfExportModelFont modelFont)
        {
            this.descriptor = platformFont.Descriptor;
            this.appearanceFont = new Lazy<PdfFont>(new Func<PdfFont>(this.CreateAppearanceFont));
            this.<Metrics>k__BackingField = platformFont.Metrics;
            this.<Simulations>k__BackingField = platformFont.Simulations;
            this.<ModelFont>k__BackingField = modelFont;
        }

        public void AddGlyphs(IDictionary<int, PdfExportFontGlyphInfo> glyphs)
        {
            this.ModelFont.AddGlyphs(glyphs);
        }

        private PdfFont CreateAppearanceFont()
        {
            Encoding unicode = Encoding.Unicode;
            Encoding ansiEncoding = EncodingHelpers.AnsiEncoding;
            double[] widths = new double[0xe0];
            byte num = 0x20;
            for (int i = 0; i < 0xe0; i++)
            {
                byte[] bytes = new byte[] { num };
                double characterWidth = this.GetCharacterWidth(unicode.GetChars(Encoding.Convert(ansiEncoding, unicode, bytes))[0]);
                widths[i] = (characterWidth <= 0.0) ? 500.0 : characterWidth;
                num = (byte) (num + 1);
            }
            PdfFontDescriptor fontDescriptor = this.Font.FontDescriptor;
            PdfFontDescriptorData descriptorBuilder = new PdfFontDescriptorData();
            descriptorBuilder.FontFamily = this.descriptor.FamilyName;
            descriptorBuilder.Ascent = fontDescriptor.Ascent;
            descriptorBuilder.Descent = fontDescriptor.Descent;
            descriptorBuilder.Bold = this.descriptor.IsBold();
            descriptorBuilder.BBox = fontDescriptor.FontBBox;
            descriptorBuilder.CapHeight = fontDescriptor.CapHeight;
            descriptorBuilder.ItalicAngle = fontDescriptor.ItalicAngle;
            descriptorBuilder.NumGlyphs = 0xe0;
            descriptorBuilder.Flags = fontDescriptor.Flags;
            descriptorBuilder.StemH = fontDescriptor.StemH;
            descriptorBuilder.StemV = fontDescriptor.StemV;
            descriptorBuilder.XHeight = fontDescriptor.XHeight;
            return new PdfTrueTypeFont(this.PostScriptFontName, PdfSimpleFontEncoding.CreateWinAnsiEncoding(this.descriptor.FamilyName), new PdfFontDescriptor(descriptorBuilder), 0x20, widths);
        }

        protected abstract double GetCharacterWidth(char ch);
        public float GetGlyphWidth(int glyphCode) => 
            this.ModelFont.GetGlyphWidth(glyphCode);

        public void UpdateFont()
        {
            this.ModelFont.UpdateFont();
        }

        protected PdfExportModelFont ModelFont { get; }

        public PdfFont Font =>
            this.ModelFont.Font;

        public bool UseTwoByteCodePoints =>
            this.ModelFont.UseTwoByteCodePoints;

        public PdfFontMetrics Metrics { get; }

        public DXFontSimulations Simulations { get; }

        public PdfFont AppearanceFont =>
            this.appearanceFont.Value;

        protected virtual string PostScriptFontName =>
            this.descriptor.GetFontName();

        public string FontFamily =>
            this.descriptor.FamilyName;

        public bool Bold =>
            this.descriptor.IsBold();

        public bool Italic =>
            this.descriptor.IsItalic();
    }
}

