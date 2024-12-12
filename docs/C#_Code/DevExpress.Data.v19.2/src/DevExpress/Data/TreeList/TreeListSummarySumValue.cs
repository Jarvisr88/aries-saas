namespace DevExpress.Data.TreeList
{
    using System;

    public class TreeListSummarySumValue : TreeListSummaryValue
    {
        private object sum;
        protected bool isTimeSpan;

        public TreeListSummarySumValue(TreeListSummaryItem summaryItem, TreeListDataControllerBase controller);
        public override void BackUp();
        protected object CalcDiff(object value1, object value2);
        protected object CalcSum(object value1, object value2);
        public override void Calculate(TreeListNodeBase node, bool summariesIgnoreNullValues);
        public override void Calculate(TreeListSummaryValue val, bool summariesIgnoreNullValues);
        protected Type GetDataType(TreeListDataControllerBase controller);
        protected bool IsTimeSpanType(Type type);
        public override void Start(TreeListNodeBase node);
        public override void UpdateIncremental(TreeListSummaryValue baseValue);

        protected object Sum { get; set; }

        public override object Value { get; }
    }
}

