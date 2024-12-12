namespace DevExpress.Office.Drawing
{
    using System;

    public class DrawingBlipFillInfoIndexChangeHistoryItem : DrawingBlipFillHistoryItem
    {
        public DrawingBlipFillInfoIndexChangeHistoryItem(DrawingBlipFill obj) : base(obj)
        {
        }

        protected override void RedoCore()
        {
            base.Object.SetIndexCore(DrawingBlipFill.FillInfoIndexAccessor, base.NewIndex, base.ChangeActions);
        }

        protected override void UndoCore()
        {
            base.Object.SetIndexCore(DrawingBlipFill.FillInfoIndexAccessor, base.OldIndex, base.ChangeActions);
        }
    }
}

