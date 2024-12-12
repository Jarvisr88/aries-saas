namespace DevExpress.Office.Drawing
{
    using DevExpress.Office.Model;
    using System;

    public class SourceRectangleHistoryItem : RectangleOffsetHistoryItem
    {
        public SourceRectangleHistoryItem(DrawingBlipFill blipFill, RectangleOffset oldValue, RectangleOffset newValue) : base(blipFill, oldValue, newValue)
        {
        }

        protected override void RedoCore()
        {
            base.BlipFill.SetSourceRectangleCore(base.NewValue);
        }

        protected override void UndoCore()
        {
            base.BlipFill.SetSourceRectangleCore(base.OldValue);
        }
    }
}

