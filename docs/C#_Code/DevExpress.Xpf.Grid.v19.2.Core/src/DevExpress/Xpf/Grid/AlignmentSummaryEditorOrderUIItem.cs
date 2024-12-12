namespace DevExpress.Xpf.Grid
{
    using DevExpress.Data.Summary;
    using System;

    public class AlignmentSummaryEditorOrderUIItem : SummaryEditorOrderUIItem
    {
        private SummaryEditorOrderListHelper orderHelper;

        public AlignmentSummaryEditorOrderUIItem(SummaryItemsEditorController controller, SummaryEditorOrderListHelper orderHelper, IAlignmentItem item, string caption) : base(controller, item, caption)
        {
            this.orderHelper = orderHelper;
        }

        private int GetNextIndex()
        {
            GridSummaryItemAlignment alignment = this.orderHelper.GetSummaryItemAlignment(base.Item).Value;
            for (int i = base.Index + 1; i < base.Controller.Items.Count; i++)
            {
                GridSummaryItemAlignment? summaryItemAlignment = this.orderHelper.GetSummaryItemAlignment(base.Controller.Items[i]);
                GridSummaryItemAlignment alignment2 = alignment;
                if ((((GridSummaryItemAlignment) summaryItemAlignment.GetValueOrDefault()) == alignment2) ? (summaryItemAlignment != null) : false)
                {
                    return i;
                }
            }
            return base.Controller.Items.Count;
        }

        private int GetPreviousIndex()
        {
            GridSummaryItemAlignment alignment = this.orderHelper.GetSummaryItemAlignment(base.Item).Value;
            for (int i = base.Index - 1; i >= 0; i--)
            {
                GridSummaryItemAlignment? summaryItemAlignment = this.orderHelper.GetSummaryItemAlignment(base.Controller.Items[i]);
                GridSummaryItemAlignment alignment2 = alignment;
                if ((((GridSummaryItemAlignment) summaryItemAlignment.GetValueOrDefault()) == alignment2) ? (summaryItemAlignment != null) : false)
                {
                    return i;
                }
            }
            return -1;
        }

        private void InterchangeItems(int prevIndex)
        {
            ISummaryItem item = base.Controller.Items[base.Index];
            base.Controller.Items[base.Index] = base.Controller.Items[prevIndex];
            base.Controller.Items[prevIndex] = item;
        }

        protected override bool IsCanDown() => 
            this.GetNextIndex() < base.Controller.Items.Count;

        protected override bool IsCanUp() => 
            this.GetPreviousIndex() >= 0;

        public override void MoveDown()
        {
            if (base.CanDown)
            {
                this.InterchangeItems(this.GetNextIndex());
            }
        }

        public override void MoveUp()
        {
            if (base.CanUp)
            {
                this.InterchangeItems(this.GetPreviousIndex());
            }
        }
    }
}

