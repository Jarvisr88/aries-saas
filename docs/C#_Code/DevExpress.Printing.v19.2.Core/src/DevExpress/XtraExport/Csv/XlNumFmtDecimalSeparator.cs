namespace DevExpress.XtraExport.Csv
{
    using DevExpress.Export.Xl;
    using System;
    using System.Globalization;

    internal class XlNumFmtDecimalSeparator : IXlNumFmtElement
    {
        public static XlNumFmtDecimalSeparator Instance = new XlNumFmtDecimalSeparator();

        protected XlNumFmtDecimalSeparator()
        {
        }

        public void Format(XlVariantValue value, CultureInfo culture, XlNumFmtResult result)
        {
            this.FormatEmpty(culture, result);
        }

        public void FormatEmpty(CultureInfo culture, XlNumFmtResult result)
        {
            result.Text = result.Text + this.GetDesignator(culture);
        }

        protected virtual string GetDesignator(CultureInfo culture) => 
            culture.NumberFormat.NumberDecimalSeparator;

        public bool IsDigit =>
            false;

        public virtual XlNumFmtDesignator Designator =>
            XlNumFmtDesignator.DecimalSeparator;
    }
}

