namespace DevExpress.XtraExport.Csv
{
    using DevExpress.Export.Xl;
    using System;
    using System.Globalization;

    internal class XlNumFmtQuotedText : XlNumFmtElementBase
    {
        private string text;

        public XlNumFmtQuotedText(string text)
        {
            this.text = text;
        }

        protected override void FormatCore(XlVariantValue value, CultureInfo culture, XlNumFmtResult result)
        {
            result.Text = result.Text + this.text;
        }

        public string Text
        {
            get => 
                this.text;
            set => 
                this.text = value;
        }

        public override XlNumFmtDesignator Designator =>
            XlNumFmtDesignator.Quote;
    }
}

