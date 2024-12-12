namespace DevExpress.XtraExport.Csv
{
    using DevExpress.Export.Xl;
    using System;
    using System.Globalization;

    internal class XlShortTime12Formatter : XlDateTimeFormatterBase
    {
        protected override string FormatNumeric(XlVariantValue value, CultureInfo culture)
        {
            DateTime dateTimeValue = value.DateTimeValue;
            return (dateTimeValue.ToString(this.GetFormatString(culture), culture) + ((dateTimeValue.Hour < 12) ? " AM" : " PM"));
        }

        protected override string GetFormatString(CultureInfo culture) => 
            "h:mm";
    }
}

