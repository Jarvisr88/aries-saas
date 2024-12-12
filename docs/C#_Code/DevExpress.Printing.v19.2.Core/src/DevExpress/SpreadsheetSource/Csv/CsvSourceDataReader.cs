namespace DevExpress.SpreadsheetSource.Csv
{
    using DevExpress.Export.Xl;
    using DevExpress.Internal;
    using DevExpress.SpreadsheetSource;
    using DevExpress.SpreadsheetSource.Implementation;
    using DevExpress.Utils;
    using DevExpress.XtraExport.Csv;
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.IO;
    using System.Runtime.CompilerServices;
    using System.Text;

    public class CsvSourceDataReader : SpreadsheetDataReaderBase
    {
        private static Dictionary<string, XlVariantValue> errorByInvariantNameTable = CreateErrorByInvariantNameTable();
        private const int maxQuotedTextLength = 0x8000;
        private CsvSpreadsheetSource source;
        private Stream stream;
        private Encoding actualEncoding;
        private CsvStreamReader reader;
        private readonly List<string> lineFields = new List<string>();
        private readonly StringBuilder accumulator = new StringBuilder();
        private Predicate<char> endOfLine;
        private char delimiter;
        private char quote;
        private string quoteAsString;
        private string doubleQuoteAsString;
        private int firstRowInRangeIndex;
        private int lastRowInRangeIndex = 0x7ffffffe;
        private long savedStreamPosition;
        private ICsvSourceValueConverter valueConverter;
        private CsvNewlineType actualNewlineType;
        private const int delimiterDetectorMaxRows = 10;
        private const int delimiterDetectorMaxChars = 0x8000;
        private DelimiterStatistic total;
        private DelimiterStatistic beforeQuote;
        private DelimiterStatistic afterQuote;

        public CsvSourceDataReader(CsvSpreadsheetSource source, Stream stream)
        {
            Guard.ArgumentNotNull(source, "source");
            Guard.ArgumentNotNull(stream, "stream");
            this.source = source;
            this.stream = stream;
            this.reader = null;
            this.actualEncoding = this.Options.Encoding;
            this.actualNewlineType = this.Options.NewlineType;
            this.endOfLine = null;
            this.delimiter = this.Options.ValueSeparator;
            this.quote = this.Options.TextQualifier;
            this.quoteAsString = new string(this.quote, 1);
            this.doubleQuoteAsString = new string(this.quote, 2);
        }

        private char AnalyzeStatistics()
        {
            char one = this.total.GetOne();
            if (one != '\0')
            {
                return one;
            }
            one = (this.beforeQuote + this.afterQuote).GetOne();
            if (one != '\0')
            {
                return one;
            }
            one = this.beforeQuote.GetOne();
            return ((one == '\0') ? this.total.GetMostFrequent(this.Options.Culture) : one);
        }

        private bool ArrayEquals(byte[] array1, byte[] array2)
        {
            if (array1.Length != array2.Length)
            {
                return false;
            }
            for (int i = 0; i < array1.Length; i++)
            {
                if (array1[i] != array2[i])
                {
                    return false;
                }
            }
            return true;
        }

        private bool AutoEndOfLine(char ch)
        {
            if (this.IsCrLf(ch))
            {
                this.endOfLine = new Predicate<char>(this.IsCrLf);
                this.actualNewlineType = CsvNewlineType.CrLf;
                return true;
            }
            if (this.IsLfCr(ch))
            {
                this.endOfLine = new Predicate<char>(this.IsLfCr);
                this.actualNewlineType = CsvNewlineType.LfCr;
                return true;
            }
            if (this.IsCr(ch))
            {
                this.endOfLine = new Predicate<char>(this.IsCr);
                this.actualNewlineType = CsvNewlineType.Cr;
                return true;
            }
            if (!this.IsLf(ch))
            {
                return false;
            }
            this.endOfLine = new Predicate<char>(this.IsLf);
            this.actualNewlineType = CsvNewlineType.Lf;
            return true;
        }

        private int CalculateDateTimeFormatId(DateTime value)
        {
            DateTime date = value.Date;
            return (!(date == value) ? (!(date == DateTime.MinValue) ? 0x16 : 0x15) : 14);
        }

        private int CalculateDateTimeFormatId(string text, DateTime value)
        {
            DateTime date = value.Date;
            return (!(date == value) ? (!(date == DateTime.MinValue) ? 0x16 : this.CalculateTimeFormatId(text, value)) : (this.IsShortDateFormat(text) ? 14 : 15));
        }

        private int CalculateHourIndex(string text, int hour, bool hasTimeDesignator)
        {
            if (hasTimeDesignator)
            {
                if (hour < 13)
                {
                    return ((hour != 0) ? ((hour != 12) ? this.IndexOf(text, 0, hour.ToString()) : this.IndexOf(text, 0, "0")) : this.IndexOf(text, 0, "12"));
                }
                hour -= 12;
                return this.IndexOf(text, 0, hour.ToString());
            }
            int num = this.IndexOf(text, 0, hour.ToString("0"));
            if (num < 0)
            {
                num = this.IndexOf(text, 0, hour.ToString("00"));
            }
            return num;
        }

        private void CalculateRowRange()
        {
            if ((base.Range != null) && !base.Range.TopLeft.IsColumn)
            {
                this.firstRowInRangeIndex = base.Range.FirstRow;
                this.lastRowInRangeIndex = base.Range.LastRow;
            }
            else
            {
                this.firstRowInRangeIndex = 0;
                this.lastRowInRangeIndex = 0x7ffffffe;
            }
        }

        private int CalculateTimeDesignatorIndex(string text)
        {
            int num = -1;
            DateTimeFormatInfo dateTimeFormat = this.Options.Culture.DateTimeFormat;
            if (!string.IsNullOrEmpty(dateTimeFormat.AMDesignator))
            {
                num = Math.Max(num, this.IndexOf(text, 0, dateTimeFormat.AMDesignator));
            }
            if (!string.IsNullOrEmpty(dateTimeFormat.PMDesignator))
            {
                num = Math.Max(num, this.IndexOf(text, 0, dateTimeFormat.PMDesignator));
            }
            return Math.Max(Math.Max(num, this.IndexOf(text, 0, "AM")), this.IndexOf(text, 0, "PM"));
        }

        private int CalculateTimeFormatId(string text, DateTime value)
        {
            int num = this.CalculateTimeDesignatorIndex(text);
            int from = this.CalculateHourIndex(text, value.Hour, num > 0);
            if (from < 0)
            {
                return 0;
            }
            int num3 = this.IndexOf(text, from, value.Minute.ToString());
            if (num3 < 0)
            {
                return ((num < 0) ? 0 : 0x12);
            }
            int num4 = this.IndexOf(text, num3, value.Second.ToString());
            return ((num > 0) ? ((num4 > num3) ? 0x13 : 0x12) : ((num4 > num3) ? 0x15 : 20));
        }

        public override void Close()
        {
            this.stream.Position = this.savedStreamPosition;
            this.reader = null;
            this.stream = null;
            this.source = null;
            base.Close();
        }

        private void CollectStatistics(int maxRowCount)
        {
            int num = this.reader.Read();
            bool flag = false;
            char ch = '\0';
            int num2 = 0;
            int num3 = 0;
            this.endOfLine ??= this.GetEndOfLinePredicate();
            while ((num >= 0) && ((num2 < maxRowCount) && (num3 < 0x8000)))
            {
                num3++;
                char ch2 = (char) num;
                if (flag)
                {
                    if (ch2 == this.quote)
                    {
                        flag = false;
                    }
                }
                else if (ch2 == this.quote)
                {
                    this.beforeQuote.Add(ch);
                    flag = true;
                }
                else if (this.endOfLine(ch2))
                {
                    num2++;
                }
                else if (ch == this.quote)
                {
                    this.afterQuote.Add(ch2);
                }
                else
                {
                    this.total.Add(ch2);
                }
                ch = ch2;
                num = this.reader.Read();
            }
        }

        private static Dictionary<string, XlVariantValue> CreateErrorByInvariantNameTable() => 
            new Dictionary<string, XlVariantValue>(StringExtensions.ComparerInvariantCultureIgnoreCase) { 
                { 
                    "#NULL!",
                    XlVariantValue.ErrorNullIntersection
                },
                { 
                    "#DIV/0!",
                    XlVariantValue.ErrorDivisionByZero
                },
                { 
                    "#VALUE!",
                    XlVariantValue.ErrorInvalidValueInFunction
                },
                { 
                    "#REF!",
                    XlVariantValue.ErrorReference
                },
                { 
                    "#NAME?",
                    XlVariantValue.ErrorName
                },
                { 
                    "#NUM!",
                    XlVariantValue.ErrorNumber
                },
                { 
                    "#N/A",
                    XlVariantValue.ErrorValueNotAvailable
                }
            };

        private void DetectDelimiter()
        {
            if (this.Options.DetectValueSeparator)
            {
                char ch = this.DetectDelimiterCore();
                if (ch != '\0')
                {
                    this.delimiter = ch;
                }
            }
        }

        protected internal char DetectDelimiterCore()
        {
            char ch;
            this.total = new DelimiterStatistic();
            this.beforeQuote = new DelimiterStatistic();
            this.afterQuote = new DelimiterStatistic();
            long position = this.stream.Position;
            this.reader = new CsvStreamReader(this.stream, this.actualEncoding);
            try
            {
                this.CollectStatistics(10);
                ch = this.AnalyzeStatistics();
            }
            finally
            {
                this.reader = null;
                this.stream.Position = position;
                this.afterQuote = null;
                this.beforeQuote = null;
                this.total = null;
            }
            return ch;
        }

        private void DetectEncoding()
        {
            if (this.Options.DetectEncoding)
            {
                Encoding encoding = new InternalEncodingDetector().Detect(this.stream);
                if (encoding != null)
                {
                    this.actualEncoding = encoding;
                }
            }
        }

        private FormattedValue GetConvertedValue(string text, int columnIndex)
        {
            object obj2 = this.valueConverter.Convert(text, columnIndex);
            if (obj2 == null)
            {
                return FormattedValue.Empty;
            }
            if (Convert.IsDBNull(obj2))
            {
                return FormattedValue.Empty;
            }
            Type type = obj2.GetType();
            if (!(type == typeof(DateTime)))
            {
                return (!(type == typeof(TimeSpan)) ? new FormattedValue(XlVariantValue.FromObject(obj2), 0) : new FormattedValue((TimeSpan) obj2, 0x2e));
            }
            DateTime time = (DateTime) obj2;
            XlVariantValue value2 = time;
            value2.SetDateTimeSerial(value2.NumericValue, false);
            return new FormattedValue(value2, this.CalculateDateTimeFormatId(time));
        }

        protected override string GetDisplayTextCore(Cell cell, CultureInfo culture)
        {
            int formatIndex = cell.FormatIndex;
            IXlValueFormatter formatter = base.FormatterFactory.CreateFormatter(formatIndex);
            return ((formatter == null) ? cell.Value.ToText(culture).TextValue : formatter.Format(cell.Value, culture));
        }

        private Predicate<char> GetEndOfLinePredicate()
        {
            if (this.Options.DetectNewlineType)
            {
                return new Predicate<char>(this.AutoEndOfLine);
            }
            switch (this.Options.NewlineType)
            {
                case CsvNewlineType.Lf:
                    return new Predicate<char>(this.IsLf);

                case CsvNewlineType.Cr:
                    return new Predicate<char>(this.IsCr);

                case CsvNewlineType.LfCr:
                    return new Predicate<char>(this.IsLfCr);

                case CsvNewlineType.VerticalTab:
                    return new Predicate<char>(this.IsVerticalTab);

                case CsvNewlineType.FormFeed:
                    return new Predicate<char>(this.IsFormFeed);
            }
            return new Predicate<char>(this.IsCrLf);
        }

        private FormattedValue GetFieldValue(int index)
        {
            string text = this.StripQuotesAndBlanks(this.lineFields[index]);
            if (this.valueConverter != null)
            {
                return this.GetConvertedValue(text, index);
            }
            if (string.IsNullOrEmpty(text))
            {
                return FormattedValue.Empty;
            }
            if (this.Options.DetectValueType)
            {
                XlVariantValueType valueType = this.Options.Schema[index];
                FormattedValue value2 = this.TryParseAsDouble(text, valueType);
                if (!value2.IsEmpty)
                {
                    return value2;
                }
                value2 = this.TryParseAsDoubleWithThousands(text, valueType);
                if (!value2.IsEmpty)
                {
                    return value2;
                }
                value2 = this.TryParseAsDateTime(text, valueType);
                if (!value2.IsEmpty)
                {
                    return value2;
                }
                value2 = this.TryParseAsBoolean(text, valueType);
                if (!value2.IsEmpty)
                {
                    return value2;
                }
                value2 = this.TryParseAsError(text, valueType);
                if (!value2.IsEmpty)
                {
                    return value2;
                }
            }
            return new FormattedValue(text, 0);
        }

        private int IndexOf(string where, int from, string what)
        {
            int num = where.IndexOfInvariantCultureIgnoreCase(what, from);
            if (num >= 0)
            {
                num += what.Length;
            }
            return num;
        }

        private bool IsCorrectThousandsDouble(string text, string separator, int[] numberGroupSizes)
        {
            if (numberGroupSizes.Length == 0)
            {
                return false;
            }
            int startIndex = text.Length - 1;
            int num2 = 0;
            int index = numberGroupSizes.Length - 1;
            int num4 = 0;
            while (true)
            {
                int num5 = (num4 > index) ? numberGroupSizes[index] : numberGroupSizes[num4];
                num2 = text.LastIndexOf(separator, startIndex);
                if ((num2 < 0) || (num5 == 0))
                {
                    return true;
                }
                if ((num2 == 0) || ((startIndex - num2) < num5))
                {
                    return false;
                }
                startIndex = num2 - 1;
                num4++;
            }
        }

        private bool IsCr(char ch) => 
            ch == '\r';

        private bool IsCrLf(char ch)
        {
            if (ch == '\r')
            {
                int num = this.reader.Peek();
                if ((num >= 0) && (((ushort) num) == 10))
                {
                    this.reader.Read();
                    return true;
                }
            }
            return false;
        }

        private bool IsFormFeed(char ch) => 
            ch == '\f';

        private bool IsLf(char ch) => 
            ch == '\n';

        private bool IsLfCr(char ch)
        {
            if (ch == '\n')
            {
                int num = this.reader.Peek();
                if ((num >= 0) && (((ushort) num) == 13))
                {
                    this.reader.Read();
                    return true;
                }
            }
            return false;
        }

        private bool IsShortDateFormat(string text)
        {
            string str = text.Trim();
            int length = str.Length;
            for (int i = 0; i < length; i++)
            {
                int num3 = str[i];
                if ((num3 < 0x2d) || (num3 > 0x39))
                {
                    return false;
                }
            }
            return true;
        }

        private bool IsVerticalTab(char ch) => 
            ch == '\v';

        private bool MoveToNextRow()
        {
            int currentRowIndex;
            if (base.CurrentRowIndex != -1)
            {
                currentRowIndex = base.CurrentRowIndex;
                base.CurrentRowIndex = currentRowIndex + 1;
                if (base.CurrentRowIndex > this.lastRowInRangeIndex)
                {
                    return false;
                }
                this.ReadLine();
                return (!this.reader.EndOfStream || (this.lineFields.Count != 0));
            }
            while (base.CurrentRowIndex < this.firstRowInRangeIndex)
            {
                currentRowIndex = base.CurrentRowIndex;
                base.CurrentRowIndex = currentRowIndex + 1;
                this.ReadLine();
                if (this.reader.EndOfStream && (this.lineFields.Count == 0))
                {
                    return false;
                }
            }
            return true;
        }

        public override void Open(IWorksheet worksheet, XlCellRange range)
        {
            base.Open(worksheet, range);
            this.savedStreamPosition = this.stream.Position;
            this.CalculateRowRange();
            this.DetectEncoding();
            this.SkipPreamble();
            this.DetectDelimiter();
            this.reader = new CsvStreamReader(this.stream, this.actualEncoding);
            base.CurrentRowIndex = -1;
        }

        private void ReadCells()
        {
            int num = 0;
            int num2 = this.lineFields.Count - 1;
            if ((base.Range != null) && !base.Range.TopLeft.IsRow)
            {
                num = Math.Max(num, base.Range.TopLeft.Column);
                num2 = Math.Min(num2, base.Range.BottomRight.Column);
                if (num2 < num)
                {
                    return;
                }
            }
            this.valueConverter = this.Options.ValueConverter;
            for (int i = num; i <= num2; i++)
            {
                FormattedValue fieldValue = this.GetFieldValue(i);
                if (!fieldValue.IsEmpty)
                {
                    base.AddCell(new Cell(i - num, fieldValue.Value, i, fieldValue.NumberFormatId));
                }
            }
        }

        protected override bool ReadCore()
        {
            while (this.MoveToNextRow())
            {
                this.ReadCells();
                if (!this.Options.SkipEmptyRows || (base.ExistingCells.Count > 0))
                {
                    return true;
                }
            }
            return false;
        }

        private void ReadLine()
        {
            this.lineFields.Clear();
            this.accumulator.Clear();
            this.endOfLine ??= this.GetEndOfLinePredicate();
            bool flag = false;
            for (int i = this.reader.Read(); i >= 0; i = this.reader.Read())
            {
                char ch = (char) i;
                if (flag)
                {
                    this.accumulator.Append(ch);
                    if (this.accumulator.Length >= 0x8000)
                    {
                        this.lineFields.Add(this.accumulator.ToString());
                        this.accumulator.Clear();
                        flag = false;
                    }
                    else if (ch == this.quote)
                    {
                        flag = false;
                    }
                }
                else if (ch == this.quote)
                {
                    flag = true;
                    this.accumulator.Append(ch);
                }
                else if (ch == this.delimiter)
                {
                    this.lineFields.Add(this.accumulator.ToString());
                    this.accumulator.Clear();
                }
                else
                {
                    if (this.endOfLine(ch))
                    {
                        this.lineFields.Add(this.accumulator.ToString());
                        this.accumulator.Clear();
                        return;
                    }
                    this.accumulator.Append(ch);
                }
            }
            if (this.accumulator.Length > 0)
            {
                this.lineFields.Add(this.accumulator.ToString());
            }
        }

        private void SkipPreamble()
        {
            long position = this.stream.Position;
            byte[] preamble = this.actualEncoding.GetPreamble();
            if ((preamble.Length != 0) && (this.stream.Length >= (position + preamble.Length)))
            {
                byte[] buffer = new byte[preamble.Length];
                this.stream.Read(buffer, 0, preamble.Length);
                if (!this.ArrayEquals(buffer, preamble))
                {
                    this.stream.Position = position;
                }
            }
        }

        private string StripQuotesAndBlanks(string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                return string.Empty;
            }
            int length = text.Length;
            if ((length > 1) && (text[0] == this.quote))
            {
                length--;
                if (text[length] == this.quote)
                {
                    length--;
                }
                if (length < 0)
                {
                    return string.Empty;
                }
                text = text.Substring(1, length).Replace(this.doubleQuoteAsString, this.quoteAsString);
            }
            if (this.Options.TrimBlanks)
            {
                text = text.Trim();
            }
            return text;
        }

        private FormattedValue TryParseAsBoolean(string text, XlVariantValueType valueType) => 
            ((valueType == XlVariantValueType.None) || (valueType == XlVariantValueType.Boolean)) ? ((StringExtensions.CompareInvariantCultureIgnoreCase(text, XlVariantValue.TrueConstant) != 0) ? ((StringExtensions.CompareInvariantCultureIgnoreCase(text, XlVariantValue.FalseConstant) != 0) ? FormattedValue.Empty : new FormattedValue(false, 0)) : new FormattedValue(true, 0)) : FormattedValue.Empty;

        private FormattedValue TryParseAsDateTime(string text, XlVariantValueType valueType)
        {
            DateTime time;
            if ((valueType != XlVariantValueType.None) && (valueType != XlVariantValueType.DateTime))
            {
                return FormattedValue.Empty;
            }
            if (!DateTime.TryParse(text, this.Options.Culture, DateTimeStyles.AssumeLocal | DateTimeStyles.NoCurrentDateDefault | DateTimeStyles.AllowWhiteSpaces, out time))
            {
                return FormattedValue.Empty;
            }
            if (time < XlVariantValue.BaseDate1900)
            {
                XlVariantValue value2 = new XlVariantValue();
                value2.SetDateLessThan1900(time);
                return new FormattedValue(value2, this.CalculateDateTimeFormatId(text, time));
            }
            XlVariantValue value3 = time;
            value3.SetDateTimeSerial(value3.NumericValue, false);
            return new FormattedValue(value3, this.CalculateDateTimeFormatId(text, time));
        }

        private FormattedValue TryParseAsDouble(string text, XlVariantValueType valueType)
        {
            if ((valueType == XlVariantValueType.None) || (valueType == XlVariantValueType.Numeric))
            {
                double num;
                if (double.TryParse(text, NumberStyles.Float, this.Options.Culture, out num))
                {
                    return ((double.IsNaN(num) || double.IsInfinity(num)) ? FormattedValue.Empty : new FormattedValue(num, 0));
                }
                string str = text.Trim();
                if (!string.IsNullOrEmpty(str))
                {
                    if (str[0] == '%')
                    {
                        str = str.Substring(1, str.Length - 1);
                    }
                    else
                    {
                        if (str[str.Length - 1] != '%')
                        {
                            return this.TryParseFractionNumbers(str);
                        }
                        str = str.Substring(0, str.Length - 1);
                    }
                    if (double.TryParse(str, NumberStyles.Float, this.Options.Culture, out num))
                    {
                        return ((num != Math.Round(num)) ? new FormattedValue(num / 100.0, 10) : new FormattedValue(num / 100.0, 9));
                    }
                }
            }
            return FormattedValue.Empty;
        }

        private FormattedValue TryParseAsDoubleWithThousands(string text, XlVariantValueType valueType)
        {
            if ((valueType == XlVariantValueType.None) || (valueType == XlVariantValueType.Numeric))
            {
                double num;
                CultureInfo culture = this.Options.Culture;
                if (double.TryParse(text, NumberStyles.Float | NumberStyles.AllowThousands, culture, out num) && (!double.IsNaN(num) && !double.IsInfinity(num)))
                {
                    NumberFormatInfo numberFormat = this.Options.Culture.NumberFormat;
                    int index = text.IndexOf(numberFormat.NumberDecimalSeparator);
                    string str = (index > 0) ? text.Substring(0, index) : text;
                    if (this.IsCorrectThousandsDouble(str, numberFormat.NumberGroupSeparator, numberFormat.NumberGroupSizes))
                    {
                        return new FormattedValue(num, (num == Math.Round(num)) ? 3 : 4);
                    }
                }
            }
            return FormattedValue.Empty;
        }

        private FormattedValue TryParseAsError(string text, XlVariantValueType valueType)
        {
            XlVariantValue value2;
            return (((valueType == XlVariantValueType.None) || (valueType == XlVariantValueType.Error)) ? (errorByInvariantNameTable.TryGetValue(text, out value2) ? new FormattedValue(value2, 0) : FormattedValue.Empty) : FormattedValue.Empty);
        }

        private FormattedValue TryParseFractionNumbers(string text)
        {
            double num2;
            double num3;
            int index = text.IndexOf('/');
            if (index <= 0)
            {
                return FormattedValue.Empty;
            }
            string s = text.Substring(0, index);
            string str2 = text.Substring(index + 1);
            if (!double.TryParse(s, NumberStyles.Float, this.Options.Culture, out num2))
            {
                return FormattedValue.Empty;
            }
            if (!double.TryParse(str2, NumberStyles.Float, this.Options.Culture, out num3))
            {
                return FormattedValue.Empty;
            }
            if (((num2 == 0.0) && (num3 == 0.0)) || (num3 <= 0.0))
            {
                return FormattedValue.Empty;
            }
            return new FormattedValue(num2 / num3, ((s.Trim().Length != 1) || (str2.Trim().Length != 1)) ? 13 : 12);
        }

        private CsvSpreadsheetSourceOptions Options =>
            this.source.InnerOptions;

        protected override List<int> NumberFormatIds =>
            null;

        protected override Dictionary<int, string> NumberFormatCodes =>
            null;

        protected override bool UseDate1904 =>
            false;

        protected override int DefaultCellFormatIndex =>
            -1;

        protected internal Encoding ActualEncoding =>
            this.actualEncoding;

        protected internal CsvNewlineType ActualNewlineType =>
            this.actualNewlineType;

        protected internal char ActualValueSeparator =>
            this.delimiter;

        private class DelimiterStatistic
        {
            private const char comma = ',';
            private const char semicolon = ';';
            private const char tab = '\t';
            private const char verticalLine = '|';

            public void Add(char ch)
            {
                int commaCount;
                if (ch == ',')
                {
                    commaCount = this.CommaCount;
                    this.CommaCount = commaCount + 1;
                }
                else if (ch == ';')
                {
                    commaCount = this.SemicolonCount;
                    this.SemicolonCount = commaCount + 1;
                }
                else if (ch == '\t')
                {
                    commaCount = this.TabCount;
                    this.TabCount = commaCount + 1;
                }
                else if (ch == '|')
                {
                    commaCount = this.VerticalLineCount;
                    this.VerticalLineCount = commaCount + 1;
                }
            }

            public char GetMostFrequent(CultureInfo culture)
            {
                char ch = (culture.NumberFormat.NumberDecimalSeparator.Length == 1) ? culture.NumberFormat.NumberDecimalSeparator[0] : '\0';
                char mostFrequentCore = this.GetMostFrequentCore();
                if ((mostFrequentCore == ',') && (mostFrequentCore == ch))
                {
                    mostFrequentCore = ((this.SemicolonCount <= this.TabCount) || (this.SemicolonCount <= this.VerticalLineCount)) ? (((this.TabCount <= this.SemicolonCount) || (this.TabCount <= this.VerticalLineCount)) ? (((this.VerticalLineCount <= this.SemicolonCount) || (this.VerticalLineCount <= this.TabCount)) ? '\0' : '|') : '\t') : ';';
                }
                return mostFrequentCore;
            }

            private char GetMostFrequentCore() => 
                ((this.CommaCount <= this.SemicolonCount) || ((this.CommaCount <= this.TabCount) || (this.CommaCount <= this.VerticalLineCount))) ? (((this.SemicolonCount <= this.CommaCount) || ((this.SemicolonCount <= this.TabCount) || (this.SemicolonCount <= this.VerticalLineCount))) ? (((this.TabCount <= this.CommaCount) || ((this.TabCount <= this.SemicolonCount) || (this.TabCount <= this.VerticalLineCount))) ? (((this.VerticalLineCount <= this.CommaCount) || ((this.VerticalLineCount <= this.SemicolonCount) || (this.VerticalLineCount <= this.TabCount))) ? '\0' : '|') : '\t') : ';') : ',';

            public char GetOne() => 
                ((this.CommaCount <= 0) || ((this.SemicolonCount != 0) || ((this.TabCount != 0) || (this.VerticalLineCount != 0)))) ? (((this.SemicolonCount <= 0) || ((this.CommaCount != 0) || ((this.TabCount != 0) || (this.VerticalLineCount != 0)))) ? (((this.TabCount <= 0) || ((this.CommaCount != 0) || ((this.SemicolonCount != 0) || (this.VerticalLineCount != 0)))) ? (((this.VerticalLineCount <= 0) || ((this.CommaCount != 0) || ((this.SemicolonCount != 0) || (this.TabCount != 0)))) ? '\0' : '|') : '\t') : ';') : ',';

            public static CsvSourceDataReader.DelimiterStatistic operator +(CsvSourceDataReader.DelimiterStatistic first, CsvSourceDataReader.DelimiterStatistic second) => 
                new CsvSourceDataReader.DelimiterStatistic { 
                    CommaCount = first.CommaCount + second.CommaCount,
                    SemicolonCount = first.SemicolonCount + second.SemicolonCount,
                    TabCount = first.TabCount + second.TabCount,
                    VerticalLineCount = first.VerticalLineCount + second.VerticalLineCount
                };

            public int CommaCount { get; private set; }

            public int SemicolonCount { get; private set; }

            public int TabCount { get; private set; }

            public int VerticalLineCount { get; private set; }
        }

        private class FormattedValue
        {
            private static readonly CsvSourceDataReader.FormattedValue empty = new CsvSourceDataReader.FormattedValue();

            protected FormattedValue()
            {
                this.Value = XlVariantValue.Empty;
                this.NumberFormatId = 0;
            }

            public FormattedValue(XlVariantValue value, int numberFormatId)
            {
                this.Value = value;
                this.NumberFormatId = numberFormatId;
            }

            public static CsvSourceDataReader.FormattedValue Empty =>
                empty;

            public XlVariantValue Value { get; private set; }

            public int NumberFormatId { get; private set; }

            public bool IsEmpty =>
                this.Value.IsEmpty;
        }
    }
}

