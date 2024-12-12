namespace DevExpress.Text.Fonts
{
    using DevExpress.Pdf;
    using DevExpress.Pdf.ContentGeneration;
    using DevExpress.Pdf.Native;
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Runtime.CompilerServices;

    public class DXFont : IDisposable, IPdfExportPlatformFontProvider
    {
        private readonly DXFontFace fontFace;

        public DXFont(DXFontFace fontFace, float sizeInPoints)
        {
            this.fontFace = fontFace;
            this.<SizeInPoints>k__BackingField = sizeInPoints;
        }

        public DXCTLShaper CreateShaper() => 
            this.fontFace.CreateShaper();

        IPdfExportPlatformFont IPdfExportPlatformFontProvider.GetPlatformFont() => 
            new ExportPlatformFont(this.fontFace, new DXFontDescriptor(this.FamilyName, this.Weight, this.Style, this.Stretch), this.GetFontFile());

        public void Dispose()
        {
            this.fontFace.Dispose();
        }

        public IList<DXCharRange> GetFontCharacterRanges() => 
            this.fontFace.GetFontCharacterRanges();

        private PdfFontFile GetFontFile()
        {
            DXFontFileInfo fontFile = this.fontFace.GetFontFile();
            using (PdfBinaryStream stream = new PdfBinaryStream(fontFile.Data))
            {
                long num = 0L;
                if (0x74746366 == stream.ReadInt())
                {
                    stream.ReadInt();
                    long num2 = (long) ((ulong) stream.ReadInt());
                    int index = fontFile.Index;
                    if (index >= num2)
                    {
                        throw new Exception();
                    }
                    stream.Position += index * 4;
                    num = (long) ((ulong) stream.ReadInt());
                }
                stream.Position = num;
                return new PdfFontFile(stream);
            }
        }

        public DXShapedGlyphInfo GetShapedGlyphsInfo(string text) => 
            this.fontFace.GetShapedGlyphsInfo(text);

        public float MeasureCharacterWidth(char character) => 
            this.fontFace.MeasureCharacterWidth(character, this.SizeInPoints);

        public DXSizeF MeasureString(string text) => 
            this.fontFace.MeasureString(text, this.SizeInPoints);

        public string FamilyName =>
            this.fontFace.FamilyName;

        public DXFontWeight Weight =>
            this.fontFace.Weight;

        public DXFontStretch Stretch =>
            this.fontFace.Stretch;

        public DXFontStyle Style =>
            this.fontFace.Style;

        public DXFontSimulations Simulations =>
            this.fontFace.Simulations;

        public byte[] Panose =>
            this.fontFace.Panose;

        public DXFontMetrics Metrics =>
            this.fontFace.Metrics;

        public float SizeInPoints { get; }

        object IPdfExportPlatformFontProvider.Key =>
            new DXFontDescriptor(this.FamilyName, this.Weight, this.Style, this.Stretch);

        private class ExportPlatformFont : IPdfExportPlatformFont
        {
            private readonly DXFontFace fontFace;

            public ExportPlatformFont(DXFontFace fontFace, DXFontDescriptor descriptor, PdfFontFile fontFile)
            {
                this.<Descriptor>k__BackingField = descriptor;
                this.<FontFile>k__BackingField = fontFile;
                this.fontFace = fontFace;
            }

            public DXCTLShaper CreateCTLShaper() => 
                this.fontFace.CreateShaper();

            public Font CreateGDIPlusFont(float fontSize)
            {
                FontStyle regular = FontStyle.Regular;
                if (this.IsBold)
                {
                    regular |= FontStyle.Bold;
                }
                if (this.IsItalic)
                {
                    regular |= FontStyle.Italic;
                }
                return new Font(this.fontFace.FamilyName, fontSize, regular);
            }

            public bool ShouldEmbed(PdfCreationOptions creationOptions) => 
                creationOptions.EmbedFont(this.fontFace.FamilyName, this.IsBold, this.IsItalic);

            public DXFontDescriptor Descriptor { get; }

            public PdfFontFile FontFile { get; }

            public DXFontSimulations Simulations =>
                this.fontFace.Simulations;

            public PdfFontMetrics Metrics =>
                this.fontFace.Metrics.ToPdfMetrics();

            private bool IsItalic =>
                this.fontFace.Weight >= DXFontWeight.Bold;

            private bool IsBold =>
                this.fontFace.Weight >= DXFontWeight.Bold;
        }
    }
}

