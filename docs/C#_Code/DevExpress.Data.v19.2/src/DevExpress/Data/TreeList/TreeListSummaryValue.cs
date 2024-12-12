namespace DevExpress.Data.TreeList
{
    using DevExpress.Data;
    using System;
    using System.Runtime.CompilerServices;

    public abstract class TreeListSummaryValue
    {
        protected static ValueComparer Comparer;
        protected readonly object StartValue;
        private TreeListSummaryItem summaryItem;

        static TreeListSummaryValue();
        public TreeListSummaryValue(TreeListSummaryItem summaryItem);
        public virtual void BackUp();
        public abstract void Calculate(TreeListNodeBase node, bool summariesIgnoreNullValues);
        public virtual void Calculate(TreeListSummaryValue val, bool summariesIgnoreNullValues);
        public virtual void Finish(TreeListNodeBase node);
        protected object GetNodeValue(TreeListNodeBase node);
        public virtual void Start(TreeListNodeBase node);
        public virtual void UpdateIncremental(TreeListSummaryValue baseValue);

        public bool IsTotalValueReady { get; protected set; }

        public TreeListSummaryItem SummaryItem { get; }

        protected string FieldName { get; }

        public abstract object Value { get; }

        public object OldValue { get; protected set; }

        public bool IsValid { get; set; }
    }
}

