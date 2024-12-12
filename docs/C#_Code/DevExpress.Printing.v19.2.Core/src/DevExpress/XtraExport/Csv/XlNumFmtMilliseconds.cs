namespace DevExpress.XtraExport.Csv
{
    using DevExpress.Export.Xl;
    using System;
    using System.Globalization;

    internal class XlNumFmtMilliseconds : XlNumFmtDateBase
    {
        public XlNumFmtMilliseconds(int count) : base(count)
        {
        }

        protected override void FormatCore(XlVariantValue value, CultureInfo culture, XlNumFmtResult result)
        {
            result.Text = result.Text + Math.Round((double) (((double) value.DateTimeValue.Millisecond) / 1000.0), base.Count, MidpointRounding.AwayFromZero).ToString("." + new string('0', base.Count), culture);
        }

        public override XlNumFmtDesignator Designator =>
            XlNumFmtDesignator.DigitZero;
    }
}

