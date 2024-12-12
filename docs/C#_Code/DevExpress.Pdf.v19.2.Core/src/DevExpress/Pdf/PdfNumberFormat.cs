namespace DevExpress.Pdf
{
    using DevExpress.Pdf.Native;
    using System;

    public class PdfNumberFormat : PdfObject
    {
        private const string labelDictionaryKey = "U";
        private const string conversionFactorDictionaryKey = "C";
        private const string displayFormatDictionaryKey = "F";
        private const string precisionDictionaryKey = "D";
        private const string truncateLowOrderZerosDictionaryKey = "FD";
        private const string digitalGroupingDelimiterDictionaryKey = "RT";
        private const string decimalDelimiterDictionaryKey = "RD";
        private const string prefixDictionaryKey = "PS";
        private const string suffixDictionaryKey = "SS";
        private const string labelPositionDictionaryKey = "O";
        private const int defaultPrecision = 100;
        private const int defaultDenominator = 0x10;
        private const string defaultDigitalGroupingDelimiter = ",";
        private const string defaultDecimalDelimiter = ".";
        private const string defaultPrefixSuffix = " ";
        private readonly string label;
        private readonly double conversionFactor;
        private readonly PdfNumberFormatDisplayFormat displayFormat;
        private readonly int precision;
        private readonly int denominator;
        private readonly bool truncateLowOrderZeros;
        private readonly string digitalGroupingDelimiter;
        private readonly string decimalDelimiter;
        private readonly string prefix;
        private readonly string suffix;
        private readonly PdfNumberFormatLabelPosition labelPosition;

        internal PdfNumberFormat(PdfReaderDictionary dictionary) : base(dictionary.Number)
        {
            int? integer;
            this.precision = 100;
            this.denominator = 0x10;
            this.label = dictionary.GetString("U");
            double? number = dictionary.GetNumber("C");
            if ((this.label == null) || (number == null))
            {
                PdfDocumentStructureReader.ThrowIncorrectDataException();
            }
            this.conversionFactor = number.Value;
            this.displayFormat = dictionary.GetEnumName<PdfNumberFormatDisplayFormat>("F");
            PdfNumberFormatDisplayFormat displayFormat = this.displayFormat;
            if (displayFormat == PdfNumberFormatDisplayFormat.ShowAsDecimal)
            {
                integer = dictionary.GetInteger("D");
                this.precision = (integer != null) ? integer.GetValueOrDefault() : 100;
                if ((this.precision != 1) && ((this.precision % 10) != 0))
                {
                    PdfDocumentStructureReader.ThrowIncorrectDataException();
                }
            }
            else if (displayFormat == PdfNumberFormatDisplayFormat.ShowAsFraction)
            {
                integer = dictionary.GetInteger("D");
                this.denominator = (integer != null) ? integer.GetValueOrDefault() : 0x10;
                if (this.denominator < 0)
                {
                    PdfDocumentStructureReader.ThrowIncorrectDataException();
                }
            }
            bool? boolean = dictionary.GetBoolean("FD");
            this.truncateLowOrderZeros = (boolean != null) ? boolean.GetValueOrDefault() : false;
            string text1 = dictionary.GetString("RT");
            PdfNumberFormat format1 = this;
            if (text1 == null)
            {
                format1 = ",";
            }
            text1.digitalGroupingDelimiter = (string) format1;
            string text2 = dictionary.GetString("RD");
            string text5 = text2;
            if (text2 == null)
            {
                string local2 = text2;
                text5 = ".";
            }
            this.decimalDelimiter = text5;
            string text3 = dictionary.GetString("PS");
            string text6 = text3;
            if (text3 == null)
            {
                string local3 = text3;
                text6 = " ";
            }
            this.prefix = text6;
            string text4 = dictionary.GetString("SS");
            string text7 = text4;
            if (text4 == null)
            {
                string local4 = text4;
                text7 = " ";
            }
            this.suffix = text7;
            this.labelPosition = dictionary.GetEnumName<PdfNumberFormatLabelPosition>("O");
        }

        protected internal override object ToWritableObject(PdfObjectCollection collection)
        {
            PdfWriterDictionary dictionary = new PdfWriterDictionary(collection);
            dictionary.Add("U", this.label);
            dictionary.Add("C", this.conversionFactor);
            dictionary.AddEnumName<PdfNumberFormatDisplayFormat>("F", this.displayFormat);
            PdfNumberFormatDisplayFormat displayFormat = this.displayFormat;
            if (displayFormat == PdfNumberFormatDisplayFormat.ShowAsDecimal)
            {
                dictionary.Add("D", this.precision, (int) 100);
            }
            else if (displayFormat == PdfNumberFormatDisplayFormat.ShowAsFraction)
            {
                dictionary.Add("D", this.denominator, (int) 0x10);
            }
            dictionary.Add("FD", this.truncateLowOrderZeros, false);
            dictionary.Add("RT", this.digitalGroupingDelimiter, ",");
            dictionary.Add("RD", this.decimalDelimiter, ".");
            dictionary.Add("PS", this.prefix, " ");
            dictionary.Add("SS", this.suffix, " ");
            dictionary.AddEnumName<PdfNumberFormatLabelPosition>("O", this.labelPosition);
            return dictionary;
        }

        public string Label =>
            this.label;

        public double ConversionFactor =>
            this.conversionFactor;

        public PdfNumberFormatDisplayFormat DisplayFormat =>
            this.displayFormat;

        public int Precision =>
            this.precision;

        public int Denominator =>
            this.denominator;

        public bool TruncateLowOrderZeros =>
            this.truncateLowOrderZeros;

        public string DigitalGroupingDelimiter =>
            this.digitalGroupingDelimiter;

        public string DecimalDelimiter =>
            this.decimalDelimiter;

        public string Prefix =>
            this.prefix;

        public string Suffix =>
            this.suffix;

        public PdfNumberFormatLabelPosition LabelPosition =>
            this.labelPosition;
    }
}

