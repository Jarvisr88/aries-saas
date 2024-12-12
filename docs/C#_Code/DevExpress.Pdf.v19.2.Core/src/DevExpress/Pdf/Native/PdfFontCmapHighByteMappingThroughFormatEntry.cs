namespace DevExpress.Pdf.Native
{
    using System;
    using System.Collections.Generic;

    public class PdfFontCmapHighByteMappingThroughFormatEntry : PdfFontCmapShortFormatEntry
    {
        private const int subHeaderKeysLength = 0x200;
        private const int subHeaderLength = 8;
        private readonly short[] subHeaderKeys;
        private readonly PdfFontCmapHighByteMappingThroughSubHeader[] subHeaders;
        private readonly short[] glyphIndexArray;

        public PdfFontCmapHighByteMappingThroughFormatEntry(PdfFontPlatformID platformId, PdfFontEncodingID encodingId, PdfBinaryStream stream) : base(platformId, encodingId, stream)
        {
            this.subHeaderKeys = stream.ReadShortArray(0x100);
            int count = new HashSet<short>(this.subHeaderKeys).Count;
            this.subHeaders = new PdfFontCmapHighByteMappingThroughSubHeader[count];
            int endOfSubheadersPosition = ((int) stream.Position) + (count * 8);
            int offset = (count * 8) - 6;
            PdfFontCmapHighByteMappingThroughSubHeader header = this.ReadSubHeader(stream, endOfSubheadersPosition);
            int num4 = header.CalcGlyphIndexArraySize(offset);
            this.subHeaders[0] = header;
            offset -= 8;
            int index = 1;
            while (index < count)
            {
                PdfFontCmapHighByteMappingThroughSubHeader header2 = this.ReadSubHeader(stream, endOfSubheadersPosition);
                this.subHeaders[index] = header2;
                num4 = Math.Max(num4, header2.CalcGlyphIndexArraySize(offset));
                index++;
                offset -= 8;
            }
            this.glyphIndexArray = stream.ReadShortArray(num4);
        }

        public override int MapCode(char character)
        {
            byte index = (byte) (character >> 8);
            PdfFontCmapHighByteMappingThroughSubHeader header = this.subHeaders[this.subHeaderKeys[index] / 8];
            int num3 = ((byte) (character & '\x00ff')) - header.FirstCode;
            if ((num3 < 0) || (num3 >= header.EntryCount))
            {
                return 0;
            }
            num3 += header.GlyphOffset;
            if ((num3 < 0) || (num3 >= this.glyphIndexArray.Length))
            {
                return 0;
            }
            int num4 = (ushort) this.glyphIndexArray[num3];
            return ((num4 == 0) ? num4 : ((num4 + header.IdDelta) % 0x10000));
        }

        private PdfFontCmapHighByteMappingThroughSubHeader ReadSubHeader(PdfBinaryStream stream, int endOfSubheadersPosition)
        {
            int position = (int) stream.Position;
            short idRangeOffset = stream.ReadShort();
            return new PdfFontCmapHighByteMappingThroughSubHeader(stream.ReadShort(), stream.ReadShort(), stream.ReadShort(), idRangeOffset, (idRangeOffset - (endOfSubheadersPosition - position)) / 2);
        }

        public override void Write(PdfBinaryStream tableStream)
        {
            base.Write(tableStream);
            tableStream.WriteShortArray(this.subHeaderKeys);
            foreach (PdfFontCmapHighByteMappingThroughSubHeader header in this.subHeaders)
            {
                tableStream.WriteShort(header.FirstCode);
                tableStream.WriteShort(header.EntryCount);
                tableStream.WriteShort(header.IdDelta);
                tableStream.WriteShort(header.IdRangeOffset);
            }
            tableStream.WriteShortArray(this.glyphIndexArray);
        }

        public short[] SubHeaderKeys =>
            this.subHeaderKeys;

        public PdfFontCmapHighByteMappingThroughSubHeader[] SubHeaders =>
            this.subHeaders;

        public short[] GlyphIndexArray =>
            this.glyphIndexArray;

        public override int Length =>
            (0x206 + (8 * this.subHeaders.Length)) + (this.glyphIndexArray.Length * 2);

        protected override PdfFontCmapFormatID Format =>
            PdfFontCmapFormatID.HighByteMappingThrough;
    }
}

