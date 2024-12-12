namespace DevExpress.Data.TreeList
{
    using System;

    public class TreeListSummaryAvgValue : TreeListSummarySumValue
    {
        private int count;
        private int oldCountValue;
        private object value;

        public TreeListSummaryAvgValue(TreeListSummaryItem summaryItem, TreeListDataControllerBase controller);
        public override void BackUp();
        public override void Calculate(TreeListNodeBase node, bool summariesIgnoreNullValues);
        public override void Calculate(TreeListSummaryValue val, bool summariesIgnoreNullValues);
        public override void Finish(TreeListNodeBase node);
        public override void Start(TreeListNodeBase node);
        public override void UpdateIncremental(TreeListSummaryValue baseValue);

        public override object Value { get; }
    }
}

