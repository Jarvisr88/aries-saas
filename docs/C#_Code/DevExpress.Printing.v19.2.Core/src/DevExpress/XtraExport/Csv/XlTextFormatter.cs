namespace DevExpress.XtraExport.Csv
{
    using System;
    using System.Globalization;

    internal class XlTextFormatter : XlNumericFormatterBase
    {
        protected override string GetFormatString(CultureInfo culture) => 
            "G";
    }
}

