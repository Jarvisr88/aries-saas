namespace DevExpress.Office.Drawing
{
    using DevExpress.Office;
    using DevExpress.Office.History;
    using System;

    public class GradientStopPositionPropertyChangedHistoryItem : DrawingHistoryItem<DrawingGradientStop, int>
    {
        public GradientStopPositionPropertyChangedHistoryItem(IDocumentModelPart documentModelPart, DrawingGradientStop owner, int oldValue, int newValue) : base(documentModelPart, owner, oldValue, newValue)
        {
        }

        protected override void RedoCore()
        {
            base.Owner.SetPositionCore(base.NewValue);
        }

        protected override void UndoCore()
        {
            base.Owner.SetPositionCore(base.OldValue);
        }

        public override void Write(IHistoryWriter writer)
        {
            base.Write(writer);
            writer.Write(base.OldValue);
            writer.Write(base.NewValue);
        }
    }
}

