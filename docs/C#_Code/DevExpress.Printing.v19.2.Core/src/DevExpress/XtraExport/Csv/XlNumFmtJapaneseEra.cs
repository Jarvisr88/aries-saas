namespace DevExpress.XtraExport.Csv
{
    using DevExpress.Export.Xl;
    using System;
    using System.Globalization;

    internal class XlNumFmtJapaneseEra : XlNumFmtDateBase
    {
        public XlNumFmtJapaneseEra(int count) : base(count)
        {
        }

        protected override void FormatCore(XlVariantValue value, CultureInfo culture, XlNumFmtResult result)
        {
        }

        public override XlNumFmtDesignator Designator =>
            XlNumFmtDesignator.JapaneseEra;
    }
}

