namespace DevExpress.XtraExport.Implementation
{
    using DevExpress.Export.Xl;
    using DevExpress.XtraExport;
    using System;
    using System.Globalization;
    using System.Runtime.CompilerServices;

    internal class XlTextFunction : IXlFormulaParameter
    {
        public static XlTextFunction Create(IXlFormulaParameter formula, XlNumberFormat numberFormat) => 
            new XlTextFunction { 
                Value = formula,
                NumberFormat = numberFormat
            };

        public static XlTextFunction Create(XlVariantValue value, XlNumberFormat numberFormat) => 
            Create(new XlFormulaParameter(value), numberFormat);

        public static XlTextFunction Create(IXlFormulaParameter formula, string netFormatString, bool isDateTimeFormatString) => 
            new XlTextFunction { 
                Value = formula,
                NetFormatString = netFormatString,
                IsDateTimeFormatString = isDateTimeFormatString
            };

        public static XlTextFunction Create(XlVariantValue value, string netFormatString, bool isDateTimeFormatString) => 
            Create(new XlFormulaParameter(value), netFormatString, isDateTimeFormatString);

        public string ToString(CultureInfo culture)
        {
            culture ??= CultureInfo.InvariantCulture;
            string str = (this.Value != null) ? this.Value.ToString(culture) : "#VALUE!";
            if (this.NumberFormat != null)
            {
                return $"TEXT({str}, "{this.NumberFormat.GetLocalizedFormatCode(culture).Replace("\"", "\"\"")}")";
            }
            XlExportNumberFormatConverter converter = new XlExportNumberFormatConverter();
            ExcelNumberFormat format = converter.Convert(this.NetFormatString, this.IsDateTimeFormatString, culture);
            return $"TEXT({str}, "{(this.IsDateTimeFormatString ? converter.GetLocalDateFormatString((format != null) ? format.FormatString : string.Empty, culture) : converter.GetLocalFormatString((format != null) ? format.FormatString : string.Empty, culture)).Replace("\"", "\"\"")}")";
        }

        public IXlFormulaParameter Value { get; set; }

        public string NetFormatString { get; set; }

        public bool IsDateTimeFormatString { get; set; }

        public XlNumberFormat NumberFormat { get; set; }
    }
}

