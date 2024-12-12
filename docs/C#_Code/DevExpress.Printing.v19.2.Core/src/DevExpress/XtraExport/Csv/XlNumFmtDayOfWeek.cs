namespace DevExpress.XtraExport.Csv
{
    using DevExpress.Export.Xl;
    using System;
    using System.Globalization;

    internal class XlNumFmtDayOfWeek : XlNumFmtDateBase
    {
        public XlNumFmtDayOfWeek(int count) : base(count)
        {
        }

        protected override void FormatCore(XlVariantValue value, CultureInfo culture, XlNumFmtResult result)
        {
            string str = value.GetDateTimeForMonthName().ToString(new string('d', base.Count), culture);
            result.Text = result.Text + str;
        }

        public override XlNumFmtDesignator Designator =>
            XlNumFmtDesignator.DayOfWeek;
    }
}

