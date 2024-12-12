namespace DevExpress.XtraExport.Csv
{
    using DevExpress.Export.Xl;
    using System;
    using System.Globalization;

    internal abstract class XlDateTimeFormatterBase : XlValueFormatterBase
    {
        protected XlDateTimeFormatterBase()
        {
        }

        public override string Format(XlVariantValue value, CultureInfo culture)
        {
            if ((value.IsNumeric && (value.NumericValue < 0.0)) || XlVariantValue.IsErrorDateTimeSerial(value.NumericValue, false))
            {
                return new string('#', 0xff);
            }
            return base.Format(value, culture);
        }

        protected override string FormatNumeric(XlVariantValue value, CultureInfo culture)
        {
            string formatString = this.GetFormatString(culture);
            if (string.IsNullOrEmpty(formatString))
            {
                formatString = "d";
            }
            try
            {
                object[] args = new object[] { value.DateTimeValue };
                return string.Format(culture, XlExportNetFormatComposer.CreateFormat(formatString), args);
            }
            catch (FormatException)
            {
                object[] args = new object[] { value.DateTimeValue };
                return string.Format(culture, XlExportNetFormatComposer.CreateFormat("d"), args);
            }
        }
    }
}

