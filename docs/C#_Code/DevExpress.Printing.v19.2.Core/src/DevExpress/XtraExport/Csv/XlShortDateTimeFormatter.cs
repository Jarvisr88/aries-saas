namespace DevExpress.XtraExport.Csv
{
    using System;
    using System.Globalization;

    internal class XlShortDateTimeFormatter : XlDateTimeFormatterBase
    {
        protected override string GetFormatString(CultureInfo culture) => 
            culture.DateTimeFormat.ShortDatePattern + " H:mm";
    }
}

