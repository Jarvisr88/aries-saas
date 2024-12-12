namespace DevExpress.XtraExport.Csv
{
    using System;
    using System.Globalization;

    internal class XlShortDateFormatter : XlDateTimeFormatterBase
    {
        protected override string GetFormatString(CultureInfo culture) => 
            culture.DateTimeFormat.ShortDatePattern;
    }
}

