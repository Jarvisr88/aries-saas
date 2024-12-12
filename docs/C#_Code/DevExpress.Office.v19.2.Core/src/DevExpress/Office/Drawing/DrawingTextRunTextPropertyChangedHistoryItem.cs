namespace DevExpress.Office.Drawing
{
    using DevExpress.Office;
    using DevExpress.Office.History;
    using System;

    public class DrawingTextRunTextPropertyChangedHistoryItem : DrawingHistoryItem<DrawingTextRunStringBase, string>
    {
        public DrawingTextRunTextPropertyChangedHistoryItem(IDocumentModelPart documentModelPart, DrawingTextRunStringBase owner, string oldValue, string newValue) : base(documentModelPart, owner, oldValue, newValue)
        {
        }

        protected override void RedoCore()
        {
            base.Owner.SetTextCore(base.NewValue);
        }

        protected override void UndoCore()
        {
            base.Owner.SetTextCore(base.OldValue);
        }

        public override void Write(IHistoryWriter writer)
        {
            base.Write(writer);
            writer.Write(base.OldValue);
            writer.Write(base.NewValue);
        }
    }
}

