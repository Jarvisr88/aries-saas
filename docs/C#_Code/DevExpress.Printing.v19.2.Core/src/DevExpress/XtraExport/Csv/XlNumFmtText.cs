namespace DevExpress.XtraExport.Csv
{
    using DevExpress.Export.Xl;
    using System;
    using System.Collections.Generic;
    using System.Globalization;

    internal class XlNumFmtText : XlNumFmtSimple
    {
        protected XlNumFmtText()
        {
        }

        public XlNumFmtText(IEnumerable<IXlNumFmtElement> elements) : base(elements)
        {
        }

        public override XlNumFmtResult FormatCore(XlVariantValue value, CultureInfo culture)
        {
            XlNumFmtResult result = new XlNumFmtResult {
                Text = string.Empty
            };
            foreach (IXlNumFmtElement element in this)
            {
                element.Format(value, culture, result);
            }
            return result;
        }

        public override XlNumFmtType Type =>
            XlNumFmtType.Text;
    }
}

