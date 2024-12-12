namespace DevExpress.Pdf.Native
{
    using DevExpress.Pdf;
    using System;
    using System.Runtime.CompilerServices;

    public class PdfCCITTFaxDecoderParameters
    {
        public PdfCCITTFaxDecoderParameters(PdfCCITTFaxDecodeFilter filter)
        {
            this.<BlackIs1>k__BackingField = filter.BlackIs1;
            this.<EncodedByteAlign>k__BackingField = filter.EncodedByteAlign;
            this.<TwoDimensionalLineCount>k__BackingField = filter.TwoDimensionalLineCount;
            this.<Columns>k__BackingField = filter.Columns;
            this.<Rows>k__BackingField = filter.Rows;
            this.<EncodingScheme>k__BackingField = filter.EncodingScheme;
        }

        public PdfCCITTFaxDecoderParameters(bool blackIs1, bool encodedByteAlign, int twoDimensionalLineCount, int columns, int rows, PdfCCITTFaxEncodingScheme encodingScheme)
        {
            this.<BlackIs1>k__BackingField = blackIs1;
            this.<EncodedByteAlign>k__BackingField = encodedByteAlign;
            this.<TwoDimensionalLineCount>k__BackingField = twoDimensionalLineCount;
            this.<Columns>k__BackingField = columns;
            this.<Rows>k__BackingField = rows;
            this.<EncodingScheme>k__BackingField = encodingScheme;
        }

        public bool BlackIs1 { get; }

        public bool EncodedByteAlign { get; }

        public int TwoDimensionalLineCount { get; }

        public int Columns { get; }

        public int Rows { get; }

        public PdfCCITTFaxEncodingScheme EncodingScheme { get; }
    }
}

