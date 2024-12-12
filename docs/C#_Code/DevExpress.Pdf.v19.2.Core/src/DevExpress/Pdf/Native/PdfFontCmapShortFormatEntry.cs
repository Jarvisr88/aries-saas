namespace DevExpress.Pdf.Native
{
    using System;

    public abstract class PdfFontCmapShortFormatEntry : PdfFontCmapFormatEntry
    {
        protected const int HeaderLength = 6;
        private readonly int bodyLength;
        private readonly short language;

        protected PdfFontCmapShortFormatEntry(PdfFontPlatformID platformId, PdfFontEncodingID encodingId, PdfBinaryStream stream) : base(platformId, encodingId)
        {
            this.bodyLength = Math.Max(stream.ReadUshort() - 6, 0);
            this.language = stream.ReadShort();
        }

        protected PdfFontCmapShortFormatEntry(PdfFontPlatformID platformId, PdfFontEncodingID encodingId, short language) : base(platformId, encodingId)
        {
        }

        public override void Write(PdfBinaryStream tableStream)
        {
            base.Write(tableStream);
            tableStream.WriteShort((short) this.Length);
            tableStream.WriteShort(this.language);
        }

        protected int BodyLength =>
            this.bodyLength;

        public short Language =>
            this.language;

        public override int Length =>
            6 + this.bodyLength;
    }
}

