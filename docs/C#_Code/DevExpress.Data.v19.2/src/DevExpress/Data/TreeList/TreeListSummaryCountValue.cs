namespace DevExpress.Data.TreeList
{
    using System;

    public class TreeListSummaryCountValue : TreeListSummaryValue
    {
        private int count;

        public TreeListSummaryCountValue(TreeListSummaryItem summaryItem);
        public override void BackUp();
        public override void Calculate(TreeListNodeBase node, bool summariesIgnoreNullValues);
        public override void Calculate(TreeListSummaryValue val, bool summariesIgnoreNullValues);
        public override void Start(TreeListNodeBase node);
        public override void UpdateIncremental(TreeListSummaryValue baseValue);

        public override object Value { get; }
    }
}

