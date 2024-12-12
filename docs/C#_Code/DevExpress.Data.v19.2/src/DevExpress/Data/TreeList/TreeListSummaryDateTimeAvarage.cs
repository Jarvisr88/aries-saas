namespace DevExpress.Data.TreeList
{
    using System;

    public class TreeListSummaryDateTimeAvarage : TreeListSummaryValue
    {
        private Tuple<decimal, int> current;
        private object result;

        public TreeListSummaryDateTimeAvarage(TreeListSummaryItem item);
        public override void Calculate(TreeListNodeBase node, bool summariesIgnoreNullValues);
        public override void Finish(TreeListNodeBase node);
        public override void Start(TreeListNodeBase node);

        public override object Value { get; }
    }
}

