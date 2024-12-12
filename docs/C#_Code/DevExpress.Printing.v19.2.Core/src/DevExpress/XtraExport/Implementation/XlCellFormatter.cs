namespace DevExpress.XtraExport.Implementation
{
    using DevExpress.Export.Xl;
    using DevExpress.XtraExport;
    using DevExpress.XtraExport.Csv;
    using System;
    using System.Collections.Generic;

    internal class XlCellFormatter : IXlCellFormatter
    {
        private readonly IXlExport exporter;
        private readonly Dictionary<XlNetNumberFormat, XlNumberFormat> netNumberFormatTable = new Dictionary<XlNetNumberFormat, XlNumberFormat>();
        private readonly XlExportNumberFormatConverter numberFormatConverter = new XlExportNumberFormatConverter();
        private readonly XlFormatterFactory formatFactory = new XlFormatterFactory();

        public XlCellFormatter(IXlExport exporter)
        {
            this.exporter = exporter;
        }

        private string FormatValue(XlNumberFormat numberFormat, XlVariantValue value) => 
            this.formatFactory.CreateFormatter(numberFormat).Format(value, this.exporter.CurrentCulture);

        public string GetFormattedValue(IXlCell cell)
        {
            if (cell == null)
            {
                return string.Empty;
            }
            XlVariantValue value2 = cell.Value;
            if (value2.IsEmpty)
            {
                return string.Empty;
            }
            if (value2.IsError)
            {
                return value2.ErrorValue.Name;
            }
            XlNumberFormat general = XlNumberFormat.General;
            XlCellFormatting formatting = cell.Formatting;
            if (formatting != null)
            {
                if (string.IsNullOrEmpty(formatting.NetFormatString))
                {
                    if (formatting.NumberFormat != null)
                    {
                        general = formatting.NumberFormat;
                    }
                }
                else
                {
                    XlNetNumberFormat format1 = new XlNetNumberFormat();
                    format1.FormatString = formatting.NetFormatString;
                    format1.IsDateTimeFormat = formatting.IsDateTimeFormatString;
                    XlNetNumberFormat key = format1;
                    if (!this.netNumberFormatTable.TryGetValue(key, out general))
                    {
                        ExcelNumberFormat format3 = this.numberFormatConverter.Convert(key.FormatString, key.IsDateTimeFormat, this.exporter.CurrentCulture);
                        general = ((format3 == null) || string.IsNullOrEmpty(format3.FormatString)) ? XlNumberFormat.General : format3.FormatString;
                        this.netNumberFormatTable.Add(key, general);
                    }
                }
            }
            return this.FormatValue(general, value2);
        }
    }
}

