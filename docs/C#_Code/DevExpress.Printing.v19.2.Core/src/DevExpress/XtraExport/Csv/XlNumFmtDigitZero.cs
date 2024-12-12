namespace DevExpress.XtraExport.Csv
{
    using DevExpress.Export.Xl;
    using System;
    using System.Globalization;

    internal class XlNumFmtDigitZero : IXlNumFmtElement
    {
        public static XlNumFmtDigitZero Instance = new XlNumFmtDigitZero();

        protected XlNumFmtDigitZero()
        {
        }

        public void Format(XlVariantValue value, CultureInfo culture, XlNumFmtResult result)
        {
            result.Text = result.Text + value.NumericValue;
        }

        public virtual void FormatEmpty(CultureInfo culture, XlNumFmtResult result)
        {
            result.Text = result.Text + this.NonLocalizedDesignator.ToString();
        }

        public bool IsDigit =>
            true;

        public virtual char NonLocalizedDesignator =>
            '0';

        public virtual XlNumFmtDesignator Designator =>
            XlNumFmtDesignator.DigitZero;
    }
}

