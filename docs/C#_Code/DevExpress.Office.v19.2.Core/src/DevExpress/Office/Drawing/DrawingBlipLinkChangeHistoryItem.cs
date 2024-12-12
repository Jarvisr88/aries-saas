namespace DevExpress.Office.Drawing
{
    using DevExpress.Office.History;
    using System;

    public class DrawingBlipLinkChangeHistoryItem : DrawingHistoryItem<DrawingBlip, string>
    {
        public DrawingBlipLinkChangeHistoryItem(DrawingBlip owner, string oldValue, string newValue) : base(owner.DocumentModel.MainPart, owner, oldValue, newValue)
        {
        }

        protected override void RedoCore()
        {
            base.Owner.SetLinkCore(base.NewValue);
        }

        protected override void UndoCore()
        {
            base.Owner.SetLinkCore(base.OldValue);
        }

        public override void Write(IHistoryWriter writer)
        {
            base.Write(writer);
            writer.Write(base.OldValue);
            writer.Write(base.NewValue);
        }
    }
}

