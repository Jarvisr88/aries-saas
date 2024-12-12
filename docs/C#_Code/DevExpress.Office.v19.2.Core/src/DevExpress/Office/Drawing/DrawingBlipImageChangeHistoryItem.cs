namespace DevExpress.Office.Drawing
{
    using DevExpress.Office.History;
    using DevExpress.Office.Utils;
    using System;

    public class DrawingBlipImageChangeHistoryItem : DrawingHistoryItem<DrawingBlip, OfficeImage>
    {
        public DrawingBlipImageChangeHistoryItem(DrawingBlip owner, OfficeImage oldValue, OfficeImage newValue) : base(owner.DocumentModel.MainPart, owner, oldValue, newValue)
        {
        }

        protected override void RedoCore()
        {
            base.Owner.SetImageCore(base.NewValue);
        }

        public override void Register(IHistoryWriter writer, bool undone)
        {
            base.Register(writer, undone);
            writer.RegisterObject(base.OldValue);
            writer.RegisterObject(base.NewValue);
        }

        protected override void UndoCore()
        {
            base.Owner.SetImageCore(base.OldValue);
        }

        public override void Write(IHistoryWriter writer)
        {
            base.Write(writer);
            writer.Write(writer.GetObjectId(base.OldValue));
            writer.Write(writer.GetObjectId(base.NewValue));
        }

        public override void WriteObjects(IHistoryWriter writer, bool undone)
        {
            base.WriteObjects(writer, undone);
            writer.WriteObject(undone ? base.NewValue : base.OldValue);
        }
    }
}

