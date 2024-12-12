namespace DevExpress.Pdf
{
    using DevExpress.Pdf.Native;
    using DevExpress.Utils;
    using System;

    public class PdfLZWDecodeFilter : PdfFlateLZWDecodeFilter
    {
        internal const string Name = "LZWDecode";
        internal const string ShortName = "LZW";
        private const int defaultEarlyChange = 1;
        private const string earlyChangeDictionaryKey = "EarlyChange";
        private readonly bool earlyChange;

        internal PdfLZWDecodeFilter(PdfReaderDictionary parameters) : base(parameters)
        {
            this.earlyChange = true;
            if (parameters != null)
            {
                int? integer = parameters.GetInteger("EarlyChange");
                if (integer != null)
                {
                    int num = integer.Value;
                    if (num == 0)
                    {
                        this.earlyChange = false;
                    }
                    else if (num != 1)
                    {
                        PdfDocumentStructureReader.ThrowIncorrectDataException();
                    }
                }
            }
        }

        internal PdfLZWDecodeFilter(bool earlyChange, PdfFlateLZWFilterPredictor predictor, int colors, int bitsPerComponent, int columns) : base(predictor, colors, bitsPerComponent, columns)
        {
            this.earlyChange = true;
            this.earlyChange = earlyChange;
        }

        protected override byte[] PerformDecode(byte[] data) => 
            new LZWDecoder(data, 9, this.earlyChange).Decode();

        protected internal override PdfWriterDictionary Write(PdfObjectCollection objects)
        {
            PdfWriterDictionary dictionary = base.Write(objects);
            if (!this.earlyChange)
            {
                dictionary = new PdfWriterDictionary(objects);
                dictionary.Add("EarlyChange", 0);
            }
            return dictionary;
        }

        public bool EarlyChange =>
            this.earlyChange;

        protected internal override string FilterName =>
            "LZWDecode";
    }
}

