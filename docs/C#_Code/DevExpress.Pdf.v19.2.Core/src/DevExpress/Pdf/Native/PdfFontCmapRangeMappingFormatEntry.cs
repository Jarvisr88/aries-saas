namespace DevExpress.Pdf.Native
{
    using System;

    public abstract class PdfFontCmapRangeMappingFormatEntry : PdfFontCmapLongFormatEntry
    {
        private const int headerLength = 0x10;
        private readonly PdfFontCmapGroup[] groups;

        protected PdfFontCmapRangeMappingFormatEntry(PdfFontPlatformID platformId, PdfFontEncodingID encodingId, PdfBinaryStream stream) : base(platformId, encodingId, stream)
        {
            this.groups = PdfFontCmapGroup.ReadGroups(stream, stream.ReadInt());
        }

        public override void Write(PdfBinaryStream tableStream)
        {
            base.Write(tableStream);
            tableStream.WriteInt(this.groups.Length);
            PdfFontCmapGroup.WriteGroups(this.groups, tableStream);
        }

        public PdfFontCmapGroup[] Groups =>
            this.groups;

        public override int Length =>
            0x10 + (this.groups.Length * 12);
    }
}

