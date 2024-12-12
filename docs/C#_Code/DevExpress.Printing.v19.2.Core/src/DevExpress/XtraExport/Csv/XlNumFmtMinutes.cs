namespace DevExpress.XtraExport.Csv
{
    using DevExpress.Export.Xl;
    using System;
    using System.Globalization;

    internal class XlNumFmtMinutes : XlNumFmtTimeBase
    {
        public XlNumFmtMinutes(int count, bool elapsed) : base(count, elapsed)
        {
        }

        protected override void FormatCore(XlVariantValue value, CultureInfo culture, XlNumFmtResult result)
        {
            string str;
            if (base.Elapsed)
            {
                str = ((long) ((value.NumericValue * 24.0) * 60.0)).ToString(new string('0', base.Count), culture);
            }
            else
            {
                DateTime dateTimeValue = value.DateTimeValue;
                str = !base.IsDefaultNetDateTimeFormat ? dateTimeValue.ToString(new string('m', 2), culture) : dateTimeValue.ToString($"%{'m'}", culture);
            }
            result.Text = result.Text + str;
        }

        public override XlNumFmtDesignator Designator =>
            XlNumFmtDesignator.Minute;
    }
}

