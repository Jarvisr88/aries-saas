namespace DevExpress.Data.Summary
{
    using System;

    public class SummaryEditorOrderUIItem
    {
        private SummaryItemsEditorController controller;
        private ISummaryItem item;
        private string caption;

        public SummaryEditorOrderUIItem(SummaryItemsEditorController controller, ISummaryItem item, string caption);
        protected virtual bool IsCanDown();
        protected virtual bool IsCanUp();
        public virtual void MoveDown();
        public virtual void MoveUp();
        public override string ToString();

        public string Caption { get; protected set; }

        public bool CanUp { get; }

        public bool CanDown { get; }

        public ISummaryItem Item { get; }

        public int Index { get; }

        protected SummaryItemsEditorController Controller { get; }
    }
}

