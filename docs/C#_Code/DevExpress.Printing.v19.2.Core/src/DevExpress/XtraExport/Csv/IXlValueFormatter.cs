namespace DevExpress.XtraExport.Csv
{
    using DevExpress.Export.Xl;
    using System;
    using System.Globalization;

    public interface IXlValueFormatter
    {
        string Format(XlVariantValue value, CultureInfo culture);
    }
}

