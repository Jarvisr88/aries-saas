namespace DevExpress.XtraExport.Csv
{
    using System;
    using System.Globalization;

    internal class XlNumFmtDigitSpace : XlNumFmtDigitZero
    {
        public static XlNumFmtDigitSpace Instance = new XlNumFmtDigitSpace();

        protected XlNumFmtDigitSpace()
        {
        }

        public override void FormatEmpty(CultureInfo culture, XlNumFmtResult result)
        {
            result.Text = result.Text + " ";
        }

        public override char NonLocalizedDesignator =>
            '?';

        public override XlNumFmtDesignator Designator =>
            XlNumFmtDesignator.DigitSpace;
    }
}

