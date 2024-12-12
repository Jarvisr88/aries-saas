namespace DevExpress.Office.Drawing
{
    using DevExpress.Office;
    using DevExpress.Office.History;
    using System;

    public class DrawingTextFieldTypePropertyChangedHistoryItem : DrawingHistoryItem<DrawingTextField, string>
    {
        public DrawingTextFieldTypePropertyChangedHistoryItem(IDocumentModelPart documentModelPart, DrawingTextField owner, string oldValue, string newValue) : base(documentModelPart, owner, oldValue, newValue)
        {
        }

        protected override void RedoCore()
        {
            base.Owner.SetFieldTypeCore(base.NewValue);
        }

        protected override void UndoCore()
        {
            base.Owner.SetFieldTypeCore(base.OldValue);
        }

        public override void Write(IHistoryWriter writer)
        {
            base.Write(writer);
            writer.Write(base.OldValue);
            writer.Write(base.NewValue);
        }
    }
}

