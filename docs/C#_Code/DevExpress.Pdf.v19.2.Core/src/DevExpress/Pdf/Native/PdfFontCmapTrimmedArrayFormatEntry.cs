namespace DevExpress.Pdf.Native
{
    using System;

    public class PdfFontCmapTrimmedArrayFormatEntry : PdfFontCmapLongFormatEntry
    {
        private const int headerLength = 20;
        private readonly int characterCount;
        private readonly short[] glyphs;
        private readonly int startCharacterCode;

        public PdfFontCmapTrimmedArrayFormatEntry(PdfFontPlatformID platformId, PdfFontEncodingID encodingId, PdfBinaryStream stream) : base(platformId, encodingId, stream)
        {
            this.startCharacterCode = stream.ReadInt();
            this.characterCount = stream.ReadInt();
            this.glyphs = stream.ReadShortArray(this.characterCount);
        }

        public override void Write(PdfBinaryStream tableStream)
        {
            base.Write(tableStream);
            tableStream.WriteInt(this.startCharacterCode);
            tableStream.WriteInt(this.characterCount);
            tableStream.WriteShortArray(this.glyphs);
        }

        public int CharacterCount =>
            this.characterCount;

        public short[] Glyphs =>
            this.glyphs;

        public override int Length =>
            20 + (this.glyphs.Length * 2);

        public int StartCharacterCode =>
            this.startCharacterCode;

        protected override PdfFontCmapFormatID Format =>
            PdfFontCmapFormatID.TrimmedArray;
    }
}

