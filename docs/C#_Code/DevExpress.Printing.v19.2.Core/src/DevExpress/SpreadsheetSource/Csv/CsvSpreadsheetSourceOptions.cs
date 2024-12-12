namespace DevExpress.SpreadsheetSource.Csv
{
    using DevExpress.SpreadsheetSource;
    using DevExpress.XtraExport.Csv;
    using System;
    using System.Globalization;
    using System.IO;
    using System.Runtime.CompilerServices;
    using System.Text;

    public class CsvSpreadsheetSourceOptions : SpreadsheetSourceOptions
    {
        private CultureInfo culture = CultureInfo.InvariantCulture;
        private System.Text.Encoding encoding = System.Text.Encoding.UTF8;
        private readonly CsvSourceSchema schema = new CsvSourceSchema();

        public CsvSpreadsheetSourceOptions()
        {
            this.ValueSeparator = ',';
            this.TextQualifier = '"';
            this.NewlineType = CsvNewlineType.CrLf;
            this.TrimBlanks = true;
            this.DetectEncoding = true;
            this.DetectValueType = true;
        }

        public void AutoDetect(Stream stream)
        {
            CsvSpreadsheetSourceOptions options = this.Clone();
            options.DetectEncoding = true;
            options.DetectNewlineType = true;
            options.DetectValueSeparator = true;
            using (ISpreadsheetSource source = SpreadsheetSourceFactory.CreateSource(stream, SpreadsheetDocumentFormat.Csv, options))
            {
                CsvSourceDataReader dataReader = source.GetDataReader(source.Worksheets[0]) as CsvSourceDataReader;
                this.Encoding = dataReader.ActualEncoding;
                this.NewlineType = dataReader.ActualNewlineType;
                this.ValueSeparator = dataReader.ActualValueSeparator;
            }
        }

        private CsvSpreadsheetSourceOptions Clone()
        {
            CsvSpreadsheetSourceOptions options = new CsvSpreadsheetSourceOptions {
                Culture = this.Culture.Clone() as CultureInfo,
                DetectEncoding = this.DetectEncoding,
                DetectNewlineType = this.DetectNewlineType,
                DetectValueSeparator = this.DetectValueSeparator,
                DetectValueType = this.DetectValueType,
                Encoding = this.Encoding.Clone() as System.Text.Encoding,
                NewlineType = this.NewlineType,
                Password = base.Password
            };
            foreach (CsvSourceSchemaItem item in this.Schema.Items)
            {
                options.Schema.Add(item.Index, item.ValueType);
            }
            options.SkipEmptyRows = base.SkipEmptyRows;
            options.SkipHiddenColumns = base.SkipHiddenColumns;
            options.SkipHiddenRows = base.SkipHiddenRows;
            options.TextQualifier = this.TextQualifier;
            options.TrimBlanks = this.TrimBlanks;
            options.ValueConverter = this.ValueConverter;
            options.ValueSeparator = this.ValueSeparator;
            return options;
        }

        public static CsvSpreadsheetSourceOptions ConvertToCsvOptions(ISpreadsheetSourceOptions other)
        {
            CsvSpreadsheetSourceOptions options = other as CsvSpreadsheetSourceOptions;
            if (options == null)
            {
                options = new CsvSpreadsheetSourceOptions();
                if (other != null)
                {
                    options.Password = other.Password;
                    options.SkipEmptyRows = other.SkipEmptyRows;
                    options.SkipHiddenRows = other.SkipHiddenRows;
                    options.SkipHiddenColumns = other.SkipHiddenColumns;
                }
            }
            return options;
        }

        public char ValueSeparator { get; set; }

        public char TextQualifier { get; set; }

        public CsvNewlineType NewlineType { get; set; }

        public CultureInfo Culture
        {
            get => 
                this.culture;
            set
            {
                value ??= CultureInfo.InvariantCulture;
                this.culture = value;
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

        public bool TrimBlanks { get; set; }

        public bool DetectEncoding { get; set; }

        public bool DetectNewlineType { get; set; }

        public bool DetectValueSeparator { get; set; }

        public bool DetectValueType { get; set; }

        public CsvSourceSchema Schema =>
            this.schema;

        public ICsvSourceValueConverter ValueConverter { get; set; }
    }
}

