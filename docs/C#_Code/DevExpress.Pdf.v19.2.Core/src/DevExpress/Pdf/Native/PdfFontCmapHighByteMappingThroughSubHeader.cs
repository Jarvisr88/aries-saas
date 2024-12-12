namespace DevExpress.Pdf.Native
{
    using System;

    public class PdfFontCmapHighByteMappingThroughSubHeader
    {
        private readonly short firstCode;
        private readonly short entryCount;
        private readonly short idDelta;
        private readonly short idRangeOffset;
        private readonly int glyphOffset;

        public PdfFontCmapHighByteMappingThroughSubHeader(short firstCode, short entryCount, short idDelta, short idRangeOffset, int glyphOffset)
        {
            this.firstCode = firstCode;
            this.entryCount = entryCount;
            this.idDelta = idDelta;
            this.idRangeOffset = idRangeOffset;
            this.glyphOffset = glyphOffset;
        }

        public int CalcGlyphIndexArraySize(int offset) => 
            ((this.idRangeOffset + (this.entryCount * 2)) - offset) / 2;

        public short FirstCode =>
            this.firstCode;

        public short EntryCount =>
            this.entryCount;

        public short IdDelta =>
            this.idDelta;

        public short IdRangeOffset =>
            this.idRangeOffset;

        public int GlyphOffset =>
            this.glyphOffset;
    }
}

