namespace DevExpress.Office.Drawing
{
    using DevExpress.Office.History;
    using System;

    public class DrawingTextCharacterPropertiesBookmarkChangedHistoryItem : DrawingHistoryItem<DrawingTextCharacterProperties, string>
    {
        public DrawingTextCharacterPropertiesBookmarkChangedHistoryItem(DrawingTextCharacterProperties owner, string oldValue, string newValue) : base(owner.DocumentModelPart, owner, oldValue, newValue)
        {
        }

        protected override void RedoCore()
        {
            base.Owner.SetBookmarkCore(base.NewValue);
        }

        protected override void UndoCore()
        {
            base.Owner.SetBookmarkCore(base.OldValue);
        }

        public override void Write(IHistoryWriter writer)
        {
            base.Write(writer);
            writer.Write(base.OldValue);
            writer.Write(base.NewValue);
        }
    }
}

