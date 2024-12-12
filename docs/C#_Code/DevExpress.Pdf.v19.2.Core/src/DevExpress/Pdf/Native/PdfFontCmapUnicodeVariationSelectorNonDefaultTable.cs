namespace DevExpress.Pdf.Native
{
    using System;

    public class PdfFontCmapUnicodeVariationSelectorNonDefaultTable
    {
        private readonly int unicodeValue;
        private readonly short glyphId;

        public PdfFontCmapUnicodeVariationSelectorNonDefaultTable(int unicodeValue, short glyphId)
        {
            this.unicodeValue = unicodeValue;
            this.glyphId = glyphId;
        }

        public void Write(PdfBinaryStream tableStream)
        {
            tableStream.Write24BitInt(this.unicodeValue);
            tableStream.WriteShort(this.glyphId);
        }

        public int UnicodeValue =>
            this.unicodeValue;

        public short GlyphId =>
            this.glyphId;
    }
}

