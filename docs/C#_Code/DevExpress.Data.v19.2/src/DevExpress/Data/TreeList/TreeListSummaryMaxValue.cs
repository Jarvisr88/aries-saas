namespace DevExpress.Data.TreeList
{
    using System;

    public class TreeListSummaryMaxValue : TreeListSummaryValue
    {
        private object max;

        public TreeListSummaryMaxValue(TreeListSummaryItem summaryItem);
        public override void Calculate(TreeListNodeBase node, bool summariesIgnoreNullValues);
        public override void Calculate(TreeListSummaryValue val, bool summariesIgnoreNullValues);
        public override void Start(TreeListNodeBase node);

        public override object Value { get; }
    }
}

