namespace DevExpress.Office.Drawing
{
    using DevExpress.Office.Model;
    using System;

    public class FillRectangleHistoryItem : RectangleOffsetHistoryItem
    {
        public FillRectangleHistoryItem(DrawingBlipFill blipFill, RectangleOffset oldValue, RectangleOffset newValue) : base(blipFill, oldValue, newValue)
        {
        }

        protected override void RedoCore()
        {
            base.BlipFill.SetFillRectangleCore(base.NewValue);
        }

        protected override void UndoCore()
        {
            base.BlipFill.SetFillRectangleCore(base.OldValue);
        }
    }
}

