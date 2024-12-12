namespace DevExpress.XtraExport.Csv
{
    using DevExpress.Export.Xl;
    using System;
    using System.Globalization;

    internal interface IXlNumFmtElement
    {
        void Format(XlVariantValue value, CultureInfo culture, XlNumFmtResult result);
        void FormatEmpty(CultureInfo culture, XlNumFmtResult result);

        bool IsDigit { get; }

        XlNumFmtDesignator Designator { get; }
    }
}

