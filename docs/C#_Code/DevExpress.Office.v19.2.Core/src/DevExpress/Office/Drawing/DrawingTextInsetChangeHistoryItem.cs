namespace DevExpress.Office.Drawing
{
    using DevExpress.Office.History;
    using System;

    public class DrawingTextInsetChangeHistoryItem : DrawingHistoryItem<DrawingTextInset, float>
    {
        public DrawingTextInsetChangeHistoryItem(DrawingTextInset owner, int index, float oldValue, float newValue) : base(owner.DocumentModel.MainPart, owner, index, oldValue, newValue)
        {
        }

        protected override void RedoCore()
        {
            base.Owner.SetInsetCore(base.Index, base.NewValue);
        }

        protected override void UndoCore()
        {
            base.Owner.SetInsetCore(base.Index, base.OldValue);
        }

        public override void Write(IHistoryWriter writer)
        {
            base.Write(writer);
            writer.Write(base.Index);
            writer.Write(base.OldValue);
            writer.Write(base.NewValue);
        }
    }
}

