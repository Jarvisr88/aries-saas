namespace DevExpress.Office.Drawing
{
    using DevExpress.Office.History;
    using System;

    public class DrawingBlipEmbeddedChangeHistoryItem : DrawingHistoryItem<DrawingBlip, bool>
    {
        public DrawingBlipEmbeddedChangeHistoryItem(DrawingBlip owner, bool oldValue, bool newValue) : base(owner.DocumentModel.MainPart, owner, oldValue, newValue)
        {
        }

        protected override void RedoCore()
        {
            base.Owner.SetEmbeddedCore(base.NewValue);
        }

        protected override void UndoCore()
        {
            base.Owner.SetEmbeddedCore(base.OldValue);
        }

        public override void Write(IHistoryWriter writer)
        {
            base.Write(writer);
            writer.Write(base.OldValue);
            writer.Write(base.NewValue);
        }
    }
}

