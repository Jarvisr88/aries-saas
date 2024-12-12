namespace DevExpress.XtraExport.Csv
{
    using DevExpress.Export.Xl;
    using System;
    using System.Globalization;

    internal class XlNumFmtSeconds : XlNumFmtTimeBase
    {
        public XlNumFmtSeconds(int count, bool elapsed) : base(count, elapsed)
        {
        }

        protected override void FormatCore(XlVariantValue value, CultureInfo culture, XlNumFmtResult result)
        {
            DateTime dateTimeValue = value.DateTimeValue;
            result.Text = result.Text + (base.Elapsed ? ((long) (((value.NumericValue * 24.0) * 60.0) * 60.0)) : ((long) dateTimeValue.Second)).ToString(new string('0', base.Count), culture);
        }

        public override XlNumFmtDesignator Designator =>
            XlNumFmtDesignator.Second;
    }
}

