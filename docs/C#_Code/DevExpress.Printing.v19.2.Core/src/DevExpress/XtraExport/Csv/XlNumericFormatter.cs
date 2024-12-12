namespace DevExpress.XtraExport.Csv
{
    using System;
    using System.Globalization;

    internal class XlNumericFormatter : XlNumericFormatterBase
    {
        private string formatString;

        public XlNumericFormatter(string formatString)
        {
            this.formatString = formatString;
        }

        protected override string GetFormatString(CultureInfo culture) => 
            this.formatString;
    }
}

