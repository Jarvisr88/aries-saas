namespace DevExpress.Pdf.Native
{
    using System;

    public class PdfFontCmapTrimmedMappingFormatEntry : PdfFontCmapShortFormatEntry
    {
        private readonly ushort firstCode;
        private readonly short entryCount;
        private readonly short[] glyphIdArray;

        public PdfFontCmapTrimmedMappingFormatEntry(PdfFontPlatformID platformId, PdfFontEncodingID encodingId, PdfBinaryStream stream) : base(platformId, encodingId, stream)
        {
            this.firstCode = (ushort) stream.ReadUshort();
            this.entryCount = stream.ReadShort();
            this.glyphIdArray = stream.ReadShortArray(this.entryCount);
        }

        public override int MapCode(char character)
        {
            ushort num = character;
            if (base.IsSymbolEncoding && (this.firstCode >= 0xf000))
            {
                num = (ushort) (num + 0xf000);
            }
            int index = num - this.firstCode;
            return (((index < 0) || (index >= this.entryCount)) ? 0 : ((ushort) this.glyphIdArray[index]));
        }

        public override void Write(PdfBinaryStream tableStream)
        {
            base.Write(tableStream);
            tableStream.WriteShort((short) this.firstCode);
            tableStream.WriteShort(this.entryCount);
            tableStream.WriteShortArray(this.glyphIdArray);
        }

        public short FirstCode =>
            (short) this.firstCode;

        public short EntryCount =>
            this.entryCount;

        public short[] GlyphIdArray =>
            this.glyphIdArray;

        public override int Length =>
            10 + (this.glyphIdArray.Length * 2);

        protected override PdfFontCmapFormatID Format =>
            PdfFontCmapFormatID.TrimmedMapping;
    }
}

