namespace DevExpress.XtraExport.Csv
{
    using DevExpress.Utils;
    using System;
    using System.Globalization;

    internal class XlNumFmtDateSeparator : XlNumFmtDecimalSeparator
    {
        public static XlNumFmtDateSeparator Instance = new XlNumFmtDateSeparator();

        protected XlNumFmtDateSeparator()
        {
        }

        protected override string GetDesignator(CultureInfo culture) => 
            culture.GetDateSeparator();

        public override XlNumFmtDesignator Designator =>
            XlNumFmtDesignator.DateSeparator;
    }
}

