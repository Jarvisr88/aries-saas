namespace DevExpress.XtraExport.Csv
{
    using DevExpress.Export.Xl;
    using DevExpress.Utils;
    using System;
    using System.Collections.Generic;
    using System.Globalization;

    internal class XlNumFmtDateTime : XlNumFmtDateTimeBase
    {
        private bool hasMilliseconds;
        private XlNumFmtDisplayLocale locale;

        protected XlNumFmtDateTime()
        {
        }

        public XlNumFmtDateTime(IEnumerable<IXlNumFmtElement> elements, XlNumFmtDisplayLocale locale, bool hasMilliseconds) : base(elements)
        {
            this.locale = locale;
            this.hasMilliseconds = hasMilliseconds;
        }

        private CultureInfo CalculateSpecificCulture(int hexCode) => 
            LanguageIdToCultureConverter.Convert((int) (hexCode & 0xffff));

        public override XlNumFmtResult FormatCore(XlVariantValue value, CultureInfo culture)
        {
            if ((this.locale == null) || (this.locale.HexCode < 0))
            {
                return base.FormatCore(value, culture);
            }
            CultureInfo info = this.CalculateSpecificCulture(this.locale.HexCode);
            return base.FormatCore(value, info);
        }

        protected override bool HasMilliseconds =>
            this.hasMilliseconds;
    }
}

