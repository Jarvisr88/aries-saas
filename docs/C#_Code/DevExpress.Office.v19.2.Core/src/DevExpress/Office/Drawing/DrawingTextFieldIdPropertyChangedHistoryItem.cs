namespace DevExpress.Office.Drawing
{
    using DevExpress.Office;
    using DevExpress.Office.History;
    using System;

    public class DrawingTextFieldIdPropertyChangedHistoryItem : DrawingHistoryItem<DrawingTextField, Guid>
    {
        public DrawingTextFieldIdPropertyChangedHistoryItem(IDocumentModelPart documentModelPart, DrawingTextField owner, Guid oldValue, Guid newValue) : base(documentModelPart, owner, oldValue, newValue)
        {
        }

        protected override void RedoCore()
        {
            base.Owner.SetFieldIdCore(base.NewValue);
        }

        protected override void UndoCore()
        {
            base.Owner.SetFieldIdCore(base.OldValue);
        }

        public override void Write(IHistoryWriter writer)
        {
            base.Write(writer);
            writer.Write(base.OldValue.ToByteArray());
            writer.Write(base.NewValue.ToByteArray());
        }
    }
}

