namespace DevExpress.Pdf.Native
{
    using System;

    public abstract class PdfFontCmapFormatEntry
    {
        public const int NotdefGlyphIndex = 0;
        internal const ushort SymbolicEncodingMicrosoftOffset = 0xf000;
        private readonly PdfFontPlatformID platformId;
        private readonly PdfFontEncodingID encodingId;

        protected PdfFontCmapFormatEntry(PdfFontPlatformID platformId, PdfFontEncodingID encodingId)
        {
            this.platformId = platformId;
            this.encodingId = encodingId;
        }

        public static PdfFontCmapFormatEntry CreateEntry(PdfFontPlatformID platformId, PdfFontEncodingID encodingId, PdfFontCmapFormatID format, PdfBinaryStream stream)
        {
            switch (format)
            {
                case PdfFontCmapFormatID.ByteEncoding:
                    return new PdfFontCmapByteEncodingFormatEntry(platformId, encodingId, stream);

                case PdfFontCmapFormatID.HighByteMappingThrough:
                    return new PdfFontCmapHighByteMappingThroughFormatEntry(platformId, encodingId, stream);

                case PdfFontCmapFormatID.SegmentMapping:
                    return new PdfFontCmapSegmentMappingFormatEntry(platformId, encodingId, stream);

                case PdfFontCmapFormatID.TrimmedMapping:
                    return new PdfFontCmapTrimmedMappingFormatEntry(platformId, encodingId, stream);

                case PdfFontCmapFormatID.MixedCoverage:
                    return new PdfFontCmapMixedCoverageFormatEntry(platformId, encodingId, stream);

                case PdfFontCmapFormatID.TrimmedArray:
                    return new PdfFontCmapTrimmedArrayFormatEntry(platformId, encodingId, stream);

                case PdfFontCmapFormatID.SegmentedCoverage:
                    return new PdfFontCmapSegmentedCoverageFormatEntry(platformId, encodingId, stream);

                case PdfFontCmapFormatID.ManyToOneRangeMapping:
                    return new PdfFontCmapManyToOneRangeMappingFormatEntry(platformId, encodingId, stream);

                case PdfFontCmapFormatID.UnicodeVariationSequences:
                    return new PdfFontCmapUnicodeVariationSequencesFormatEntry(platformId, encodingId, stream);
            }
            PdfDocumentStructureReader.ThrowIncorrectDataException();
            return null;
        }

        public virtual int MapCode(char character) => 
            character;

        public virtual void Write(PdfBinaryStream tableStream)
        {
            tableStream.WriteShort((short) this.Format);
        }

        public PdfFontPlatformID PlatformId =>
            this.platformId;

        public PdfFontEncodingID EncodingId =>
            this.encodingId;

        public abstract int Length { get; }

        protected abstract PdfFontCmapFormatID Format { get; }

        protected bool IsSymbolEncoding =>
            (this.PlatformId == PdfFontPlatformID.Microsoft) && (this.EncodingId == PdfFontEncodingID.Symbol);
    }
}

