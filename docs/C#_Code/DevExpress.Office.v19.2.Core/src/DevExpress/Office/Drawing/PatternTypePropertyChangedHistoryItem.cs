namespace DevExpress.Office.Drawing
{
    using DevExpress.Office;
    using DevExpress.Office.History;
    using System;

    public class PatternTypePropertyChangedHistoryItem : DrawingHistoryItem<DrawingPatternFill, DrawingPatternType>
    {
        public PatternTypePropertyChangedHistoryItem(IDocumentModelPart documentModelPart, DrawingPatternFill owner, DrawingPatternType oldValue, DrawingPatternType newValue) : base(documentModelPart, owner, oldValue, newValue)
        {
        }

        protected override void RedoCore()
        {
            base.Owner.SetPatternTypeCore(base.NewValue);
        }

        protected override void UndoCore()
        {
            base.Owner.SetPatternTypeCore(base.OldValue);
        }

        public override void Write(IHistoryWriter writer)
        {
            base.Write(writer);
            writer.Write((short) base.OldValue);
            writer.Write((short) base.NewValue);
        }
    }
}

