namespace DevExpress.XtraExport.Csv
{
    using System;
    using System.Globalization;

    internal class XlNumFmtDigitEmpty : XlNumFmtDigitZero
    {
        public static XlNumFmtDigitEmpty Instance = new XlNumFmtDigitEmpty();

        protected XlNumFmtDigitEmpty()
        {
        }

        public override void FormatEmpty(CultureInfo culture, XlNumFmtResult result)
        {
        }

        public override char NonLocalizedDesignator =>
            '#';

        public override XlNumFmtDesignator Designator =>
            XlNumFmtDesignator.DigitEmpty;
    }
}

