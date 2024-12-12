namespace DevExpress.Pdf.Native
{
    using System;

    public abstract class PdfFontCmapLongFormatEntry : PdfFontCmapFormatEntry
    {
        private readonly int language;

        protected PdfFontCmapLongFormatEntry(PdfFontPlatformID platformId, PdfFontEncodingID encodingId, PdfBinaryStream stream) : base(platformId, encodingId)
        {
            stream.ReadShort();
            stream.ReadInt();
            this.language = stream.ReadInt();
        }

        public override void Write(PdfBinaryStream tableStream)
        {
            base.Write(tableStream);
            tableStream.WriteShort(0);
            tableStream.WriteInt(this.Length);
            tableStream.WriteInt(this.Language);
        }

        public int Language =>
            this.language;
    }
}

