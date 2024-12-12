namespace DevExpress.Office.Drawing
{
    using DevExpress.Office.History;
    using System;

    public class DrawingTextParagraphOptionsChangedHistoryItem : DrawingHistoryItem<DrawingTextParagraph, bool>
    {
        public DrawingTextParagraphOptionsChangedHistoryItem(DrawingTextParagraph owner, int index, bool oldValue, bool newValue) : base(owner.DocumentModel.MainPart, owner, index, oldValue, newValue)
        {
        }

        protected override void RedoCore()
        {
            base.Owner.SetOptionsCore(base.Index, base.NewValue);
        }

        protected override void UndoCore()
        {
            base.Owner.SetOptionsCore(base.Index, base.OldValue);
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

