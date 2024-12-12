namespace DevExpress.Pdf
{
    using DevExpress.Pdf.Native;
    using System;

    public abstract class PdfFlateLZWDecodeFilter : PdfFilter
    {
        private const PdfFlateLZWFilterPredictor defaultPredictor = PdfFlateLZWFilterPredictor.NoPrediction;
        private const int defaultColors = 1;
        private const int defaultBitsPerComponent = 8;
        private const int defaultColumns = 1;
        private const string predictorDictionaryKey = "Predictor";
        private const string colorsDictionaryKey = "Colors";
        private const string bitsPerComponentDictionaryKey = "BitsPerComponent";
        private const string columnsDictionaryKey = "Columns";
        private readonly PdfFlateLZWFilterPredictor predictor;
        private readonly int colors;
        private readonly int bitsPerComponent;
        private readonly int columns;

        protected PdfFlateLZWDecodeFilter(PdfReaderDictionary parameters)
        {
            if (parameters == null)
            {
                this.predictor = PdfFlateLZWFilterPredictor.NoPrediction;
                this.colors = 1;
                this.bitsPerComponent = 8;
                this.columns = 1;
            }
            else
            {
                int? integer = parameters.GetInteger("Predictor");
                this.predictor = (integer != null) ? ((PdfFlateLZWFilterPredictor) integer.Value) : PdfFlateLZWFilterPredictor.NoPrediction;
                int? nullable2 = parameters.GetInteger("Colors");
                this.colors = (nullable2 != null) ? nullable2.GetValueOrDefault() : 1;
                nullable2 = parameters.GetInteger("BitsPerComponent");
                this.bitsPerComponent = (nullable2 != null) ? nullable2.GetValueOrDefault() : 8;
                nullable2 = parameters.GetInteger("Columns");
                this.columns = (nullable2 != null) ? nullable2.GetValueOrDefault() : 1;
                bool flag = false;
                foreach (object obj2 in Enum.GetValues(typeof(PdfFlateLZWFilterPredictor)))
                {
                    if (obj2.Equals(this.predictor))
                    {
                        flag = true;
                        break;
                    }
                }
                if ((!flag || ((this.colors < 1) || ((this.bitsPerComponent != 1) && ((this.bitsPerComponent != 2) && ((this.bitsPerComponent != 4) && ((this.bitsPerComponent != 8) && (this.bitsPerComponent != 0x10))))))) || (this.columns < 1))
                {
                    PdfDocumentStructureReader.ThrowIncorrectDataException();
                }
            }
        }

        protected PdfFlateLZWDecodeFilter(PdfFlateLZWFilterPredictor predictor, int colors, int bitsPerComponent, int columns)
        {
            this.predictor = predictor;
            this.colors = colors;
            this.bitsPerComponent = bitsPerComponent;
            this.columns = columns;
        }

        protected internal override byte[] Decode(byte[] data)
        {
            byte[] buffer = this.PerformDecode(data);
            return ((this.predictor == PdfFlateLZWFilterPredictor.NoPrediction) ? buffer : PdfFlateLZWDecodeFilterPredictor.Decode(buffer, this));
        }

        protected abstract byte[] PerformDecode(byte[] data);
        protected internal override PdfWriterDictionary Write(PdfObjectCollection objects)
        {
            PdfWriterDictionary dictionary = new PdfWriterDictionary(objects);
            dictionary.Add("Predictor", (int) this.predictor, 1);
            dictionary.Add("Colors", this.colors, 1);
            dictionary.Add("BitsPerComponent", this.bitsPerComponent, 8);
            dictionary.Add("Columns", this.columns, 1);
            return ((dictionary.Count == 0) ? null : dictionary);
        }

        public PdfFlateLZWFilterPredictor Predictor =>
            this.predictor;

        public int Colors =>
            this.colors;

        public int BitsPerComponent =>
            this.bitsPerComponent;

        public int Columns =>
            this.columns;
    }
}

