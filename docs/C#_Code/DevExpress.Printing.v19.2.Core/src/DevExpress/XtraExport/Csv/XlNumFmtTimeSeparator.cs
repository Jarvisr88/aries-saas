namespace DevExpress.XtraExport.Csv
{
    using DevExpress.Utils;
    using System;
    using System.Globalization;

    internal class XlNumFmtTimeSeparator : XlNumFmtDateSeparator
    {
        public static XlNumFmtTimeSeparator Instance = new XlNumFmtTimeSeparator();

        protected XlNumFmtTimeSeparator()
        {
        }

        protected override string GetDesignator(CultureInfo culture) => 
            culture.GetTimeSeparator();

        public override XlNumFmtDesignator Designator =>
            XlNumFmtDesignator.TimeSeparator;
    }
}

