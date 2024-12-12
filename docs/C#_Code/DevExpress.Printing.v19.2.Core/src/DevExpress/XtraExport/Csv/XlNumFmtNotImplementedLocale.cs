namespace DevExpress.XtraExport.Csv
{
    using DevExpress.Export.Xl;
    using System;
    using System.Globalization;

    internal class XlNumFmtNotImplementedLocale : XlNumFmtCondition
    {
        private int locale;

        public XlNumFmtNotImplementedLocale(int locale)
        {
            this.locale = locale;
        }

        protected override void FormatCore(XlVariantValue value, CultureInfo culture, XlNumFmtResult result)
        {
            result.Text = result.Text + "B" + this.locale.ToString(culture);
        }
    }
}

