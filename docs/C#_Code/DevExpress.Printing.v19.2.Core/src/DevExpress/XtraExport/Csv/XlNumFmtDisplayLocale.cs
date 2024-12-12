namespace DevExpress.XtraExport.Csv
{
    using DevExpress.Export.Xl;
    using System;
    using System.Globalization;

    internal class XlNumFmtDisplayLocale : XlNumFmtCondition
    {
        private int hexCode;
        private string currency;

        public XlNumFmtDisplayLocale(int code, string currency)
        {
            this.hexCode = code;
            this.currency = currency;
        }

        protected override void FormatCore(XlVariantValue value, CultureInfo culture, XlNumFmtResult result)
        {
            result.Text = result.Text + this.currency;
        }

        public int HexCode =>
            this.hexCode;
    }
}

