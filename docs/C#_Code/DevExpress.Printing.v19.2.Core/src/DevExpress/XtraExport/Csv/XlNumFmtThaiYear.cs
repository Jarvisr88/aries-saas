namespace DevExpress.XtraExport.Csv
{
    using DevExpress.Export.Xl;
    using System;
    using System.Globalization;

    internal class XlNumFmtThaiYear : XlNumFmtYear
    {
        public XlNumFmtThaiYear(int count) : base(count)
        {
        }

        protected override DateTime GetDateTime(XlVariantValue value, CultureInfo culture) => 
            base.GetDateTime(value, culture).AddYears(0x21f);

        public override XlNumFmtDesignator Designator =>
            XlNumFmtDesignator.ThaiYear;
    }
}

