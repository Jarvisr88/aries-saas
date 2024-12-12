namespace DevExpress.XtraExport.Csv
{
    using DevExpress.Export.Xl;
    using System;
    using System.Globalization;

    internal class XlNumFmtTextContent : XlNumFmtElementBase
    {
        public static XlNumFmtTextContent Instance = new XlNumFmtTextContent();

        private XlNumFmtTextContent()
        {
        }

        protected override void FormatCore(XlVariantValue value, CultureInfo culture, XlNumFmtResult result)
        {
            result.Text = result.Text + value.ToText(culture).TextValue;
        }

        public override XlNumFmtDesignator Designator =>
            XlNumFmtDesignator.At;
    }
}

