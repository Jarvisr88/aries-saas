namespace DevExpress.XtraExport.Csv
{
    using System;
    using System.Globalization;

    internal class XlLongDateFormatter : XlDateTimeFormatterBase
    {
        protected override string GetFormatString(CultureInfo culture) => 
            culture.DateTimeFormat.LongDatePattern;
    }
}

