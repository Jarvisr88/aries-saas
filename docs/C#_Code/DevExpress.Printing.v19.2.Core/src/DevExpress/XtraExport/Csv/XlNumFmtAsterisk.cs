namespace DevExpress.XtraExport.Csv
{
    using DevExpress.Export.Xl;
    using System;
    using System.Globalization;

    internal class XlNumFmtAsterisk : XlNumFmtElementBase
    {
        private char symbol;

        public XlNumFmtAsterisk(char symbol)
        {
            this.symbol = symbol;
        }

        protected override void FormatCore(XlVariantValue value, CultureInfo culture, XlNumFmtResult result)
        {
            result.Text = result.Text + this.symbol.ToString();
        }

        public override XlNumFmtDesignator Designator =>
            XlNumFmtDesignator.Asterisk;
    }
}

