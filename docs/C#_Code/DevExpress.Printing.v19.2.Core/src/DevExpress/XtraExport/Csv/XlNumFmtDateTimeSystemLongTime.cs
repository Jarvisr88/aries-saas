namespace DevExpress.XtraExport.Csv
{
    using DevExpress.Export.Xl;
    using System;
    using System.Collections.Generic;
    using System.Globalization;

    internal class XlNumFmtDateTimeSystemLongTime : XlNumFmtDateTimeBase
    {
        protected XlNumFmtDateTimeSystemLongTime()
        {
        }

        public XlNumFmtDateTimeSystemLongTime(IEnumerable<IXlNumFmtElement> elements) : base(elements)
        {
        }

        protected override void FormatDateTime(XlVariantValue value, CultureInfo culture, XlNumFmtResult result)
        {
            base.FormatDateTime(value, culture, result);
            result.Text = value.DateTimeValue.ToString(culture.DateTimeFormat.LongTimePattern, culture);
        }
    }
}

