namespace DevExpress.XtraExport.Csv
{
    using DevExpress.Export.Xl;
    using System;
    using System.Globalization;

    internal interface IXlNumFmt
    {
        bool EnclosedInParenthesesForPositive();
        XlNumFmtResult Format(XlVariantValue value, CultureInfo culture);
        XlVariantValue Round(XlVariantValue value, CultureInfo culture);

        XlNumFmtType Type { get; }
    }
}

