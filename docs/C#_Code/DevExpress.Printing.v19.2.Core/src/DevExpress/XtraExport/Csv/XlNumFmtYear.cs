namespace DevExpress.XtraExport.Csv
{
    using DevExpress.Export.Xl;
    using System;
    using System.Globalization;

    internal class XlNumFmtYear : XlNumFmtDateBase
    {
        public XlNumFmtYear(int count) : base(count)
        {
        }

        protected override void FormatCore(XlVariantValue value, CultureInfo culture, XlNumFmtResult result)
        {
            string str = this.GetDateTime(value, culture).Year.ToString();
            result.Text = result.Text + str.Substring(str.Length - base.Count, base.Count);
        }

        protected virtual DateTime GetDateTime(XlVariantValue value, CultureInfo culture) => 
            value.GetDateTimeForMonthName();

        public override XlNumFmtDesignator Designator =>
            XlNumFmtDesignator.Year;
    }
}

