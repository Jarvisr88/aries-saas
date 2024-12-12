namespace DevExpress.Data.TreeList
{
    using DevExpress.Data;
    using System;

    public class TreeListSummaryCustomValue : TreeListSummaryValue
    {
        private IDataProvider dataProvider;
        private object value;

        public TreeListSummaryCustomValue(TreeListSummaryItem summaryItem, IDataProvider clientContol);
        public override void Calculate(TreeListNodeBase node, bool summariesIgnoreNullValues);
        public override void Finish(TreeListNodeBase node);
        private void RaiseEvent(TreeListNodeBase node, CustomSummaryProcess process);
        public override void Start(TreeListNodeBase node);

        public override object Value { get; }

        protected IDataProvider DataProvider { get; }
    }
}

