namespace DevExpress.Office.Drawing
{
    using DevExpress.Office.History;
    using System;

    public class DrawingTextInsetHasValuesChangeHistoryItem : DrawingHistoryItem<DrawingTextInset, bool>
    {
        public DrawingTextInsetHasValuesChangeHistoryItem(DrawingTextInset owner, int index, bool oldValue, bool newValue) : base(owner.DocumentModel.MainPart, owner, index, oldValue, newValue)
        {
        }

        protected override void RedoCore()
        {
            base.Owner.SetInsetHasValuesCore(base.Index, base.NewValue);
        }

        protected override void UndoCore()
        {
            base.Owner.SetInsetHasValuesCore(base.Index, base.OldValue);
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

