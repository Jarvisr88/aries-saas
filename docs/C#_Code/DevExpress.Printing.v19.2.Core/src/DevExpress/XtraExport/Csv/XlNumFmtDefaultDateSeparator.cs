namespace DevExpress.XtraExport.Csv
{
    using System;
    using System.Globalization;

    internal class XlNumFmtDefaultDateSeparator : XlNumFmtDecimalSeparator
    {
        public static XlNumFmtDefaultDateSeparator Instance = new XlNumFmtDefaultDateSeparator();

        protected XlNumFmtDefaultDateSeparator()
        {
        }

        protected override string GetDesignator(CultureInfo culture) => 
            "/";

        public override XlNumFmtDesignator Designator =>
            XlNumFmtDesignator.FractionOrDateSeparator;
    }
}

