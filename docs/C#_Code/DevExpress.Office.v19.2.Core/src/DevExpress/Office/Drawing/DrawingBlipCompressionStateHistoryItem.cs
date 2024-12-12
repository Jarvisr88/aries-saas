namespace DevExpress.Office.Drawing
{
    using DevExpress.Office.History;
    using System;

    public class DrawingBlipCompressionStateHistoryItem : DrawingHistoryItem<DrawingBlip, CompressionState>
    {
        public DrawingBlipCompressionStateHistoryItem(DrawingBlip owner, CompressionState oldValue, CompressionState newValue) : base(owner.DocumentModel.MainPart, owner, oldValue, newValue)
        {
        }

        protected override void RedoCore()
        {
            base.Owner.SetCompressionStateCore(base.NewValue);
        }

        protected override void UndoCore()
        {
            base.Owner.SetCompressionStateCore(base.OldValue);
        }

        public override void Write(IHistoryWriter writer)
        {
            base.Write(writer);
            writer.Write((byte) base.OldValue);
            writer.Write((byte) base.NewValue);
        }
    }
}

