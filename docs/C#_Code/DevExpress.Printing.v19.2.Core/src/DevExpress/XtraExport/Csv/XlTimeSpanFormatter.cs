namespace DevExpress.XtraExport.Csv
{
    using DevExpress.Export.Xl;
    using DevExpress.Utils;
    using System;
    using System.Globalization;

    internal class XlTimeSpanFormatter : XlDateTimeFormatterBase
    {
        protected override string FormatNumeric(XlVariantValue value, CultureInfo culture)
        {
            long totalHours = (long) TimeSpan.FromDays(value.NumericValue).TotalHours;
            return $"{totalHours:0}{culture.GetTimeSeparator()}{value.DateTimeValue.ToString(this.GetFormatString(culture), culture)}";
        }

        protected override string GetFormatString(CultureInfo culture) => 
            "mm:ss";
    }
}

