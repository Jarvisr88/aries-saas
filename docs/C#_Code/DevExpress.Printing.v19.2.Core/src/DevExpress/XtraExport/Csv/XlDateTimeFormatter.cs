namespace DevExpress.XtraExport.Csv
{
    using System;
    using System.Globalization;

    internal class XlDateTimeFormatter : XlDateTimeFormatterBase
    {
        private string formatString;

        public XlDateTimeFormatter(string formatString)
        {
            this.formatString = formatString;
        }

        protected override string GetFormatString(CultureInfo culture) => 
            this.formatString;
    }
}

