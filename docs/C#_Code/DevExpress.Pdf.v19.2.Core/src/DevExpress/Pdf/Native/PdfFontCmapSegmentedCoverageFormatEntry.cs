namespace DevExpress.Pdf.Native
{
    using System;

    public class PdfFontCmapSegmentedCoverageFormatEntry : PdfFontCmapRangeMappingFormatEntry
    {
        public PdfFontCmapSegmentedCoverageFormatEntry(PdfFontPlatformID platformId, PdfFontEncodingID encodingId, PdfBinaryStream stream) : base(platformId, encodingId, stream)
        {
        }

        protected override PdfFontCmapFormatID Format =>
            PdfFontCmapFormatID.SegmentedCoverage;
    }
}

