namespace DevExpress.XtraExport.Csv
{
    using DevExpress.Export.Xl;
    using System;
    using System.Globalization;

    internal class XlNumFmtBackslashedText : XlNumFmtElementBase
    {
        private char text;

        public XlNumFmtBackslashedText(char text)
        {
            this.text = text;
        }

        protected override void FormatCore(XlVariantValue value, CultureInfo culture, XlNumFmtResult result)
        {
            result.Text = result.Text + this.text.ToString();
        }

        public char Text
        {
            get => 
                this.text;
            set => 
                this.text = value;
        }

        public override XlNumFmtDesignator Designator =>
            XlNumFmtDesignator.Backslash;
    }
}

