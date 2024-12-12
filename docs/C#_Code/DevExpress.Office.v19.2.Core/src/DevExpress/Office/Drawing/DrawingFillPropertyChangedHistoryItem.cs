namespace DevExpress.Office.Drawing
{
    using DevExpress.Office;
    using DevExpress.Office.History;
    using System;

    public class DrawingFillPropertyChangedHistoryItem : DrawingHistoryItem<IFillOwner, IDrawingFill>
    {
        public DrawingFillPropertyChangedHistoryItem(IDocumentModelPart documentModelPart, IFillOwner owner, IDrawingFill oldValue, IDrawingFill newValue) : base(documentModelPart, owner, oldValue, newValue)
        {
        }

        protected override void RedoCore()
        {
            base.Owner.SetDrawingFillCore(base.NewValue);
        }

        public override void Register(IHistoryWriter writer, bool undone)
        {
            base.Register(writer, undone);
            writer.RegisterObject(base.OldValue);
            writer.RegisterObject(base.NewValue);
        }

        protected override void UndoCore()
        {
            base.Owner.SetDrawingFillCore(base.OldValue);
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

