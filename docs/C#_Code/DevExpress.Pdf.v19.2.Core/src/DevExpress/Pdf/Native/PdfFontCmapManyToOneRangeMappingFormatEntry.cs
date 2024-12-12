namespace DevExpress.Pdf.Native
{
    using System;

    public class PdfFontCmapManyToOneRangeMappingFormatEntry : PdfFontCmapRangeMappingFormatEntry
    {
        public PdfFontCmapManyToOneRangeMappingFormatEntry(PdfFontPlatformID platformId, PdfFontEncodingID encodingId, PdfBinaryStream stream) : base(platformId, encodingId, stream)
        {
        }

        protected override PdfFontCmapFormatID Format =>
            PdfFontCmapFormatID.ManyToOneRangeMapping;
    }
}

