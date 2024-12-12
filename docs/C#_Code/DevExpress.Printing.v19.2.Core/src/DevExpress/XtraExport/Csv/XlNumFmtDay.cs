namespace DevExpress.XtraExport.Csv
{
    using DevExpress.Export.Xl;
    using System;
    using System.Globalization;

    internal class XlNumFmtDay : XlNumFmtDateBase
    {
        public XlNumFmtDay(int count) : base(count)
        {
        }

        protected override void FormatCore(XlVariantValue value, CultureInfo culture, XlNumFmtResult result)
        {
            DateTime dateTimeForDayOfWeek;
            if (base.Count > 2)
            {
                dateTimeForDayOfWeek = value.GetDateTimeForDayOfWeek();
            }
            else
            {
                double numericValue = value.NumericValue;
                if (numericValue < 1.0)
                {
                    result.Text = result.Text + new string('0', base.Count);
                    return;
                }
                if (((int) numericValue) == 60)
                {
                    result.Text = result.Text + "29";
                    return;
                }
                dateTimeForDayOfWeek = value.DateTimeValue;
            }
            string str = !base.IsDefaultNetDateTimeFormat ? dateTimeForDayOfWeek.ToString(new string('d', base.Count), culture) : dateTimeForDayOfWeek.ToString("%d", culture);
            result.Text = result.Text + str;
        }

        public override XlNumFmtDesignator Designator =>
            XlNumFmtDesignator.Day;
    }
}

