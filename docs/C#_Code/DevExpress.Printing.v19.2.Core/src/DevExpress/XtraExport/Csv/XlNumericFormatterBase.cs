namespace DevExpress.XtraExport.Csv
{
    using DevExpress.Export.Xl;
    using System;
    using System.Globalization;

    internal abstract class XlNumericFormatterBase : XlValueFormatterBase
    {
        protected XlNumericFormatterBase()
        {
        }

        protected override string FormatNumeric(XlVariantValue value, CultureInfo culture)
        {
            string formatString = this.GetFormatString(culture);
            if (string.IsNullOrEmpty(formatString))
            {
                formatString = "G";
            }
            try
            {
                object[] args = new object[] { value.NumericValue };
                return string.Format(culture, XlExportNetFormatComposer.CreateFormat(formatString), args);
            }
            catch (FormatException)
            {
                object[] args = new object[] { value.NumericValue };
                return string.Format(culture, XlExportNetFormatComposer.CreateFormat("G"), args);
            }
        }
    }
}

