namespace DevExpress.XtraExport.Csv
{
    using System;
    using System.Globalization;

    internal class XlNumFmtGroupSeparator : XlNumFmtDecimalSeparator
    {
        public static XlNumFmtGroupSeparator Instance = new XlNumFmtGroupSeparator();

        protected XlNumFmtGroupSeparator()
        {
        }

        protected override string GetDesignator(CultureInfo culture) => 
            culture.NumberFormat.NumberGroupSeparator.Replace('\x00a0', ' ');

        public override XlNumFmtDesignator Designator =>
            XlNumFmtDesignator.GroupSeparator;
    }
}

