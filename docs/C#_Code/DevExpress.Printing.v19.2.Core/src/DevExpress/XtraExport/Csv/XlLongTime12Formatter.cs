namespace DevExpress.XtraExport.Csv
{
    using System;
    using System.Globalization;

    internal class XlLongTime12Formatter : XlShortTime12Formatter
    {
        protected override string GetFormatString(CultureInfo culture) => 
            "h:mm:ss";
    }
}

