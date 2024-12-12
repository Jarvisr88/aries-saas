namespace DevExpress.XtraExport.Csv
{
    using DevExpress.Export.Xl;
    using System;
    using System.Globalization;

    internal class XlNumFmtUnderline : XlNumFmtElementBase
    {
        public XlNumFmtUnderline(char symbol)
        {
        }

        protected override void FormatCore(XlVariantValue value, CultureInfo culture, XlNumFmtResult result)
        {
            result.Text = result.Text + " ";
        }

        public override XlNumFmtDesignator Designator =>
            XlNumFmtDesignator.Underline;
    }
}

