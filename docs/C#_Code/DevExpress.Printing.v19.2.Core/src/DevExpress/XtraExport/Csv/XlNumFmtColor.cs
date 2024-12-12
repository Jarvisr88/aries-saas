namespace DevExpress.XtraExport.Csv
{
    using DevExpress.Export.Xl;
    using System;
    using System.Drawing;
    using System.Globalization;

    internal class XlNumFmtColor : XlNumFmtCondition
    {
        public XlNumFmtColor(Color color)
        {
        }

        protected override void FormatCore(XlVariantValue value, CultureInfo culture, XlNumFmtResult result)
        {
        }
    }
}

