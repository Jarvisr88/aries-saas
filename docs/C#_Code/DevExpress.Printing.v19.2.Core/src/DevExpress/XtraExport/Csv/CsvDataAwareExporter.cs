namespace DevExpress.XtraExport.Csv
{
    using DevExpress.Export.Xl;
    using DevExpress.Office.Crypto;
    using DevExpress.Utils;
    using DevExpress.XtraExport.Implementation;
    using DevExpress.XtraExport.Xls;
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.IO;
    using System.Runtime.InteropServices;
    using System.Text;

    public class CsvDataAwareExporter : IXlExport, IXlFormulaEngine, IXlExporter, IXlCellFormatter
    {
        private readonly XlFormatterFactory formatFactory = new XlFormatterFactory();
        private const int contentCapacity = 0x4000;
        private int sheetCount;
        private int columnIndex;
        private int currentRowIndex;
        private int currentColumnIndex;
        private Stream outputStream;
        private XlDocument currentDocument;
        private XlSheet currentSheet;
        private XlRow currentRow;
        private XlColumn currentColumn;
        private XlCell currentCell;
        private readonly Dictionary<int, IXlColumn> columns = new Dictionary<int, IXlColumn>();
        private readonly CsvDataAwareExporterOptions options = new CsvDataAwareExporterOptions();
        private StringBuilder contentBuilder = new StringBuilder(0x4000);
        private string newline;
        private char[] escape;
        private string textQualifier;
        private string escapedTextQualifier;
        private XlDocumentProperties documentProperties;
        private bool escapeInitialized;
        private bool rowContentStarted;
        private XlCellFormatter cellFormatter;
        private XlPicture currentPicture;
        private static char[] possibleInjection = new char[] { '=', '@' };
        private static char[] possibleInjectionSigns = new char[] { '+', '-' };

        private void AppendNewlines(int rowIndex)
        {
            if (this.currentColumnIndex == 0)
            {
                int num = rowIndex - this.currentRowIndex;
                if (this.currentRowIndex > 0)
                {
                    num++;
                }
                for (int i = 0; i < num; i++)
                {
                    this.contentBuilder.Append(this.newline);
                }
            }
        }

        public IXlCell BeginCell()
        {
            IXlColumn column;
            this.AppendNewlines(this.currentRow.RowIndex);
            this.currentCell = new XlCell();
            this.currentCell.RowIndex = this.CurrentRowIndex;
            this.currentCell.ColumnIndex = this.currentColumnIndex;
            this.currentCell.Formatting = XlFormatting.CopyObject<XlCellFormatting>(this.currentRow.Formatting);
            if (this.columns.TryGetValue(this.currentColumnIndex, out column))
            {
                this.currentCell.Formatting = XlFormatting.CopyObject<XlCellFormatting>(column.Formatting);
            }
            return this.currentCell;
        }

        public IXlColumn BeginColumn()
        {
            if (this.rowContentStarted)
            {
                throw new InvalidOperationException("Columns have to be created before rows and cells.");
            }
            this.currentColumn = new XlColumn(this.currentSheet);
            this.currentColumn.ColumnIndex = this.columnIndex;
            return this.currentColumn;
        }

        public IXlDocument BeginExport(Stream outputStream)
        {
            Guard.ArgumentNotNull(outputStream, "outputStream");
            this.outputStream = outputStream;
            this.cellFormatter = new XlCellFormatter(this);
            this.InitializeExport();
            this.documentProperties = new XlDocumentProperties();
            this.documentProperties.Created = DateTime.Now;
            this.currentDocument = new XlDocument(this);
            return this.currentDocument;
        }

        public IXlDocument BeginExport(Stream outputStream, EncryptionOptions encryptionOptions) => 
            this.BeginExport(outputStream);

        public IXlGroup BeginGroup() => 
            new XlGroup();

        public IXlPicture BeginPicture()
        {
            this.currentPicture = new XlPicture(this);
            return this.currentPicture;
        }

        public IXlRow BeginRow()
        {
            if (!this.escapeInitialized)
            {
                char[] chArray1 = new char[] { '\0', '\r', '\n', '\v', '\f' };
                chArray1[0] = this.Options.TextQualifier;
                this.escape = chArray1;
                this.textQualifier = new string(this.Options.TextQualifier, 1);
                this.escapedTextQualifier = new string(this.Options.TextQualifier, 2);
                this.escapeInitialized = true;
            }
            this.rowContentStarted = true;
            this.currentColumnIndex = 0;
            this.currentRow = new XlRow(this);
            this.currentRow.RowIndex = this.currentRowIndex;
            this.currentSheet.ExtendTableRanges(this.currentRowIndex);
            return this.currentRow;
        }

        public IXlSheet BeginSheet()
        {
            if (this.sheetCount > 0)
            {
                throw new InvalidOperationException("Only one worksheet can be exported to CSV.");
            }
            this.rowContentStarted = false;
            this.escapeInitialized = false;
            this.currentRowIndex = 0;
            this.currentColumnIndex = 0;
            this.sheetCount++;
            this.columnIndex = 0;
            this.columns.Clear();
            this.contentBuilder.Clear();
            if (this.Options.WritePreamble)
            {
                this.WritePreamble();
            }
            this.currentSheet = new XlSheet(this);
            this.currentSheet.Name = $"Sheet{this.sheetCount}";
            return this.currentSheet;
        }

        public IXlDocument CreateDocument(Stream stream) => 
            this.BeginExport(stream);

        public IXlDocument CreateDocument(Stream stream, EncryptionOptions encryptionOptions) => 
            this.BeginExport(stream, encryptionOptions);

        string IXlCellFormatter.GetFormattedValue(IXlCell cell) => 
            this.cellFormatter.GetFormattedValue(cell);

        IXlFormulaParameter IXlFormulaEngine.Concatenate(params IXlFormulaParameter[] parameters) => 
            null;

        IXlFormulaParameter IXlFormulaEngine.Param(XlVariantValue value) => 
            null;

        IXlFormulaParameter IXlFormulaEngine.Subtotal(XlCellRange range, XlSummary summary, bool ignoreHidden) => 
            null;

        IXlFormulaParameter IXlFormulaEngine.Subtotal(IList<XlCellRange> ranges, XlSummary summary, bool ignoreHidden) => 
            null;

        IXlFormulaParameter IXlFormulaEngine.Text(IXlFormulaParameter value, string netFormatString, bool isDateTimeFormatString) => 
            null;

        IXlFormulaParameter IXlFormulaEngine.Text(XlVariantValue value, string netFormatString, bool isDateTimeFormatString) => 
            null;

        IXlFormulaParameter IXlFormulaEngine.VLookup(IXlFormulaParameter lookupValue, XlCellRange table, int columnIndex, bool rangeLookup) => 
            null;

        IXlFormulaParameter IXlFormulaEngine.VLookup(XlVariantValue lookupValue, XlCellRange table, int columnIndex, bool rangeLookup) => 
            null;

        public void EndCell()
        {
            if (this.currentCell == null)
            {
                throw new InvalidOperationException("BeginCell/EndCell calls consistency.");
            }
            if ((this.currentCell.ColumnIndex < 0) || (this.currentCell.ColumnIndex >= this.options.MaxColumnCount))
            {
                throw new ArgumentOutOfRangeException($"Cell column index out of range 0..{this.options.MaxColumnCount - 1}.");
            }
            if (this.currentCell.ColumnIndex < this.currentColumnIndex)
            {
                throw new InvalidOperationException("Cell column index consistency.");
            }
            if (this.currentCell.Value.IsNumeric && XNumChecker.IsNegativeZero(this.currentCell.Value.NumericValue))
            {
                this.currentCell.Value = 0.0;
            }
            XlDifferentialFormatting differentialFormat = this.currentSheet.GetDifferentialFormat(this.currentCell.ColumnIndex, this.currentCell.RowIndex);
            this.currentCell.ApplyDifferentialFormatting(differentialFormat);
            this.currentSheet.RegisterCellPosition(this.currentCell);
            if (this.currentCell.ColumnIndex > 0)
            {
                int num = this.currentCell.ColumnIndex - this.currentColumnIndex;
                if (this.currentColumnIndex > 0)
                {
                    num++;
                }
                for (int i = 0; i < num; i++)
                {
                    this.contentBuilder.Append(this.Options.ValueSeparatorString);
                }
            }
            this.GenerateCellValue(this.currentCell);
            this.currentColumnIndex = this.currentCell.ColumnIndex + 1;
            this.currentCell = null;
        }

        public void EndColumn()
        {
            if (this.currentColumn == null)
            {
                throw new InvalidOperationException("BeginColumn/EndColumn calls consistency.");
            }
            if (this.rowContentStarted)
            {
                throw new InvalidOperationException("Columns have to be created before rows and cells.");
            }
            if ((this.currentColumn.ColumnIndex < 0) || (this.currentColumn.ColumnIndex >= this.options.MaxColumnCount))
            {
                throw new ArgumentOutOfRangeException($"Column index out of range 0...{this.options.MaxColumnCount - 1}");
            }
            this.currentSheet.RegisterColumnIndex(this.currentColumn);
            this.columns[this.currentColumn.ColumnIndex] = this.currentColumn;
            this.columnIndex = this.currentColumn.ColumnIndex + 1;
            this.currentColumn = null;
        }

        public void EndExport()
        {
            this.currentDocument = null;
            this.documentProperties = null;
            this.cellFormatter = null;
            if (this.outputStream == null)
            {
                throw new InvalidOperationException("BeginExport/EndExport calls consistency.");
            }
            this.outputStream = null;
        }

        public void EndGroup()
        {
        }

        public void EndPicture()
        {
            this.currentPicture = null;
        }

        public void EndRow()
        {
            if (this.currentRow == null)
            {
                throw new InvalidOperationException("BeginRow/EndRow calls consistency.");
            }
            if ((this.currentRow.RowIndex < 0) || (this.currentRow.RowIndex >= this.options.MaxRowCount))
            {
                throw new ArgumentOutOfRangeException($"Row index out of range 0..{this.options.MaxRowCount - 1}.");
            }
            if (this.currentRow.RowIndex < this.currentRowIndex)
            {
                throw new InvalidOperationException("RowIndex consistency.");
            }
            this.AppendNewlines(this.currentRow.RowIndex);
            if (this.currentColumnIndex > 0)
            {
                this.WriteContent(this.contentBuilder.ToString());
                this.contentBuilder.Clear();
            }
            this.currentRowIndex = this.currentRow.RowIndex + 1;
            this.currentRow = null;
        }

        public void EndSheet()
        {
            if (this.currentSheet == null)
            {
                throw new InvalidOperationException("BeginSheet/EndSheet calls consistency.");
            }
            if (this.Options.NewlineAfterLastRow && (this.currentRowIndex > 0))
            {
                this.WriteContent(this.newline);
            }
            this.currentSheet = null;
        }

        private string FormatValue(XlNumberFormat numberFormat, XlVariantValue value) => 
            this.formatFactory.CreateFormatter(numberFormat).Format(value, this.CurrentCulture);

        private void GenerateCellValue(IXlCell cell)
        {
            if (!this.Options.UseCellNumberFormat)
            {
                this.GenerateNonFormattedCellValue(cell);
            }
            else
            {
                this.GenerateFormattedCellValue(cell);
            }
        }

        private void GenerateDateTimeFormattedValue(XlVariantValue value, XlExportNetFormatParser parser)
        {
            string str2;
            string formatString = parser.FormatString;
            if (string.IsNullOrEmpty(formatString))
            {
                formatString = "d";
            }
            try
            {
                object[] args = new object[] { value.DateTimeValue };
                str2 = string.Format(this.CurrentCulture, XlExportNetFormatComposer.CreateFormat(parser.Prefix, formatString, parser.Postfix), args);
            }
            catch (FormatException)
            {
                object[] args = new object[] { value.DateTimeValue };
                str2 = string.Format(this.CurrentCulture, XlExportNetFormatComposer.CreateFormat(parser.Prefix, "d", parser.Postfix), args);
            }
            this.GenerateTextValue(str2, CsvQuotation.Auto);
        }

        private void GenerateFormattedCellValue(IXlCell cell)
        {
            XlVariantValue value2 = cell.Value;
            if (!value2.IsEmpty)
            {
                if (value2.IsError)
                {
                    this.contentBuilder.Append(value2.ErrorValue.Name);
                }
                else
                {
                    string netFormatString = string.Empty;
                    bool isDateTimeFormat = false;
                    XlCellFormatting formatting = cell.Formatting;
                    if (formatting != null)
                    {
                        if (!string.IsNullOrEmpty(formatting.NetFormatString))
                        {
                            netFormatString = formatting.NetFormatString;
                            isDateTimeFormat = formatting.IsDateTimeFormatString;
                        }
                        else if (formatting.NumberFormat != null)
                        {
                            this.GenerateTextValue(this.FormatValue(formatting.NumberFormat, cell.Value), CsvQuotation.Auto);
                            return;
                        }
                    }
                    if (string.IsNullOrEmpty(netFormatString))
                    {
                        this.GenerateNonFormattedCellValue(cell);
                    }
                    else
                    {
                        XlExportNetFormatParser parser = new XlExportNetFormatParser(netFormatString, isDateTimeFormat);
                        if (value2.IsNumeric)
                        {
                            if (isDateTimeFormat)
                            {
                                this.GenerateDateTimeFormattedValue(value2, parser);
                            }
                            else
                            {
                                this.GenerateNumericFormattedValue(value2, parser);
                            }
                        }
                        else if (value2.IsText)
                        {
                            this.GenerateTextValue(string.Format(XlExportNetFormatComposer.CreateFormat(parser.Prefix, string.Empty, parser.Postfix), value2.TextValue), this.Options.QuoteTextValues);
                        }
                        else if (value2.IsBoolean)
                        {
                            this.GenerateTextValue(string.Format(XlExportNetFormatComposer.CreateFormat(parser.Prefix, string.Empty, parser.Postfix), value2.BooleanValue ? "TRUE" : "FALSE"), CsvQuotation.Auto);
                        }
                    }
                }
            }
        }

        private void GenerateNonFormattedCellValue(IXlCell cell)
        {
            XlVariantValue value2 = cell.Value;
            if (!value2.IsEmpty)
            {
                if (value2.IsNumeric)
                {
                    if ((cell.Formatting != null) ? cell.Formatting.IsDateTimeFormatString : false)
                    {
                        this.GenerateTextValue(value2.DateTimeValue.ToString("d", this.CurrentCulture), CsvQuotation.Auto);
                    }
                    else
                    {
                        this.GenerateTextValue(value2.NumericValue.ToString("G", this.CurrentCulture), CsvQuotation.Auto);
                    }
                }
                else if (value2.IsText)
                {
                    this.GenerateTextValue(value2.TextValue, this.Options.QuoteTextValues);
                }
                else if (value2.IsBoolean)
                {
                    this.contentBuilder.Append(value2.BooleanValue ? "TRUE" : "FALSE");
                }
                else if (value2.IsError)
                {
                    this.contentBuilder.Append(value2.ErrorValue.Name);
                }
            }
        }

        private void GenerateNumericFormattedValue(XlVariantValue value, XlExportNetFormatParser parser)
        {
            string str2;
            int num;
            string formatString = parser.FormatString;
            if (string.IsNullOrEmpty(formatString))
            {
                formatString = "G";
            }
            if (this.TryGetIntegralValue(value.NumericValue, out num) && (num == value.NumericValue))
            {
                try
                {
                    object[] args = new object[] { num };
                    str2 = string.Format(this.CurrentCulture, XlExportNetFormatComposer.CreateFormat(parser.Prefix, formatString, parser.Postfix), args);
                }
                catch (FormatException)
                {
                    object[] args = new object[] { num };
                    str2 = string.Format(this.CurrentCulture, XlExportNetFormatComposer.CreateFormat(parser.Prefix, "G", parser.Postfix), args);
                }
            }
            else
            {
                string prefix = parser.Prefix;
                char ch = formatString[0];
                if ((ch == 'd') || (ch == 'D'))
                {
                    formatString = "G";
                }
                else if ((ch == 'x') || (ch == 'X'))
                {
                    formatString = "G";
                    if (prefix == "0x")
                    {
                        prefix = string.Empty;
                    }
                }
                try
                {
                    object[] args = new object[] { value.NumericValue };
                    str2 = string.Format(this.CurrentCulture, XlExportNetFormatComposer.CreateFormat(prefix, formatString, parser.Postfix), args);
                }
                catch (FormatException)
                {
                    object[] args = new object[] { value.NumericValue };
                    str2 = string.Format(this.CurrentCulture, XlExportNetFormatComposer.CreateFormat(prefix, "G", parser.Postfix), args);
                }
            }
            this.GenerateTextValue(str2, CsvQuotation.Auto);
        }

        private void GenerateTextValue(string text, CsvQuotation quotationMode = 0)
        {
            bool flag = (quotationMode != CsvQuotation.Auto) ? (quotationMode == CsvQuotation.Always) : ((text.IndexOfAny(this.escape) >= 0) || (text.IndexOf(this.Options.ValueSeparatorString) >= 0));
            if (this.Options.PreventCsvInjection)
            {
                if (this.HasInjection(text))
                {
                    text = this.textQualifier + text + this.textQualifier;
                    flag = true;
                }
                else if (this.HasQuotedInjection(text))
                {
                    flag = true;
                }
            }
            if (!flag)
            {
                this.contentBuilder.Append(text);
            }
            else
            {
                this.contentBuilder.Append(this.Options.TextQualifier);
                this.contentBuilder.Append(text.Replace(this.textQualifier, this.escapedTextQualifier));
                this.contentBuilder.Append(this.Options.TextQualifier);
            }
        }

        private string GetNewline(CsvNewlineType newlineType)
        {
            switch (newlineType)
            {
                case CsvNewlineType.Lf:
                    return "\n";

                case CsvNewlineType.Cr:
                    return "\r";

                case CsvNewlineType.LfCr:
                    return "\n\r";

                case CsvNewlineType.VerticalTab:
                    return "\v";

                case CsvNewlineType.FormFeed:
                    return "\f";
            }
            return "\r\n";
        }

        private bool HasExpression(string text)
        {
            double num;
            return (!string.IsNullOrEmpty(text) ? ((text.IndexOfAny(possibleInjectionSigns, 0, 1) >= 0) ? !double.TryParse(text, NumberStyles.Any, this.CurrentCulture, out num) : false) : false);
        }

        private bool HasInjection(string text) => 
            !string.IsNullOrEmpty(text) && ((text.Length >= 2) && ((text.IndexOfAny(possibleInjection, 0, 1) < 0) ? this.HasExpression(text) : true));

        private bool HasQuotedInjection(string text) => 
            !string.IsNullOrEmpty(text) && ((text.Length >= 4) && (((text[0] == this.Options.DefaultTextQualifier) || (text[0] == this.Options.TextQualifier)) ? ((text.IndexOfAny(possibleInjection, 1, 1) < 0) ? this.HasExpression(text.Substring(1, text.Length - 2)) : true) : false));

        private void InitializeExport()
        {
            this.sheetCount = 0;
            this.currentRowIndex = 0;
            this.currentColumnIndex = 0;
            this.newline = this.GetNewline(this.Options.NewlineType);
            this.escapeInitialized = false;
        }

        public void SkipCells(int count)
        {
            Guard.ArgumentPositive(count, "count");
            if (this.currentCell != null)
            {
                throw new InvalidOperationException("Operation cannot be executed inside BeginCell/EndCell scope.");
            }
            if ((this.currentColumnIndex + count) >= this.options.MaxColumnCount)
            {
                throw new ArgumentOutOfRangeException($"Cell column index goes beyond range 0..{this.options.MaxColumnCount - 1}.");
            }
            this.AppendNewlines(this.currentRow.RowIndex);
            int num = count - 1;
            if (this.currentColumnIndex > 0)
            {
                num++;
            }
            for (int i = 0; i < num; i++)
            {
                this.contentBuilder.Append(this.Options.ValueSeparatorString);
            }
            this.currentColumnIndex += count;
        }

        public void SkipColumns(int count)
        {
            Guard.ArgumentPositive(count, "count");
            if (this.currentColumn != null)
            {
                throw new InvalidOperationException("Operation cannot be executed inside BeginColumn/EndColumn scope.");
            }
            if ((this.columnIndex + count) >= this.options.MaxColumnCount)
            {
                throw new ArgumentOutOfRangeException($"Column index goes beyond range 0..{this.options.MaxColumnCount - 1}.");
            }
            this.columnIndex += count;
        }

        public void SkipRows(int count)
        {
            Guard.ArgumentPositive(count, "count");
            if (this.currentRow != null)
            {
                throw new InvalidOperationException("Operation cannot be executed inside BeginRow/EndRow scope.");
            }
            int num = this.currentRowIndex + count;
            if (num >= this.options.MaxRowCount)
            {
                throw new ArgumentOutOfRangeException($"Row index goes beyond range 0..{this.options.MaxRowCount - 1}.");
            }
            this.currentColumnIndex = 0;
            this.AppendNewlines(num - 1);
            this.currentRowIndex = num;
        }

        private bool TryGetIntegralValue(double value, out int integralValue)
        {
            try
            {
                integralValue = Convert.ToInt32(value);
                return true;
            }
            catch (OverflowException)
            {
                integralValue = 0;
                return false;
            }
        }

        private void WriteContent(string value)
        {
            byte[] bytes = this.Options.Encoding.GetBytes(value);
            this.outputStream.Write(bytes, 0, bytes.Length);
        }

        private void WritePreamble()
        {
            byte[] preamble = this.Options.Encoding.GetPreamble();
            if (preamble.Length != 0)
            {
                this.outputStream.Write(preamble, 0, preamble.Length);
            }
        }

        public IXlDocumentOptions DocumentOptions =>
            this.options;

        public CsvDataAwareExporterOptions Options =>
            this.options;

        public XlDocumentProperties DocumentProperties =>
            this.documentProperties;

        public CultureInfo CurrentCulture =>
            this.Options.Culture;

        public int CurrentRowIndex =>
            (this.currentRow == null) ? this.currentRowIndex : this.currentRow.RowIndex;

        public int CurrentColumnIndex =>
            this.currentColumnIndex;

        public int CurrentOutlineLevel =>
            0;

        public IXlDocument CurrentDocument =>
            this.currentDocument;

        public IXlSheet CurrentSheet =>
            this.currentSheet;

        public IXlFormulaEngine FormulaEngine =>
            this;

        public IXlFormulaParser FormulaParser =>
            null;
    }
}

