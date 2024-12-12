namespace DevExpress.Office.Drawing
{
    using DevExpress.Office.History;
    using System;

    public class DrawingTextFontByteChangedHistoryItem : DrawingHistoryItem<DrawingTextFont, byte>
    {
        public DrawingTextFontByteChangedHistoryItem(DrawingTextFont owner, int index, byte oldValue, byte newValue) : base(owner.DocumentModel.MainPart, owner, index, oldValue, newValue)
        {
        }

        protected override void RedoCore()
        {
            base.Owner.SetByteCore(base.Index, base.NewValue);
        }

        protected override void UndoCore()
        {
            base.Owner.SetByteCore(base.Index, base.OldValue);
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

