namespace DevExpress.XtraExport.Helpers
{
    using DevExpress.Export.Xl;
    using DevExpress.SpreadsheetSource;
    using DevExpress.SpreadsheetSource.Csv;
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Text;
    using System.Windows.Forms;

    internal class ExcelClipboardSource<TCol, TRow> : IClipboardSource where TCol: class, IColumn where TRow: class, IRowBase
    {
        private int[] indents;
        private MemoryStream clipboardMemoryStream;
        private bool isBiff8Stream;
        private ISpreadsheetSource spreadsheetSource;
        private IClipboardPasteHelper<TCol, TRow> clipboardPasteHelper;

        public ExcelClipboardSource(string format, IClipboardPasteHelper<TCol, TRow> helper)
        {
            this.clipboardPasteHelper = helper;
            CsvSpreadsheetSourceOptions options = new CsvSpreadsheetSourceOptions {
                SkipEmptyRows = false
            };
            if (format == "Biff8")
            {
                this.isBiff8Stream = true;
                this.clipboardMemoryStream = Clipboard.GetData("Biff8") as MemoryStream;
                this.spreadsheetSource = SpreadsheetSourceFactory.CreateSource(this.clipboardMemoryStream, SpreadsheetDocumentFormat.Xls, options);
            }
            else if (format == "UnicodeText")
            {
                this.clipboardMemoryStream = this.ConvertTextToCsvStream(Clipboard.GetText(TextDataFormat.UnicodeText));
                this.SetupCsvOptions(this.clipboardMemoryStream, options, false);
                this.spreadsheetSource = SpreadsheetSourceFactory.CreateSource(this.clipboardMemoryStream, SpreadsheetDocumentFormat.Csv, options);
            }
            else if (format != "Text")
            {
                this.clipboardMemoryStream = this.RemoveTrailingZeroByte(Clipboard.GetData(DataFormats.CommaSeparatedValue) as MemoryStream);
                this.SetupCsvOptions(this.clipboardMemoryStream, options, true);
                this.spreadsheetSource = SpreadsheetSourceFactory.CreateSource(this.clipboardMemoryStream, SpreadsheetDocumentFormat.Csv, options);
            }
            else
            {
                this.clipboardMemoryStream = this.ConvertTextToCsvStream(Clipboard.GetText(TextDataFormat.Text));
                this.SetupCsvOptions(this.clipboardMemoryStream, options, false);
                this.spreadsheetSource = SpreadsheetSourceFactory.CreateSource(this.clipboardMemoryStream, SpreadsheetDocumentFormat.Csv, options);
            }
        }

        private unsafe MemoryStream ConvertTextToCsvStream(string rawData)
        {
            char[] separator = new char[] { '\n' };
            Func<string, string> selector = <>c<TCol, TRow>.<>9__7_0;
            if (<>c<TCol, TRow>.<>9__7_0 == null)
            {
                Func<string, string> local1 = <>c<TCol, TRow>.<>9__7_0;
                selector = <>c<TCol, TRow>.<>9__7_0 = x => x.TrimEnd(new char[] { '\r' });
            }
            Func<string, bool> predicate = <>c<TCol, TRow>.<>9__7_1;
            if (<>c<TCol, TRow>.<>9__7_1 == null)
            {
                Func<string, bool> local2 = <>c<TCol, TRow>.<>9__7_1;
                predicate = <>c<TCol, TRow>.<>9__7_1 = x => string.IsNullOrWhiteSpace(x);
            }
            Func<string, bool> func3 = <>c<TCol, TRow>.<>9__7_2;
            if (<>c<TCol, TRow>.<>9__7_2 == null)
            {
                Func<string, bool> local3 = <>c<TCol, TRow>.<>9__7_2;
                func3 = <>c<TCol, TRow>.<>9__7_2 = x => string.IsNullOrWhiteSpace(x);
            }
            string[] strArray = rawData.Split(separator).Select<string, string>(selector).SkipWhile<string>(predicate).Reverse<string>().SkipWhile<string>(func3).Reverse<string>().ToArray<string>();
            StringBuilder builder = new StringBuilder();
            this.indents = new int[strArray.Length];
            for (int i = 0; i < strArray.Length; i++)
            {
                this.indents[i] = strArray[i].Length;
                char[] trimChars = new char[] { '\t' };
                string str = strArray[i].TrimStart(trimChars);
                int* numPtr1 = &(this.indents[i]);
                numPtr1[0] -= str.Length;
                char[] chArray3 = new char[] { '\t' };
                string[] strArray2 = strArray[i].Split(chArray3);
                builder.AppendLine(string.Join(",", (IEnumerable<string>) (from value in strArray2 select base.GetNormalizedCsvString(value))));
            }
            return new MemoryStream(Encoding.UTF8.GetBytes(builder.ToString()));
        }

        public IClipboardData GetData()
        {
            XlCellRange range;
            if (this.spreadsheetSource == null)
            {
                return null;
            }
            ClipboardData data = new ClipboardData();
            ISpreadsheetDataReader dataReader = this.spreadsheetSource.GetDataReader(this.spreadsheetSource.Worksheets[0]);
            using (RangeHelper helper = new RangeHelper(this.isBiff8Stream ? this.clipboardMemoryStream : null))
            {
                range = helper.GetRange();
            }
            int firstColumn = 0;
            int columnCount = 0;
            if (range != null)
            {
                firstColumn = range.FirstColumn;
                columnCount = range.ColumnCount;
            }
            bool flag = false;
            while (dataReader.Read())
            {
                if (dataReader.ExistingCells.Count == 0)
                {
                    ICell cell = dataReader.ExistingCells.FirstOrDefault<ICell>();
                    if ((cell != null) && (cell.Value.TextValue == "\0"))
                    {
                        continue;
                    }
                    if ((cell == null) && (data.RowCount == 0))
                    {
                        continue;
                    }
                }
                if (!flag && (dataReader.ExistingCells.Count > 1))
                {
                    flag = true;
                }
                if (range == null)
                {
                    columnCount = dataReader.FieldsCount;
                }
                object[] rowCells = new object[columnCount];
                int num3 = 0;
                while (true)
                {
                    if (num3 >= dataReader.ExistingCells.Count)
                    {
                        if (this.indents != null)
                        {
                            data.AddRow(new ClipboardRow(rowCells, this.indents[data.RowCount]));
                        }
                        else
                        {
                            data.AddRow(new ClipboardRow(rowCells));
                        }
                        break;
                    }
                    ICell cell2 = dataReader.ExistingCells[num3];
                    int fieldIndex = cell2.FieldIndex;
                    if (cell2.FieldIndex >= firstColumn)
                    {
                        fieldIndex = cell2.FieldIndex - firstColumn;
                    }
                    switch (cell2.Value.Type)
                    {
                        case XlVariantValueType.Boolean:
                            rowCells[fieldIndex] = cell2.Value.BooleanValue;
                            break;

                        case XlVariantValueType.Text:
                            rowCells[fieldIndex] = cell2.Value.TextValue;
                            break;

                        case XlVariantValueType.Numeric:
                            rowCells[fieldIndex] = cell2.Value.NumericValue;
                            break;

                        case XlVariantValueType.DateTime:
                            rowCells[fieldIndex] = cell2.Value.DateTimeValue;
                            break;

                        default:
                            rowCells[fieldIndex] = null;
                            break;
                    }
                    num3++;
                }
            }
            dataReader.Close();
            if ((data.RowCount == 1) && ((range == null) && !flag))
            {
                object[] cells = data.Rows[0].Cells;
                data.Reset();
                object[] objArray1 = new object[1];
                Func<object, bool> predicate = <>c<TCol, TRow>.<>9__6_0;
                if (<>c<TCol, TRow>.<>9__6_0 == null)
                {
                    Func<object, bool> local1 = <>c<TCol, TRow>.<>9__6_0;
                    predicate = <>c<TCol, TRow>.<>9__6_0 = e => e != null;
                }
                new object[1][0] = cells.FirstOrDefault<object>(predicate);
                data.AddRow(new ClipboardRow(new object[1]));
            }
            return data;
        }

        private string GetNormalizedCsvString(string value)
        {
            string str = this.ReplaceQuotes(value);
            return this.QuoteCommaDelimeters(str);
        }

        private string QuoteCommaDelimeters(string value)
        {
            if (value.Contains<char>(','))
            {
                string format = "\"{0}\"";
                int num = 0;
                foreach (char ch in value)
                {
                    if (ch == '"')
                    {
                        num++;
                    }
                    else if ((ch == ',') && ((num % 2) == 0))
                    {
                        return string.Format(format, value);
                    }
                }
            }
            return value;
        }

        private MemoryStream RemoveTrailingZeroByte(MemoryStream memoryStream)
        {
            MemoryStream stream;
            if (memoryStream == null)
            {
                return null;
            }
            byte[] source = memoryStream.ToArray();
            if ((source.Length == 0) || (source.Last<byte>() != 0))
            {
                stream = memoryStream;
            }
            else
            {
                stream = new MemoryStream(source, 0, source.Length - 1);
                memoryStream.Close();
                memoryStream.Dispose();
            }
            return stream;
        }

        private string ReplaceQuotes(string value) => 
            value.Replace("\"", "\"\"");

        private void SetupCsvOptions(MemoryStream ms, CsvSpreadsheetSourceOptions options, bool detectEncoding = true)
        {
            if (detectEncoding)
            {
                options.AutoDetect(ms);
            }
            else
            {
                Encoding encoding = options.Encoding;
                options.AutoDetect(ms);
                options.Encoding = encoding;
                options.DetectEncoding = detectEncoding;
            }
            options.Culture = CultureInfo.CurrentCulture;
            options.ValueConverter = new CustomCsvSourceValueConverter<TCol, TRow>(this.clipboardPasteHelper, options.Culture);
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly ExcelClipboardSource<TCol, TRow>.<>c <>9;
            public static Func<object, bool> <>9__6_0;
            public static Func<string, string> <>9__7_0;
            public static Func<string, bool> <>9__7_1;
            public static Func<string, bool> <>9__7_2;

            static <>c()
            {
                ExcelClipboardSource<TCol, TRow>.<>c.<>9 = new ExcelClipboardSource<TCol, TRow>.<>c();
            }

            internal string <ConvertTextToCsvStream>b__7_0(string x)
            {
                char[] trimChars = new char[] { '\r' };
                return x.TrimEnd(trimChars);
            }

            internal bool <ConvertTextToCsvStream>b__7_1(string x) => 
                string.IsNullOrWhiteSpace(x);

            internal bool <ConvertTextToCsvStream>b__7_2(string x) => 
                string.IsNullOrWhiteSpace(x);

            internal bool <GetData>b__6_0(object e) => 
                e != null;
        }
    }
}

