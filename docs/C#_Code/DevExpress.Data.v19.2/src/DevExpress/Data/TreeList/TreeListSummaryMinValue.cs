namespace DevExpress.Data.TreeList
{
    using System;

    public class TreeListSummaryMinValue : TreeListSummaryValue
    {
        private object min;

        public TreeListSummaryMinValue(TreeListSummaryItem summaryItem);
        public override void Calculate(TreeListNodeBase node, bool summariesIgnoreNullValues);
        public override void Calculate(TreeListSummaryValue val, bool summariesIgnoreNullValues);
        public override void Start(TreeListNodeBase node);

        public override object Value { get; }
    }
}

