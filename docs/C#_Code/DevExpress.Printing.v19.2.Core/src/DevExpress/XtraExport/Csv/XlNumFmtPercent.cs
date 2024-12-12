namespace DevExpress.XtraExport.Csv
{
    using DevExpress.Export.Xl;
    using System;
    using System.Globalization;

    internal class XlNumFmtPercent : XlNumFmtElementBase
    {
        public static XlNumFmtPercent Instance = new XlNumFmtPercent();

        private XlNumFmtPercent()
        {
        }

        protected override void FormatCore(XlVariantValue value, CultureInfo culture, XlNumFmtResult result)
        {
            result.Text = result.Text + "%";
        }

        public override XlNumFmtDesignator Designator =>
            XlNumFmtDesignator.Percent;
    }
}

