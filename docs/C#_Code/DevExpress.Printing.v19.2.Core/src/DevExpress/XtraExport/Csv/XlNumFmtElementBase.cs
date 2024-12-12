namespace DevExpress.XtraExport.Csv
{
    using DevExpress.Export.Xl;
    using System;
    using System.Globalization;

    internal abstract class XlNumFmtElementBase : IXlNumFmtElement
    {
        protected XlNumFmtElementBase()
        {
        }

        public void Format(XlVariantValue value, CultureInfo culture, XlNumFmtResult result)
        {
            this.FormatCore(value, culture, result);
        }

        protected abstract void FormatCore(XlVariantValue value, CultureInfo culture, XlNumFmtResult result);
        public void FormatEmpty(CultureInfo culture, XlNumFmtResult result)
        {
            this.FormatCore(XlVariantValue.Empty, culture, result);
        }

        public bool IsDigit =>
            false;

        public abstract XlNumFmtDesignator Designator { get; }
    }
}

