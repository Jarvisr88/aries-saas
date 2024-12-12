namespace DevExpress.XtraExport.Csv
{
    using DevExpress.Export.Xl;
    using DevExpress.Utils;
    using System;
    using System.Globalization;
    using System.Runtime.CompilerServices;
    using System.Text;

    public class CsvDataAwareExporterOptions : IXlDocumentOptions
    {
        private const string defaultValueSeparator = ",";
        private const string alternativeValueSeparator = ";";
        private const char defaultQuoteChar = '"';
        private string valueSeparator = ",";
        private System.Text.Encoding encoding = System.Text.Encoding.UTF8;
        private CultureInfo culture = CultureInfo.InvariantCulture;
        private bool isCustomValueSeparator = false;

        public CsvDataAwareExporterOptions()
        {
            this.TextQualifier = '"';
            this.NewlineType = CsvNewlineType.CrLf;
            this.UseCellNumberFormat = true;
            this.QuoteTextValues = CsvQuotation.Auto;
        }

        public void ResetValueSeparator()
        {
            this.valueSeparator = ",";
            this.isCustomValueSeparator = false;
            if ((this.valueSeparator == ",") && (this.culture.NumberFormat.NumberDecimalSeparator.IndexOf(","[0]) >= 0))
            {
                this.valueSeparator = ";";
            }
        }

        public char ValueSeparator
        {
            get => 
                this.valueSeparator[0];
            set
            {
                this.valueSeparator = new string(value, 1);
                this.isCustomValueSeparator = true;
            }
        }

        protected internal string ValueSeparatorString
        {
            get => 
                this.valueSeparator;
            set
            {
                Guard.ArgumentIsNotNullOrEmpty(value, "ValueSeparatorString");
                this.valueSeparator = value;
                this.isCustomValueSeparator = true;
            }
        }

        public bool IsCustomValueSeparator =>
            this.isCustomValueSeparator;

        public char TextQualifier { get; set; }

        protected internal char DefaultTextQualifier =>
            '"';

        public CsvNewlineType NewlineType { get; set; }

        public bool UseCellNumberFormat { get; set; }

        public bool WritePreamble { get; set; }

        public bool NewlineAfterLastRow { get; set; }

        public CultureInfo Culture
        {
            get => 
                this.culture;
            set
            {
                value ??= CultureInfo.InvariantCulture;
                this.culture = value;
                if ((this.valueSeparator == ",") && ((this.culture.NumberFormat.NumberDecimalSeparator.IndexOf(","[0]) >= 0) && !this.isCustomValueSeparator))
                {
                    this.valueSeparator = ";";
                }
            }
        }

        public System.Text.Encoding Encoding
        {
            get => 
                this.encoding;
            set
            {
                value ??= System.Text.Encoding.UTF8;
                this.encoding = value;
            }
        }

        public CsvQuotation QuoteTextValues { get; set; }

        public bool PreventCsvInjection { get; set; }

        public bool SupportsFormulas =>
            false;

        public bool SupportsDocumentParts =>
            false;

        public bool SupportsOutlineGrouping =>
            false;

        public int MaxColumnCount =>
            0x7fffffff;

        public int MaxRowCount =>
            0x7fffffff;

        public XlDocumentFormat DocumentFormat =>
            XlDocumentFormat.Csv;
    }
}

