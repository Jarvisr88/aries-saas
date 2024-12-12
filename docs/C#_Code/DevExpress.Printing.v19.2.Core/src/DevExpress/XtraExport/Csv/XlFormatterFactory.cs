namespace DevExpress.XtraExport.Csv
{
    using DevExpress.Export.Xl;
    using System;
    using System.Collections.Generic;

    public class XlFormatterFactory
    {
        private readonly Dictionary<int, IXlValueFormatter> predefinedFormatters = new Dictionary<int, IXlValueFormatter>();
        private readonly Dictionary<string, IXlValueFormatter> customFormatters = new Dictionary<string, IXlValueFormatter>();
        private readonly XlNumFmtParser parser = new XlNumFmtParser();

        public XlFormatterFactory()
        {
            this.predefinedFormatters.Add(0, new XlNumericFormatter("G"));
            this.predefinedFormatters.Add(1, new XlNumericFormatter("0"));
            this.predefinedFormatters.Add(2, new XlNumericFormatter("0.00"));
            this.predefinedFormatters.Add(3, new XlNumericFormatter("#,##0"));
            this.predefinedFormatters.Add(4, new XlNumericFormatter("#,##0.00"));
            this.predefinedFormatters.Add(9, new XlNumericFormatter("0%"));
            this.predefinedFormatters.Add(10, new XlNumericFormatter("0.00%"));
            this.predefinedFormatters.Add(11, new XlNumericFormatter("0.00E+00"));
            this.predefinedFormatters.Add(12, new XlFractionFormatter(1));
            this.predefinedFormatters.Add(13, new XlFractionFormatter(2));
            this.predefinedFormatters.Add(14, new XlShortDateFormatter());
            this.predefinedFormatters.Add(15, new XlLongDateFormatter());
            this.predefinedFormatters.Add(0x10, new XlDateTimeFormatter("dd/MMM"));
            this.predefinedFormatters.Add(0x11, new XlDateTimeFormatter("MMM/yy"));
            this.predefinedFormatters.Add(0x12, new XlShortTime12Formatter());
            this.predefinedFormatters.Add(0x13, new XlLongTime12Formatter());
            this.predefinedFormatters.Add(20, new XlDateTimeFormatter("H:mm"));
            this.predefinedFormatters.Add(0x15, new XlDateTimeFormatter("H:mm:ss"));
            this.predefinedFormatters.Add(0x16, new XlShortDateTimeFormatter());
            this.predefinedFormatters.Add(0x25, new XlNumericFormatter("#,##0;(#,##0)"));
            this.predefinedFormatters.Add(0x26, new XlNumericFormatter("#,##0;(#,##0)"));
            this.predefinedFormatters.Add(0x27, new XlNumericFormatter("#,##0.00;(#,##0.00)"));
            this.predefinedFormatters.Add(40, new XlNumericFormatter("#,##0.00;(#,##0.00)"));
            this.predefinedFormatters.Add(0x2d, new XlDateTimeFormatter("mm:ss"));
            this.predefinedFormatters.Add(0x2e, new XlTimeSpanFormatter());
            this.predefinedFormatters.Add(0x2f, new XlMinuteSecondsMsFormatter());
            this.predefinedFormatters.Add(0x30, new XlNumericFormatter("##0.0E+0"));
            this.predefinedFormatters.Add(0x31, new XlTextFormatter());
        }

        private IXlValueFormatter CreateCustomFormatter(XlNumberFormat numberFormat)
        {
            try
            {
                return new XlCustomFormatter(this.parser.Parse(numberFormat.FormatCode));
            }
            catch
            {
                return null;
            }
        }

        public IXlValueFormatter CreateFormatter(XlNumberFormat numberFormat)
        {
            if (this.predefinedFormatters.ContainsKey(numberFormat.FormatId))
            {
                return this.predefinedFormatters[numberFormat.FormatId];
            }
            if (this.customFormatters.ContainsKey(numberFormat.FormatCode))
            {
                return this.customFormatters[numberFormat.FormatCode];
            }
            IXlValueFormatter formatter = this.CreateCustomFormatter(numberFormat);
            formatter ??= (numberFormat.IsDateTime ? this.predefinedFormatters[14] : this.predefinedFormatters[0]);
            this.customFormatters.Add(numberFormat.FormatCode, formatter);
            return formatter;
        }

        public IXlValueFormatter CreateFormatter(int numberFormatId) => 
            !this.predefinedFormatters.ContainsKey(numberFormatId) ? this.predefinedFormatters[0] : this.predefinedFormatters[numberFormatId];
    }
}

