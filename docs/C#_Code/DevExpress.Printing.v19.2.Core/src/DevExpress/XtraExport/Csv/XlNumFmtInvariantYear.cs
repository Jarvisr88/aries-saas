namespace DevExpress.XtraExport.Csv
{
    using DevExpress.Export.Xl;
    using System;
    using System.Globalization;

    internal class XlNumFmtInvariantYear : XlNumFmtYear
    {
        public XlNumFmtInvariantYear(int count) : base(count)
        {
        }

        protected override void FormatCore(XlVariantValue value, CultureInfo culture, XlNumFmtResult result)
        {
            DateTime dateTime = this.GetDateTime(value, culture);
            result.Text = result.Text + dateTime.Year.ToString();
        }

        public override XlNumFmtDesignator Designator =>
            XlNumFmtDesignator.InvariantYear;
    }
}

