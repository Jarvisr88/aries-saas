namespace DevExpress.XtraExport.Csv
{
    using System;

    internal abstract class XlNumFmtCondition : XlNumFmtElementBase
    {
        protected XlNumFmtCondition()
        {
        }

        public override XlNumFmtDesignator Designator =>
            XlNumFmtDesignator.Bracket;
    }
}

