namespace DevExpress.Pdf.Native
{
    using System;

    public class PdfFontCmapByteEncodingFormatEntry : PdfFontCmapShortFormatEntry
    {
        private readonly byte[] glyphIdArray;

        public PdfFontCmapByteEncodingFormatEntry(PdfFontPlatformID platformId, PdfFontEncodingID encodingId, PdfBinaryStream stream) : base(platformId, encodingId, stream)
        {
            this.glyphIdArray = stream.ReadArray(base.BodyLength);
        }

        public override int MapCode(char character) => 
            (character >= this.glyphIdArray.Length) ? 0 : this.glyphIdArray[character];

        public override void Write(PdfBinaryStream tableStream)
        {
            base.Write(tableStream);
            tableStream.WriteArray(this.glyphIdArray);
        }

        public byte[] GlyphIdArray =>
            this.glyphIdArray;

        protected override PdfFontCmapFormatID Format =>
            PdfFontCmapFormatID.ByteEncoding;
    }
}

