namespace DevExpress.Pdf.Native
{
    using System;

    public class PdfFontCmapMixedCoverageFormatEntry : PdfFontCmapLongFormatEntry
    {
        private const int headerLength = 0x2010;
        private readonly byte[] is32;
        private readonly PdfFontCmapGroup[] groups;

        public PdfFontCmapMixedCoverageFormatEntry(PdfFontPlatformID platformId, PdfFontEncodingID encodingId, PdfBinaryStream stream) : base(platformId, encodingId, stream)
        {
            this.is32 = stream.ReadArray(0x2000);
            this.groups = PdfFontCmapGroup.ReadGroups(stream, stream.ReadInt());
        }

        public override void Write(PdfBinaryStream tableStream)
        {
            base.Write(tableStream);
            tableStream.WriteArray(this.is32);
            tableStream.WriteInt(this.groups.Length);
            PdfFontCmapGroup.WriteGroups(this.groups, tableStream);
        }

        public byte[] Is32 =>
            this.is32;

        public PdfFontCmapGroup[] Groups =>
            this.groups;

        public override int Length =>
            0x2010 + (this.groups.Length * 12);

        protected override PdfFontCmapFormatID Format =>
            PdfFontCmapFormatID.MixedCoverage;
    }
}

