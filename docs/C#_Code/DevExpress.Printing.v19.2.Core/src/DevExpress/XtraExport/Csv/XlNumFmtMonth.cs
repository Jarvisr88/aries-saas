namespace DevExpress.XtraExport.Csv
{
    using DevExpress.Export.Xl;
    using System;
    using System.Globalization;

    internal class XlNumFmtMonth : XlNumFmtDateBase
    {
        public XlNumFmtMonth(int count) : base(count)
        {
        }

        protected override void FormatCore(XlVariantValue value, CultureInfo culture, XlNumFmtResult result)
        {
            string str;
            DateTime dateTimeForMonthName = value.GetDateTimeForMonthName();
            if (base.IsDefaultNetDateTimeFormat)
            {
                str = dateTimeForMonthName.ToString("%M", culture);
            }
            else
            {
                str = dateTimeForMonthName.ToString(new string('M', base.Count), culture);
                if (base.Count == 5)
                {
                    str = str.Substring(0, 1);
                }
            }
            result.Text = result.Text + str;
        }

        public override XlNumFmtDesignator Designator =>
            XlNumFmtDesignator.Month;
    }
}

