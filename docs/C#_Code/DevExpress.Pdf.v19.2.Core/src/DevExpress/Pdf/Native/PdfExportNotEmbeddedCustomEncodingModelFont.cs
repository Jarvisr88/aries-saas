namespace DevExpress.Pdf.Native
{
    using DevExpress.Pdf.ContentGeneration;
    using System;
    using System.Collections.Generic;

    public class PdfExportNotEmbeddedCustomEncodingModelFont : PdfExportNotEmbeddedModelFont
    {
        private const byte notdefCode = 0;
        private const double notdefWidth = 500.0;
        private const string uniPrefix = "uni";
        private readonly IDictionary<string, int> mappingCache;
        private int lastGlyphIndex;

        public PdfExportNotEmbeddedCustomEncodingModelFont(IPdfExportPlatformFont platformFont) : base(platformFont)
        {
            this.mappingCache = new Dictionary<string, int>();
            this.mappingCache.Add(".notdef", 0);
            this.AddGlyphToEncoding(".notdef", 0, 500.0);
        }

        private void AddGlyphToEncoding(string glyphName, byte glyphIndex, double width)
        {
            base.SimpleFont.Encoding.Differences.Add(glyphIndex, glyphName);
            base.SimpleFont.Widths[glyphIndex] = width;
        }

        public override float GetGlyphWidth(int mappedIndex) => 
            (mappedIndex >= base.SimpleFont.Widths.Length) ? 0f : ((float) base.SimpleFont.Widths[mappedIndex]);

        protected override short MapCharacter(char unicode, float width)
        {
            int lastGlyphIndex;
            string glyphName = PdfUnicodeConverter.GetGlyphName((short) unicode);
            if (glyphName == ".notdef")
            {
                glyphName = "uni" + ((ushort) unicode).ToString("X4");
            }
            if (!this.mappingCache.TryGetValue(glyphName, out lastGlyphIndex))
            {
                if (this.lastGlyphIndex >= 0xff)
                {
                    lastGlyphIndex = 0;
                }
                else
                {
                    int num3 = this.lastGlyphIndex + 1;
                    this.lastGlyphIndex = num3;
                    this.AddGlyphToEncoding(glyphName, (byte) num3, (double) width);
                    lastGlyphIndex = this.lastGlyphIndex;
                }
                this.mappingCache.Add(glyphName, lastGlyphIndex);
            }
            return (short) lastGlyphIndex;
        }
    }
}

