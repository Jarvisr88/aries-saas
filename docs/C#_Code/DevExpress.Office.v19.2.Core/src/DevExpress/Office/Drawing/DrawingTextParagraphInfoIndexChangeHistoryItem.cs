namespace DevExpress.Office.Drawing
{
    using System;

    public class DrawingTextParagraphInfoIndexChangeHistoryItem : DrawingTextParagraphPropertiesHistoryItem
    {
        public DrawingTextParagraphInfoIndexChangeHistoryItem(DrawingTextParagraphProperties obj) : base(obj)
        {
        }

        protected override void RedoCore()
        {
            base.Object.SetIndexCore(DrawingTextParagraphProperties.TextParagraphInfoIndexAccessor, base.NewIndex, base.ChangeActions);
        }

        protected override void UndoCore()
        {
            base.Object.SetIndexCore(DrawingTextParagraphProperties.TextParagraphInfoIndexAccessor, base.OldIndex, base.ChangeActions);
        }
    }
}

