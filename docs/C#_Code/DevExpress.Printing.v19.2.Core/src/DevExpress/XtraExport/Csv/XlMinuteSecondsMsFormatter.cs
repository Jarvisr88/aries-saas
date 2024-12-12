namespace DevExpress.XtraExport.Csv
{
    using System;
    using System.Globalization;

    internal class XlMinuteSecondsMsFormatter : XlDateTimeFormatterBase
    {
        protected override string GetFormatString(CultureInfo culture) => 
            $"mm:ss{culture.NumberFormat.NumberDecimalSeparator}f";
    }
}

