namespace DevExpress.Office.Drawing
{
    using DevExpress.Office.History;
    using System;

    public class DrawingTextFontStringChangedHistoryItem : DrawingHistoryItem<DrawingTextFont, string>
    {
        public DrawingTextFontStringChangedHistoryItem(DrawingTextFont owner, int index, string oldValue, string newValue) : base(owner.DocumentModel.MainPart, owner, index, oldValue, newValue)
        {
        }

        protected override void RedoCore()
        {
            base.Owner.SetStringCore(base.Index, base.NewValue);
        }

        protected override void UndoCore()
        {
            base.Owner.SetStringCore(base.Index, base.OldValue);
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

