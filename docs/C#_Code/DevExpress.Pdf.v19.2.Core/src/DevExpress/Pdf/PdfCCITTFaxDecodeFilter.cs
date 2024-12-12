namespace DevExpress.Pdf
{
    using DevExpress.Pdf.ContentGeneration.TiffParsing;
    using DevExpress.Pdf.Native;
    using System;

    public class PdfCCITTFaxDecodeFilter : PdfFilter
    {
        internal const string Name = "CCITTFaxDecode";
        internal const string ShortName = "CCF";
        private const int defaultColumns = 0x6c0;
        private const string encodingSchemeDictionaryKey = "K";
        private const string endOfLineDictionaryKey = "EndOfLine";
        private const string encodedByteAlignDictionaryKey = "EncodedByteAlign";
        private const string columnsDictionaryKey = "Columns";
        private const string rowsDictionaryKey = "Rows";
        private const string endOfBlockDictionaryKey = "EndOfBlock";
        private const string blackIs1DictionaryKey = "BlackIs1";
        private const string damagedRowsBeforeErrorDictionaryKey = "DamagedRowsBeforeError";
        private readonly PdfCCITTFaxEncodingScheme encodingScheme;
        private readonly int twoDimensionalLineCount;
        private readonly bool endOfLine;
        private readonly bool encodedByteAlign;
        private readonly int columns;
        private readonly int rows;
        private readonly bool endOfBlock;
        private readonly bool blackIs1;
        private readonly int damagedRowsBeforeError;

        internal PdfCCITTFaxDecodeFilter(CCITTFilterParameters parameters)
        {
            this.columns = 0x6c0;
            this.endOfBlock = true;
            this.encodingScheme = parameters.EncodingScheme;
            this.twoDimensionalLineCount = parameters.TwoDimensionalLineCount;
            this.endOfLine = parameters.EndOfLine;
            this.encodedByteAlign = parameters.EncodedByteAlign;
            this.columns = parameters.Columns;
            this.rows = parameters.Rows;
            this.endOfBlock = true;
            this.blackIs1 = parameters.BlackIs1;
        }

        internal PdfCCITTFaxDecodeFilter(PdfReaderDictionary parameters)
        {
            this.columns = 0x6c0;
            this.endOfBlock = true;
            if (parameters == null)
            {
                this.encodingScheme = PdfCCITTFaxEncodingScheme.OneDimensional;
            }
            else
            {
                int? integer = parameters.GetInteger("K");
                int num = (integer != null) ? integer.GetValueOrDefault() : 0;
                if (num < 0)
                {
                    this.encodingScheme = PdfCCITTFaxEncodingScheme.TwoDimensional;
                }
                else if (num == 0)
                {
                    this.encodingScheme = PdfCCITTFaxEncodingScheme.OneDimensional;
                }
                else
                {
                    this.encodingScheme = PdfCCITTFaxEncodingScheme.Mixed;
                    this.twoDimensionalLineCount = num - 1;
                }
                bool? boolean = parameters.GetBoolean("EndOfLine");
                this.endOfLine = (boolean != null) ? boolean.GetValueOrDefault() : false;
                boolean = parameters.GetBoolean("EncodedByteAlign");
                this.encodedByteAlign = (boolean != null) ? boolean.GetValueOrDefault() : false;
                integer = parameters.GetInteger("Columns");
                this.columns = (integer != null) ? integer.GetValueOrDefault() : 0x6c0;
                integer = parameters.GetInteger("Rows");
                this.rows = (integer != null) ? integer.GetValueOrDefault() : 0;
                boolean = parameters.GetBoolean("EndOfBlock");
                this.endOfBlock = (boolean != null) ? boolean.GetValueOrDefault() : true;
                boolean = parameters.GetBoolean("BlackIs1");
                this.blackIs1 = (boolean != null) ? boolean.GetValueOrDefault() : false;
                integer = parameters.GetInteger("DamagedRowsBeforeError");
                this.damagedRowsBeforeError = (integer != null) ? integer.GetValueOrDefault() : 0;
                if ((this.columns <= 0) || ((this.rows < 0) || (this.damagedRowsBeforeError < 0)))
                {
                    PdfDocumentStructureReader.ThrowIncorrectDataException();
                }
            }
        }

        protected internal override byte[] Decode(byte[] data) => 
            PdfCCITTFaxDecoder.Decode(this, data);

        protected internal override PdfWriterDictionary Write(PdfObjectCollection objects)
        {
            PdfWriterDictionary dictionary = new PdfWriterDictionary(objects);
            PdfCCITTFaxEncodingScheme encodingScheme = this.encodingScheme;
            if (encodingScheme == PdfCCITTFaxEncodingScheme.TwoDimensional)
            {
                dictionary.Add("K", -1);
            }
            else if (encodingScheme == PdfCCITTFaxEncodingScheme.Mixed)
            {
                dictionary.Add("K", this.twoDimensionalLineCount + 1);
            }
            dictionary.Add("EndOfLine", this.endOfLine, false);
            dictionary.Add("EncodedByteAlign", this.encodedByteAlign, false);
            dictionary.Add("Columns", this.columns, 0x6c0);
            dictionary.Add("Rows", this.rows, 0);
            dictionary.Add("EndOfBlock", this.endOfBlock, true);
            dictionary.Add("BlackIs1", this.blackIs1, false);
            dictionary.Add("DamagedRowsBeforeError", this.damagedRowsBeforeError, 0);
            return ((dictionary.Count == 0) ? null : dictionary);
        }

        public PdfCCITTFaxEncodingScheme EncodingScheme =>
            this.encodingScheme;

        public int TwoDimensionalLineCount =>
            this.twoDimensionalLineCount;

        public bool EndOfLine =>
            this.endOfLine;

        public bool EncodedByteAlign =>
            this.encodedByteAlign;

        public int Columns =>
            this.columns;

        public int Rows =>
            this.rows;

        public bool EndOfBlock =>
            this.endOfBlock;

        public bool BlackIs1 =>
            this.blackIs1;

        public int DamagedRowsBeforeError =>
            this.damagedRowsBeforeError;

        protected internal override string FilterName =>
            "CCITTFaxDecode";
    }
}

